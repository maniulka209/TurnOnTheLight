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

            Position = new Vector2(
                Position.X + (movementDirectory.X * (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED),
                Position.Y + (movementDirectory.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * PLAYER_SPEED)
                );

            if (IsGravitiOn) { 
                this.Position = new Vector2(
                    Position.X,
                    Position.Y + (GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds)
                    );
            }
            if(State == PlayerState.Jump)
            {
                Position = new Vector2(
                   Position.X,
                   Position.Y - ((float)gameTime.ElapsedGameTime.TotalSeconds * JUMP_VELOCITY)
                   );

                _passedJumpDistance += (float)gameTime.ElapsedGameTime.TotalSeconds * JUMP_VELOCITY;
                if(_passedJumpDistance >= MAX_JUMP_HEIGHT)
                {
                    this.State = PlayerState.idle;
                }

            }
        }

        public void Jump()
        {
            if(State != PlayerState.Jump)
            {
                State = PlayerState.Jump;
                _passedJumpDistance = 0;
            }
        }

        private Sprite _idleSprite;

        private const int PLAYER_WIDTH = 16;
        private const int PLAYER_HEIGHT = 16;
        private const float PLAYER_SCALE = 2f;

        private const int PLAYER_SPEED = 200;
        private const int GRAVITY = 400;
        
        private const  int JUMP_VELOCITY = 500;
        private float MAX_JUMP_HEIGHT = 100;
        private float _passedJumpDistance;

    }
}
