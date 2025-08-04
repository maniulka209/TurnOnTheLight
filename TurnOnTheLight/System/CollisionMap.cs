using Accessibility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TurnOnTheLight.Entities;
using TurnOnTheLight.Graphics;

namespace TurnOnTheLight.System
{
    class CollisionMap
    {
        public CollisionMap(string fileSrc, Texture2D  texture)
        {
            _fileSrc = fileSrc;
            _collisionMap = new Dictionary<Vector2, int>();
            _testSprite = new Sprite(0, 0, 16, 16, texture, TILE_SCALE);
            _playerCollisionArea = new List<Vector2>();

            setCollisonMap();
           
        }

        public EventHandler OnCollison;
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _collisionMap)
            {
                Sprite itemSprite = null;
                if (item.Value == 1)
                {
                    itemSprite = _testSprite;
                }
                itemSprite?.Draw(spriteBatch, new Vector2(item.Key.X * TILE_WIDTH * TILE_SCALE, item.Key.Y * TILE_HEIGHT * TILE_SCALE));
            }

            foreach (var item in _playerCollisionArea)
            {
                _testSprite.Draw(spriteBatch, new Vector2(item.X * TILE_WIDTH * TILE_SCALE , item.Y * TILE_HEIGHT * TILE_SCALE));
            }
        }

        public void Update(GameTime gameTime, Rectangle playerRect)
        {
            setPlayerArea(playerRect);
            CheckCollison(playerRect);
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
                        if(value == 1)
                        {
                            _collisionMap.Add(new Vector2(x, y), value);
                        }
                    }
                }
                y++;
            }
        }
        private void CheckCollison(Rectangle playerRect )
        {
            foreach(Vector2 item in _playerCollisionArea)
            {   
                Rectangle itemRec =  new Rectangle(
                        (int)(item.X * TILE_WIDTH * TILE_SCALE),
                        (int)(item.Y * TILE_HEIGHT * TILE_SCALE),
                        (int)(TILE_WIDTH * TILE_SCALE),
                        (int)(TILE_HEIGHT * TILE_SCALE)

                    );
                if (_collisionMap.ContainsKey(item) && itemRec.Intersects(playerRect))
                {
                    OnCollison?.Invoke(this, EventArgs.Empty);  
                }
            }
        }
        private void setPlayerArea(Rectangle playerRect)
        {
            _playerCollisionArea.Clear();
            int tileX = (int)(playerRect.X / (TILE_WIDTH * TILE_SCALE));
            int tileY = (int)(playerRect.Y / (TILE_HEIGHT * TILE_SCALE));

            _playerCollisionArea.Add(new Vector2(tileX, tileY));

            _playerCollisionArea.Add(new Vector2(tileX-1, tileY+1));
            _playerCollisionArea.Add(new Vector2(tileX+1, tileY+1));
            _playerCollisionArea.Add(new Vector2(tileX+1, tileY-1));
            _playerCollisionArea.Add(new Vector2(tileX-1, tileY-1));

            _playerCollisionArea.Add(new Vector2(tileX+1, tileY));
            _playerCollisionArea.Add(new Vector2(tileX-1, tileY));

            _playerCollisionArea.Add(new Vector2(tileX, tileY+1));
            _playerCollisionArea.Add(new Vector2(tileX, tileY-1));
        }

        private Dictionary<Vector2, int> _collisionMap;
        private List<Vector2> _playerCollisionArea;
        private string _fileSrc;

        private Sprite _testSprite;

        private const int TILE_WIDTH = 16;
        private const int TILE_HEIGHT = 16;
        private const float TILE_SCALE = 4f;

    }                                           
}
