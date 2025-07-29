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
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void Load()
        {
            _test = _contentManager.Load<Texture2D>("test");

            _spriteTest = new Sprite(0, 0, 16, 16, _test, 10f);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        private ContentManager _contentManager;
        private Texture2D _test;
        private Sprite _spriteTest;

    }
}
