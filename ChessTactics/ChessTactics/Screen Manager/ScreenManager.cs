using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessTactics
{
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields
        //List of current screens in the manager
        List<GameScreen> screens = new List<GameScreen>();

        //Another list dedicated to the screens that will be 
        //updated in the current game loop.
        List<GameScreen> screensToUpdate = new List<GameScreen>();

        //Spritebatch for 2D drawings
        SpriteBatch spriteBatch;
        
        //An input system so we can have control
        InputSystem inputSystem;

        //Is the screen manager initialized?
        bool isInitialized;
        #endregion

        #region Properties
        /// <summary>
        /// Return the sprite batch object.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        /// <summary>
        /// The input system property
        /// </summary>
        public InputSystem InputSystem
        {
            get { return inputSystem; }
        }

        /// <summary>
        /// Gets the content manager
        /// </summary>
        public ContentManager Content
        {
            get { return Game.Content; }
        }

        /// <summary>
        /// Gets the viewport object
        /// </summary>
        public Viewport Viewport
        {
            get { return GraphicsDevice.Viewport; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor, initializes the manager
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
            base.Initialize();

            //Creates a new InputSystem
            inputSystem = new InputSystem();
            isInitialized = true;
        }

        /// <summary>
        /// Initialize the spriteBatch and screen dedicated content.
        /// </summary>
        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load screen dedicated content
            foreach (GameScreen screen in screens)
                screen.LoadContent();
        }

        /// <summary>
        /// Unload screen dedicated content
        /// </summary>
        protected override void UnloadContent()
        {
            //Tells the screen to unload their content.
            foreach (GameScreen screen in screens)
                screen.UnloadContent();
        }
        #endregion

        #region Update and Draw

        /// <summary>
        /// Update manager and screens
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            //Update the input system
            inputSystem.Update();

            //clear out the screensToUpdate list to copy the screens list
            //this allows us to add or remove screens without complaining.
            screensToUpdate.Clear();

            if (screens.Count == 0)
                this.Game.Exit();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool screenIsCovered = false;
            bool firstScreen = true;

            if (!Game.IsActive)
            {
                //Pause logic, delete this if you want to remove
                //the functionality
            }
            else
            {
                while (screensToUpdate.Count > 0)
                {
                    GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                    screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                    //Update the screen unless its frozen or inactive
                    if (screen.ScreenState != ScreenState.Frozen
                        && screen.ScreenState != ScreenState.Inactive)
                    {
                        screen.Update(gameTime, screenIsCovered);
                    }

                    if (screen.IsActive)
                    {
                        if (firstScreen)
                        {
                            screen.HandleInput();
                            firstScreen = false;
                        }

                        if (!screen.IsPopup)
                            screenIsCovered = true;
                    }
                }
            }
        }

        /// <summary>
        /// Tells each screen to draw
        /// </summary>
        /// <param name="gameTime">Time object to pass to the screens</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                //Tells the current screen to draw if its not hidden
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a screen to the manager
        /// </summary>
        /// <param name="screen">The screen to be added</param>
        public void AddScreen(GameScreen screen)
        {
            //Sets the reference to the screen manager on the screen
            screen.ScreenManager = this;
            
            //If the screen manager is initialized, perform initialize operations 
            //for the screens.
            if (this.isInitialized)
            {
                screen.LoadContent();
                screen.Initialize();
            }

            //Finally, add the screen to the list.
            screens.Add(screen);
        }

        /// <summary>
        /// Removed the desired screen from the system
        /// </summary>
        /// <param name="screen">The screen we wish to remove</param>
        public void RemoveScreen(GameScreen screen)
        {
            //If the screen manager is initialized, unload the screen content.
            if (this.isInitialized)
            {
                screen.UnloadContent();
            }

            //Finally, remove the screen from both lists.
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }
        #endregion
    }
}
