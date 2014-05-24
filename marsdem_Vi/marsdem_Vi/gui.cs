using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using MonoGui001;
using Gdal020;
using System.IO;


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
        List<StreamingElements> browsemapThumb = new List<StreamingElements>(); // adding a thumb list for browsemap

        List<guiElements> viewmap = new List<guiElements>();
        List<StreamingElements> viewmapStream = new List<StreamingElements>();

        List<guiElements> mapinfo = new List<guiElements>();
        

        // StreamingElements adjustment + misc
        SpriteFont sf;
        SpriteFont seni;
        int Newwidth;
        int Scale;
        int posX;
        int posY;

        //fullscreen resolution
        private int ResolutionWidth;
        private int ResolutionHeight;
        
        // gdal elements
        GdalHub gdalHub;
        string MapName;
        
        string GdalDirectory = "marsmaps";

        public gui()
        {
            /*********************************************
            * gui elements in context => !! raster bottom to top 
            *********************************************/
            // main menu
            main.Add(new guiElements("BkgNormal"));
            main.Add(new guiElements("MainMenu"));
            main.Add(new guiElements("MarsDem"));
            main.Add(new guiElements("About"));
            main.Add(new guiElements("NewMap"));
            main.Add(new guiElements("BrowseMap"));

            // about menu
            about.Add(new guiElements("BkgBlur"));
            about.Add(new guiElements("BlankWindow"));
            about.Add(new guiElements("MainMenu"));
            about.Add(new guiElements("MarsDem"));
            about.Add(new guiElements("About"));
            about.Add(new guiElements("NewMap"));
            about.Add(new guiElements("BrowseMap"));

            // newmap menu
            newmap.Add(new guiElements("BkgNormal"));           
            newmap.Add(new guiElements("MainMenu"));
            newmap.Add(new guiElements("MarsDem"));
            newmap.Add(new guiElements("About"));
            newmap.Add(new guiElements("NewMap"));
            newmap.Add(new guiElements("BrowseMap"));

            // browsemap menu
            browsemap.Add(new guiElements("BkgBlur"));
            browsemap.Add(new guiElements("BlankWindow"));
            browsemap.Add(new guiElements("MainMenu"));
            browsemap.Add(new guiElements("MarsDem"));
            browsemap.Add(new guiElements("About"));
            browsemap.Add(new guiElements("NewMap"));
            browsemap.Add(new guiElements("BrowseMap"));
            foreach (string Files in Directory.GetFiles(GdalDirectory,"*_icon.bmp"))
            {
                browsemapThumb.Add(new StreamingElements(Path.GetFileNameWithoutExtension(Files)));
            }

            // viewmap menu
            viewmap.Add(new guiElements("BkgBlur"));
            viewmap.Add(new guiElements("BlankWindow"));
            viewmap.Add(new guiElements("MainMenu"));
            viewmap.Add(new guiElements("MarsDem"));
            viewmap.Add(new guiElements("About"));
            viewmap.Add(new guiElements("NewMap"));
            viewmap.Add(new guiElements("BrowseMap"));
            viewmap.Add(new guiElements("MapInfo"));
            foreach (string Files in Directory.GetFiles(GdalDirectory, "*_icon.bmp"))
            {
               viewmapStream.Add(new StreamingElements(Path.GetFileNameWithoutExtension(Files)));
            }

            // mapinfo menu
            mapinfo.Add(new guiElements("BkgBlur"));
            mapinfo.Add(new guiElements("BlankWindow"));
            mapinfo.Add(new guiElements("MainMenu"));
            mapinfo.Add(new guiElements("MarsDem"));
            mapinfo.Add(new guiElements("About"));
            mapinfo.Add(new guiElements("NewMap"));
            mapinfo.Add(new guiElements("BrowseMap"));
            mapinfo.Add(new guiElements("MapInfo"));
            
        }

        /*********************************************
         * POSITIONNING (loadContent)
         *********************************************/
        public void Positionning()
        {
            int paddingX = -50;
            int paddingY = 25;

            // main menu
            LFind(main, "BkgNormal").IsImage(0, 0);
            LFind(main, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(main, "MarsDem").IsImage(LFind(main, "MainMenu").PosX() + paddingX, paddingY);
            LFind(main, "About").IsImage(LFind(main, "MainMenu").PosX() + paddingX, LFind(main, "MarsDem").PosY() + LFind(main, "MarsDem").Height() + paddingY);
            LFind(main, "NewMap").IsImage(LFind(main, "MainMenu").PosX() + paddingX, LFind(main, "About").PosY() + LFind(main, "About").Height() + paddingY);
            LFind(main, "BrowseMap").IsImage(LFind(main, "MainMenu").PosX() + paddingX, LFind(main, "NewMap").PosY() + LFind(main, "NewMap").Height() + paddingY);

            // about menu
            LFind(about, "BkgBlur").IsImage(0, 0);
            LFind(about, "BlankWindow").IsTexture(100, 100, ResolutionWidth - 200 - 100 * 2, ResolutionHeight - 100 * 2);
            LFind(about, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(about, "MarsDem").IsImage(LFind(about, "MainMenu").PosX() + paddingX, paddingY);
            LFind(about, "About").IsImage(LFind(about, "MainMenu").PosX() + paddingX, LFind(about, "MarsDem").PosY() + LFind(about, "MarsDem").Height() + paddingY);
            LFind(about, "NewMap").IsImage(LFind(about, "MainMenu").PosX() + paddingX, LFind(about, "About").PosY() + LFind(about, "About").Height() + paddingY);
            LFind(about, "BrowseMap").IsImage(LFind(about, "MainMenu").PosX() + paddingX, LFind(about, "NewMap").PosY() + LFind(about, "NewMap").Height() + paddingY);

            // newmap menu
            LFind(newmap, "BkgNormal").IsImage(0, 0);            
            LFind(newmap, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(newmap, "MarsDem").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, paddingY);
            LFind(newmap, "About").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, LFind(newmap, "MarsDem").PosY() + LFind(newmap, "MarsDem").Height() + paddingY);
            LFind(newmap, "NewMap").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, LFind(newmap, "About").PosY() + LFind(newmap, "About").Height() + paddingY);
            LFind(newmap, "BrowseMap").IsImage(LFind(newmap, "MainMenu").PosX() + paddingX, LFind(newmap, "NewMap").PosY() + LFind(newmap, "NewMap").Height() + paddingY);

            // browsemap menu
            LFind(browsemap, "BkgBlur").IsImage(0, 0);
            LFind(browsemap, "BlankWindow").IsTexture(100, 100, ResolutionWidth - 200 - 100 * 2, ResolutionHeight - 100 * 2);
            LFind(browsemap, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(browsemap, "MarsDem").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, paddingY);
            LFind(browsemap, "About").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, LFind(browsemap, "MarsDem").PosY() + LFind(browsemap, "MarsDem").Height() + paddingY);
            LFind(browsemap, "NewMap").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, LFind(browsemap, "About").PosY() + LFind(browsemap, "About").Height() + paddingY);
            LFind(browsemap, "BrowseMap").IsImage(LFind(browsemap, "MainMenu").PosX() + paddingX, LFind(browsemap, "NewMap").PosY() + LFind(browsemap, "NewMap").Height() + paddingY);

                            // creating the thumbs here for the browsemap
            int counterX =0;
            int counterY = 0;
            

            foreach (StreamingElements elements in browsemapThumb)  
            {
                Newwidth = LFind(browsemap, "BlankWindow").Width()/4-50;
                Scale = (int)elements.Width() / Newwidth;
                posX= ( LFind(browsemap, "BlankWindow").PosX()+10)+ (Newwidth+10) *counterX;
                posY = ( LFind(browsemap, "BlankWindow").PosY()+40 )+ (int)elements.Height() * counterY / Scale+counterY*50;
                elements.Resize(posX,posY, Newwidth, (int)elements.Height() / Scale);
                if (counterX == 3) { counterX = 0; counterY++; }
                else { counterX++; }

            }


            // viewmap menu
            LFind(viewmap, "BkgBlur").IsImage(0, 0);
            //LFind(viewmap, "BlankWindow").IsTexture(100, 100, ResolutionWidth - 200 - 100 * 2, ResolutionHeight - 100 * 2);
            LFind(viewmap, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(viewmap, "MarsDem").IsImage(LFind(viewmap, "MainMenu").PosX() + paddingX, paddingY);
            LFind(viewmap, "About").IsImage(LFind(viewmap, "MainMenu").PosX() + paddingX, LFind(viewmap, "MarsDem").PosY() + LFind(viewmap, "MarsDem").Height() + paddingY);
            LFind(viewmap, "NewMap").IsImage(LFind(viewmap, "MainMenu").PosX() + paddingX, LFind(viewmap, "About").PosY() + LFind(viewmap, "About").Height() + paddingY);
            LFind(viewmap, "BrowseMap").IsImage(LFind(viewmap, "MainMenu").PosX() + paddingX, LFind(viewmap, "NewMap").PosY() + LFind(viewmap, "NewMap").Height() + paddingY);
            LFind(viewmap, "MapInfo").IsImage(LFind(viewmap, "MainMenu").PosX() + paddingX, LFind(viewmap, "BrowseMap").PosY() + LFind(viewmap, "BrowseMap").Height() + paddingY);
            
            foreach (StreamingElements elements in viewmapStream)
            {
                elements.Resize((ResolutionWidth-1024-200)/2,(ResolutionHeight-elements.Height())/2,elements.Width(),elements.Height());
            }

            // mapinfo menu
            LFind(mapinfo, "BkgBlur").IsImage(0, 0);
            LFind(mapinfo, "BlankWindow").IsTexture(100, 50, ResolutionWidth - 200 - 100 * 2, ResolutionHeight - 100);
            LFind(mapinfo, "MainMenu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            LFind(mapinfo, "MarsDem").IsImage(LFind(mapinfo, "MainMenu").PosX() + paddingX, paddingY);
            LFind(mapinfo, "About").IsImage(LFind(mapinfo, "MainMenu").PosX() + paddingX, LFind(mapinfo, "MarsDem").PosY() + LFind(mapinfo, "MarsDem").Height() + paddingY);
            LFind(mapinfo, "NewMap").IsImage(LFind(mapinfo, "MainMenu").PosX() + paddingX, LFind(mapinfo, "About").PosY() + LFind(mapinfo, "About").Height() + paddingY);
            LFind(mapinfo, "BrowseMap").IsImage(LFind(mapinfo, "MainMenu").PosX() + paddingX, LFind(mapinfo, "NewMap").PosY() + LFind(mapinfo, "NewMap").Height() + paddingY);
            LFind(mapinfo, "MapInfo").IsImage(LFind(mapinfo, "MainMenu").PosX() + paddingX, LFind(mapinfo, "BrowseMap").PosY() + LFind(mapinfo, "BrowseMap").Height() + paddingY);
            
            
        }

        // List find .
        private guiElements LFind(List<guiElements> ListItem, string guielement)
        {
            return ListItem.Find(x => x.AssetName == guielement);
        }

        private StreamingElements LFindThumb(List<StreamingElements> ListItem, string streamingElements)
        {
            return ListItem.Find(x => x.AssetName == streamingElements);
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
                    LFind(main, "BrowseMap").TextCenter(spritebatch, sf, "Browse Maps", Color.White);
                    break;
                case GuiState.About:
                    LFind(about, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(about, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(about, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(about, "BrowseMap").TextCenter(spritebatch, sf, "Browse Maps", Color.White);
                    string aboutText = " Lorem ipsum dolor sit amet, consectetur adipiscing elit"+
                        ". Mauris tincidunt erat eros. Aenean fringilla ligula sit amet sem dapibus euismod. Vestibulum tincidunt lobortis leo, at dignissim nibh dapibus et. Suspendisse quis eros convallis, eleifend neque id, porttitor lectus. Quisque a lectus magna. Curabitur laoreet augue nunc, sit amet tristique nulla tincidunt sit amet. Cras eu bibendum diam, ac gravida ante.Maecenas auctor purus id commodo fermentum. In facilisis ut sem ut tempor. Mauris eget rutrum libero. Vivamus sit amet lacinia eros, sit amet convallis lacus. Fusce at quam elit. Phasellus vel ultricies turpis. In est ante, lobortis id massa at, sollicitudin blandit eros. Maecenas tincidunt massa non rutrum pretium. Praesent id placerat sapien. ";
                    LFind(about, "BlankWindow").TextFill(spritebatch, sf, aboutText, 10, 10, Color.Black);
                    break;
                case GuiState.NewMap:
                    LFind(newmap, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(newmap, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(newmap, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(newmap, "BrowseMap").TextCenter(spritebatch, sf, "Browse Maps", Color.White);
                    break;
                case GuiState.BrowseMap:
                    LFind(browsemap, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(browsemap, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(browsemap, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(browsemap, "BrowseMap").TextCenter(spritebatch, sf, "Browse Maps", Color.White);
                    // making the labels for the thumbs
                    foreach (StreamingElements elements in browsemapThumb)
                    {
                        elements.label(spritebatch, sf, elements.AssetName.Replace("_icon",""), Color.Black);
                    }
                    break;
                case GuiState.ViewMap:
                    LFind(viewmap, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(viewmap, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(viewmap, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(viewmap, "BrowseMap").TextCenter(spritebatch, sf, "Browse Maps", Color.White);
                    LFind(viewmap, "MapInfo").TextCenter(spritebatch, sf, "Map infos", Color.White);
                    break;
                case GuiState.MapInfo:
                    LFind(mapinfo, "MarsDem").TextCenter(spritebatch, sf, "Mars Dem", Color.Black);
                    LFind(mapinfo, "About").TextCenter(spritebatch, sf, "About...", Color.Black);
                    LFind(mapinfo, "NewMap").TextCenter(spritebatch, sf, "New Map", Color.White);
                    LFind(mapinfo, "BrowseMap").TextCenter(spritebatch, sf, "Browse Maps", Color.White);
                    LFind(mapinfo, "MapInfo").TextCenter(spritebatch, sf, "Map infos", Color.White);
                    LFind(mapinfo, "BlankWindow").TextFromFile(spritebatch, seni, GdalDirectory + "/" + MapName + ".nfo", Color.Black);
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
            if (element == "About") { guistate = GuiState.About;  }
            if (element == "NewMap") { guistate = GuiState.NewMap;  }
            if (element == "BrowseMap") { guistate = GuiState.BrowseMap; }
            if (LFindThumb(browsemapThumb, element) != null && guistate == GuiState.BrowseMap) { guistate = GuiState.ViewMap; MapName = LFindThumb(browsemapThumb, element).AssetName.Replace("_icon", "");}
            if (element == "MapInfo" ) { guistate = GuiState.MapInfo;  }
            if (element == "BlankWindow" && guistate == GuiState.MapInfo) { guistate = GuiState.ViewMap; }
            
        }


        /*********************************************
         * LOADING CONTENTS 
         *********************************************/

        public void LoadContent(ContentManager content)
        {
            // font load
            sf = content.Load<SpriteFont>("MyFont");
            seni = content.Load<SpriteFont>("seni");
            // gui load
            LoadContentLoop(content, main);
            LoadContentLoop(content, about);
            LoadContentLoop(content, newmap);
            LoadContentLoop(content, browsemap);
            LoadThumbLoop(content, browsemapThumb);
            LoadContentLoop(content, viewmap);
            LoadThumbLoop(content, viewmapStream);
            LoadContentLoop(content, mapinfo);
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

        private void LoadThumbLoop(ContentManager content, List<StreamingElements> listing)
        {
            foreach (StreamingElements element in listing)
            {
                
                element.LoadContent(GdalDirectory, content);
                element.ClickEvent += Onclick;
            }
        }

        /*********************************************
         * UPDATING CONTENTS 
         *********************************************/
        public void Update() 
        {
            UpdateLoop(main);
            UpdateLoopThumb(browsemapThumb);
            UpdateLoopThumb(viewmapStream);
            UpdateLoop(viewmap);
            UpdateLoop(mapinfo);
            
        }

        private void UpdateLoop(List<guiElements> listing)
        {
            foreach (guiElements elements in listing)
            {
                elements.Update();
            }
            
        }
        private void UpdateLoopThumb(List<StreamingElements> listing)
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
                    else if(explorer.FilePath != string.Empty )
                    {
                        
                        if (explorer.FilePath != "search cancelled")
                        {
                            // GDAL TRANSFORM HERE
                            gdalHub = new GdalHub(explorer.FilePath,GdalDirectory);
                            MapName = gdalHub.MapName;
                            
                            guistate = GuiState.BrowseMap;
                        }
                        else { guistate = GuiState.ViewMap; }
                        
                        
                        explorer.FilePath = string.Empty;
                    }
                    
                    break;
                case GuiState.BrowseMap:
                    DrawLoop(spritebatch, browsemap);
                    DrawThumb(spritebatch, browsemapThumb);
                    break;
                case GuiState.ViewMap:
                    DrawLoop(spritebatch, viewmap);
                    LFindThumb(viewmapStream, MapName + "_icon").Draw(spritebatch);
                    
                    break;
                case GuiState.MapInfo:
                    DrawLoop(spritebatch, mapinfo);
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

        private void DrawThumb(SpriteBatch spritebatch, List<StreamingElements> listing)
        {
            foreach (StreamingElements element in listing)
            {
                element.Draw(spritebatch);
            }
        }
            

    }
}
