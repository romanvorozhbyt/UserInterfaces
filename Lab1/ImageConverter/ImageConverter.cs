using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace ImageConverter
{
    public static class ImageConverter
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png", ".ico", ".tif" };
        /// <summary>
        ///Converts image for given full path to specified extension
        /// </summary>
        /// <param name="file">full path to image including extension</param>
        /// <param name="extension">output file extension</param>
        public static void ConvertTo(string file, string extension)
        {
            ConvertTo(file,extension,-1,-1);
        }

        /// <summary>
        /// Converts image for given full path to specified extension, and resize it to specified width and height
        /// </summary>
        /// <param name="file">full path to image including extension</param>
        /// <param name="extension">output file extension</param>
        /// <param name="width">output image width</param>
        /// <param name="height">output image height</param>
        public static void ConvertTo(string file, string extension, int width, int height)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"File {file} doesn't exists. Check path and try again");
            }
            if (!ImageExtensions.Contains(Path.GetExtension(file)?.ToLower()))
            {
                throw new ArgumentException($"Given file is not an image or have not supported extension", nameof(file));
            }
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }
            
            string name = Path.GetFileNameWithoutExtension(file);
            string path = Path.GetDirectoryName(file);
            ImageFormat format = GetImageFormat(extension);
            Image img;
            using (img = Image.FromFile(file))
            {
                if (width > 0 && height > 0)
                {
                    img = ResizeImage(img, new Size(width, height));
                }
                img.Save(path + @"/" + name + extension, format);
                
            }
        }

        private static ImageFormat GetImageFormat(string extension)
        {
            ImageFormat format;
            switch (extension.ToLower())
            {
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".jpeg":
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".ico":
                    format = ImageFormat.Icon;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".tif":
                    format = ImageFormat.Tiff;
                    break;
                default:
                    throw new ArgumentException($"Unsupported extension {extension}", nameof(extension));
            }

            return format;
        }

        private static Image ResizeImage(Image image, Size size,
            bool preserveAspectRatio = false)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }


    }
}
