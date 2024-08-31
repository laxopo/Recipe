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
        public List<ItemObject> IOs { get; set; }


        public Clip() { }

        public Clip(List<ItemObject> buffer, Rectangle box)
        {
            IOs = buffer;
            Box = box;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Clip Deserialize(object data)
        {
            return JsonConvert.DeserializeObject<Clip>(data as string);
        }
    }
}
