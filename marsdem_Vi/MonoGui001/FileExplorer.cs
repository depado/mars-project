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
        public bool FileDialogFocus = false;
        public string FilePath= string.Empty;
        OpenFileDialog browseDialog; 
        Thread t;

        public FileExplorer() 
        {

            
        }

        public void launch()
        {
            
            if (FileDialogFocus == false && t == null )
            {
                
                t = new Thread(new ThreadStart(launchbrowse)); 
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                FileDialogFocus = true;
                
             }
            else if (FileDialogFocus == false && t != null)
            {
                
                t = null;
                
            }
            

            

            
            
        }

        private void launchbrowse()
        {


            browseDialog = new OpenFileDialog();
            browseDialog.Filter = "lbl files|*.lbl";
            browseDialog.InitialDirectory = "C:";
            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = browseDialog.FileName;
                FileDialogFocus = false;
            }
            if (browseDialog.ShowDialog() == DialogResult.Cancel)
            {
                FilePath = "search cancelled";
                FileDialogFocus = false;
                
            }
           
        }


        
} 
    }

