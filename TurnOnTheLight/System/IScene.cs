using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TurnOnTheLight.System
{
    interface IScene
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Load();
    }
}
