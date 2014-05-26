using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace gui_test_vII
{
    class MainMenu
    {
        enum GameState { Mainmenu, entername, InGame }
        GameState gamestate ;

        List<GuiElements> main = new List<GuiElements>();
        List<GuiElements> entername = new List<GuiElements>();
        private int ResolutionWidth;
        private int ResolutionHeight;

        private Keys[] lastpressedkeys = new Keys[5];
        private string MyName = string.Empty;
        private SpriteFont sf;

        public MainMenu()
        {
            main.Add(new GuiElements("menu"));
            main.Add(new GuiElements("play"));
            main.Add(new GuiElements("name"));

            entername.Add(new GuiElements("playerName"));
            entername.Add(new GuiElements("done"));
            

            
            
        }

        public void LoadContent(ContentManager content)
        {
            sf = content.Load<SpriteFont>("MyFont");

            foreach (GuiElements element in main)
            {

               element.LoadContent(content);
               element.clickEvent += Onclick;
            }

            foreach (GuiElements element in entername)
            {
                element.LoadContent(content);
                element.clickEvent += Onclick;

            }
            Positionning();
            
        }

        // positionning the elements
        public void Positionning()
        {
            main.Find(x => x.AssetName == "menu").IsTexture(ResolutionWidth - 200, 0, 200, ResolutionHeight);
            main.Find(x => x.AssetName == "play").IsImage((ResolutionWidth - 175), 40); // 150X40
            main.Find(x => x.AssetName == "name").IsImage((ResolutionWidth - 175), 160); // 150X40

            entername.Find(x => x.AssetName == "playerName").IsImage((ResolutionWidth / 2 - 200), (ResolutionHeight/2-200)); // 400X400
            entername.Find(x => x.AssetName == "done").IsImage((ResolutionWidth / 2 - 75), (ResolutionHeight / 2 - 20 + 110));// 150X40
        }

        public void Update() 
        {

            switch (gamestate)
            {
                case GameState.Mainmenu:
                    foreach (GuiElements element in main)
                    {
                        element.Update();
                    }
                    break;
                case GameState.entername:
                    foreach (GuiElements element in main)
                    {
                        element.Update();
                    }
                    foreach (GuiElements element in entername)
                    {
                        element.Update();
                    }
                    GetKeys();
                    break;
                case GameState.InGame:
                    break;
                default:
                    break;
            }

            

            

        }

        // events here !
        public void Onclick(string element) 
        {
            if (element == "play")
            { 
                // play the game 
                gamestate = GameState.InGame;
            }
            if (element == "name")
            {
                gamestate = GameState.entername;
            }
            if (element == "done")
            {
                gamestate = GameState.Mainmenu;
            }
        }

        public void Draw(SpriteBatch spritebatch) 
        {
            switch (gamestate)
            {
                case GameState.Mainmenu:
                    foreach (GuiElements element in main)
                    {
                        element.Draw(spritebatch);

                    }
                    spritebatch.DrawString(sf, MyName, new Vector2((ResolutionWidth -150 ), (200)), Color.Black);
                    break;
                case GameState.entername:
                    foreach (GuiElements element in main)
                    {
                        element.Draw(spritebatch);
                    }
                    foreach (GuiElements element in entername)
                    {
                        element.Draw(spritebatch);
                    }
                    spritebatch.DrawString(sf, MyName, new Vector2((ResolutionWidth / 2 - 140), (ResolutionHeight /2)), Color.Black);
                    break;
                case GameState.InGame:
                    break;
                default:
                    break;
            }
            
            

        }

        public void ScreenResolution(int ResolutionWidth, int ResolutionHeight)
            {
                this.ResolutionHeight = ResolutionHeight;
                this.ResolutionWidth = ResolutionWidth;
            }
        public void GetKeys() 
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();
            foreach (Keys key in lastpressedkeys)
            {
                if (!pressedKeys.Contains(key))
                { 
                    // key is no longer pressed 
                    onKeyUp(key);
                }
            }
            foreach (Keys key in pressedKeys)
            {
                if (!lastpressedkeys.Contains(key))
                {
                    onKeyDown(key);
                }

                
            }
            lastpressedkeys = pressedKeys;
        }

        public void onKeyUp(Keys key  ) 
        {

        }

        public void onKeyDown(Keys key)
        {
            if (key == Keys.Back && MyName.Length > 0)
            {
                MyName = MyName.Remove(MyName.Length - 1);
            }
            else
            {
                MyName += key.ToString();
            }
        }


        }

    }

