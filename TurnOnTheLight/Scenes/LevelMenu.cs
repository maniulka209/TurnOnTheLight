using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using TurnOnTheLight.System;
using TurnOnTheLight.Graphics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace TurnOnTheLight.Scenes
{
    class LevelMenu: IScene
    {
        public LevelMenu(ContentManager content, SceneManager sceneManager)
        {
            _contentManager = content;
            _sceneManager = sceneManager;
            Load();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _backgroundSprite.Draw(spriteBatch, Vector2.Zero);
            _levelButton.Draw(spriteBatch);
        }

        public void Load()
        {
            _backgroundSpritesheet = _contentManager.Load<Texture2D>("backgroundLevelMenu");
            _levelButtonSpritesheet = _contentManager.Load<Texture2D>("levelButton");

            _backgroundSprite = new Sprite(0, 0, BACKGROUND_WIDTH, BACKGROUND_HEIGHT, _backgroundSpritesheet, BACKGROUND_SCALE);
            _levelButton = new Button(_levelButtonSpritesheet, new Vector2(BUTTON_1_POS_X, BUTTON_1_POS_Y), BUTTON_WIDTH, BUTTON_HEIGHT, BUTTON_SCALE);
        }

        public void Update(GameTime gameTime)
        {
            _levelButton.Update(gameTime);
            if (_levelButton.IsClicked)
            {
                _sceneManager.AddScene(new Level1(_contentManager, _sceneManager));
            }
        }

        private ContentManager _contentManager;
        private SceneManager _sceneManager;


        private Texture2D _backgroundSpritesheet;
        private Texture2D _levelButtonSpritesheet;

        private const int BACKGROUND_WIDTH = 128;
        private const int BACKGROUND_HEIGHT = 128;
        private const float BACKGROUND_SCALE = 10f;

        private const int BUTTON_1_POS_X = 10;
        private const int BUTTON_1_POS_Y = 10;
        private const int BUTTON_WIDTH = 16;
        private const int BUTTON_HEIGHT = 16;
        private const float BUTTON_SCALE = 3f;

        private Sprite _backgroundSprite;
        private Button _levelButton;
    }
}
