using System;
using System.IO;

namespace Gdal020
{
    public class GdalHub
    {
        // var
        DirectoryHandle directoryHandle;
        MapInfo mapInfo;
        MapBmp mapBmp;
        public string MapName = string.Empty;

        public GdalHub(string FilePath, string Directory)
        {
            directoryHandle = new DirectoryHandle(Directory);
            mapInfo = new MapInfo(FilePath,Directory);
            mapBmp = new MapBmp(FilePath, Directory);
            MapName = Path.GetFileNameWithoutExtension(FilePath);
        }
    }
}
