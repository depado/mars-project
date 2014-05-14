using System;
using Gtk;
using Gdk; // needed to handle key events . brings conflict between gtk.window and gdk.window.


namespace Gui_Test_Vi
{
    class ApplicationRun
    {

        // var 
        static string MainWindowTitle = "This is my main window title";
        static string RandomLabel = "Escape will get you out of the programm";


        static void main()
        {
            

        }

        // functions 

        public static Gtk.Window AppInit()
        {

            // starting init of the window
            Gtk.Window MyWin = new Gtk.Window(MainWindowTitle);
            MyWin.Fullscreen();

            /**************************************************
             * testing boxes
             ***************************************************/

            //defining elements

            


            Alignment Align = new Alignment(1,0, 0, 0);
            Align.SetPadding(100, 0, 10, 10);


            VBox main_menu = new VBox(false, 10);
            

            HBox menu1 = new HBox(false, 0);
            Label menu1_label = new Label("Menu 1");
            Button add = new Button();
            add.Label = " add";
            Button delete = new Button();
            delete.Label = "delete";

            HBox menu2 = new HBox(false, 0);
            Label menu2_label = new Label("Menu2");
            Button view = new Button("view");
            Button hide = new Button("hide");

            VBox menu3 = new VBox(false, 0);
            Label menu3_title = new Label("Menu3");
            Notebook content = new Notebook();

            menu1.Add(menu1_label);
            menu1.Add(add);
            menu1.Add(delete);

            menu2.Add(menu2_label);
            menu2.Add(view);
            menu2.Add(hide);

            menu3.Add(menu3_title);
            menu3.Add(content);

            main_menu.Add(menu1);
            main_menu.Add(menu2);
            main_menu.Add(menu3);

            







            

            
            // piling elements ...
            Align.Add(main_menu);

            // launch
            MyWin.Add(Align);

            
            /*
            HBox hbox = new HBox(false, 3);

            Alignment halign = new Alignment(0, 0, 0, 0);

            Button cutton1 = new Button("Button1");
            Button cutton2 = new Button("Button2");
            cutton1.SetSizeRequest(70,30);
            cutton2.SetSizeRequest(70,30);

            
            hbox.Add(cutton1);
            hbox.Add(cutton2);

            halign.Add(hbox);
            MyWin.Add(halign);
            */
           


            

            // keypress Handle
            MyWin.KeyPressEvent += KeyEvents;


            // ending the init of the window
            MyWin.ShowAll();
            return MyWin;
             
        }

        

        // key press event handler ( ESC )
        static void KeyEvents(object obj, KeyPressEventArgs args)
        {

            string keytest = args.Event.KeyValue.ToString();
            Console.WriteLine(keytest); // this is a reminder how to "print " keys 
            if (keytest == "65307") // this is ESC key
            {
                AppClose();
            }
            
            

        }

        public static void AppClose()
        {
            Application.Quit();
            

        }
    }
}
