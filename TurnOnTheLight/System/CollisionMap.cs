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
        public CollisionMap(string fileSrc, Texture2D  texture, Player player)
        {
            _fileSrc = fileSrc;
            _collisionMap = new Dictionary<Vector2, int>();
            _collisionSprite = new Sprite(0, 0, 16, 16, texture, TILE_SCALE);
            _playerCollisionArea = new List<Vector2>();
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

            foreach (var item in _playerCollisionArea)
            {
                _collisionSprite.Draw(spriteBatch, new Vector2(item.X * TILE_WIDTH * TILE_SCALE , item.Y * TILE_HEIGHT * TILE_SCALE));
            }
        }

        public void Update(GameTime gameTime)
        {
            setPlayerArea(_player.Rectangle);
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
                    if( _collisionMap[item] == 1)
                    {
                        _player.RevertToPreviousPosition();
                    }
                    else if ( _collisionMap[item] == 2)
                    {
                       OnPlayerTouchTheDoor?.Invoke(this, EventArgs.Empty);
                    }
                    
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
        private Player _player;
        private string _fileSrc;

        private Sprite _collisionSprite;

        private const int TILE_WIDTH = 16;
        private const int TILE_HEIGHT = 16;
        private const float TILE_SCALE = 4f;

    }                                           
}
