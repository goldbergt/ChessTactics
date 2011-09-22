using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ChessTactics
{
    public class InputSystem
    {

        #region Fields and Properties
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        //Mouse and GamePad states
        #endregion

        #region Properties
        public bool MenuUp
        {
            get { return IsNewPressedKey(Keys.Up); }
        }
        public bool MenuDown
        {
            get { return IsNewPressedKey(Keys.Down); }
        }
        public bool MenuSelect
        {
            get { return IsNewPressedKey(Keys.Enter); }
        }
        public bool MenuCancel
        {
            get { return IsNewPressedKey(Keys.Escape); }
        }
        #endregion

        #region Input System Methods
        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        private bool IsNewPressedKey(Keys key)
        {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
        private bool IsPressedKey(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        #endregion

    }
}
