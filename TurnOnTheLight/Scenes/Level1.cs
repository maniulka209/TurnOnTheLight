using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnOnTheLight.System;
using Microsoft.Xna.Framework.Graphics;
using TurnOnTheLight.Graphics;

namespace TurnOnTheLight.Scenes
{
    class Level1: IScene
    {
        public Level1(ContentManager content, SceneManager sceneManager)
        {
            _contentManager = content;
            _sceneManager = sceneManager;
            Load();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _tileMap.Draw(spriteBatch);
        }

        public void Load()
        {
            _tilesSpritesheet = _contentManager.Load<Texture2D>("tiles");
            _tileMap = new TileMap("../../../Assets/TileMap/level1.csv", _tilesSpritesheet);
        }

        public void Update(GameTime gameTime)
        {

        }

        private ContentManager _contentManager;
        private SceneManager _sceneManager;

        private Texture2D _tilesSpritesheet;
        private TileMap _tileMap;
    }
}
