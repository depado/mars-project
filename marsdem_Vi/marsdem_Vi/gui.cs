using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using MonoGui001;

namespace marsdem_Vi
{
    class gui
    {
        // context names
        List<guiElements> main = new List<guiElements>();

        //fullscreen resolution
        private int ResolutionWidth;
        private int ResolutionHeight;
        


        public gui()
        {

            // gui elements in context => !! raster bottom to top 
            main.Add(new guiElements("BkgNormal"));
            main.Add(new guiElements("MainMenu"));
            main.Add(new guiElements("MarsDem"));
            main.Add(new guiElements("About"));
            main.Add(new guiElements("NewMap"));
            main.Add(new guiElements("BrowseMap"));
            


        }

        /*********************************************
         * POSITIONNING
         *********************************************/
        public void Positionning()
        {
            int paddingX = -50;
            int paddingY = 25;
            LFind(main, "BkgNormal").IsImage(0, 0);
            LFind(main, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(main, "MarsDem").IsImage(LFind(main, "MainMenu").PosX() + paddingX, paddingY);
            LFind(main, "About").IsImage(LFind(main, "MainMenu").PosX() + paddingX, LFind(main, "MarsDem").PosY() + LFind(main, "MarsDem").Height() + paddingY);
            LFind(main, "NewMap").IsImage(LFind(main, "MainMenu").PosX() + paddingX, LFind(main, "About").PosY() + LFind(main, "About").Height() + paddingY);
            LFind(main, "BrowseMap").IsImage(LFind(main, "MainMenu").PosX() + paddingX, LFind(main, "NewMap").PosY() + LFind(main, "NewMap").Height() + paddingY);

        }

        // List find .
        private guiElements LFind(List<guiElements> ListItem,string guielement)
        {
            return ListItem.Find(x => x.AssetName == guielement);
        }

        // in full screen , working with responsive design.
        public void ScreenResolution(int ResolutionWidth, int ResolutionHeight)
        {
            
            this.ResolutionWidth = ResolutionWidth;
            this.ResolutionHeight = ResolutionHeight;
        }



        /*********************************************
         * LOADING CONTENTS 
         *********************************************/

        public void LoadContent(ContentManager content)
        {
            LoadContentLoop(content, main);

            Positionning();
        }

        // to ease the code reading up there 
        private void LoadContentLoop(ContentManager content, List<guiElements> listing)
        {
            foreach (guiElements element in listing)
            { 
                element.LoadContent(content);
            }
        }

        /*********************************************
         * UPDATING CONTENTS 
         *********************************************/
        public void Update() 
        {
            UpdateLoop(main);
        }

        private void UpdateLoop(List<guiElements> listing)
        {
            foreach (guiElements elements in listing)
            {
                elements.Update();
            }

        }

        /*********************************************
         * DRAW CONTENTS 
         *********************************************/
        public void Draw(SpriteBatch spritebatch)
        {
            DrawLoop(spritebatch, main);
        }

        private void DrawLoop(SpriteBatch spritebatch, List<guiElements> listing)
        {
            foreach (guiElements element in listing)
            {
                element.Draw(spritebatch);
                
            }
        }

    }
}
