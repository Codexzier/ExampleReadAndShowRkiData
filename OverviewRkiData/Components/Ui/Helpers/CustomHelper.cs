using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace OverviewRkiData.Components.Ui.Helpers
{
    public static class CustomHelper
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }

        public static string GetFilename(string path, string filename, int number = 0)
        {
            var r = $"{path}\\{filename}_{number}.tmp";

            if (File.Exists(r))
            {
                number++;
                return GetFilename(path, filename, number);
            }

            return r;
        }

        public static TFrameElement GetContentPresenter<TFrameElement>(this FrameworkElement control) where TFrameElement : FrameworkElement
        {
            var num = VisualTreeHelper.GetChildrenCount(control);

            if (num == 0)
            {
                return null;
            }

            for (int index = 0; index < num; index++)
            {
                var obj = VisualTreeHelper.GetChild(control, index);

                if (obj == null)
                {
                    continue;
                }

                var v = (Visual)obj;
                if (v is TFrameElement frameElement)
                {
                    return frameElement;
                }

                if (obj is FrameworkElement fe)
                {
                    var t = fe.GetContentPresenter<TFrameElement>();
                    if (t != null)
                    {
                        return t;
                    }
                }
            }

            return null;
        }
    }
}
