using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Newtonsoft.Json;

namespace Recipe.Editor
{
    public static class Editor
    {
        private const string FileFormat = "Recipe project file";
        private const string FileVersion = "1.0";

        public static Size AreaSizeMin = new Size(200, 200);
        public static Size AreaSizeMax = new Size(30000, 30000);

        public static Library.Item InsertItem;
        public static List<ItemObject> ItemObjects = new List<ItemObject>();
        public static PictureBox CurrentVObj;
        public static PictureBox Area;
        public static Button AreaResize;
        public static bool linkProcess;
        public static PropEditor propEditor;
        public static Config Configuration;

        private static bool retraceRq = false;
        private static int idCount = 0;

        //Rectangle select
        private static List<PictureBox> selectedVOs = new List<PictureBox>();
        private static List<Point> locationVOs = new List<Point>();
        private static Rectangle rectSelect;
        private static bool rsEnable;
        
        private static Form mainForm;
        private static string _filePath;
        private static bool _changed = false;
        private static string defCaption;


        public static string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                _filePath = value;
                CaptionUpdate();
            }
        }

        public static bool Changed
        {
            get
            {
                return _changed;
            }

            set
            {
                _changed = value;
                CaptionUpdate();
            }
        }

        /**/

        public static void Initialize(Form formMain, PropEditor propEditor, Panel editorWindow, Config config)
        {
            foreach (Control ctrl in editorWindow.Controls)
            {
                if (ctrl.Name == "pictureBoxArea")
                {
                    Area = ctrl as PictureBox;
                }
                else if (ctrl.Name == "buttonAreaResize")
                {
                    AreaResize = ctrl as Button;
                }
            }

            if (Area == null || AreaResize == null)
            {
                throw
                    new Exception("Editor control not found");
            }

            mainForm = formMain;
            Editor.propEditor = propEditor;
            defCaption = formMain.Text;
            Configuration = config;
        }

        /*File operations*/

        public static void NewSheet()
        {
            FilePath = null;
            Changed = false;
            idCount = 0;
            InsertItem = null;
            ItemObjects.Clear();
            Area.Controls.Clear();
            Area.Location = new Point(0, 0);
            AreaResize.Location = new Point(728, 383);
            RetraceArea();
        }

        public static void LoadSheet(string path)
        {
            string serialData = File.ReadAllText(path);

            Format format = null;
            try
            {
                format = JsonConvert.DeserializeObject<Format>(serialData);
            }
            catch
            {
                MessageBox.Show("File reading error. Invalid data", "Loading Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (format.FileFormat != FileFormat)
            {
                MessageBox.Show("This file is not a Recipe project", "Loading Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (format.Version != FileVersion)
            {
                if (MessageBox.Show("This project version is different. It may load incorrectly. Continue?", "Loading Project",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            InsertItem = null;
            Changed = false;
            FilePath = path;

            //Prepare area
            Area.Controls.Clear();
            Area.Location = new Point(0, 0);
            AreaResize.Left = format.SheetSize.Width;
            AreaResize.Top = format.SheetSize.Height;

            ItemObjects = format.Data;

            idCount = format.Count;

            //Generate iobj reference tags
            foreach (ItemObject iobjBeg in ItemObjects) //Get the beginning iobj
            {
                for (int i = iobjBeg.LinksOut.Count - 1; i > -1; i--)  //Get id of the ending iobj
                {
                    int idEnd = iobjBeg.LinksOut[i];
                    ItemObject iobjEnd = ItemObjects.Find(x => x.ID == idEnd); //Get the ending iobj
                    if (iobjEnd == null)
                    {
                        iobjBeg.LinksOut.RemoveAt(i);
                    }
                    else
                    {
                        iobjBeg.LinkOutTags.Add(iobjEnd);
                        iobjEnd.LinkInTags.Add(iobjBeg);
                    }
                }
            }

            //Generate visual objects
            foreach (ItemObject iobj in ItemObjects)
            {
                var ct = VisualObject.VisualObject.GenerateVO(iobj.Item, iobj.Location);
                ct.Icon.Tag = iobj;
                ct.Label.Tag = iobj;
                iobj.TagIcon = ct.Icon;
                iobj.TagLabel = ct.Label;
                Area.Controls.Add(ct.Icon);
                Area.Controls.Add(ct.Label);
                VisualObject.VisualObject.LocateLabels(ct);
            }

            RetraceArea();
        }

        public static void SaveSheet(string path)
        {
            Format format = new Format() {
                FileFormat = FileFormat,
                Version = FileVersion,
                SheetSize = Area.Size,
                Data = ItemObjects,
                Count = idCount
            };

            string serial = JsonConvert.SerializeObject(format, Formatting.Indented);
            File.WriteAllText(path, serial);

            Changed = false;
            FilePath = path;
        }

        /*Linking*/

        public static void LinkingEnable()
        {
            linkProcess = true;
        }

        public static void LinkingDisable()
        {
            linkProcess = false;
            DeselectVOs();
        }

        public static void CreateLink(PictureBox endVObj)
        {
            ItemObject beg = CurrentVObj.Tag as ItemObject;
            ItemObject end = endVObj.Tag as ItemObject;

            //Find dublicate
            if (beg.LinkOutTags.Contains(end))
            {
                LinkingDisable();
                return;
            }

            //Remove opposite link
            end.LinkOutTags.Remove(beg);

            beg.LinksOut.Add(end.ID);
            beg.LinkOutTags.Add(end);

            end.LinksIn.Add(beg.ID);
            end.LinkInTags.Add(beg);

            Changed = true;

            LinkingDisable();
            RetraceArea();
        }

        /*Area Drawing*/

        public static void RetraceArea()
        {
            retraceRq = true;
            Area.Invalidate();
            Area.Update();
        }

        public static void Tracing(Graphics gpx, bool forced)
        {
            if (!retraceRq && !forced)
            {
                return;
            }

            foreach (ItemObject iobjBeg in ItemObjects)
            {
                Point beg = iobjBeg.Location;

                if (iobjBeg.LinkOutTags.Count == 0)
                {
                    continue;
                }

                foreach (ItemObject iobjEnd in iobjBeg.LinkOutTags)
                {
                    Pen arrow, line;

                    if (iobjBeg.LinkOutHLs.Contains(iobjEnd) && !forced) //Highlight beg output link
                    {
                        arrow = new Pen(Color.Red, 2);
                        line = new Pen(Color.Red, 2);
                    }
                    else 
                    {
                        if (iobjEnd.LinkInHLs.Contains(iobjBeg) && !forced) //Highlight end input link
                        {
                            arrow = new Pen(Color.Orange, 2);
                            line = new Pen(Color.Orange, 2);
                        }
                        else //Default
                        {
                            arrow = new Pen(Color.Black, 2);
                            line = new Pen(Color.Black, 2);
                        }
                    }
                    arrow.CustomEndCap = new AdjustableArrowCap(5, 10);

                    Point end = iobjEnd.Location;

                    Point lbLoc = iobjEnd.TagLabel.Location;
                    lbLoc.Offset(-2, -2);
                    Size lbSize = iobjEnd.TagLabel.Size;
                    lbSize.Width += 4;

                    //Get size offset
                    int iconSize = VisualObject.VisualObject.IconSize;
                    int offset = iconSize / 2;

                    //Get center
                    int xBeg = beg.X + offset;
                    int yBeg = beg.Y + offset;
                    int xEnd = end.X + offset;
                    int yEnd = end.Y + offset;

                    //Equation coefficients
                    double dx = xEnd - xBeg;
                    double dy = yEnd - yBeg;

                    //End point
                    int xLeft = xEnd - offset; //left
                    int xRight = xEnd + offset; //right
                    int yTop = yEnd - offset; //top
                    int yBot = yEnd + offset + lbSize.Height; //bottom

                    if ((Math.Abs(dx) > iconSize || Math.Abs(dy) > iconSize)) //VOs don't intersect each other
                    {
                        if (dy != 0) //Vertical
                        {
                            double k = dx / dy;
                            double xTop = k * (yTop - yBeg) + xBeg;
                            double xBot = k * (yBot - yBeg) + xBeg;
                            if (xTop >= xLeft && xTop <= xRight && Math.Abs(yBeg - yBot) >= Math.Abs(yBeg - yTop)) //up
                            {
                                xEnd = Convert.ToInt32(xTop);
                                yEnd = Convert.ToInt32(yTop);
                            }
                            else if (xBot >= xLeft && xBot <= xRight && Math.Abs(yBeg - yBot) <= Math.Abs(yBeg - yTop)) //down
                            {
                                xEnd = Convert.ToInt32(xBot);
                                yEnd = Convert.ToInt32(yBot);
                            }
                        }
                        if (dx != 0) //Horizontal
                        {
                            double k = dy / dx;
                            double yLeft = k * (xLeft - xBeg) + yBeg;
                            double yRight = k * (xRight - xBeg) + yBeg;
                            if (yLeft >= yTop && yLeft <= yBot && Math.Abs(xBeg - xRight) >= Math.Abs(xBeg - xLeft)) //left
                            {
                                double ki = (yEnd - yBeg) / (double)(xLeft - xBeg);

                                double ix = (lbLoc.Y - yBeg) / k + xBeg;
                                double ixi = (lbLoc.Y - yBeg) / ki + xBeg;

                                if (dy < 0 && ix >= lbLoc.X && ix <= xLeft && ixi >= lbLoc.X) //label intersection
                                {
                                    if (xBeg > lbLoc.X) //down
                                    {
                                        yEnd = yBot;
                                    }
                                    else //segment arrow
                                    {
                                        gpx.DrawLine(line, xBeg, yBeg, lbLoc.X, lbLoc.Y - 1);
                                        xBeg = lbLoc.X - 1;
                                        yBeg = lbLoc.Y;
                                    }
                                }
                                else
                                {
                                    yEnd = Convert.ToInt32(yLeft);
                                }
                                xEnd = Convert.ToInt32(xLeft);
                            }
                            else if (yRight >= yTop && yRight <= yBot && Math.Abs(xBeg - xRight) <= Math.Abs(xBeg - xLeft)) //right
                            {
                                double ki = (yEnd - yBeg) / (double)(xRight - xBeg);

                                double ix = (lbLoc.Y - yBeg) / k + xBeg;
                                double ixi = (lbLoc.Y - yBeg) / ki + xBeg;
                                int lbxRight = lbLoc.X + lbSize.Width;

                                if (dy < 0 && ix <= lbxRight && ix >= xRight && ixi <= lbxRight) //label intersection
                                {
                                    if (xBeg < lbxRight) //down
                                    {
                                        yEnd = yBot;
                                    }
                                    else //segment arrow
                                    {
                                        gpx.DrawLine(line, xBeg, yBeg, lbxRight, lbLoc.Y - 1);
                                        xBeg = lbxRight + 1;
                                        yBeg = lbLoc.Y;
                                    }
                                }
                                else
                                {
                                    yEnd = Convert.ToInt32(yRight);
                                }
                                xEnd = Convert.ToInt32(xRight);
                            }
                        }

                        gpx.DrawLine(arrow, xBeg, yBeg, xEnd, yEnd);
                    }
                }
            }

            //rectangle select
            if (rsEnable)
            {
                Pen dashline = new Pen(Color.Gray, 1);
                float[] dashPattern = { 5, 5 };
                dashline.DashPattern = dashPattern;
                gpx.DrawRectangle(dashline, rectSelect);
            }

            retraceRq = false;
        }

        /*VObject*/

        public static void SelectVO(object sender) //HL single VO and activate it, deselect and deact other
        {
            var vobj = sender as PictureBox;

            //Select and HL this VO
            if (!selectedVOs.Contains(vobj))
            {
                //unHL other VOs
                foreach (PictureBox oth in selectedVOs)
                {
                    HighLightVO(oth, false);
                }

                //Deselect all VOs
                selectedVOs.Clear();
                locationVOs.Clear();

                //Selest this VO
                selectedVOs.Add(vobj);
                locationVOs.Add(vobj.Location);
                HighLightVO(vobj, true);

                //activate this VO
                CurrentVObj = vobj;
                propEditor.LoadItem();
            }
        }

        public static void DeselectVOs() //unHL all VOs and deactivate
        {
            //unHL all VOs
            foreach (PictureBox vobj in selectedVOs)
            {
                HighLightVO(vobj, false);
            }

            //Deselect all VOs
            selectedVOs.Clear();
            locationVOs.Clear();

            //Deactivate VO
            CurrentVObj = null;
            propEditor.LoadItem();
        }

        public static void RectangleSelectDraw(Point begin, Point end)
        {
            
            int dx = begin.X - end.X;
            int dy = begin.Y - end.Y;
            int x1, x2, y1, y2;

            if (dx < 0)
            {
                x1 = begin.X;
                x2 = end.X;
            }
            else
            {
                x1 = end.X;
                x2 = begin.X;
            }
            if (dy < 0)
            {
                y1 = begin.Y;
                y2 = end.Y;
            }
            else
            {
                y1 = end.Y;
                y2 = begin.Y;
            }

            rectSelect = new Rectangle(new Point(x1, y1), new Size(Math.Abs(dx), Math.Abs(dy)));

            rsEnable = true;
            RetraceArea();
        }//Draw rectangle

        public static void RectangleSelectVOs() //HL all VO in the rectangle bounds
        {
            DeselectVOs();

            foreach (Control vobj in Area.Controls)
            {
                if (vobj.Name == VisualObject.VisualObject.IconName)
                {
                    if (rectSelect.Contains(vobj.Location))
                    {
                        PictureBox pb = vobj as PictureBox;
                        HighLightVO(pb, true);
                        selectedVOs.Add(pb);
                        locationVOs.Add(pb.Location);
                    }
                }
            }

            //Clear rectangle drawing
            rsEnable = false;
            RetraceArea();
        }

        public static void MoveSelectedVOs(int dx, int dy)
        {
            //check bounds
            for (int i = 0; i < selectedVOs.Count; i++)
            {
                PictureBox icon = selectedVOs[i];

                Label label = (selectedVOs[i].Tag as ItemObject).TagLabel;

                var rf = locationVOs[i];
                var pos = new Point(rf.X + dx, rf.Y + dy);

                Point bound = new Point()
                {
                    X = Area.Width - icon.Width,
                    Y = Area.Height - icon.Height - label.Height
                };

                if (pos.X < 0)
                {
                    dx += -pos.X;
                }
                else if (pos.X > bound.X)
                {
                    dx -= pos.X - bound.X;
                }

                if (pos.Y < 0)
                {
                    dy += -pos.Y;
                }
                else if (pos.Y > bound.Y)
                {
                    dy -= pos.Y - bound.Y;
                }
            }

            //set new location
            for (int i = 0; i < selectedVOs.Count; i++)
            {
                PictureBox icon = selectedVOs[i];
                Label label = (selectedVOs[i].Tag as ItemObject).TagLabel;

                var rf = locationVOs[i];
                var pos = new Point(rf.X + dx, rf.Y + dy);

                icon.Location = pos;
                label.Location = new Point(
                    icon.Left + VisualObject.VisualObject.IconSize / 2 - label.Width / 2,
                    icon.Top + VisualObject.VisualObject.IconSize);

                icon.BringToFront();
                label.BringToFront();
            }
        }

        public static void ShiftVOs(int dx, int dy)
        {
            foreach (Control icon in Area.Controls)
            {
                if (icon.Name == VisualObject.VisualObject.IconName)
                {
                    Label label = (icon.Tag as ItemObject).TagLabel;

                    var rf = icon.Location;
                    var pos = new Point(rf.X + dx, rf.Y + dy);

                    Point bound = new Point()
                    {
                        X = Area.Width - icon.Width,
                        Y = Area.Height - icon.Height - label.Height
                    };

                    if (pos.X < 0)
                    {
                        dx += -pos.X;
                    }

                    if (pos.Y < 0)
                    {
                        dy += -pos.Y;
                    }
                }
            }

            foreach (Control ctrl in Area.Controls)
            {
                var rf = ctrl.Location;
                var pos = new Point(rf.X + dx, rf.Y + dy);
                ctrl.Location = pos;
            }
        }

        public static void FinishMovingVOs()
        {
            if (selectedVOs.Count < 0)
            {
                return;
            }

            for (int i = 0; i < selectedVOs.Count; i++)
            {
                locationVOs[i] = selectedVOs[i].Location;
            }
        }

        public static void CancelMovingVOs()
        {
            for (int i = 0; i < selectedVOs.Count; i++)
            {
                selectedVOs[i].Location = locationVOs[i];
            }
        }

        public static void CreateVisualObject(Point location)
        {
            CreateVO(InsertItem.Clone() as Library.Item, location);
        }

        public static void CreateVisualObject(Library.Item item, Point location)
        {
            CreateVO(item.Clone() as Library.Item, location);
        }

        public static void RemoveVOs()
        {
            for (int i = 0; i < selectedVOs.Count; i++)
            {
                var delIObj = selectedVOs[i].Tag as ItemObject;

                if (CurrentVObj == delIObj.TagIcon)
                {
                    CurrentVObj = null;
                }

                //delete links
                foreach (ItemObject beg in delIObj.LinkInTags)
                {
                    beg.LinkOutTags.Remove(delIObj);
                    beg.LinksOut.Remove(delIObj.ID);
                }
                foreach (ItemObject end in delIObj.LinkOutTags)
                {
                    end.LinkInTags.Remove(delIObj);
                    end.LinksIn.Remove(delIObj.ID);
                }

                //delete VO and IO record
                Area.Controls.Remove(delIObj.TagIcon);
                Area.Controls.Remove(delIObj.TagLabel);
                ItemObjects.Remove(delIObj);
            }

            selectedVOs.Clear();
            locationVOs.Clear();

            //Update area
            RetraceArea();
        }
        /**/

        private static void CreateVO(Library.Item item, Point location)
        {
            var ct = VisualObject.VisualObject.GenerateVO(item, location);

            ItemObjects.Add(new ItemObject()
            {
                Location = location,
                Item = item,
                ID = idCount++,
                TagIcon = ct.Icon,
                TagLabel = ct.Label
            });

            ct.Icon.Tag = ItemObjects.Last();
            ct.Label.Tag = ItemObjects.Last();

            InsertItem = null;
            Changed = true;

            Area.Controls.AddRange(new Control[] { 
                ct.Icon, 
                ct.Label
            });

            VisualObject.VisualObject.LocateLabels(ct);
        }

        private static void CaptionUpdate()
        {
            var caption = defCaption;

            if (_filePath != null || _changed)
            {
                caption = " - " + caption;
            }

            if (_filePath != null)
            {
                caption = Path.GetFileName(_filePath) + caption;
            }

            if (_changed)
            {
                caption = "*" + caption;
            }

            mainForm.Text = caption;
        }

        private static void HighLightVO(PictureBox vobj, bool highLight)
        {
            Label label = (vobj.Tag as ItemObject).TagLabel;
            if (highLight)
            {
                label.BackColor = VisualObject.VisualObject.HighlightColor;
            }
            else
            {
                label.BackColor = VisualObject.VisualObject.DefaultColor;
            }
        }
    }
}
