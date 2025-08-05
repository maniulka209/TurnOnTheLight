using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnOnTheLight.Graphics;

namespace TurnOnTheLight.Entities
{
    public enum PlayerState
    {
        idle,
        Go,
    }

    class Player : IEntity
    {
        public Player(Texture2D texture, Vector2 position)
        {
            _idleSprite = new Sprite(0, 0, PLAYER_HEIGHT, PLAYER_HEIGHT, texture, PLAYER_SCALE);
            this.Position = position;
        }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public PlayerState State { get; set; } = PlayerState.idle;

        public Rectangle Rectangle {
            get 
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(PLAYER_WIDTH * PLAYER_SCALE), (int)(PLAYER_HEIGHT * PLAYER_SCALE));
            } 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _idleSprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            _prevPosition = Position;

            if(State == PlayerState.Go)
            {
                Position = new Vector2(
                    Position.X +( Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds),
                    Position.Y + ((Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds))
                    );
            }
        }

        public void RevertToPreviousPosition()
        {
            Position = _prevPosition;
        }

        private Sprite _idleSprite;
        private Vector2 _prevPosition;

        private const int PLAYER_WIDTH = 16;
        private const int PLAYER_HEIGHT = 16;
        private const float PLAYER_SCALE = 2f;

        private const int PLAYER_SPEED = 200;
        

    }
}
