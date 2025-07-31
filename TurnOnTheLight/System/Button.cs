using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurnOnTheLight.Graphics;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TurnOnTheLight.System
{
    class Button
    {
        public enum ButtonState
        {
            Normal,
            Hover
        }
        public Button(Texture2D texture, Vector2 position, int width, int height, float scale)
        {
            this.Position = position;
            _normalButtonSprite = new Sprite(0, 0, width, height, texture, scale);
            _hoverButtonSprite = new Sprite(width, 0, width, height, texture, scale);

            _buttonRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, width * (int)scale, height * (int)scale);
        }
        public ButtonState State { get; private set; } = ButtonState.Normal;
        public bool IsClicked { get; private set; } = false;
        public Vector2 Position { get; private set; }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(State == ButtonState.Normal)
            {
                _normalButtonSprite.Draw(spriteBatch, this.Position);
            }
            else if(State == ButtonState.Hover)
            {
                _hoverButtonSprite.Draw(spriteBatch, this.Position);
            }
        }
        public void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();

            float correctedX = (_mouseState.X - RenderTarget.DestinationRectangle.X) / RenderTarget.Scale;
            float correctedY = (_mouseState.Y - RenderTarget.DestinationRectangle.Y) / RenderTarget.Scale;

            Point mousePosition = new Point((int)correctedX, (int)correctedY);

            if (_buttonRectangle.Contains(mousePosition))
            {
                this.State = ButtonState.Hover;
                if (
                    _mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                    _prevMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released
                )
                {
                    this.IsClicked = true;
                }
            }
            else
            {
                this.State = ButtonState.Normal;
            }

            _prevMouseState = _mouseState;
            
        }
        public void SetNewPosition(Vector2 newPosition)
        {
            this.Position = newPosition;
            _buttonRectangle.X = (int)this.Position.X;
            _buttonRectangle.Y = (int)this.Position.Y;
        }

        private Sprite _normalButtonSprite;
        private Sprite _hoverButtonSprite;

        private MouseState _mouseState;
        private MouseState _prevMouseState;

        private Rectangle _buttonRectangle;


        private const int NATIVE_WINDOW_WIDTH = 1280;
        private const int NATIVE_WINDOW_HEIGHT = 720;


    }
}
