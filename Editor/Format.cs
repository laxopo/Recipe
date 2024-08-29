using System.Collections.Generic;
using System.Drawing;

namespace Recipe.Editor
{
    public class Format
    {
        public string FileFormat { get; set; }
        public string Version { get; set; }
        public Size SheetSize { get; set; }
        public int Count { get; set; }
        public List<ItemObject> Data { get; set; }
    }
}
