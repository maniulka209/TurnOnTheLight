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

            _player.Velocity = Vector2.Zero;
            _player.State = PlayerState.idle;

            //MOVE HORIZONAL
            if ( _keyboardState.IsKeyDown(Keys.Left))
            {
                _player.Velocity = new Vector2(-PLAYER_SPEED, 0);
                _player.State = PlayerState.Go;
            }
            else if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _player.Velocity = new Vector2(PLAYER_SPEED, 0);
                _player.State = PlayerState.Go;
            }

            //MOVE VERTICLY
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _player.Velocity = new Vector2(_player.Velocity.X,-PLAYER_SPEED);
                _player.State = PlayerState.Go;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _player.State = PlayerState.Go;
                _player.Velocity = new Vector2(_player.Velocity.X, PLAYER_SPEED);
            }

            _prevKeyboardState = _keyboardState;
        } 

        private KeyboardState _keyboardState;
        private KeyboardState _prevKeyboardState;
        private Player _player;

        private const int PLAYER_SPEED = 200;
    }
}
