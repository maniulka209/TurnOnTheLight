using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Drawing;
using TurnOnTheLight.Scenes;
using TurnOnTheLight.System;
using Blend = Microsoft.Xna.Framework.Graphics.Blend;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;

namespace TurnOnTheLight;

public class TurnOnTheLight : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public TurnOnTheLight()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        RenderTarget.InitRenerTarget2D(GraphicsDevice);
        _maskRenderTarget = new RenderTarget2D(GraphicsDevice,NATIVE_WINDOW_WIDTH,NATIVE_WINDOW_HEIGHT);
        Mask.MaskInit(_backgroundMaskTexture, _lightTexture);

        _graphics.PreferredBackBufferWidth = NATIVE_WINDOW_WIDTH;
        _graphics.PreferredBackBufferHeight = NATIVE_WINDOW_HEIGHT;
        this.Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnWindowResize;

        _graphics.ApplyChanges();

        RenderTarget.CalculateDestinationRectangle();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _lightTexture = Content.Load<Texture2D>("light");
        _backgroundMaskTexture = Content.Load<Texture2D>("backgroundMask");

        _sceneManager = new SceneManager();
        _menu = new Menu(Content, _sceneManager);

        _sceneManager.AddScene(_menu);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _sceneManager.CurrentScene?.Update(gameTime);
        Mask.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        /* ===========GAME=========== */

        GraphicsDevice.SetRenderTarget(RenderTarget.RenderTarget2D);
        GraphicsDevice.Clear(Color.Transparent);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _sceneManager.CurrentScene?.Draw(_spriteBatch);

        _spriteBatch.End();

        /* ===========MASK=========== */

        GraphicsDevice.SetRenderTarget(_maskRenderTarget);
        GraphicsDevice.Clear(Color.Transparent);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        Mask.DrawBackground(_spriteBatch);

        _spriteBatch.End();

        _spriteBatch.Begin(SpriteSortMode.Immediate, blendState: _eraseBlend, SamplerState.PointClamp );

        Mask.DrawLight(_spriteBatch);

        _spriteBatch.End();

        /* ===========RENDER TARGETS=========== */

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(RenderTarget.RenderTarget2D, RenderTarget.DestinationRectangle, Color.White);
        _spriteBatch.Draw(_maskRenderTarget, RenderTarget.DestinationRectangle, Color.White);

        _spriteBatch.End();
               
        base.Draw(gameTime);
    }
    
    private void OnWindowResize(object sender, EventArgs e)
    {
        if (!_isResizing && Window.ClientBounds.Size.X > 0 && Window.ClientBounds.Size.Y > 0)
        {
            _isResizing = true;
            RenderTarget.CalculateDestinationRectangle();
            _isResizing = false;
        }
    }



    private SceneManager _sceneManager;
    private Menu _menu;
    private LevelMenu _levelMenu;

    private Texture2D _lightTexture;
    private Texture2D _backgroundMaskTexture;

    private RenderTarget2D _maskRenderTarget;

    private bool _isResizing = false;
    private const int NATIVE_WINDOW_WIDTH = 1280;
    private const int NATIVE_WINDOW_HEIGHT = 720;

    private BlendState _eraseBlend = new BlendState()
    {
        ColorSourceBlend = Blend.Zero,
        ColorDestinationBlend = Blend.InverseSourceAlpha,

        AlphaSourceBlend = Blend.Zero,
        AlphaDestinationBlend = Blend.InverseSourceAlpha
    };

}
