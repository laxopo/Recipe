using System.Windows.Forms;

namespace Recipe.Editor.VisualObject
{
    public class Container
    {
        public PictureBox Icon { get; set; }
        public Label Label { get; set; }

        public Container(PictureBox icon, Label label)
        {
            Icon = icon;
            Label = label;
        }
    }
}
