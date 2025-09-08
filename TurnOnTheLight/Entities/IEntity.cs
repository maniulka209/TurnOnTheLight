using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnOnTheLight.Entities
{
    interface IEntity
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);

    }
}
