using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.UI.Helpers
{
    /// <summary>
    /// Class representation of the modificable image
    /// </summary>
    public class EditableImage
    {
        private int OFFSET_X = (int)System.Windows.SystemParameters.PrimaryScreenWidth / 2;
        private int OFFSET_Y = (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2;
        /// <summary>
        /// loaded 2d array of pixels of the image
        /// </summary>
        protected RgbPixel[,] pixels;
        protected int width;
        protected int height;
        private Grid grid;
        public int ImageWidth { get { return width; } }
        public int ImageHeight { get { return height; } }

        public RgbPixel[,] Pixels { get { return pixels; } }

        protected Bitmap bitmap = null;

        public void UpdateImage()
        {
            foreach (var cellEntry in grid.VisitedCells)
            {
                RgbPixel pixel;
                switch (cellEntry.Value.State)
                {
                    case CellState.Alive:
                        pixel = new RgbPixel(255,0,0);
                        break;
                    case CellState.Dead:
                        pixel = new RgbPixel(0,0,0);
                        break;
                    case CellState.Unvisited:
                        pixel = new RgbPixel(255, 255, 255);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                PutPixel((int)cellEntry.Key.X + OFFSET_X, (int)cellEntry.Key.Y + OFFSET_Y, pixel);
            }
        }

        public EditableImage(int width, int height, Grid grid) : this(width, height)
        {
            this.grid = grid;
            UpdateImage();
        }

        public EditableImage(int width, int height)
        {
            pixels = new RgbPixel[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixels[i, j] = new RgbPixel(255, 255, 255);
                }
            }
            this.width = width;
            this.height = height;
        }

        public EditableImage(RgbPixel[,] pixels)
        {
            this.pixels = pixels;
            this.width = pixels.GetLength(0);
            this.height = pixels.GetLength(1);
        }

        public Bitmap getBitmap()
        {
            int ch = 4; //number of channels (ie. assuming 24 bit RGB in this case)

            List<byte> imageData = new List<byte>();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    imageData.Add(pixels[j, i].Blue);
                    imageData.Add(pixels[j, i].Green);
                    imageData.Add(pixels[j, i].Red);

                    imageData.Add(pixels[j, i].Alpha);
                }
            }

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr pNative = bmData.Scan0;
            Marshal.Copy(imageData.ToArray(), 0, pNative, width*height*ch);
            bitmap.UnlockBits(bmData);

            this.bitmap = bitmap;
            return bitmap;
        }

        public System.Windows.Media.ImageSource GetImageSource()
        {
            using (MemoryStream memory = new MemoryStream())
            {
                this.getBitmap().Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public void PutPixel(int x, int y, RgbPixel pixel)
        {
            if (x >= width || y >= height || y < 0 || x < 0)
            {
                //throw new Exception("Out of picture!");
                return;
            }
            pixels[x, y] = pixel;
        }
    }
}