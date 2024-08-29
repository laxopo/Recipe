using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace Recipe
{
    public class FormatTable
    {
        public List<Coll> Collection { get; set; }

        public FormatTable(List<Coll> collection)
        {
            Collection = collection;
        }


        public class Coll
        {
            public string Caption { get; set; }
            public string[] Extensions { get; set; }
            public ImageFormat Format { get; set; }

            public Coll(string caption, string[] extensions, ImageFormat format)
            {
                Caption = caption;
                Extensions = extensions;
                Format = format;
            }
        }

        public string GetExtFilter()
        {
            string filter = "";

            bool first = true;
            foreach (Coll row in Collection)
            {
                if (!first)
                {
                    filter += "|";
                }

                filter += row.Caption + "|";
                for (int i = 0; i < row.Extensions.Length; i++)
                {
                    if (i > 0)
                    {
                        filter += ";";
                    }
                    filter += "*." + row.Extensions[i];
                }
                first = false;
            }

            return filter;
        }

        public ImageFormat GetImageFormat(int index)
        {
            return Collection[index - 1].Format;
        }
    }
}
