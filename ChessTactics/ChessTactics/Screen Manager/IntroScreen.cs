using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ChessTactics
{
    public class IntroScreen:GameScreen
    {
        #region Fields and Properties

        //texture holds our logo
        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        //pixel is just a single pixel that we can extend
        Texture2D pixel;
        public Texture2D Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }

        //How long the intro screen will be shown
        TimeSpan screenTime;
        public TimeSpan ScreenTime
        {
            get { return screenTime; }
            set { screenTime = value; }
        }

        //Used for the fade effect
        public byte Alpha
        {
            get { return (byte)(TransitionPercent * 255); }
        }

        //What is the desired opacity?
        float fadeOpacity;
        public float FadeOpacity
        {
            get { return fadeOpacity; }
            set { fadeOpacity = value; }
        }

        //Main background color (color of the pixel texture)
        Color fadeColor;
        public Color FadeColor
        {
            get { return fadeColor; }
            set { fadeColor = value; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// The constructor which will set properties
        /// across every intro screen
        /// </summary>
        public IntroScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(2.5);
            TransitionOffTime = TimeSpan.FromSeconds(2.5);
        }

        /// <summary>
        /// Unloads the texture and pixel if they exist
        /// </summary>
        public override void UnloadContent()
        {
            if(texture != null)
                texture = null;

            if (pixel != null)
                pixel = null;
        }
        #endregion

        #region Update and Draw
        /// <summary>
        /// Updates the screen from the timer
        /// </summary>
        /// <param name="gameTime">The time object</param>
        /// <param name="covered">Is the screen covered?</param>
        public override void Update(GameTime gameTime, bool covered)
        {
            if (ScreenState == ScreenState.Active)
            {
                //Update the timer
                screenTime = screenTime.Subtract(gameTime.ElapsedGameTime);

                //If our screen is up, exit
                if (screenTime.TotalSeconds <= 0)
                    ExitScreen();
            }
            base.Update(gameTime, covered);
        }

        /// <summary>
        /// Draw the logo and pixel.  Override to perform more logic
        /// </summary>
        /// <param name="gameTime">Time object for time based drawings</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;

            //Centers the texture on the screen
            Vector2 centerTexture = new Vector2((viewport.Width / 2) - (texture.Width / 2), 
                (viewport.Height / 2) - (texture.Height / 2));
            spriteBatch.Begin();

            //If our texture does not fill the entire screen, use the pixel to have a nice background
            if (texture.Width < viewport.Width || texture.Height < viewport.Height)
                DrawFade(spriteBatch, viewport);
            spriteBatch.Draw(texture, centerTexture, Color.White);
            spriteBatch.End();
        }

        /// <summary>
        /// Draws the fade by extending the pixel texture to fill the entire window
        /// </summary>
        /// <param name="spriteBatch">The current spriteBatch object</param>
        /// <param name="viewport">Useful for the screen dimensions</param>
        private void DrawFade(SpriteBatch spriteBatch, Viewport viewport)
        {
            if (pixel != null)
                spriteBatch.Draw(pixel, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
        }
        #endregion
    }
}
