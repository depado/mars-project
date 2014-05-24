using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.IO;


namespace MonoGui001
{
    public class guiElements
    {
        // var
        public string AssetName;
        protected Texture2D GUITexture;
        protected Rectangle GUIRectangle;
        
        // mouseEvent handling
        public delegate void ElementClicked(string element);
        public event ElementClicked ClickEvent;


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
        public void TextCenter(SpriteBatch spritebatch, SpriteFont sf, string text, Color color)
        {
            Vector2 size = sf.MeasureString(text);
            int positionX = PosX() + Width() / 2 - (int)size.X/2;
            int positionY = PosY() + Height() / 2 - (int)size.Y / 2;

            spritebatch.DrawString(sf, text, new Vector2(positionX, positionY), color);
        }

        public void TextFill(SpriteBatch spritebatch, SpriteFont sf, string text, int PaddingLeft, int PaddingTop,Color color)
        {
            // splitting text 
            List<string> TextProcessing = new List<string>();
            int StringLength = (Width()-PaddingLeft*2)*26 / (int)sf.MeasureString("azertyuiopqsdfghjklmwxcvbn").X;
            int j = 0;
            

            
            while (j != text.Length)
            {
                if (j + StringLength > text.Length) { StringLength = text.Length - j; }

                TextProcessing.Add(text.Substring(j,StringLength));
                j = j + StringLength;
                
                

            }

            text = "";
            foreach (string TextChunk in TextProcessing)
            {
                text = text + TextChunk + "\n";
            }

            int positionX = PosX() + PaddingLeft;
            int positionY = PosY() + PaddingTop;
            spritebatch.DrawString(sf, text, new Vector2(positionX, positionY), color);
        }

        public void TextFromFile(SpriteBatch spritebatch, SpriteFont sf, string file, Color color)
        {
            
            int positionX = PosX();
            int positionY = PosY() + 10;
            TextReader tr = new StreamReader(file);
            spritebatch.DrawString(sf, tr.ReadToEnd(), new Vector2(positionX, positionY), color);
            
            tr.Close();

        }

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
            if (GUIRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ClickEvent(AssetName);
            }
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
