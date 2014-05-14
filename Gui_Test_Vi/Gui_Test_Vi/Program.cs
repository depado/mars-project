using System;
using Gtk;

namespace Gui_Test_Vi
{
    class Program
    {
        static void Main()
        {
            Application.Init();
            ApplicationRun MyApp = new ApplicationRun();
            Window MyWin = ApplicationRun.AppInit();
            Application.Run();
            
           
        }
    }
}
