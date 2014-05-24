/******************************************************************************
 * using http://svn.osgeo.org/gdal/trunk/gdal/swig/csharp/apps/GDALRead.cs
 * fixing an issue regarding palette interpretation : GCI_Undefined
 * cf : http://stackoverflow.com/questions/16449735/support-for-arc-info-binary-grid-format-by-c-bindings-of-gdal
 * Note regarding system.drawing : need to include the DLL because of System.drawing.Imaging  . 
 *****************************************************************************/

/******************************************************************************
 * $Id$
 *
 * Name:     GDALRead.cs
 * Project:  GDAL CSharp Interface
 * Purpose:  A sample app to read GDAL raster data.
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
using System.Drawing;
using System.Drawing.Imaging;
using OSGeo.GDAL;

namespace Gdal020
{
    class MapBmp
    {
        public MapBmp(string FilePath, string Directory)
        {
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


                /* -------------------------------------------------------------------- */
                /*      Get driver                                                      */
                /* -------------------------------------------------------------------- */
                Driver drv = ds.GetDriver();


                /* -------------------------------------------------------------------- */
                /*      Get raster band                                                 */
                /* -------------------------------------------------------------------- */
                for (int iBand = 1; iBand <= ds.RasterCount; iBand++)
                {
                    Band band = ds.GetRasterBand(iBand);
                    Console.WriteLine("Band " + iBand + " :");
                    Console.WriteLine("   DataType: " + band.DataType);
                    Console.WriteLine("   Size (" + band.XSize + "," + band.YSize + ")");
                    Console.WriteLine("   PaletteInterp: " + band.GetRasterColorInterpretation().ToString());

                    for (int iOver = 0; iOver < band.GetOverviewCount(); iOver++)
                    {
                        Band over = band.GetOverview(iOver);
                        Console.WriteLine("      OverView " + iOver + " :");
                        Console.WriteLine("         DataType: " + over.DataType);
                        Console.WriteLine("         Size (" + over.XSize + "," + over.YSize + ")");
                        Console.WriteLine("         PaletteInterp: " + over.GetRasterColorInterpretation().ToString());
                    }
                }

                /* -------------------------------------------------------------------- */
                /*      Processing the raster                                           */
                /* -------------------------------------------------------------------- */
                SaveBitmapBuffered(ds, Directory +"/" + Path.GetFileNameWithoutExtension(FilePath) , 100);



            }
            catch (Exception e)
            {
                Console.WriteLine("Error type : {0}", e.Message);
                
            }

        }

        //functions 

        private static void SaveBitmapBuffered(Dataset ds, string filename, int iOverview)
        {
            // Get the GDAL Band objects from the Dataset
            Band redBand = ds.GetRasterBand(1);

            if (redBand.GetRasterColorInterpretation() == ColorInterp.GCI_PaletteIndex)
            {
                SaveBitmapPaletteBuffered(ds, filename, iOverview);
                return;
            }

            if (redBand.GetRasterColorInterpretation() == ColorInterp.GCI_GrayIndex || redBand.GetRasterColorInterpretation() == ColorInterp.GCI_Undefined)
            {
                SaveBitmapGrayBuffered(ds, filename, iOverview);
                return;
            }

            if (redBand.GetRasterColorInterpretation() != ColorInterp.GCI_RedBand)
            {
                Console.WriteLine("Non RGB images are not supported by this sample! ColorInterp = " +
                    redBand.GetRasterColorInterpretation().ToString());
                return;
            }

            if (ds.RasterCount < 3)
            {
                Console.WriteLine("The number of the raster bands is not enough to run this sample");
                System.Environment.Exit(-1);
            }

            if (iOverview >= 0 && redBand.GetOverviewCount() > iOverview)
                redBand = redBand.GetOverview(iOverview);

            Band greenBand = ds.GetRasterBand(2);

            if (greenBand.GetRasterColorInterpretation() != ColorInterp.GCI_GreenBand)
            {
                Console.WriteLine("Non RGB images are not supported by this sample! ColorInterp = " +
                    greenBand.GetRasterColorInterpretation().ToString());
                return;
            }

            if (iOverview >= 0 && greenBand.GetOverviewCount() > iOverview)
                greenBand = greenBand.GetOverview(iOverview);

            Band blueBand = ds.GetRasterBand(3);

            if (blueBand.GetRasterColorInterpretation() != ColorInterp.GCI_BlueBand)
            {
                Console.WriteLine("Non RGB images are not supported by this sample! ColorInterp = " +
                    blueBand.GetRasterColorInterpretation().ToString());
                return;
            }

            if (iOverview >= 0 && blueBand.GetOverviewCount() > iOverview)
                blueBand = blueBand.GetOverview(iOverview);

            // Get the width and height of the raster
            int width = redBand.XSize;
            int height = redBand.YSize;

            // Create a Bitmap to store the GDAL image in
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            DateTime start = DateTime.Now;

            byte[] r = new byte[width * height];
            byte[] g = new byte[width * height];
            byte[] b = new byte[width * height];

            redBand.ReadRaster(0, 0, width, height, r, width, height, 0, 0);
            greenBand.ReadRaster(0, 0, width, height, g, width, height, 0, 0);
            blueBand.ReadRaster(0, 0, width, height, b, width, height, 0, 0);
            TimeSpan renderTime = DateTime.Now - start;
            

            int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    Color newColor = Color.FromArgb(Convert.ToInt32(r[i + j * width]), Convert.ToInt32(g[i + j * width]), Convert.ToInt32(b[i + j * width]));
                    bitmap.SetPixel(i, j, newColor);
                }
            }

            bitmap.Save(filename + ".bmp");
            float scale = bitmap.Width / 1024;
            Bitmap map = new Bitmap(bitmap, new Size(1024,(int)(bitmap.Height/scale)));
            map.Save(filename + "_icon.bmp");

        }

        private static void SaveBitmapPaletteBuffered(Dataset ds, string filename, int iOverview)
        {
            // Get the GDAL Band objects from the Dataset
            Band band = ds.GetRasterBand(1);
            if (iOverview >= 0 && band.GetOverviewCount() > iOverview)
                band = band.GetOverview(iOverview);

            ColorTable ct = band.GetRasterColorTable();
            if (ct == null)
            {
                Console.WriteLine("   Band has no color table!");
                return;
            }

            if (ct.GetPaletteInterpretation() != PaletteInterp.GPI_RGB)
            {
                Console.WriteLine("   Only RGB palette interp is supported by this sample!");
                return;
            }

            // Get the width and height of the Dataset
            int width = band.XSize;
            int height = band.YSize;

            // Create a Bitmap to store the GDAL image in
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            DateTime start = DateTime.Now;

            byte[] r = new byte[width * height];

            band.ReadRaster(0, 0, width, height, r, width, height, 0, 0);
            TimeSpan renderTime = DateTime.Now - start;
           

            int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    ColorEntry entry = ct.GetColorEntry(r[i + j * width]);
                    Color newColor = Color.FromArgb(Convert.ToInt32(entry.c1), Convert.ToInt32(entry.c2), Convert.ToInt32(entry.c3));
                    bitmap.SetPixel(i, j, newColor);
                }
            }

            bitmap.Save(filename + ".bmp");
            float scale = bitmap.Width / 1024;
            Bitmap map = new Bitmap(bitmap, new Size(1024, (int)(bitmap.Height / scale)));
            map.Save(filename + "_icon.bmp");
        }

        private static void SaveBitmapGrayBuffered(Dataset ds, string filename, int iOverview)
        {
            // Get the GDAL Band objects from the Dataset
            Band band = ds.GetRasterBand(1);
            if (iOverview >= 0 && band.GetOverviewCount() > iOverview)
                band = band.GetOverview(iOverview);

            // Get the width and height of the Dataset
            int width = band.XSize;
            int height = band.YSize;

            // Create a Bitmap to store the GDAL image in
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

            DateTime start = DateTime.Now;

            byte[] r = new byte[width * height];

            band.ReadRaster(0, 0, width, height, r, width, height, 0, 0);
            TimeSpan renderTime = DateTime.Now - start;
            Console.WriteLine("SaveBitmapBuffered fetch time: " + renderTime.TotalMilliseconds + " ms");

            int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    Color newColor = Color.FromArgb(Convert.ToInt32(r[i + j * width]), Convert.ToInt32(r[i + j * width]), Convert.ToInt32(r[i + j * width]));
                    bitmap.SetPixel(i, j, newColor);
                }
            }

            bitmap.Save(filename + ".bmp");
            float scale = bitmap.Width / 1024;
            Bitmap map = new Bitmap(bitmap, new Size(1024, (int)(bitmap.Height / scale)));
            map.Save(filename + "_icon.bmp");
        }
    }
}
