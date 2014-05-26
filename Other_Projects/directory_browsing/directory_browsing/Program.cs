using System;
using System.IO;

namespace directory_browsing
{
    class Program
    {
        static void Main(string[] args)
        {
            // get current directory
            string[] Folders = Directory.GetDirectories(Directory.GetCurrentDirectory());
            Console.WriteLine("Current Directory : {0}", Directory.GetCurrentDirectory());

            //get files in current directory 
            string[] Files = Directory.GetFiles(Directory.GetCurrentDirectory(),"*.exe");
            Console.WriteLine("{0} .exe Files in main directory ", Files.Length);
            for (int j = 0; j < Files.Length; j++)
            {
                Console.WriteLine("    " + Files[j].Replace(Directory.GetCurrentDirectory() + "\\", ""));
            }


            // get directories in current directory
            for (int j = 0; j < Folders.Length; j++)
            {
                Console.WriteLine("    "+ Folders[j].Replace(Directory.GetCurrentDirectory(),""));
            }

           
            
           

            Console.ReadKey();
        
        }
    }
}
