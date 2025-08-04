using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        GoRight,
        GoLeft,
        GoUp,
        GoDown
    }

    class Player : IEntity
    {
        public Player(Texture2D texture, Vector2 position)
        {
            _idleSprite = new Sprite(0, 0, PLAYER_HEIGHT, PLAYER_HEIGHT, texture, PLAYER_SCALE);
            this.Position = position;
        }
        public Vector2 Position { get; set; }
        public PlayerState State { get; set; } = PlayerState.idle;
        public void Draw(SpriteBatch spriteBatch)
        {
            _idleSprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            switch(State)
            {
                case PlayerState.GoRight:
                    Position = new Vector2(Position.X  + (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED , Position.Y);
                    break;

                case PlayerState.GoLeft:
                    Position = new Vector2(Position.X - (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED, Position.Y);
                    break;

                case PlayerState.GoUp:
                    Position = new Vector2(Position.X, Position.Y - (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED);
                    break;

                case PlayerState.GoDown:
                    Position = new Vector2(Position.X, Position.Y + (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED);
                    break;

            }
        }

        private Sprite _idleSprite;

        private const int PLAYER_WIDTH = 16;
        private const int PLAYER_HEIGHT = 16;
        private const float PLAYER_SCALE = 2f;

        private const int PLAYER_SPEED = 200;
        

    }
}
