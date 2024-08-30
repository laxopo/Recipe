using System.Windows.Forms;
using System.Drawing;

namespace Recipe.Editor
{
    public class VOBuf
    {
        public PictureBox VO { get; set; }
        public Point Location;

        public VOBuf(PictureBox visualObject)
        {
            VO = visualObject;
            Location = visualObject.Location;
        }
    }
}
