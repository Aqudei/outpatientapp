using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OutPatientApp
{
    public static class Extentions
    {
        public static void SaveImage(this BitmapImage bitmap, string filename)
        {
            filename = Path.ChangeExtension(filename, ".png");

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap.Clone()));

            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            try
            {
                var img = (Bitmap)bitmap.Clone();
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // ignored
                return null;
            }
        }
    }
}
