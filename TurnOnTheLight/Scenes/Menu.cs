using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnOnTheLight.Graphics;
using TurnOnTheLight.System;

namespace TurnOnTheLight.Scenes
{
    class Menu : IScene
    {
        public Menu(ContentManager content, SceneManager sceneManager)
        {
            _contentManager = content;
            _sceneManager = sceneManager;
            Load();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch, Vector2.Zero);
            _playButton.Draw(spriteBatch);
        }

        public void Load()
        {
            _playButtonSpritesheet = _contentManager.Load<Texture2D>("playButtont");
            _backgroundSpritesheet = _contentManager.Load<Texture2D>("backgroundMenu");

            _playButton = new Button(_playButtonSpritesheet, new Vector2(PLAY_BUTTON_POS_X, PLAY_BUTTON_POS_Y), PLAY_BUTTON_WIDTH, PLAY_BUTTON_HEIGHT, PLAY_BUTTON_SCALE);
            _background = new Sprite(0, 0, BACKGROUND_WIDTH, BACKGROUND_HEIGHT, _backgroundSpritesheet, BACKGROUND_SCALE);
        }

        public void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime);
            if (_playButton.IsClicked)
            {
                _sceneManager.AddScene(new LevelMenu(_contentManager, _sceneManager));
            }
        }

        private Texture2D _playButtonSpritesheet;
        private Texture2D _backgroundSpritesheet;

        private Sprite _background;
        private const int BACKGROUND_WIDTH = 128;
        private const int BACKGROUND_HEIGHT = 128;
        private const float BACKGROUND_SCALE = 10f;

        private Button _playButton;
        private const int PLAY_BUTTON_POS_X = 560;
        private const int PLAY_BUTTON_WIDTH = 40;
        private const int PLAY_BUTTON_HEIGHT = 16;
        private const int PLAY_BUTTON_POS_Y = 500;
        private const float PLAY_BUTTON_SCALE = 4f;

        private ContentManager _contentManager;
        private SceneManager _sceneManager;


    }
}
