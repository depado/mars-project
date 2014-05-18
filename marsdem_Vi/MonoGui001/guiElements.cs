using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace MonoGui001
{
    public class guiElements
    {
        // var
        public string AssetName;
        private Texture2D GUITexture;
        private Rectangle GUIRectangle;
        

        public guiElements(string AssetName)
        {
            this.AssetName = AssetName;
        }

        /*********************************************
         * POSITIONNING
         *********************************************/

        public void IsTexture(int x, int y, int width, int height)
        {

            GUIRectangle = new Rectangle(x, y, width, height);

        }

        public void IsImage(int x, int y)
        {
           GUIRectangle = new Rectangle(GUIRectangle.X += x, GUIRectangle.Y += y, GUIRectangle.Width, GUIRectangle.Height);
        }

        public int PosX()   {   return GUIRectangle.X;  }
        public int PosY()   {   return GUIRectangle.Y;  }
        public int Width() { return GUIRectangle.Width; }
        public int Height() { return GUIRectangle.Height; }

        /*********************************************
         * TEXT ADDING
         *********************************************/

        /*********************************************
         * LOADING CONTENTS 
         *********************************************/


        public void LoadContent(ContentManager Content)
        {
            GUITexture = Content.Load<Texture2D>(AssetName);
            GUIRectangle = new Rectangle(0,0,GUITexture.Width, GUITexture.Height);
        }

        /*********************************************
         * UPDATING CONTENTS 
         *********************************************/

        public void Update() 
        {
        }

        /*********************************************
         * DRAW CONTENTS 
         *********************************************/
        public void  Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(GUITexture, GUIRectangle, Color.White);

        }

        /*********************************************
         * MISC 
         *********************************************/


    }
}
