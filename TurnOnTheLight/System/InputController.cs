using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnOnTheLight.Entities;

namespace TurnOnTheLight.System
{
    class InputController
    {
        public InputController(Player player)
        {
            _player = player;
        }

        public void ControlInputs()
        {
            _keyboardState = Keyboard.GetState();

            _player.movementDirectory = Vector2.Zero;

            //MOVE HORIZONAL
            if ( _keyboardState.IsKeyDown(Keys.Left))
            {
                _player.movementDirectory = new Vector2(-1, 0);
            }
            else if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _player.movementDirectory = new Vector2(1, 0);
            }

            //MOVE VERTICLY
            if (_keyboardState.IsKeyDown(Keys.Up) && !_prevKeyboardState.IsKeyDown(Keys.Up))
            {
                _player.movementDirectory = new Vector2(_player.movementDirectory.X,-1);
                _player.Jump();
            }



            if(_player.movementDirectory != Vector2.Zero)
            {
               float lenght = (float)Math.Sqrt(Math.Pow(_player.movementDirectory.X, 2) + Math.Pow(_player.movementDirectory.Y, 2));
               _player.movementDirectory = new Vector2(_player.movementDirectory.X / lenght, _player.movementDirectory.Y / lenght);
            }
            _prevKeyboardState = _keyboardState;
        } 

        private KeyboardState _keyboardState;
        private KeyboardState _prevKeyboardState;
        private Player _player;


    }
}
