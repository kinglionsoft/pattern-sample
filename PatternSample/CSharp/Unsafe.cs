using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;

namespace PatternSample.CSharp
{
    public class Unsafe
    {
        public static void Run()
        {
            var sw = new Stopwatch();
            const string file = @"C:\Users\yang\Pictures\1.jpg";

            sw.Start();
            UnManaged(file);
            sw.Stop();
            Console.WriteLine("UnManaged: {0}, Memory：{1}", sw.ElapsedMilliseconds, GC.GetTotalMemory(false));
            sw.Restart();
            Managed(file);
            sw.Stop();
            Console.WriteLine("Managed: {0}, Memory：{1}", sw.ElapsedMilliseconds, GC.GetTotalMemory(false));
        }

        public static unsafe void Ptr()
        {
            byte* ptr = stackalloc byte[2];
            try
            {
                ptr[3] = byte.MaxValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var span = new Span<byte>(ptr, 2);
            try
            {
                span[3] = byte.MaxValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Managed(string file)
        {
            using var bitmap = (Bitmap)Image.FromFile(file);
            var height = bitmap.Height;
            var width = bitmap.Width;
            using var output = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            var bitmapDataOutput = output.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            var outputBuffer = new byte[bitmapDataOutput.Height * bitmapDataOutput.Stride];
            Marshal.Copy(bitmapDataOutput.Scan0, outputBuffer, 0, outputBuffer.Length);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var color = bitmap.GetPixel(j, i);
                    var gray = Gray(color.R, color.G, color.B);
                    outputBuffer[i * bitmapDataOutput.Stride + j] = gray;
                }
            }

            Marshal.Copy(outputBuffer, 0, bitmapDataOutput.Scan0, outputBuffer.Length);

            var palette = output.Palette;
            for (var i = 0; i < palette.Entries.Length; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            output.Palette = palette;

            output.Save(GetOutputFile(file));
        }

        public static unsafe void UnManaged(string file)
        {
            using var bitmap = (Bitmap)Image.FromFile(file);
            var height = bitmap.Height;
            var width = bitmap.Width;

            using var output = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            var bitmapDataOutput = output.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            var scanOutput = new Span<byte>((byte*)bitmapDataOutput.Scan0.ToPointer(), height * bitmapDataOutput.Stride);

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var span = new Span<byte>((byte*)bitmapData.Scan0.ToPointer(), height * bitmapData.Stride);
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var pixel = i * bitmapData.Stride + j * 3;
                    var gray = Gray(span[pixel + 2], span[pixel + 1], span[pixel]);
                    scanOutput[i * bitmapDataOutput.Stride + j] = gray;
                }
            }
            bitmap.UnlockBits(bitmapData);
            output.UnlockBits(bitmapDataOutput);

            var palette = output.Palette;
            for (var i = 0; i < palette.Entries.Length; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            output.Palette = palette;
            output.Save(GetOutputFile(file));
        }

        private static byte Gray(byte r, byte g, byte b)
        {
            var gray = (byte)(r * 0.30F + g * 0.59F + b * 0.11F);
            return gray;
        }
        
        private static string GetOutputFile(string file)
        {
            var ext = Path.GetExtension(file);
            return Path.ChangeExtension(file, ".out" + ext);
        }
    }
}