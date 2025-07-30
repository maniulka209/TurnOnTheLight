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
        public Menu(ContentManager content)
        {
            _contentManager = content;
            Load();
        }
        public EventHandler OnPlayButtonPressed;
        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch, new Vector2(0,0));
            _playButton.Draw(spriteBatch);
        }

        public void Load()
        {
            _playButtonSpritesheet = _contentManager.Load<Texture2D>("playButtont");
            _backgroundSpritesheet = _contentManager.Load<Texture2D>("background");

            _playButton = new Button(_playButtonSpritesheet, new Vector2(PLAY_BUTTON_POS_X, PLAY_BUTTON_POS_Y), PLAY_BUTTON_WIDTH, PLAY_BUTTON_HEIGHT, PLAY_BUTTON_SCALE);
            _background = new Sprite(0, 0, 128, 72, _backgroundSpritesheet, 10f);
        }

        public void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime);
            if (_playButton.IsClicked)
            {
                OnPlayButtonPressed?.Invoke(this, EventArgs.Empty);
            }
        }

        private Texture2D _playButtonSpritesheet;
        private Texture2D _backgroundSpritesheet;

        private Sprite _background;

        private Button _playButton;
        private const int PLAY_BUTTON_POS_X = 560;
        private const int PLAY_BUTTON_WIDTH = 40;
        private const int PLAY_BUTTON_HEIGHT = 16;
        private const int PLAY_BUTTON_POS_Y = 500;
        private const float PLAY_BUTTON_SCALE = 4f;

        private ContentManager _contentManager;


    }
}
