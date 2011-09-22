using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChessTactics
{
    class MouseHandler
    {
        private Vector2 pos;
        private Texture2D tex;
        private MouseState mouseState;
        //We create variables

        public MouseHandler(Vector2 pos, Texture2D tex)
        {
            this.pos = pos; //Inital pos (0,0)
            this.tex = tex; //Cursor texture
        }
        //On Update we will call this function
        public void Update()
        {
            mouseState = Mouse.GetState(); //Needed to find the most current mouse states.
            this.pos.X = mouseState.X; //Change x pos to mouseX
            this.pos.Y = mouseState.Y; //Change y pos to mouseY
        }
        //Drawing function to be called in the main Draw function.
        public void Draw(SpriteBatch spriteBatch) //SpriteBatch to use.
        {
            spriteBatch.Draw(this.tex, this.pos, Color.White); //Draw it using the batch.
        }

        public bool ButtonClick(Button b)
        {
            if (this.pos.X >= b.position.X // To the right of the left side
            && this.pos.X < b.position.X + b.tex.Width //To the left of the right side
            && this.pos.Y > b.position.Y //Below the top side
            && this.pos.Y < b.position.Y + b.tex.Height) //Above the bottom side
                return true; //We are; return true.
            else
                return false; //We're not; return false.
        } 
    }
}
