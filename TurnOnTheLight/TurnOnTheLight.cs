using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;
using TurnOnTheLight.Scenes;
using TurnOnTheLight.System;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

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


        _sceneManager = new SceneManager();
        _menu = new Menu(Content);
        _levelMenu = new LevelMenu(Content);

        _menu.OnPlayButtonPressed += (object sender, EventArgs e) => _sceneManager.AddScene(_levelMenu);

        _sceneManager.AddScene(_menu);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _sceneManager.CurrentScene?.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        GraphicsDevice.SetRenderTarget(RenderTarget.RenderTarget2D);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        if(_sceneManager.CurrentScene != _levelMenu)
        {
            _sceneManager.CurrentScene?.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

        if (_sceneManager.CurrentScene == _levelMenu)
        {
            _sceneManager.CurrentScene?.Draw(_spriteBatch);
        }

        _spriteBatch.End();



        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(RenderTarget.RenderTarget2D, RenderTarget.DestinationRectangle, Color.White);
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

    private bool _isResizing = false;

    private const int NATIVE_WINDOW_WIDTH = 1280;
    private const int NATIVE_WINDOW_HEIGHT = 720;
   
}
