using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ChessTactics
{
    public class MainMenuScreen : MenuScreen
    {
        MenuEntry entry1, entry2;
        public MainMenuScreen()
        {
            TransitionOffTime = TimeSpan.Zero;

            //The selected and nonselected color
            Selected = Color.Yellow;
            NonSelected = Color.White;

            Removed += new EventHandler(MainMenuRemove);
        }

        public override void Initialize()
        {
            BackgroundPosition = new Vector2(0, 0);
            //Add the menu entries
            //Change the string to change the entry titles
            entry1 = new MenuEntry(this, "Logo Screen");
            entry1.SetPosition(new Vector2(100, 200), true);
            entry1.Selected += new EventHandler(Entry1Select);
            MenuEntries.Add(entry1);

            entry2 = new MenuEntry(this, "Quit");
            entry2.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 10), entry1, true);
            entry2.Selected += new EventHandler(Entry2Select);
            MenuEntries.Add(entry2);
        }

        public override void LoadContent()
        {
            //Load the font that this menu will use
            ContentManager content = ScreenManager.Game.Content;
            SpriteFont = content.Load<SpriteFont>("font");
            BackgroundTexture = content.Load<Texture2D>("ChessTacticsMenuBackground");
        }

        void MainMenuRemove(object sender, EventArgs e)
        {
            MenuEntries.Clear();
        }

        void LogoScreenOnRemove(object sender, EventArgs e)
        {
            MenuEntries.Clear();
            new TestMenu();
        }
        void Entry1Select(object sender, EventArgs e)
        {
            Removed += new EventHandler(LogoScreenOnRemove);
            ExitScreen();
        }
        void Entry2Select(object sender, EventArgs e)
        {
            TransitionOffTime = TimeSpan.FromSeconds(1.5);
            Removed += new EventHandler(MainMenuRemove);
            ExitScreen();
        }
    }
}
