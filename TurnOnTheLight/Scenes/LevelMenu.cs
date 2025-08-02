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
        }

        public void Load()
        {
            _backgroundSpritesheet = _contentManager.Load<Texture2D>("backgroundMenu");

            _backgroundSprite = new Sprite(0, 0, BACKGROUND_WIDTH, BACKGROUND_HEIGHT, _backgroundSpritesheet, BACKGROUND_SCALE);
            Mask.TurnOn();
            Mask.TurnOffWithAniamtion();
        }
         
        public void Update(GameTime gameTime)
        {
           

        }

        private ContentManager _contentManager;
        private SceneManager _sceneManager;

        private float _lightAnimationTimer = 0;


        private Texture2D _backgroundSpritesheet;
        private const int BACKGROUND_WIDTH = 128;
        private const int BACKGROUND_HEIGHT = 128;
        private const float BACKGROUND_SCALE = 10f;

        private Sprite _backgroundSprite;
    }
}
