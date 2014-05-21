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
        // gameState
        enum GuiState { MainMenu, About, NewMap, BrowseMap, ViewMap, MapInfo };
        GuiState guistate;
        FileExplorer explorer = new FileExplorer();
        // context names
        List<guiElements> main = new List<guiElements>();
        List<guiElements> about = new List<guiElements>();
        List<guiElements> newmap = new List<guiElements>();
        List<guiElements> browsemap = new List<guiElements>();

        
        SpriteFont sf;

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

            about.Add(new guiElements("BkgBlur"));
            about.Add(new guiElements("BlankWindow"));
            about.Add(new guiElements("MainMenu"));
            about.Add(new guiElements("MarsDem"));
            about.Add(new guiElements("About"));
            about.Add(new guiElements("NewMap"));
            about.Add(new guiElements("BrowseMap"));

            newmap.Add(new guiElements("BkgNormal"));           
            newmap.Add(new guiElements("MainMenu"));
            newmap.Add(new guiElements("MarsDem"));
            newmap.Add(new guiElements("About"));
            newmap.Add(new guiElements("NewMap"));
            newmap.Add(new guiElements("BrowseMap"));

            browsemap.Add(new guiElements("BkgBlur"));
            browsemap.Add(new guiElements("BlankWindow"));
            browsemap.Add(new guiElements("MainMenu"));
            browsemap.Add(new guiElements("MarsDem"));
            browsemap.Add(new guiElements("About"));
            browsemap.Add(new guiElements("NewMap"));
            browsemap.Add(new guiElements("BrowseMap"));
        }

        /*********************************************
         * POSITIONNING (loadContent)
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


            LFind(about, "BkgBlur").IsImage(0, 0);
            LFind(about, "BlankWindow").IsTexture(100, 100, ResolutionWidth - 200 - 100 * 2, ResolutionHeight - 100 * 2);
            LFind(about, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(about, "MarsDem").IsImage(LFind(about, "MainMenu").PosX() + paddingX, paddingY);
            LFind(about, "About").IsImage(LFind(about, "MainMenu").PosX() + paddingX, LFind(about, "MarsDem").PosY() + LFind(about, "MarsDem").Height() + paddingY);
            LFind(about, "NewMap").IsImage(LFind(about, "MainMenu").PosX() + paddingX, LFind(about, "About").PosY() + LFind(about, "About").Height() + paddingY);
            LFind(about, "BrowseMap").IsImage(LFind(about, "MainMenu").PosX() + paddingX, LFind(about, "NewMap").PosY() + LFind(about, "NewMap").Height() + paddingY);

            LFind(newmap, "BkgNormal").IsImage(0, 0);            
            LFind(newmap, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(newmap, "MarsDem").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, paddingY);
            LFind(newmap, "About").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, LFind(newmap, "MarsDem").PosY() + LFind(newmap, "MarsDem").Height() + paddingY);
            LFind(newmap, "NewMap").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, LFind(newmap, "About").PosY() + LFind(newmap, "About").Height() + paddingY);
            LFind(newmap, "BrowseMap").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, LFind(newmap, "NewMap").PosY() + LFind(newmap, "NewMap").Height() + paddingY);

            LFind(browsemap, "BkgBlur").IsImage(0, 0);
            LFind(browsemap, "BlankWindow").IsTexture(100, 100, ResolutionWidth - 200 - 100 * 2, ResolutionHeight - 100 * 2);
            LFind(browsemap, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(browsemap, "MarsDem").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, paddingY);
            LFind(browsemap, "About").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, LFind(browsemap, "MarsDem").PosY() + LFind(browsemap, "MarsDem").Height() + paddingY);
            LFind(browsemap, "NewMap").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, LFind(browsemap, "About").PosY() + LFind(browsemap, "About").Height() + paddingY);
            LFind(browsemap, "BrowseMap").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, LFind(browsemap, "NewMap").PosY() + LFind(browsemap, "NewMap").Height() + paddingY);

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
         * ADDING TEXT (Draw)
         *********************************************/
        private void AddText(SpriteBatch spritebatch)
        {
            switch (guistate)
            {
                case GuiState.MainMenu:
                    LFind(main, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(main, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(main, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(main, "BrowseMap").TextCenter(spritebatch, sf, "Browse Map", Color.White);
                    break;
                case GuiState.About:
                    LFind(about, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(about, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(about, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(about, "BrowseMap").TextCenter(spritebatch, sf, "Browse Map", Color.White);
                    string aboutText = " Lorem ipsum dolor sit amet, consectetur adipiscing elit"+
                        ". Mauris tincidunt erat eros. Aenean fringilla ligula sit amet sem dapibus euismod. Vestibulum tincidunt lobortis leo, at dignissim nibh dapibus et. Suspendisse quis eros convallis, eleifend neque id, porttitor lectus. Quisque a lectus magna. Curabitur laoreet augue nunc, sit amet tristique nulla tincidunt sit amet. Cras eu bibendum diam, ac gravida ante.Maecenas auctor purus id commodo fermentum. In facilisis ut sem ut tempor. Mauris eget rutrum libero. Vivamus sit amet lacinia eros, sit amet convallis lacus. Fusce at quam elit. Phasellus vel ultricies turpis. In est ante, lobortis id massa at, sollicitudin blandit eros. Maecenas tincidunt massa non rutrum pretium. Praesent id placerat sapien. ";
                    LFind(about, "BlankWindow").TextFill(spritebatch, sf, aboutText, 10, 10, Color.Black);
                    break;
                case GuiState.NewMap:
                    LFind(newmap, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(newmap, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(newmap, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(newmap, "BrowseMap").TextCenter(spritebatch, sf, "Browse Map", Color.White);
                    break;
                case GuiState.BrowseMap:
                    LFind(browsemap, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(browsemap, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(browsemap, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(browsemap, "BrowseMap").TextCenter(spritebatch, sf, "Browse Map", Color.White);
                    string browsemapText = "Browse map element here ";
                    LFind(browsemap, "BlankWindow").TextFill(spritebatch, sf, browsemapText, 10, 10, Color.Black);
                    break;
                case GuiState.ViewMap:
                    break;
                case GuiState.MapInfo:
                    break;
                default:
                    break;
            }
            

        }

        /*********************************************
         * MOUSE EVENTS
         *********************************************/
        public void Onclick(string element)
        {
            if (element == "About") { guistate = GuiState.About; }
            if (element == "NewMap") { guistate = GuiState.NewMap; }
            if (element == "BrowseMap") { guistate = GuiState.BrowseMap; }

        }


        /*********************************************
         * LOADING CONTENTS 
         *********************************************/

        public void LoadContent(ContentManager content)
        {
            // font load
            sf = content.Load<SpriteFont>("MyFont");

            // gui load
            LoadContentLoop(content, main);
            LoadContentLoop(content, about);
            LoadContentLoop(content, newmap);
            LoadContentLoop(content, browsemap);
            Positionning();
        }

        // to ease the code reading up there 
        private void LoadContentLoop(ContentManager content, List<guiElements> listing)
        {
            foreach (guiElements element in listing)
            { 
                element.LoadContent(content);
                element.ClickEvent += Onclick;
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
            switch (guistate)
            {
                case GuiState.MainMenu:
                    DrawLoop(spritebatch, main);
                    break;
                case GuiState.About:
                    DrawLoop(spritebatch, about);
                    break;
                case GuiState.NewMap:
                    DrawLoop(spritebatch, newmap);
                    explorer.launch();
                    if (explorer.FilePath == string.Empty) { guistate = GuiState.NewMap; }
                    else { guistate = GuiState.BrowseMap; 
                            // GDAL TRANSFORM HERE
                            explorer.FilePath = string.Empty; }
                    break;
                case GuiState.BrowseMap:
                    DrawLoop(spritebatch, browsemap);
                    break;
                case GuiState.ViewMap:
                    break;
                case GuiState.MapInfo:
                    break;
                default:
                    break;  
            }

           
            
            AddText(spritebatch);

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
