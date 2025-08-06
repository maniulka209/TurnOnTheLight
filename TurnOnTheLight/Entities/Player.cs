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
        Jump,
    }

    class Player : IEntity
    {
        public Player(Texture2D texture, Vector2 position)
        {
            _idleSprite = new Sprite(0, 0, PLAYER_HEIGHT, PLAYER_HEIGHT, texture, PLAYER_SCALE);
            this.Position = position;
        }
        public Vector2 Position { get; set; }
        public Vector2 PreviousPosition { get; set; }
        public Vector2 movementDirectory { get; set; }

        public bool IsGravitiOn { get; set; } = true;
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

            PreviousPosition = Position;

            if (IsGravitiOn) { 
                this.Position = new Vector2(
                    Position.X,
                    Position.Y + (GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    );
            }

            if(State == PlayerState.Go)
            {
                Position = new Vector2(
                    Position.X +( movementDirectory.X * (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED),
                    Position.Y + (movementDirectory.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED)
                    );
            }

        }

        private Sprite _idleSprite;

        private const int PLAYER_WIDTH = 16;
        private const int PLAYER_HEIGHT = 16;
        private const float PLAYER_SCALE = 2f;

        private const int PLAYER_SPEED = 200;
        private const int GRAVITY = 400;
        

    }
}
