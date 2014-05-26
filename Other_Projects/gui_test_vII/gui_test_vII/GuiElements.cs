using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace gui_test_vII
{
    class GuiElements
    {
        // vars
        private Texture2D GUItexture;
        private Rectangle GUIrectangle;
        public string AssetName;
        public delegate void ElementClicked(string element);
        public event ElementClicked clickEvent;


        public GuiElements(string AssetName)
        {
            this.AssetName = AssetName;
           
        }

        

        public void Update()
        {
            if(GUIrectangle.Contains(new Point(Mouse.GetState().X,Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(AssetName);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            
            GUItexture = Content.Load<Texture2D>(AssetName);
            GUIrectangle = new Rectangle(0, 0, GUItexture.Width, GUItexture.Height);

        }

        public void IsTexture( int x, int y , int width, int height )
        {
            
            GUIrectangle = new Rectangle(x, y, width, height);

        }

        public void IsImage(int x, int y)
        {
            GUIrectangle = new Rectangle(GUIrectangle.X += x, GUIrectangle.Y += y, GUIrectangle.Width, GUIrectangle.Height);
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(GUItexture, GUIrectangle, Color.White);


        }

       

             
    }
}
