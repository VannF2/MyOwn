using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace lab6.Models
{
    public static class ImageHelper
    {
        public static Image ToImage(this string text)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);

            var drawing = Graphics.FromImage(img);
            var font = new Font("Courier New", 15);
            var textColor = Color.Black;
            var backColor = Color.White;

            //measure the string to see how big the image needs to be
            var textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        public static Stream ToStream(this Image image)
        {
            var stream = new MemoryStream();
            var format = ImageFormat.Png;

            image.Save(stream, format);
            stream.Position = 0;

            return stream;
        }
    }
}