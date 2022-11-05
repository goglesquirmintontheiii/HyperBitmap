using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace HyperBitmap
{
    /// <summary>
    /// Extensions required for this to work.
    /// </summary>
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] arr, int size)
        {
            return arr.Select((s, i) => arr.Skip(i * size).Take(size)).Where(a => a.Any());
        }
    }

    /// <summary>
    /// A 'wrapper' for the Bitmap class. Way faster.
    /// (Probably) only supports 32bpp ARGB
    /// </summary>
    public class FastBmp
    {
        public int Height = 0;
        public int Width = 0;
        private byte[] pixels = new byte[0];
        private int stride = 0;
        public PixelFormat PixelFormat = PixelFormat.Format32bppArgb;
        public Size Size = new Size(0, 0);
        public int Flags = 0;
        public float HorizontalResolution = 0;
        public float VerticalResolution = 0;
        public object Tag;
        public IntPtr IntPtr;
        public ColorPalette Pallette;
        public SizeF PhysicalDimension;
        public Guid[] FrameDimensionsList;
        public PropertyItem[] PropertyItems;
        public int[] PropertyIdList;
        public ImageFormat RawFormat;
        private int RawStride = 0;
        /// <summary>
        /// Sets the properties of this to those of a Bitmap.
        /// </summary>
        /// <param name="myImage"></param>
        private void SetExtras(Bitmap myImage)
        {
            Size = new Size(myImage.Width, myImage.Height);

            stride = myImage.Width * 4;
            Flags = myImage.Flags;
            HorizontalResolution = myImage.HorizontalResolution;
            VerticalResolution = myImage.VerticalResolution;
            Tag = myImage.Tag;

            PhysicalDimension = myImage.PhysicalDimension;
            FrameDimensionsList = myImage.FrameDimensionsList;
            PropertyItems = myImage.PropertyItems;
            PropertyIdList = myImage.PropertyIdList;
            RawFormat = myImage.RawFormat;

        }
        /// <summary>
        /// Creates a FastBmp from a file.
        /// </summary>
        /// <param name="filename">The path of the file as a string.</param>
        public FastBmp(string filename)
        {
            Bitmap myImage = new Bitmap(filename);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);
            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp from an existing Bitmap.
        /// </summary>
        /// <param name="myImage">The existing Bitmap.</param>
        public FastBmp(Bitmap myImage)
        {
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp from an existing Image.
        /// </summary>
        /// <param name="MyImage">The existing Image.</param>
        public FastBmp(Image MyImage)
        {
            Bitmap myImage = new Bitmap(MyImage);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public FastBmp(Stream stream)
        {
            Bitmap myImage = new Bitmap(stream);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp of a given size from an existing Image.
        /// </summary>
        /// <param name="original">The existing Image.</param>
        /// <param name="size">The new size.</param>
        public FastBmp(Image original, Size size)
        {
            Bitmap myImage = new Bitmap(original, size);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp of a given size from an existing FastBmp.
        /// </summary>
        /// <param name="original">The existing FastBmp.</param>
        /// <param name="size">The new size.</param>
        public FastBmp(FastBmp original, Size size)
        {
            Bitmap myImage = new Bitmap(original.AsBitmap(), size);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp of a given size from an existing Bitmap.
        /// </summary>
        /// <param name="original">The existing Image.</param>
        /// <param name="size">The new size.</param>
        public FastBmp(Bitmap original, Size size)
        {
            Bitmap myImage = new Bitmap(original, size);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp with a given size.
        /// </summary>
        /// <param name="width">Width of the new FastBmp.</param>
        /// <param name="height">Height of the new FastBmp.</param>
        public FastBmp(int width, int height)
        {
            Bitmap myImage = new Bitmap(width, height);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        public FastBmp(Stream stream, bool usecm)
        {
            Bitmap myImage = new Bitmap(stream, usecm);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }

        public FastBmp(string filename, bool usecm)
        {
            Bitmap myImage = new Bitmap(filename, usecm);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        public FastBmp(Type type, string resource)
        {
            Bitmap myImage = new Bitmap(type, resource);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        public FastBmp(int width, int height, Graphics g)
        {
            Bitmap myImage = new Bitmap(width, height, g);
            Height = myImage.Height;
            Width = myImage.Width;
            BitmapData bmpData1 = myImage.LockBits(new Rectangle(0, 0, myImage.Width, myImage.Height),
                             System.Drawing.Imaging.ImageLockMode.ReadWrite, myImage.PixelFormat);
            byte[] data = new byte[bmpData1.Stride * bmpData1.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data, 0
                                       , data.Length);

            myImage.UnlockBits(bmpData1);
            pixels = data;
            IntPtr = bmpData1.Scan0;
            RawStride = bmpData1.Stride;
            SetExtras(myImage);
        }
        /// <summary>
        /// Creates a FastBmp from pixel data. It is recommended you do not use this.
        /// </summary>
        /// <param name="p">
        /// Pixel data. Must be in rgba format.
        /// </param>
        /// <param name="w">
        /// Width of the image. An incorrect width will result in odd/unexpected behaviour.
        /// </param>
        /// <param name="h">
        /// Height of the image.
        /// </param>
        public FastBmp(byte[] p, int w, int h)
        {
            pixels = p;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Saves the FastBmp to a file.
        /// </summary>
        /// <param name="filename">The path of the file to save to. Creates the file if it doesn't already exist.</param>
        public void Save(string filename)
        {
            this.AsBitmap().Save(filename);
        }
        /// <summary>
        /// Creates a FastBmp from a file.
        /// </summary>
        /// <param name="filename">The path of the file.</param>
        /// <returns>A FastBmp from the file.</returns>
        static FastBmp FromFile(string filename)
        {
            return new FastBmp(filename);
        }
        /// <summary>
        /// Creates a FastBmp from a Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>A FastBmp from the Stream.</returns>
        static FastBmp FromStream(Stream stream)
        {
            return new FastBmp(stream);
        }
        /// <summary>
        /// Converts the FastBmp to a Bitmap.
        /// </summary>
        /// <returns>The Bitmap.</returns>
        public Bitmap AsBitmap()
        {
            var b = new Bitmap(Width, Height, RawStride, PixelFormat.Format32bppArgb, IntPtr);

            var BoundsRect = new Rectangle(0, 0, Width, Height);
            BitmapData bmpData = b.LockBits(BoundsRect,
                                            ImageLockMode.WriteOnly,
                                            b.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * b.Height;
            Marshal.Copy(pixels, 0, ptr, bytes);
            b.UnlockBits(bmpData);
            return b;
        }
        /// <summary>
        /// Clones this FastBmp by converting it to a Bitmap and converting that to a FastBmp.
        /// </summary>
        /// <returns>A clone of this FastBmp.</returns>
        public FastBmp Clone()
        {
            return new FastBmp(this.AsBitmap());
        }
        /// <summary>
        /// Gets the color of a pixel.
        /// </summary>
        /// <param name="x">X position of the pixel to get the color of.</param>
        /// <param name="y">Y position of the pixel to get the color of.</param>
        public Color GetPixel(int x, int y)
        {

            if (x > Width || y > Height)
            {
                throw new ArgumentException($"X must be =< Width and Y must be =< Height. X:{x}, Y:{y}");
            }
            int index = y * stride + 4 * x;
            byte red = pixels[index + 2];
            byte green = pixels[index + 1];
            byte blue = pixels[index];
            byte alpha = pixels[index + 3];
            return Color.FromArgb(alpha, red, green, blue);
        }
        /// <summary>
        /// Gets the color of a pixel. Use this if you find R and B values are swapped.
        /// </summary>
        /// <param name="x">X position of the pixel to get the color of.</param>
        /// <param name="y">Y position of the pixel to get the color of.</param>
        public Color FlipGetPixel(int x, int y)
        {

            if (x > Width || y > Height)
            {
                throw new ArgumentException($"X must be =< Width and Y must be =< Height. X:{x}, Y:{y}");
            }
            int index = y * stride + 4 * x;
            byte red = pixels[index];
            byte green = pixels[index + 1];
            byte blue = pixels[index + 2];
            byte alpha = pixels[index + 3];
            return Color.FromArgb(alpha, red, green, blue);
        }
        /// <summary>
        /// Sets the color of a pixel.
        /// </summary>
        /// <param name="x">X position of the pixel to change the color of.</param>
        /// <param name="y">Y position of the pixel to change the color of.</param>
        /// <param name="c">Color to set the pixel to.</param>
        public void SetPixel(int x, int y, Color c)
        {
            if (x > Width || y > Height)
            {
                throw new ArgumentException($"X must be =< Width and Y must be =< Height. X:{x}, Y:{y}");
            }
            int index = y * stride + 4 * x;
            pixels[index + 2] = c.R;
            pixels[index + 1] = c.G;
            pixels[index] = c.B;
            pixels[index + 3] = c.A;
        }
        /// <summary>
        /// Sets the color of a pixel. Use this if you find R and B values are swapped.
        /// </summary>
        /// <param name="x">X position of the pixel to change the color of.</param>
        /// <param name="y">Y position of the pixel to change the color of.</param>
        /// <param name="c">Color to set the pixel to.</param>
        public void FlipSetPixel(int x, int y, Color c)
        {
            if (x > Width || y > Height)
            {
                throw new ArgumentException($"X must be =< Width and Y must be =< Height. X:{x}, Y:{y}");
            }
            int index = y * stride + 4 * x;
            pixels[index + 2] = c.R;
            pixels[index + 1] = c.G;
            pixels[index] = c.B;
            pixels[index + 3] = c.A;
        }
        ///<summary>
        ///Draws a rectangle on the image.
        ///</summary>
        ///<param name="x1">
        ///X of the top left rectangle corner.
        ///</param>
        ///<param name="y1">
        ///Y of the top left rectangle corner.
        ///</param>
        ///<param name="x2">
        ///X of the bottom right rectangle corner.
        /// </param>
        /// <param name="y2">
        ///Y of the bottom right rectangle corner.
        /// </param>
        public void DrawRect(int x1, int y1, int x2, int y2, Color c)
        {
            if (x1 > Width || y1 > Height || y2 > Height || x2 > Width)
            {
                throw new ArgumentException($"X1 must be =< Width and Y1 must be =< Height. X1:{x1}, Y1:{y1}, X2:{x2}, Y2:{y2}");
            }
            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    this.SetPixel(x, y, c);
                }
            }

        }
        /// <summary>
        /// Replaces color c with color r in the image. Acts mostly like the paint bucket tool in popular paint programs.
        /// </summary>
        /// <param name="c">Color to replace.</param>
        /// <param name="r">Color to replace with.</param>
        /// <param name="tol">Total color variation tolerance. Use 0 to replace only the exact color provided.</param>
        public void Paint(Color c, Color r, int tol)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color p = GetPixel(x, y);
                    int rd = Math.Abs(p.R - c.R);
                    int gd = Math.Abs(p.G - c.G);
                    int bd = Math.Abs(p.G - c.G);
                    if (rd + gd + bd < tol)
                    {
                        SetPixel(x, y, r);
                    }
                }
            }
        }
        /// <summary>
        /// Finds the location of the first pixel with color c. Returns (-1,-1) if not found.
        /// </summary>
        /// <param name="c">The color to locate.</param>
        /// <param name="tol">Total color variation tolerance. Use 0 to locate only the exact color provided.</param>
        public Point LocateColor(Color c, int tol)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color p = GetPixel(x, y);
                    int rd = Math.Abs(p.R - c.R);
                    int gd = Math.Abs(p.G - c.G);
                    int bd = Math.Abs(p.G - c.G);
                    if (rd + gd + bd < tol)
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1, -1);
        }
        /// <summary>
        /// Finds the location of every pixel with color c. Returns empty array if not found.
        /// </summary>
        /// <param name="c">The color to locate.</param>
        /// <param name="tol">Total color variation tolerance. Use 0 to locate only the exact color provided.</param>
        public Point[] LocateColors(Color c, int tol)
        {
            List<Point> otp = new List<Point>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color p = GetPixel(x, y);
                    int rd = Math.Abs(p.R - c.R);
                    int gd = Math.Abs(p.G - c.G);
                    int bd = Math.Abs(p.G - c.G);
                    if (rd + gd + bd < tol)
                    {
                        otp.Add(new Point(x, y));
                    }
                }
            }
            return otp.ToArray();


        }
    }
}