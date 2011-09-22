using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessTactics
{
    class Button
    {
        public Vector2 position;
        public Texture2D tex;
        //Set up variables
        public Button(Vector2 position, Texture2D tex) //Our constructor
        {
            this.position = position; //Position in 2D
            this.tex = tex; //Our texture to draw
        }
        public void Draw(SpriteBatch batch) //Draw function, same as mousehandler one.
        {
            batch.Draw(tex, position, Color.White);
        }
    }
}
