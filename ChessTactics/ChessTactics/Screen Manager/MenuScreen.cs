using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessTactics
{

    public abstract class MenuScreen : GameScreen
    {
        #region Fields and Properties

        /// <summary>
        /// The parent screen to revert back to when the menu is done.
        /// </summary>
        GameScreen parent;
        public GameScreen Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// List of menu entries to be displayed
        /// </summary>
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        public List<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        /// <summary>
        /// The spritefont object that controls the text for this menu
        /// </summary>
        SpriteFont spriteFont;
        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }

        /// <summary>
        /// The texture to use as the background
        /// </summary>
        Texture2D backgroundTexture;
        public Texture2D BackgroundTexture
        {
            get { return backgroundTexture; }
            set { backgroundTexture = value; }
        }

        /// <summary>
        /// The position of the background texture
        /// </summary>
        Vector2 backgroundPosition;
        public Vector2 BackgroundPosition
        {
            get { return backgroundPosition; }
            set { backgroundPosition = value; }
        }

        //The position of our items
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //The color of the item when it is selected
        Color selected;
        public Color Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        //The color of the text when it is not selected
        Color nonselected;
        public Color NonSelected
        {
            get { return nonselected; }
            set { nonselected = value; }
        }

        //The selected menu item
        int selectedEntry = 0;

        public event EventHandler Cancel;
        #endregion

        #region Menu Operations
        //When we cancel the menu, this method is called
        public virtual void MenuCancel() 
        {
            ExitScreen();
            if (parent != null && parent.ScreenState == ScreenState.Frozen)
                parent.ScreenState = ScreenState.Active;
            if (Cancel != null)
                Cancel(this, EventArgs.Empty);
        }
        #endregion

        #region Initialization

        //Constructor to set the transition timing
        public MenuScreen()
        {
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(1.5);
        }

        public MenuScreen(GameScreen screen)
        {
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(1.5);
            if (screen.ScreenState != ScreenState.Frozen)
                screen.ScreenState = ScreenState.Frozen;
            parent = screen;
        }
        //Unloads the font, if there is one
        public override void UnloadContent()
        {
            if (SpriteFont != null)
                SpriteFont = null;
        }
        #endregion

        #region Update and Draw

        //Moves to a different selected menu item and accepts an item
        public override void HandleInput()
        {
            //Grab a reference to the InputSystem object
            InputSystem input = ScreenManager.InputSystem;

            //If we move up or down, select a different entry
            if (input.MenuUp)
            {
                selectedEntry--;
                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }
            if (input.MenuDown)
            {
                selectedEntry++;
                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            //If we press the select button, call the MenuSelect method
            if (input.MenuSelect)
            {
                menuEntries[selectedEntry].Select();
            }

            //If we press the cancel button, call the MenuCancel method
            if (input.MenuCancel)
            {
                MenuCancel();
            }
        }

        //Updates the menu items position based on transition
        public override void Update(GameTime gameTime, bool covered)
        {
            //We want base.Update to come before our actual logic so the transition updates.
            base.Update(gameTime, covered);
            for (int i = 0; i < menuEntries.Count; i++)
            {
                menuEntries[i].Update(gameTime, i == selectedEntry);
                menuEntries[selectedEntry].Highlight();
            }
        }

        //Draws all the menu items, and increments the positions Y component
        //for every new item
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            Vector2 menuPosition = new Vector2(position.X, position.Y);
            spriteBatch.Begin();
            if (backgroundTexture != null)
                spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = (i == selectedEntry);
                //DrawEntry(spriteBatch, gameTime, menuEntries[i], menuPosition, isSelected);
                menuEntries[i].Draw(gameTime, isSelected);
                menuPosition.Y += spriteFont.LineSpacing;
            }
            spriteBatch.End();
        }
        #endregion
    }
}
