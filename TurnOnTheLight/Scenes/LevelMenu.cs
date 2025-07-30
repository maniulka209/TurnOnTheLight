using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using TurnOnTheLight.System;

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
        }

        public void Load()
        {
           
        }

        public void Update(GameTime gameTime)
        {
        }

        private ContentManager _contentManager;
    }
}
