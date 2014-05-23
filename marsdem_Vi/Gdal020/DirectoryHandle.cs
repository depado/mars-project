using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;
using System.Collections;

namespace Gdal020
{
    class DirectoryHandle
    {
        public DirectoryHandle(string directory) 
        {
            
            if (!Directory.Exists(directory))
            { Directory.CreateDirectory(directory); }


        }
    }
}
