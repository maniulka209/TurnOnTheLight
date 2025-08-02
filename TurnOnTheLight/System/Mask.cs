using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TurnOnTheLight.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace TurnOnTheLight.System
{
    static class Mask
    {

        //TODO: better light drawing (mouse at the center of the light)
        public static Vector2 LightPosition { get; set; } = Vector2.Zero;

        public static bool IsOn { get; private set; } = false;

        public static float LightScale { get; private set; } = 7f;

        public static void MaskInit(Texture2D backgroundTexture, Texture2D lightSprite)
        {
            _lightSprite = new Sprite(0, 0, LIGHT_WIDTH, LIGHT_HEIGHT, lightSprite, LightScale);
            _backgroundSprite = new Sprite(0, 0, BACKGROUND_WIDTH, BACKGROUND_HEIGHT, backgroundTexture, BACKGROUND_SCALE);
            _isLightAnimationOn = false;
        }
        public static void DrawBackground(SpriteBatch spriteBatch)
        {
            if (IsOn)
            {
                _backgroundSprite.Draw(spriteBatch, Vector2.Zero);
            }
        }
        public static void DrawLight(SpriteBatch spriteBatch)
        {
            if (IsOn)
            {
               _lightSprite.Draw(spriteBatch, LightPosition);
            }
        }
        public static void Update(GameTime gameTime)
        {
            if (IsOn)
            {
                _mouseState = Mouse.GetState();
                float correctedX = (_mouseState.X - RenderTarget.DestinationRectangle.X) / RenderTarget.Scale;
                float correctedY = (_mouseState.Y - RenderTarget.DestinationRectangle.Y) / RenderTarget.Scale;

                Point mousePosition = new Point((int)correctedX, (int)correctedY);

                LightPosition = new Vector2(mousePosition.X - (LIGHT_WIDTH*LightScale)/2 , mousePosition.Y - (LIGHT_HEIGHT * LightScale)/2 );

                if(_isLightAnimationOn)
                {
                    _lightAnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_lightAnimationTimer < LIGHT_ANIMATION_DURATION)
                    {
                        ChangeLightSize(LightScale * LIGHT_ANIMATION_VELOCITY);
                    }
                    else
                    {
                        TurnOff();
                    }
                }

            }
        }
        public static void ChangeLightSize(float size)
        {
            _lightSprite.Scale = size;
            LightScale = size;
        }
        public static  void TurnOn()
        {
            IsOn = true;
        }
        public static void TurnOff()
        {
            IsOn = false;
        }
        public static void TurnOffWithAniamtion()
        {
            if (!_isLightAnimationOn)
            {
               _isLightAnimationOn = true;
               _lightAnimationTimer = 0;
            }
        }

        private static Sprite _backgroundSprite;
        private static Sprite _lightSprite;

        private static float _lightAnimationTimer;
        private static bool _isLightAnimationOn;
        private const float LIGHT_ANIMATION_VELOCITY = 1.03f;
        private const float LIGHT_ANIMATION_DURATION = 2f;

        private const int BACKGROUND_WIDTH = 128;
        private const int BACKGROUND_HEIGHT = 72;
        private const float BACKGROUND_SCALE = 10f;

        private const int LIGHT_WIDTH = 16;
        private const int LIGHT_HEIGHT = 16;

        private static MouseState _mouseState;



    }
}
