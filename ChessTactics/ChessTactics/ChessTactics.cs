#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessTactics
{
    /// <summary>
    /// Game extending the functionality of chess into a tactics game
    /// </summary>
    public class ChessTactics : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        ScreenFactory screenFactory;
        GameTime Time = new GameTime();

        /// <summary>
        /// The main game constructor.
        /// </summary>
        public ChessTactics()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Create the screen factory and add it to the Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            // Create the screen manager component.
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            // On Windows and Xbox we just add the initial screens
            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            //Activate the background
            screenManager.AddScreen(new BackgroundScreen(), null);

            // We have the menu laid on top of the background
            screenManager.AddScreen(new MainMenuScreen(), null);

            // Activate the logo screens.
            screenManager.AddScreen(new LogoScreen(screenManager, Time.TotalGameTime.TotalSeconds, 6), null);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //Background color behind all screens
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }
    }
}
