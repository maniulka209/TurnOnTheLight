using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TurnOnTheLight.Graphics;

namespace TurnOnTheLight.System
{
    public enum Tile
    {
        Empty,
        Solid,
        Door,
    }
    class TileMap
    { 
        public TileMap(string fileSrc, Texture2D tilesTexture)
        {
            _fileSrc = fileSrc;
            _tileMap = new Dictionary<Vector2, Tile>();

            _solidSprite = new Sprite(0,0,TILE_WIDTH,TILE_HEIGHT, tilesTexture, TILE_SCALE);
            _doorSprite = new Sprite(TILE_WIDTH,0,TILE_WIDTH,TILE_HEIGHT,tilesTexture, TILE_SCALE);
            setTileMap();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _tileMap)
            {
                Sprite itemSprite = null;
                if (item.Value == Tile.Solid)
                {
                    itemSprite = _solidSprite;
                }
                else if (item.Value == Tile.Door)
                {
                    itemSprite = _doorSprite;
                }
                    itemSprite?.Draw(spriteBatch, new Vector2(item.Key.X * TILE_WIDTH * TILE_SCALE, item.Key.Y * TILE_HEIGHT * TILE_SCALE));
            }
        }
        private void setTileMap()
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
                        _tileMap.Add(new Vector2(x, y), (Tile)value );
                    }
                }
                y++;
            }
        }

        private Dictionary<Vector2, Tile> _tileMap;
        private string _fileSrc;

        private Sprite _solidSprite;
        private Sprite _doorSprite;

        private const int TILE_WIDTH = 16;
        private const int TILE_HEIGHT = 16;
        private const float TILE_SCALE = 4f;
       
    }
}
