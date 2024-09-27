using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Recipe.Editor.VisualObject
{
    public static class Constructor
    {
        public const int IconSize = 32;
        public const string IconName = "voIcon";
        public const string LabelName = "voLabel";

        public static Color DefaultColor = Color.Transparent;
        public static Color HighlightColor = Color.Yellow;

        private static Point MouseDownLocation;

        public static Container GenerateVO(Library.Item item, Point location, bool interactive)
        {
            //object declaration
            PictureBox icon = new PictureBox() {
                Name = IconName,
                Location = location,
                Size = new Size(IconSize, IconSize),
                Image = LoadImage(item),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BorderStyle = Engine.configuration.VObjStyle.IconBorder,
                BackColor = DefaultColor,
                Cursor = Cursors.Hand
            };

            Label label = new Label() {
                Name = LabelName,
                Text = item.Name,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                BackColor = DefaultColor,
                BorderStyle = Engine.configuration.VObjStyle.LabelBorder
            };

            label.Size = label.CreateGraphics().MeasureString(label.Text, label.Font).ToSize();
            LocateLabel(icon, label);
            SetItemTypeStyle(item, label);

            //object event handlers
            bool moveEn = false;
            Point bufPos = new Point(0, 0);
            bool move = false;

            void VOMouseDown(object sender, MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: 
                        if (Engine.linkProcess) //complete the link
                        {
                            Engine.CreateLinks(icon, Control.ModifierKeys == Keys.Control); // beg/array -> end (this icon)
                        }
                        else
                        {
                            if (Engine.InsertItem != null && Engine.Replacing) //replace
                            {
                                Engine.Replacing = false;
                                var iobj = icon.Tag as ItemObject;
                                iobj.Item = Engine.InsertItem.Clone() as Library.Item;
                                icon.Image = LoadImage(iobj.Item);
                                icon.Cursor = Cursors.Hand;
                                label.Text = iobj.Item.Name;
                                LabelPosUpdate(label);
                                Engine.InsertItem = null;
                                Engine.Changed = true;
                                Engine.DeselectVOs();
                                Calculator.CEngine.IsActual = false;
                            }
                            else
                            {
                                MouseDownLocation = Cursor.Position; //move
                                Engine.SelectVO(sender, Control.ModifierKeys == Keys.Control);
                                bufPos = icon.Location;
                                moveEn = true;
                            }
                        }
                        icon.BringToFront();
                        label.BringToFront();
                        break;

                    case MouseButtons.Right: 
                        if (!Engine.linkProcess) //context menu
                        {
                            Engine.SelectVO(sender, false);
                            Menu.ShowMenu(sender as PictureBox, Cursor.Position);
                        }
                        break;
                }
            }

            void VOMouseMove(object sender, MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: 
                        if (!Engine.linkProcess && moveEn) //move obj
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

                                Engine.MoveSelectedVOs(dx, dy);
                            }
                            Engine.RetraceArea();
                        }
                        break;

                    case MouseButtons.None:
                        if (Engine.InsertItem != null && Engine.Replacing)
                        {
                            icon.Cursor = Cursors.Cross;
                        }
                        else if (icon.Cursor != Cursors.Hand)
                        {
                            icon.Cursor = Cursors.Hand;
                        }
                        break;
                }
            }

            void VOMouseUp(object sender, MouseEventArgs e)
            {
                if (move)
                {
                    move = false;
                    Engine.Changed = true;
                    Engine.FinishMovingVOs();
                }
                
                moveEn = false;
            }

            void VOMove(object sender, EventArgs e)
            {
                var iobj = icon.Tag as ItemObject;
                iobj.Location = icon.Location;
            }

            void VODoubleClick(object sender, EventArgs e)
            {
                Engine.propEditor.Show();
            }

            if (interactive)
            {
                icon.MouseDown += new MouseEventHandler(VOMouseDown);
                icon.MouseMove += new MouseEventHandler(VOMouseMove);
                icon.MouseUp += new MouseEventHandler(VOMouseUp);
                icon.Move += new EventHandler(VOMove);
                icon.DoubleClick += new EventHandler(VODoubleClick);
            }

            return new Container(icon, label);
        }

        public static void LabelPosUpdate(Label label)
        {
            PictureBox icon = (label.Tag as ItemObject).TagIcon;
            LocateVO(icon, icon.Location);
        }

        public static void LocateVO(Container ct, Point location)
        {
            ct.Icon.Location = location;
            LocateLabel(ct.Icon, ct.Label);
        }

        public static void LocateVO(PictureBox icon, Point location)
        {
            icon.Location = location;
            Label label = (icon.Tag as ItemObject).TagLabel;
            LocateLabel(icon, label);
        }

        public static void LocateLabel(PictureBox icon, Label label)
        {
            label.Location = new Point(
                icon.Left + icon.Width / 2 - label.Width / 2,
                icon.Top + icon.Height
                );
        }

        public static void ItemTypeStyleUpdate(ItemObject iobj)
        {
            SetItemTypeStyle(iobj.Item, iobj.TagLabel);
            LabelPosUpdate(iobj.TagLabel);
            Engine.Changed = true;
            Calculator.CEngine.IsActual = false;
        }

        public static void VOTextUpdate(ItemObject iobj)
        {
            var label = iobj.TagLabel;
            label.Text = iobj.Item.Name;
            LabelPosUpdate(label);
            Engine.Changed = true;
            Calculator.CEngine.IsActual = false;
        }

        /**/

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

        private static void SetItemTypeStyle(Library.Item item, Label label)
        {
            switch (item.Type)
            {
                case Library.Item.ItemType.Mechanism:
                    label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
                    label.ForeColor = SystemColors.ControlText;
                    break;

                case Library.Item.ItemType.Block:
                    label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
                    label.ForeColor = Color.Maroon;
                    break;

                case Library.Item.ItemType.Fluid:
                    label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
                    label.ForeColor = Color.Blue;
                    break;

                default:
                    label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
                    label.ForeColor = SystemColors.ControlText;
                    break;
            }
        }
    }
}
