using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MonoGui001
{
    public class StreamingElements : guiElements
    {
        public StreamingElements(string AssetName) : base(AssetName) { }

        /*********************************************
         * POSITIONNING
         *********************************************/

        public void Resize(int x, int y, int width, int height)
        {
            GUIRectangle = new Rectangle(x, y, width, height);
        }

        



        public int PosX() { return GUIRectangle.X; }
        public int PosY() { return GUIRectangle.Y; }
        public int Width() { return GUIRectangle.Width; }
        public int Height() { return GUIRectangle.Height; }

        /*********************************************
         * TEXT ADDING
         *********************************************/
        public void label(SpriteBatch spritebatch, SpriteFont sf, string text, Color color)
        {
            Vector2 size = sf.MeasureString(text);
            int positionX = PosX();
            int positionY = PosY() + Height() + 10;
            spritebatch.DrawString(sf, text, new Vector2(positionX, positionY), color);
        }

        

        /*********************************************
         * LOADING CONTENTS 
         *********************************************/


        public void LoadContent(string Directory, ContentManager content)
        {
            //GUITexture 
            GUITexture = content.Load<Texture2D>("../" + Directory + "/" + AssetName+".bmp");
            GUIRectangle = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
        }

        /*********************************************
         * DRAW CONTENTS 
         *********************************************/
        public void Draw(SpriteBatch spritebatch)
        {
            
            spritebatch.Draw(GUITexture, GUIRectangle, Color.White);
            

        }
    }
}
