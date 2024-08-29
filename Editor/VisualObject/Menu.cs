using System;
using System.Windows.Forms;
using System.Drawing;

namespace Recipe.Editor.VisualObject
{
    public static class Menu
    {
        public static ContextMenuStrip ObjMenu 
        { 
            get
            {
                if (!init)
                {
                    init = true;
                    link.Click += new EventHandler(LinkToolStripMenuItem_Click);
                    props.Click += new EventHandler(PropsToolStripMenuItem_Click);
                    clone.Click += new EventHandler(CloneToolStripMenuItem_Click);
                    delete.Click += new EventHandler(DeleteToolStripMenuItem_Click);
                }

                return new ContextMenuStrip() {
                    Name = "contextMenuStripIObj",
                    Size = new Size(104, 48),
                    Items = {
                        link,
                        props,
                        clone,
                        delete
                    }
                };
            }
        }

        private static bool init = false;
        private static PictureBox callVO;

        /*Menu items*/

        private static ToolStripMenuItem link = new ToolStripMenuItem()
        {
            Name = "linkToolStripMenuItem",
            Size = new Size(103, 22),
            Text = "Link",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        };

        private static ToolStripMenuItem props = new ToolStripMenuItem()
        {
            Name = "propsToolStripMenuItem",
            Size = new Size(103, 22),
            Text = "Properties"
        };

        private static ToolStripMenuItem clone = new ToolStripMenuItem()
        {
            Name = "cloneToolStripMenuItem",
            Size = new Size(103, 22),
            Text = "Clone"
        };

        private static ToolStripMenuItem delete = new ToolStripMenuItem()
        {
            Name = "deleteToolStripMenuItem",
            Size = new Size(103, 22),
            Text = "Delete"
        };

        /*Item event handlers*/

        private static void LinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.LinkingEnable();
        }

        private static void PropsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.propEditor.Show();
        }

        private static void CloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.CurrentVObj != null) //one vo (same as item inserting)
            {
                Editor.InsertItem = (Editor.CurrentVObj.Tag as ItemObject).Item;
            }
            else //a massive cloning
            {
                Editor.CloneVOsStart(callVO);
            }
        }

        private static void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.RemoveVOs();
        }

        /**/

        public static void ShowMenu(PictureBox sender, Point location)
        {
            bool en = Editor.CurrentVObj != null;
            link.Enabled = en;
            callVO = sender;

            ObjMenu.Show(location);
        }
    }
}
