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
        public LevelMenu(ContentManager content)
        {
            _contentManager = content;
            Load();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _backgroundSprite.Draw(spriteBatch, new Vector2(0,0));
        }

        public void Load()
        {
            _backgroundSpritesheet = _contentManager.Load<Texture2D>("background");

            _backgroundSprite = new Sprite(0, 0, 128, 72, _backgroundSpritesheet, 10f);
        }

        public void Update(GameTime gameTime)
        {
        }

        private ContentManager _contentManager;
        
        private Texture2D _backgroundSpritesheet;

        private Sprite _backgroundSprite;
    }
}
