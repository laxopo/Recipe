using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace Recipe.Editor
{
    public class Clip
    {
        public Rectangle Box { get; set; }
        public Point Center { get; set; }
        public Point Offset { get; set; }
        public List<ItemObject> IOs { get; set; }


        public Clip() { }

        public Clip(List<ItemObject> buffer, Rectangle box)
        {
            IOs = buffer;
            Box = box;
            Offset = new Point(-box.Width / 2, -box.Height / 2);
            Center = new Point(box.X - Offset.X, box.Y - Offset.Y);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Clip Deserialize(object data)
        {
            try
            {
                return JsonConvert.DeserializeObject<Clip>(data as string);
            }
            catch
            {
                return null;
            }
        }
    }
}
