using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Recipe.Editor.VisualObject
{
    public static class VisualObject
    {
        public const int IconSize = 32;
        public const string IconName = "voIcon";
        public const string LabelName = "voLabel";

        public static Color DefaultColor = Color.Transparent;
        public static Color HighlightColor = Color.Yellow;

        private static Point MouseDownLocation;

        public static Container GenerateVO(Library.Item item, Point location)
        {
            //object declaration
            PictureBox icon = new PictureBox() {
                Name = IconName,
                Location = location,
                Size = new Size(IconSize, IconSize),
                Image = LoadImage(item),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BorderStyle = Editor.Configuration.VObjStyle.IconBorder,
                BackColor = DefaultColor,
                Cursor = Cursors.Hand
            };

            Label label = new Label() {
                Name = LabelName,
                Text = item.Name,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                BackColor = DefaultColor,
                BorderStyle = Editor.Configuration.VObjStyle.LabelBorder
            };

            //object event handlers
            bool moveEn = false;
            Point bufPos = new Point(0, 0);

            void VOMouseDown(object sender, MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: 
                        if (Editor.linkProcess) //complete the link
                        {
                            Editor.CreateLink(icon);
                        }
                        else
                        {
                            if (Editor.InsertItem != null && Editor.Replacing)
                            {
                                Editor.Replacing = false;
                                var iobj = icon.Tag as ItemObject;
                                iobj.Item = Editor.InsertItem.Clone() as Library.Item;
                                icon.Image = LoadImage(iobj.Item);
                                icon.Cursor = Cursors.Hand;
                                label.Text = iobj.Item.Name;
                                LabelPosUpdate(label);
                                Editor.InsertItem = null;
                                Editor.Changed = true;
                            }
                            else
                            {
                                Editor.SelectVO(sender);
                                MouseDownLocation = Cursor.Position; //move
                                bufPos = icon.Location;
                                moveEn = true;
                            }
                        }
                        icon.BringToFront();
                        label.BringToFront();
                        break;

                    case MouseButtons.Right: 
                        if (!Editor.linkProcess) //context menu
                        {
                            Editor.SelectVO(sender);
                            Menu.ShowMenu(sender as PictureBox, Cursor.Position);
                        }
                        break;
                }
            }

            bool move = false;

            void VOMouseMove(object sender, MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: 
                        if (!Editor.linkProcess && moveEn) //move obj
                        {
                            int dx, dy;
                            dx = Cursor.Position.X - MouseDownLocation.X;  //get cursor location
                            dy = Cursor.Position.Y - MouseDownLocation.Y;
                            if (Math.Abs(dx) >= 3 || Math.Abs(dy) >= 3 || move)
                            {
                                Point pos = new Point();
                                move = true; //move event

                                if (Control.ModifierKeys == Keys.Shift)
                                {
                                    if (Math.Abs(dx) > Math.Abs(dy))
                                    {
                                        pos.X = bufPos.X + dx;
                                        pos.Y = bufPos.Y;
                                    }
                                    else
                                    {
                                        pos.X = bufPos.X;
                                        pos.Y = bufPos.Y + dy;
                                    }
                                }
                                else
                                {
                                    pos.X = bufPos.X + dx;
                                    pos.Y = bufPos.Y + dy;
                                }

                                Editor.MoveSelectedVOs(dx, dy);
                            }
                            Editor.RetraceArea();
                        }
                        break;

                    case MouseButtons.None:
                        if (Editor.InsertItem != null && Editor.Replacing)
                        {
                            icon.Cursor = Cursors.Cross;
                        }
                        break;
                }
            }

            void VOMouseUp(object sender, MouseEventArgs e)
            {
                if (move)
                {
                    move = false;
                    Editor.Changed = true;
                    Editor.FinishMovingVOs();
                }
                
                moveEn = false;
            }

            void VOMove(object sender, EventArgs e)
            {
                var iobj = icon.Tag as ItemObject;
                iobj.Location = icon.Location;
            }

            icon.MouseDown += new MouseEventHandler(VOMouseDown);
            icon.MouseMove += new MouseEventHandler(VOMouseMove);
            icon.MouseUp += new MouseEventHandler(VOMouseUp);
            icon.Move += new EventHandler(VOMove);

            return new Container(icon, label);
        }

        public static void LabelPosUpdate(Label label)
        {
            PictureBox icon = (label.Tag as ItemObject).TagIcon;
            LocateVO(icon, icon.Location);
        }

        public static void LocateVO(PictureBox vObj, Point location)
        {
            vObj.Location = location;
            Label label = (vObj.Tag as ItemObject).TagLabel;
            label.Location = new Point(
                vObj.Left + vObj.Width / 2 - label.Width / 2,
                vObj.Top + vObj.Height
                );
        }

        private static Image LoadImage(Library.Item item)
        {
            Image image;
            var filePath = Path.Combine(Routine.Directories.Library, item.IconPath);
            if (File.Exists(filePath))
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                image = Routine.ImageNB(Image.FromStream(fs), new Size(IconSize, IconSize), 0, 0);
                fs.Close();
            }
            else
            {
                image = new PictureBox().ErrorImage;
            }

            return image;
        }
    }
}
