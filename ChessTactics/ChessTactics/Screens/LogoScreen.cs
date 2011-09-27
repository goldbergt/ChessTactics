#region File Description
//-----------------------------------------------------------------------------
// LogoScreen.cs
//
// Microsoft XNA Community Game Platform
// Created by: Noah Castro
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
#endregion

namespace ChessTactics
{
    class LogoScreen : GameScreen
    {
        #region Fields

        //Content  manager to load the displayed logo
        ContentManager content;

        //texture will store the company logo to be displayed
        Texture2D logotexture;
        Texture2D pixeltexture;

        //Time coming in and the amount of time the logo will be on the screen
        Double InitialTime;
        Double TimeLimit;

        #endregion

        #region Initialization

        /// <summary>
        /// The constructor
        /// </summary>
        public LogoScreen(ScreenManager screenManager, Double InitialTime, Double TimeLimit)
        {
            //Assignment of the constructor values
            this.content = new ContentManager(screenManager.Game.Services, "Content");
            this.TimeLimit = TimeLimit;
            this.InitialTime = InitialTime;
            this.TransitionOffTime = TimeSpan.FromSeconds(5.0);
            this.TransitionOnTime = TimeSpan.FromSeconds(5.0);

            //Add logo screen to screenManager
            screenManager.AddScreen(this, null);
        }


        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            //Checks to see if the amount of time has passed for the screen to exit
            if ((gameTime.TotalGameTime.TotalSeconds - InitialTime) > TimeLimit)
            {
              this.ExitScreen();
            }
        }


        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //A new SpriteBatch is born
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            
            //Assign the viewport to the current window
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

            //Define the size and position of the logo to be drawn and its background
            Rectangle logo = new Rectangle(viewport.Width/4, viewport.Height/4, viewport.Width/2, viewport.Height/2);
            Rectangle pixel = new Rectangle(0, 0, viewport.Width, viewport.Height);
            //Load the image we will use for the logo
            logotexture = content.Load<Texture2D>("CompanyLogo");
            pixeltexture = content.Load<Texture2D>("singlePixel");

            //Draw the logo
            spriteBatch.Begin();

            spriteBatch.Draw(pixeltexture, pixel, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.Draw(logotexture, logo, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

            spriteBatch.End();
        }

        #endregion
    }
}
