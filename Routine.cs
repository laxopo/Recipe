using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Recipe
{
    public static class Routine
    {
        public static class Directories
        {
            public static string Root = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetEntryAssembly().Location);
            public static string Library = Root + @"\library";
        }
        public static class Files
        {
            public static string Library = Directories.Root + @"\library.json";
            public static string Config = Directories.Root + @"\config.json";
        }

        public static bool IsFormOpen(Form form)
        {
            foreach (Form Iform in Application.OpenForms)
            {
                if (Iform.Name == form.Name)
                {
                    return true;
                }
            }

            return false;
        }

        public static Image ImageNB(Image image, Size size, int x, int y)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics itemIcon = Graphics.FromImage(bmp);
            itemIcon.InterpolationMode = InterpolationMode.NearestNeighbor;
            itemIcon.PixelOffsetMode = PixelOffsetMode.Half;

            itemIcon.DrawImage(image, x, y, size.Width, size.Height);

            return bmp;
        }

        public static Point Limiter(Point input, Point location, Size min, Size max)
        {

            if (input.X < location.X + min.Width)
            {
                input.X = location.X + min.Width;
            }
            else if (input.X > location.X + max.Width)
            {
                input.X = location.X + max.Width;
            }

            if (input.Y < location.Y + min.Height)
            {
                input.Y = location.Y + min.Height;
            }
            else if (input.Y > location.Y + max.Height)
            {
                input.Y = location.Y + max.Height;
            }

            return input;
        }
    }
}
