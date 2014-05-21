using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MonoGui001
{
    public class FileExplorer
    {
        // var
        bool threadExist = false;
        public string FilePath= string.Empty;
        OpenFileDialog browseDialog = new OpenFileDialog();
        Thread t;

        public FileExplorer() 
        {

            
        }

        public void launch()
        {
            if (threadExist == false ) 
            {
                
                t = new Thread(new ThreadStart(launchbrowse)); 
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                threadExist = true;
             }

            

            
            
        }

        private void launchbrowse()
        {
            
            browseDialog.Filter = "lbl files|*.lbl";
            browseDialog.InitialDirectory = "C:";
            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = browseDialog.FileName;
                Console.WriteLine(FilePath);
                
                threadExist = false;
            }
           
        }
    }
}
