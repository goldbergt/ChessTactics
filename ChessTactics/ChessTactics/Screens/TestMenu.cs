using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ChessTactics
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TestMenu : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch Batch;//Needed for 2D drawing
        MouseHandler ourMouse; //An instance of the MouseHandler class
        Texture2D onHover; //The Hover image texture
        Texture2D onClick; //The Click image texxture
        Vector2 hoverPos; //The position of the Hover sprite
        Vector2 clickPos; //The position of the Click sprite
        List<Button> buttonlist; //Our list of buttons
        Button mainButton; //An instance of Button
        bool Clicking; //Are we clicking?
        bool Hovering; //Are we hovering?
        MouseState mouseState; //MouseState needed for what checking 

        public TestMenu()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Batch = new SpriteBatch(GraphicsDevice);
            //Handle the button creation
            mainButton = new Button(new Vector2(250, 100), Content.Load<Texture2D>("Button"));
            //Actually creating the button.
            this.buttonlist = new List<Button>(); //Actually creating the list
            this.buttonlist.Add(mainButton); //Add the button we recently made to the list.

            //Handle the cursor creation
            ourMouse = new MouseHandler(new Vector2(0, 0), Content.Load<Texture2D>("Cursor"));
            //Actually creating the MouseHandler now.

            //Handle extra sprites.
            onHover = Content.Load<Texture2D>("Hovering"); //Sets texture to be used
            onClick = Content.Load<Texture2D>("Clicked"); //Sets texture to be used
            hoverPos = new Vector2(250, 200); //Sets position
            clickPos = new Vector2(250, 300); //Sets position

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleMouse()
        {
            mouseState = Mouse.GetState(); //Get the current state of the mouse
            if (ourMouse.ButtonClick(mainButton)) //There's only one button, so we just hardcode it.
                //If we're hovering over the mouse
                Hovering = true; //We ARE hovering
            else
                Hovering = false; //Not hovering.
            //We don't even need to use ButtonClick() again if we know if we're hovering.
            if (mouseState.LeftButton == ButtonState.Pressed && Hovering) //If we're clicking with the Left Mouse Button and we're over the button.
                Clicking = true; //We ARE clicking
            else
                Clicking = false; //Not clicking
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            HandleMouse(); //Check clicking
            ourMouse.Update(); //Update the mouse's position.

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Batch.Begin();
            //Between batch.Begin() and batch.End() we are allowed to draw.
            foreach (Button b in buttonlist) //For every button run through the code between the {}
            {
                //For every button in our list, draw it
                b.Draw(Batch);
            }
            if (Hovering) //If we are hovering
            {
                //Draw the hover sprite
                Batch.Draw(this.onHover, this.hoverPos, Color.White);
            }
            if (Clicking) //If we are clicking
            {
                //Draw the clicking sprite.
                Batch.Draw(this.onClick, this.clickPos, Color.White);
            }
            //Draw the mouse.
            ourMouse.Draw(Batch);
            //Done
            Batch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
