using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnOnTheLight.System
{
    static class RenderTarget
    {
        static public void CalculateDestinationRectangle()
        {
            Point size = _graphicsDevice.Viewport.Bounds.Size;

            float scaleX = (float)size.X / RenderTarget2D.Width;
            float scaleY = (float)size.Y / RenderTarget2D.Height;
            float scale = Math.Min(scaleX, scaleY);

            int newWidth = RenderTarget2D.Width * (int)scale;
            int newHeight = RenderTarget2D.Height * (int)scale;
            int newX = (size.X - newWidth) /2;
            int newY = (size.Y - newHeight) /2;


            DestinationRectangle = new Rectangle(newX, newY, newWidth, newHeight);
        }
        static public void InitRenerTarget2D(GraphicsDevice graphicsDevice)
        {
            RenderTarget2D = new RenderTarget2D(graphicsDevice, NATIVE_WINDOW_WIDTH, NATIVE_WINDOW_HEIGHT);
            _graphicsDevice = graphicsDevice;
        }

        static public Rectangle DestinationRectangle { get; private set; }
        static public RenderTarget2D RenderTarget2D { get; private set; }
        static public float Scale { 
            get
            {   
                return RenderTarget2D.Height / DestinationRectangle.Height;
            }
        }

        private static GraphicsDevice _graphicsDevice;

        private const int NATIVE_WINDOW_WIDTH = 1280;
        private const int NATIVE_WINDOW_HEIGHT = 720;


    }
}
