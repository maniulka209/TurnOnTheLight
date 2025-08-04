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

            if( _keyboardState.IsKeyDown(Keys.Left))
            {
                _player.State = PlayerState.GoLeft;
            }
            else if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _player.State = PlayerState.GoRight;
            }
            else if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _player.State = PlayerState.GoUp;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _player.State = PlayerState.GoDown;
            }
            else
            {
                _player.State = PlayerState.idle;
            }

                _prevKeyboardState = _keyboardState;
        } 

        private KeyboardState _keyboardState;
        private KeyboardState _prevKeyboardState;
        private Player _player;
    }
}
