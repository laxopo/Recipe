using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Recipe.Editor
{
    public class ItemObject
    {
        public Library.Item Item { get; set; }

        public Point Location { get; set; }

        public int ID { get; set; }

        public int QuantityIn { get; set; }

        public int QuantityOut { get; set; }

        public Type IOType { get; set; } 

        public List<int> LinksIn { get; set; }
        
        public List<int> LinksOut { get; set; }

        [JsonIgnore]
        public Point StartLocation { get; set; }
        [JsonIgnore]
        public int OldID { get; set; }
        [JsonIgnore]
        public List<ItemObject> LinkInTags { get; set; }
        [JsonIgnore]
        public List<ItemObject> LinkOutTags { get; set; }
        [JsonIgnore]
        public List<ItemObject> LinkInHLs { get; set; }
        [JsonIgnore]
        public List<ItemObject> LinkOutHLs { get; set; }
        [JsonIgnore]
        public PictureBox TagIcon { get; set; }
        [JsonIgnore]
        public Label TagLabel { get; set; }
        [JsonIgnore]
        public Calculator.Tree Tree { get; set; }


        public ItemObject()
        {
            LinksIn = new List<int>();
            LinksOut = new List<int>();
            LinkInTags = new List<ItemObject>();
            LinkOutTags = new List<ItemObject>();
            LinkInHLs = new List<ItemObject>();
            LinkOutHLs = new List<ItemObject>();
            OldID = -1;
            QuantityIn = 1;
            QuantityOut = 1;
        }

        public enum LinkType
        {
            Input,
            Output,
            Both
        }

        public enum Type
        {
            Auto,
            Input,
            Output
        }

        public void ResetRelocation()
        {
            StartLocation = Location;
        }
    }
}
