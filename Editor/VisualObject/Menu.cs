using System;
using System.Windows.Forms;
using System.Drawing;

namespace Recipe.Editor.VisualObject
{
    public static class Menu
    {
        private static bool init = false;
        private static PictureBox voSender;


        public static ContextMenuStrip ObjMenu 
        { 
            get
            {
                if (!init)
                {
                    init = true;
                    link.Click += new EventHandler(LinkToolStripMenuItem_Click);
                    unlink.Click += new EventHandler(UnlinkToolStripMenuItem_Click);
                    props.Click += new EventHandler(PropsToolStripMenuItem_Click);
                    library.Click += new EventHandler(LibraryToolStripMenuItem_Click);
                    clone.Click += new EventHandler(CloneToolStripMenuItem_Click);
                    copy.Click += new EventHandler(CopyToolStripMenuItem_Click);
                    delete.Click += new EventHandler(DeleteToolStripMenuItem_Click);
                }

                return new ContextMenuStrip() {
                    Name = "contextMenuStripVObj",
                    Items = {
                        link,
                        unlink,
                        props,
                        library,
                        clone,
                        copy,
                        delete
                    }
                };
            }
        }

        public static void ShowMenu(PictureBox sender, Point location)
        {
            bool en = Engine.CurrentVObj != null;
            library.Enabled = en;
            props.Enabled = en;
            voSender = sender;

            ObjMenu.Show(location);
        }

        /*Menu items*/

        private static ToolStripMenuItem link = new ToolStripMenuItem()
        {
            Name = "linkToolStripMenuItem",
            Text = "Link",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        };

        private static ToolStripMenuItem unlink = new ToolStripMenuItem()
        {
            Name = "unlinkToolStripMenuItem",
            Text = "Unlink",
        };

        private static ToolStripMenuItem props = new ToolStripMenuItem()
        {
            Name = "propsToolStripMenuItem",
            Text = "Properties"
        };

        private static ToolStripMenuItem library = new ToolStripMenuItem()
        {
            Name = "libraryToolStripMenuItem",
            Text = "Show In Library"
        };

        private static ToolStripMenuItem clone = new ToolStripMenuItem()
        {
            Name = "cloneToolStripMenuItem",
            Text = "Clone"
        };

        private static ToolStripMenuItem copy = new ToolStripMenuItem()
        {
            Name = "copyToolStripMenuItem",
            Text = "Copy"
        };

        private static ToolStripMenuItem delete = new ToolStripMenuItem()
        {
            Name = "deleteToolStripMenuItem",
            Text = "Delete",
        };

        /*Item event handlers*/

        private static void LinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.LinkingEnable(voSender);
        }

        private static void UnlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.propEditor.Unlink();
        }

        private static void PropsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.propEditor.Show();
        }

        private static void LibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = (voSender.Tag as ItemObject).Item;
            Engine.libraryForm.OpenItem(item);
        }

        private static void CloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Engine.CurrentVObj != null) //one vo (same as item inserting)
            {
                Engine.InsertItem = (Engine.CurrentVObj.Tag as ItemObject).Item;
            }
            else //a massive cloning
            {
                Engine.CloneVOsStart(voSender.Location);
            }
        }

        private static void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Copy();
        }

        private static void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.RemoveVOs();
        }
    }
}
