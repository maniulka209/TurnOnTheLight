using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            spriteBatch.Draw(_test, new Vector2(0,0), Color.White);
        }
        public void Load()
        {
            _test = _contentManager.Load<Texture2D>("test");
        }

        public void Update(GameTime gameTime)
        {
        }

        private ContentManager _contentManager;
        private Texture2D _test;
    }
}
