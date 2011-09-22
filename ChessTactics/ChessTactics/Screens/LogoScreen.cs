using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ChessTactics
{
    //Logo screen is an intro screen
    public class LogoScreen:IntroScreen
    {
        public LogoScreen()
        {
            //Set all the properties to make an intro screen
            ScreenTime = TimeSpan.FromSeconds(3);
            FadeColor = Color.Black;
            FadeOpacity = 0.9f;
        }

        public override void LoadContent()
        {
            //Load the logo and pixel
            ContentManager content = ScreenManager.Game.Content;
            Texture = content.Load<Texture2D>("Company Logo");
            Pixel = content.Load<Texture2D>("singlePixel");
        }

        //Logic that occurs when the screen is done and needs removal
        public override void Remove()
        {
            //Add the menu screen to the manager
            ScreenManager.AddScreen(new MainMenuScreen());
            base.Remove();
        }
    }
}
