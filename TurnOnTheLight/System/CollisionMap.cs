using Accessibility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TurnOnTheLight.Entities;
using TurnOnTheLight.Graphics;

namespace TurnOnTheLight.System
{
    class CollisionMap
    {
        public CollisionMap(string fileSrc, Texture2D  texture, Player player)
        {
            _fileSrc = fileSrc;
            _collisionMap = new Dictionary<Vector2, int>();
            _collisionSprite = new Sprite(0, 0, 16, 16, texture, TILE_SCALE);
            _player = player;

            setCollisonMap();
           
        }

        public EventHandler OnPlayerTouchTheDoor;
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _collisionMap)
            {
                _collisionSprite.Draw(spriteBatch, new Vector2(item.Key.X * TILE_WIDTH * TILE_SCALE, item.Key.Y * TILE_HEIGHT * TILE_SCALE));
            }

            List<Vector2> intersection = getPlayerAreaVerticly(_player.Rectangle);
            foreach (var item in intersection)
            {
                _collisionSprite.Draw(spriteBatch, new Vector2(item.X * TILE_WIDTH * TILE_SCALE , item.Y * TILE_HEIGHT * TILE_SCALE));
            }

            intersection = getPlayerAreaHorizontal(_player.Rectangle);
            foreach (var item in intersection)
            {
                _collisionSprite.Draw(spriteBatch, new Vector2(item.X * TILE_WIDTH * TILE_SCALE, item.Y * TILE_HEIGHT * TILE_SCALE));
            }
        }

        public void Update(GameTime gameTime)
        {;
            CheckCollison(_player.Rectangle);
        }

        private void setCollisonMap()
        {
            StreamReader reader = new StreamReader(_fileSrc);
            string line;
            int y = 0;

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(",");
                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if(value != 0)
                        {
                            _collisionMap.Add(new Vector2(x, y), value);
                        }
                    }
                }
                y++;
            }
        }
        private void CheckCollison(Rectangle playerRect)
        {
            List<Vector2> intersection = getPlayerAreaVerticly(_player.Rectangle);
            float positionX = _player.Position.X;
            float positionY = _player.Position.Y;
            
            foreach(Vector2 item in intersection)
            {
                if (_collisionMap.TryGetValue(item,out int value))
                {
                    Rectangle itemRect = new Rectangle(
                        (int)(item.X * (TILE_WIDTH * TILE_SCALE)),
                        (int)(item.Y * (TILE_HEIGHT * TILE_SCALE)),
                        (int)(TILE_WIDTH * TILE_SCALE),
                        (int)(TILE_HEIGHT * TILE_SCALE)
                        );

                    if (itemRect.Intersects(playerRect) && value == 1)
                    {
                        positionY = _player.PreviousPosition.Y;
                    }
                    else if(itemRect.Intersects(playerRect) && value == 2)
                    {
                        OnPlayerTouchTheDoor?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            intersection = getPlayerAreaHorizontal(_player.Rectangle);

            playerRect = new Rectangle(playerRect.X, (int)positionY, playerRect.Width, playerRect.Height);

            foreach (Vector2 item in intersection)
            {
                if (_collisionMap.TryGetValue(item, out int value))
                {
                    Rectangle itemRect = new Rectangle(
                        (int)(item.X * (TILE_WIDTH * TILE_SCALE)),
                        (int)(item.Y * (TILE_HEIGHT * TILE_SCALE)),
                        (int)(TILE_WIDTH * TILE_SCALE),
                        (int)(TILE_HEIGHT * TILE_SCALE)
                        );

                    if (itemRect.Intersects(playerRect) && value == 1)
                    {
                        positionX = _player.PreviousPosition.X;
                    }
                    else if (itemRect.Intersects(playerRect) && value == 2)
                    {
                        OnPlayerTouchTheDoor?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            _player.Position = new Vector2(positionX, positionY);

        }

        private List<Vector2> getPlayerAreaHorizontal(Rectangle playerRect)          
        {
            List<Vector2> result = new List<Vector2>();
            int tileX = (int)(playerRect.X / (TILE_WIDTH * TILE_SCALE));
            int tileY = (int)(playerRect.Y / (TILE_HEIGHT * TILE_SCALE));

            result.Add(new Vector2(tileX, tileY));

            result.Add(new Vector2(tileX-1, tileY+1));
            result.Add(new Vector2(tileX+1, tileY+1));
            result.Add(new Vector2(tileX+1, tileY-1));
            result.Add(new Vector2(tileX-1, tileY-1));

            result.Add(new Vector2(tileX+1, tileY));
            result.Add(new Vector2(tileX-1, tileY));
            return result;
        }

        private List<Vector2> getPlayerAreaVerticly(Rectangle playerRect)
        {
            List<Vector2> result = new List<Vector2>();
            int tileX = (int)(playerRect.X / (TILE_WIDTH * TILE_SCALE));
            int tileY = (int)(playerRect.Y / (TILE_HEIGHT * TILE_SCALE));

            result.Add(new Vector2(tileX - 1, tileY + 1));
            result.Add(new Vector2(tileX + 1, tileY + 1));
            result.Add(new Vector2(tileX + 1, tileY - 1));
            result.Add(new Vector2(tileX - 1, tileY - 1));

            result.Add(new Vector2(tileX, tileY + 1));
            result.Add(new Vector2(tileX, tileY - 1));
            return result;
        }

        private Dictionary<Vector2, int> _collisionMap;
        private Player _player;
        private string _fileSrc;


        private Sprite _collisionSprite;

        private const int TILE_WIDTH = 16;
        private const int TILE_HEIGHT = 16;
        private const int TILE_SIZE = (int)(16 * TILE_SCALE);
        private const float TILE_SCALE = 4f;

    }                                           
}
