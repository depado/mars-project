/****************************************
 * this a try and understand test on GDALInfo runned on a MOLA megt00n000hb found here : 
 * http://pds-geosciences.wustl.edu/mgs/mgs-m-mola-5-megdr-l3-v1/mgsl_300x/meg128/
 *  see reference here : 
 *  http://svn.osgeo.org/gdal/trunk/gdal/swig/csharp/apps/GDALInfo.cs
 **********************************/

/******************************************************************************
 * $Id$
 *
 * Name:     GDALInfo.cs
 * Project:  GDAL CSharp Interface
 * Purpose:  A sample app to read GDAL raster data information.
 * Author:   Tamas Szekeres, szekerest@gmail.com
 *
 ******************************************************************************
 * Copyright (c) 2007, Tamas Szekeres
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 *****************************************************************************/
using System;
using System.IO;
using OSGeo.GDAL;
using OSGeo.OSR;

/**

 * <p>Title: GDAL C# GDALRead example.</p>
 * <p>Description: A sample app to read GDAL raster data information.</p>
 * @author Tamas Szekeres (szekerest@gmail.com)
 * @version 1.0
 */

/// <summary>
/// A C# based sample to read GDAL raster data information.
/// </summary> 

namespace Gdal020
{
    class MapInfo
    {
        // var
        TextWriter tw;
        string Text = string.Empty;

        public MapInfo(string FilePath, string Directory)
        {
            // get file name without the extension 
            

            try
            {
                /* -------------------------------------------------------------------- */
                /*      Register driver(s).                                             */
                /* -------------------------------------------------------------------- */
                Gdal.AllRegister();


                /* -------------------------------------------------------------------- */
                /*      Open dataset.                                                   */
                /* -------------------------------------------------------------------- */
                Dataset ds = Gdal.Open(FilePath, Access.GA_ReadOnly);

                if (ds == null)
                {
                    Console.WriteLine("Can't open " + Path.GetFileName(FilePath));
                    System.Environment.Exit(-1);
                }
                Text += "Raster dataset parameters:\n";
                //Text += " Projection: " + ds.GetProjectionRef()+"\n";
                Text += "    RasterCount: " + ds.RasterCount+"\n";
                Text += "    RasterSize (" + ds.RasterXSize + "," + ds.RasterYSize + ")\n";
                Text += "\n";
                

                

                /* -------------------------------------------------------------------- */
                /*      Get driver                                                      */
                /* -------------------------------------------------------------------- */
                Driver drv = ds.GetDriver();

                if (drv == null)
                {
                    Console.WriteLine("Can't get driver.");
                    System.Environment.Exit(-1);
                }
                Text += "Using driver " + drv.LongName + "\n";
                Text += "\n";

                /* -------------------------------------------------------------------- */
                /*      Get metadata                                                    */
                /* -------------------------------------------------------------------- */
                string[] metadata = ds.GetMetadata("");
                if (metadata.Length > 0)
                {
                    Text += "Metadata:\n";
                    
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Text += "    " + iMeta + ":  " + metadata[iMeta] + "\n";
                    }
                    
                }


                Text += "\n";


                /* -------------------------------------------------------------------- */
                /*      Report "IMAGE_STRUCTURE" metadata.                              */
                /* -------------------------------------------------------------------- */
                metadata = ds.GetMetadata("IMAGE_STRUCTURE");
                if (metadata.Length > 0)
                {
                    Text += "Image Structure Metadata:\n";
                   
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Text += "Image Structure Metadata:";
                        Text += "   " + iMeta + "; " + metadata[iMeta] + "\n";
                        
                    }
                    
                }
                else { Text += "No image structure\n"; }


                Text += "\n";
                /* -------------------------------------------------------------------- */
                /*      Report subdatasets.                                             */
                /* -------------------------------------------------------------------- */
                metadata = ds.GetMetadata("SUBDATASETS");
                if (metadata.Length > 0)
                {
                    Text += " Subdatasets: \n";
                    
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Text += "    " + iMeta + ":  " + metadata[iMeta] + "\n";
                        
                    }
                    
                }
                else { Text +="No subdataset reported\n"; }


                Text += "\n";


                /* -------------------------------------------------------------------- */
                /*      Report geolocation.                                             */
                /* -------------------------------------------------------------------- */
                metadata = ds.GetMetadata("GEOLOCATION");
                if (metadata.Length > 0)
                {
                    Text += "  Geolocation:\n";
                    
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Text +="    " + iMeta + ":  " + metadata[iMeta]+"\n";
                    }
                    Console.WriteLine("");
                }
                else { Text +="No geolocation reported\n"; }


                Text += "\n";


                /* -------------------------------------------------------------------- */
                /*      Report corners.    see function GDALInfoGetPosition             */
                /* -------------------------------------------------------------------- */
                Text +="Corner Coordinates:\n";
                Text +="  Upper Left (" + GDALInfoGetPosition(ds, 0.0, 0.0) + ")\n";
                Text +="  Lower Left (" + GDALInfoGetPosition(ds, 0.0, ds.RasterYSize) + ")\n";
                Text +="  Upper Right (" + GDALInfoGetPosition(ds, ds.RasterXSize, 0.0) + ")\n";
                Text +="  Lower Right (" + GDALInfoGetPosition(ds, ds.RasterXSize, ds.RasterYSize) + ")\n";
                Text +="  Center (" + GDALInfoGetPosition(ds, ds.RasterXSize / 2, ds.RasterYSize / 2) + ")\n";
                Text += "\n";


                /* -------------------------------------------------------------------- */
                /*      Report projection.                                              */
                /* -------------------------------------------------------------------- */
                string projection = ds.GetProjectionRef();
                if (projection != null)
                {
                    SpatialReference srs = new SpatialReference(null);
                    if (srs.ImportFromWkt(ref projection) == 0)
                    {
                        string wkt;
                        srs.ExportToPrettyWkt(out wkt, 0);
                        Text +="Coordinate System is:\n";
                        Text +=wkt+"\n";
                    }
                    else
                    {
                        Text +="Coordinate System is:\n";
                        Text +="projection \n";
                    }
                    
                    // write the file here 
                    tw = new StreamWriter(Directory + "/" + Path.GetFileNameWithoutExtension(FilePath)+".nfo");
                    tw.Write(Text);
                    tw.Close();
                    
                }
            }
            catch (Exception e) { Console.WriteLine("error found : {0}", e.Message); }


        }


        // functions 
        private static string GDALInfoGetPosition(Dataset ds, double x, double y)
        {
            double[] adfGeoTransform = new double[6];
            double dfGeoX, dfGeoY;
            ds.GetGeoTransform(adfGeoTransform);

            dfGeoX = adfGeoTransform[0] + adfGeoTransform[1] * x + adfGeoTransform[2] * y;
            dfGeoY = adfGeoTransform[3] + adfGeoTransform[4] * x + adfGeoTransform[5] * y;

            return dfGeoX.ToString() + ", " + dfGeoY.ToString();
        }   
    }
}
