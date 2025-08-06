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
using TurnOnTheLight.Entities;

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
            _player.Draw(spriteBatch);
            _collisionMap.Draw(spriteBatch);
        }

        public void Load()
        {
            _tilesSpritesheet = _contentManager.Load<Texture2D>("tiles");
            _playerSpritesheet = _contentManager.Load<Texture2D>("player");
            _collionSpritesheet = _contentManager.Load<Texture2D>("collisonSpriteSheet");

            _tileMap = new TileMap("../../../Assets/TileMap/level1.csv", _tilesSpritesheet);
            _player = new Player(_playerSpritesheet, Vector2.Zero);
            _inputController = new InputController(_player);
            _collisionMap = new CollisionMap("../../../Assets/TileMap/level1Collison.csv", _collionSpritesheet, _player);

            _collisionMap.OnPlayerTouchTheDoor += goNextLvl;
            
        }

        public void Update(GameTime gameTime)
        {
            _inputController.ControlInputs();
            _player.Update(gameTime);
            _collisionMap.Update(gameTime);
        }

        private void goNextLvl(object sender, EventArgs e)
        {
            _sceneManager.AddScene(new Level2(_contentManager,_sceneManager));
        }

        private ContentManager _contentManager;
        private SceneManager _sceneManager;

        private InputController _inputController;

        private Texture2D _playerSpritesheet;
        private Player _player;

        private Texture2D _tilesSpritesheet;
        private Texture2D _collionSpritesheet;
        private TileMap _tileMap;
        private CollisionMap _collisionMap;


    }
}
