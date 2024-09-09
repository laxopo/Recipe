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
    public static class Engine
    {
        private const string FileFormat = "Recipe project file";
        private const string FileVersion = "1.4";
        public const string DataFormat = "RECIPE_DATA";

        public static Size AreaSizeMin = new Size(200, 200);
        public static Size AreaSizeMax = new Size(30000, 30000);
        public static Size AreaSizeDef = new Size(728, 383);

        public static Library.Item InsertItem; //for inserting and one vo cloning
        public static bool Replacing;
        public static List<ItemObject> IODataBase = new List<ItemObject>();
        public static PictureBox CurrentVObj;
        public static PictureBox LinkBegVObj;
        public static bool linkProcess;
        public static bool linkContinuous;

        private static bool retraceRq = false;
        private static int idCount = 0;

        //Selecting VOs
        public static List<ItemObject> selectedIObjs = new List<ItemObject>();
        private static Rectangle rectSelect;
        private static Rectangle selBox;
        private static Point selBoxStartPos;
        private static bool rsEnable;
        private static bool selBoxEnable;

        //VOs cloning
        public static bool Cloning;
        private static List<ItemObject> clonedIObjs = new List<ItemObject>();
        private static Point cloneCenter;
        private static Rectangle cloneRect;
        private static Point cloneOffset;

        private static string _filePath;
        private static bool _changed = false;
        private static string defCaption;

        private static Form mainForm;
        private static Action FitTheControls;
        public static FormPropEditor propEditor;
        public static Config configuration;
        public static FormLibrary libraryForm;
        public static PictureBox Area;
        public static Button AreaResizeButton;
        

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

        public static void Initialize(Form formMain, FormLibrary formLibrary, FormPropEditor propEditor, 
            Panel editorWindow, Config config, Action ctrlFit)
        {
            foreach (Control ctrl in editorWindow.Controls)
            {
                if (ctrl.Name == "pictureBoxArea")
                {
                    Area = ctrl as PictureBox;
                }
                else if (ctrl.Name == "buttonAreaResize")
                {
                    AreaResizeButton = ctrl as Button;
                }
            }

            if (Area == null || AreaResizeButton == null)
            {
                throw
                    new Exception("Editor's control not found");
            }

            mainForm = formMain;
            Engine.propEditor = propEditor;
            defCaption = formMain.Text;
            configuration = config;
            libraryForm = formLibrary;
            FitTheControls = ctrlFit;
        }

        /*File operations*/

        public static void NewSheet()
        {
            FilePath = null;
            Changed = false;
            idCount = 0;
            InsertItem = null;
            Replacing = false;
            rsEnable = false;
            selBoxEnable = false;
            linkProcess = false;
            selectedIObjs.Clear();
            clonedIObjs.Clear();
            IODataBase.Clear();
            Area.Controls.Clear();
            Area.Location = new Point(0, 0);
            AreaResizeButton.Location = (Point)AreaSizeDef;
            RetraceArea();
        }

        public static void LoadSheet(string path)
        {
            string serialData = File.ReadAllText(path);
            Format format = null;

            //Validate the file data
            try
            {
                format = JsonConvert.DeserializeObject<Format>(serialData);
            }
            catch
            {
                MessageBox.Show("This file cannot be read as a Recipe Project", "Loading Error",
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

            //Reset flags & params
            NewSheet();
            FilePath = path;

            //Prepare area
            AreaResizeButton.Left = format.SheetSize.Width;
            AreaResizeButton.Top = format.SheetSize.Height;

            //Init database
            IODataBase = format.Data;
            idCount = format.Count;

            //Load IOs & VOs and show VOs
            GenerateLinks();
            foreach (ItemObject iobj in IODataBase)
            {
                CreateVO(iobj, iobj.Location);
            }
            RetraceArea();
        }

        public static void SaveSheet(string path)
        {
            Format format = new Format() {
                FileFormat = FileFormat,
                Version = FileVersion,
                SheetSize = Area.Size,
                Data = IODataBase,
                Count = idCount
            };

            string serial = JsonConvert.SerializeObject(format, Formatting.Indented);
            File.WriteAllText(path, serial);

            Changed = false;
            FilePath = path;
        }

        /*Linking*/

        public static void LinkingEnable(PictureBox begVObj)
        {
            LinkBegVObj = begVObj;
            linkProcess = true;
        }

        public static void LinkingDisable()
        {
            linkProcess = false;
            linkContinuous = false;
            LinkBegVObj = null;
            DeselectVOs();
        }

        public static void CreateLinks(PictureBox endVObj, bool continuously)
        {
            if (!continuously && linkContinuous)
            {
                LinkingDisable();
                RetraceArea();
                return;
            }

            if (endVObj == null) //beg to end array
            {
                ItemObject beg = LinkBegVObj.Tag as ItemObject;

                foreach (var end in selectedIObjs)
                {
                    CreateLink(beg, end);
                }
            }
            else //(beg|array) to end
            {
                ItemObject end = endVObj.Tag as ItemObject;

                foreach (var beg in selectedIObjs)
                {
                    CreateLink(beg, end);
                }
            }

            Changed = true;

            if (!continuously)
            {
                LinkingDisable();
            }
            else
            {
                linkContinuous = true;
            }
            RetraceArea();
        }

        public static void CreateLink(ItemObject beg, ItemObject end)//Linking, creates new links between iobjs
        {
            //self-linking
            if (beg == end)
            {
                return;
            }

            //Remove opposite link
            end.LinksOut.Remove(beg.ID);
            end.LinkOutTags.Remove(beg);
            
            //Set an output link
            if (!beg.LinkOutTags.Contains(end))
            {
                beg.LinksOut.Add(end.ID);
                beg.LinkOutTags.Add(end);
            }

            //Set an input link
            if (!end.LinkInTags.Contains(beg))
            {
                end.LinksIn.Add(beg.ID);
                end.LinkInTags.Add(beg);
            }
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

            foreach (ItemObject iobjBeg in IODataBase)
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
                    int iconSize = VisualObject.Constructor.IconSize;
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

                                if (dy < 0 && ix >= lbLoc.X && ix <= xLeft + lbSize.Height / 2 && ixi >= lbLoc.X) //label intersection
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
                                    yEnd = Convert.ToInt32(yLeft); //left
                                }
                                xEnd = Convert.ToInt32(xLeft);
                            }
                            else if (yRight >= yTop && yRight <= yBot && Math.Abs(xBeg - xRight) <= Math.Abs(xBeg - xLeft)) //right
                            {
                                double ki = (yEnd - yBeg) / (double)(xRight - xBeg);

                                double ix = (lbLoc.Y - yBeg) / k + xBeg;
                                double ixi = (lbLoc.Y - yBeg) / ki + xBeg;
                                int lbxRight = lbLoc.X + lbSize.Width;

                                if (dy < 0 && ix <= lbxRight && ix >= xRight - lbSize.Height / 2 && ixi <= lbxRight) //label intersection
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
                                    yEnd = Convert.ToInt32(yRight); //right
                                }
                                xEnd = Convert.ToInt32(xRight);
                            }
                        }

                        gpx.DrawLine(arrow, xBeg, yBeg, xEnd, yEnd);
                    }
                }
            }

            //rectangular selection
            if (rsEnable)
            {
                Pen dashline = new Pen(Color.Gray, 1);
                float[] dashPattern = { 5, 5 };
                dashline.DashPattern = dashPattern;
                gpx.DrawRectangle(dashline, rectSelect);
            }

            //cloning a rectanglular selection
            if (Cloning)
            {
                Pen pen = new Pen(Color.Gray, 1);
                gpx.DrawRectangle(pen, cloneRect);
            }

            //selection box
            if (selBoxEnable)
            {
                Pen pen = new Pen(Color.Blue, 1);
                float[] dashPattern = { 1, 1 };
                pen.DashPattern = dashPattern;
                gpx.DrawRectangle(pen, selBox);
            }

            retraceRq = false;
        }

        /*VObject*/

        public static void SelectVO(object sender, bool additive)
        {
            SelectVO((sender as PictureBox).Tag as ItemObject, additive);
        }

        public static void SelectVO(ItemObject iobj, bool additive) //HL single VO and activate it, deselect and deact other
        {
            if (!selectedIObjs.Contains(iobj))
            {
                if (!additive)
                {
                    //unHL other VOs
                    foreach (ItemObject io in selectedIObjs)
                    {
                        HighLightVO(io.TagIcon, false);
                    }

                    //Deselect all VOs
                    selectedIObjs.Clear();
                }

                //Selest this VO
                selectedIObjs.Add(iobj);
            }
            
            HighLightVO(iobj.TagIcon, true);

            //activate this VO
            if (selectedIObjs.Count > 1)
            {
                CurrentVObj = null;
            }
            else
            {
                CurrentVObj = iobj.TagIcon;
            }
                
            propEditor.LoadItem(false);

            SelectBoxShow();
            RetraceArea();
        }

        public static void DeselectVOs() //unHL all VOs and deactivate
        {
            //unHL all VOs
            foreach (ItemObject iobj in selectedIObjs)
            {
                HighLightVO(iobj.TagIcon, false);
            }

            //Deselect all VOs
            selectedIObjs.Clear();

            //Deactivate VO
            CurrentVObj = null;
            propEditor.LoadItem(false);

            SelectBoxHide();
        }

        public static void RectangleSelectDraw(Point begin, Point end)//Draw rectangle
        {
            //get TL point of rectangle
            int dx = begin.X - end.X;
            int dy = begin.Y - end.Y;
            int xl, yt;

            if (dx < 0)
            {
                xl = begin.X;
            }
            else
            {
                xl = end.X;
            }
            if (dy < 0)
            {
                yt = begin.Y;
            }
            else
            {
                yt = end.Y;
            }

            //create rectangle of selection
            rectSelect = new Rectangle(new Point(xl, yt), new Size(Math.Abs(dx), Math.Abs(dy)));

            rsEnable = true;
            RetraceArea();
        }

        public static void RectangleSelectVOs(bool additive) //HL all VO in the rectangle bounds
        {
            //deselect previous VOs
            if (!additive)
            {
                DeselectVOs();
            }

            //Select VOs in the select bounds
            foreach (Control vobj in Area.Controls)
            {
                if (vobj.Name == VisualObject.Constructor.IconName)
                {
                    Point voRB = new Point(vobj.Right, vobj.Bottom);
                    if (rectSelect.Contains(vobj.Location) || rectSelect.Contains(voRB))
                    {
                        PictureBox pb = vobj as PictureBox;
                        HighLightVO(pb, true);
                        SelectedBufferAdd(pb.Tag as ItemObject);
                    }
                }
            }

            SelectBoxShow();

            //Clear rectangle drawing
            rsEnable = false;
            RetraceArea();
        }

        public static void MoveSelectedVOs(int dx, int dy)
        {
            //check bounds
            selBox = RelocationLimiter(ref dx, ref dy, selBox, selBoxStartPos);

            //set new location
            foreach (ItemObject iobj in selectedIObjs)
            {
                PictureBox icon = iobj.TagIcon;

                var rf = iobj.StartLocation;
                var pos = new Point(rf.X + dx, rf.Y + dy);

                VisualObject.Constructor.LocateVO(icon, pos);

                icon.BringToFront();
                (icon.Tag as ItemObject).TagLabel.BringToFront();
            }
        }

        public static void AreaResize(Size sizeNew, bool vobjShift, bool bypass)
        {
            if (!bypass)
            {
                //select all VOs
                DeselectVOs();
                foreach (ItemObject iobj in IODataBase)
                {
                    selectedIObjs.Add(iobj);
                }
                selBox = SelectedVOsBox();
                selBoxStartPos = selBox.Location;

                //limit new area size (fit to vobj array bounds if smaller)
                if (vobjShift)
                {
                    if (sizeNew.Width < selBox.Width + 2)
                    {
                        sizeNew.Width = selBox.Width + 2;
                    }
                    if (sizeNew.Height < selBox.Height + 2)
                    {
                        sizeNew.Height = selBox.Height + 2;
                    }
                }
                else
                {
                    if (sizeNew.Width < selBox.Right + 2)
                    {
                        sizeNew.Width = selBox.Right + 2;
                    }
                    if (sizeNew.Height < selBox.Bottom + 2)
                    {
                        sizeNew.Height = selBox.Bottom + 2;
                    }
                }
            }

            //check the minimal size
            if (sizeNew.Width < AreaSizeMin.Width)
            {
                sizeNew.Width = AreaSizeMin.Width;
            }
            if (sizeNew.Height < AreaSizeMin.Height)
            {
                sizeNew.Height = AreaSizeMin.Height;
            }

            //size and locatiod differentiation
            int dx = sizeNew.Width - Area.Width;
            int dy = sizeNew.Height - Area.Height;

            //Resize an area
            Area.Size = sizeNew;
            AreaResizeButton.Location = (Point)Area.Size;

            if (!bypass)
            {
                //Move VOs
                if (vobjShift)
                {
                    MoveSelectedVOs(dx, dy);
                }

                //finish
                FinishMovingVOs();
                selectedIObjs.Clear();
            }

            Changed = true;
            FitTheControls();
        }

        public static void FinishMovingVOs()
        {
            if (selectedIObjs.Count < 0)
            {
                return;
            }

            foreach (ItemObject iobj in selectedIObjs)
            {
                iobj.StartLocation = iobj.Location;
            }

            selBoxStartPos = selBox.Location;
        }

        public static void CancelMovingVOs()
        {
            foreach (ItemObject iobj in selectedIObjs)
            {
                iobj.Location = iobj.StartLocation;
            }
        }

        public static void CreateVisualObject(Point location)
        {
            if (Cloning) //array cloning
            {
                CloneVOs(location);
            }
            else //one insert/clone
            {
                CreateVO(InsertItem.Clone() as Library.Item, location);
            }
        }

        public static void CloneVOsStart(Point center)
        {
            CloneVOST(center, false, null);
        }

        public static void CloneVOsStart(Clip clipBoard)
        {
            CloneVOST(new Point(0, 0), true, clipBoard);
        }

        public static void CloneVOsFinish()
        {
            Cloning = false;
            RetraceArea();
        }

        public static void CloneBoxDraw(Point position) //Draw a rectangle
        {
            position.Offset(cloneOffset);
            cloneRect = RelocationLimiterAbs(position, cloneRect, new Rectangle(0, 0, 0, 0));

            RetraceArea();
        }

        public static void RemoveVOs()
        {
            foreach (ItemObject delIObj in selectedIObjs)
            {
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
                IODataBase.Remove(delIObj);
                Changed = true;
            }

            selectedIObjs.Clear();

            //Update area
            SelectBoxHide();
        }

        public static void Copy()
        {
            if (selectedIObjs.Count == 0)
            {
                Cloning = false;
                return;
            }

            //Push the data into clipboard
            Clipboard.Clear();
            Clip clip = new Clip(selectedIObjs, selBox);
            Clipboard.SetData(DataFormat, clip.Serialize());
        }

        public static bool Paste(Point location)
        {
            Clip clip = Clip.Deserialize(Clipboard.GetData(DataFormat));
            if (clip == null)
            {
                return false;
            }

            //Get the data from clipboard
            DeselectVOs();
            CloneVOsStart(clip);
            CloneBoxDraw(location);
            RetraceArea();

            return true;
        }

        public static void SelectAll()
        {
            rectSelect = Area.ClientRectangle;
            RectangleSelectVOs(false);
        }

        /**/

        private static void CreateVO(Library.Item item, Point location) //Insert
        {
            CreateVO(item, location, true, null);
        }

        private static void CreateVO(ItemObject source, Point location) //Load or clone
        {
            CreateVO(source.Item, location, Cloning, source);
        }

        private static void CreateVO(Library.Item item, Point location, bool createNew, ItemObject source)
        {
            //Get new VO
            var ct = VisualObject.Constructor.GenerateVO(item, location, true);

            //Register IO
            if (createNew)
            {
                //Create IO record
                IODataBase.Add(new ItemObject()
                {
                    Location = location,
                    StartLocation = location,
                    Item = item,
                    ID = idCount++,
                    TagIcon = ct.Icon,
                    TagLabel = ct.Label
                });
 
                //Create VO-IO reference
                ct.Icon.Tag = IODataBase.Last();
                ct.Label.Tag = IODataBase.Last();

                /*check area bounds*/
                var box = GetVOBox(ct);

                //shift VO
                Point newLoc = ct.Icon.Location;
                bool reloc = false;
                if (box.Left < 0)
                {
                    newLoc.X -= box.Left;
                    reloc = true;
                }
                if (box.Top < 0)
                {
                    newLoc.Y -= box.Top;
                    reloc = true;
                }
                if (reloc)
                {
                    VisualObject.Constructor.LocateVO(ct, newLoc);
                    IODataBase.Last().ResetRelocation();
                }

                //resize the area
                Size newSize = Area.Size;
                bool resize = false;
                if (box.Right > Area.Width)
                {
                    newSize.Width = box.Right;
                    resize = true;
                }
                if (box.Bottom > Area.Height)
                {
                    newSize.Height = box.Bottom;
                    resize = true;
                }
                if (resize)
                {
                    AreaResize(newSize, false, true);
                }
                /*END check area bounds*/

                //Set an insert event flags
                InsertItem = null;
                Changed = true;
            }
            else
            {
                //Create VO-IO reference
                ct.Icon.Tag = source;
                ct.Label.Tag = source;
                source.TagIcon = ct.Icon;
                source.TagLabel = ct.Label;

                //Reset iobj location
                source.StartLocation = source.Location;
            }

            //Show VO
            Area.Controls.AddRange(new Control[] { 
                ct.Icon, 
                ct.Label
            });
            VisualObject.Constructor.LabelPosUpdate(ct.Label);
        }

        private static void CloneVOs(Point location)
        {
            clonedIObjs.Clear();

            //Generate VOs & IOs
            foreach (ItemObject iobjSrc in selectedIObjs)
            {
                //Calc new IO location
                Point iObjPos = new Point()
                {
                    //old obj pos in src - old pos of center + new pos of center
                    //x = dx + xc
                    //dx: pos relative to the center (pos in the selection), xc: new center pos
                    X = iobjSrc.Location.X - cloneCenter.X + cloneRect.X - cloneOffset.X,
                    Y = iobjSrc.Location.Y - cloneCenter.Y + cloneRect.Y - cloneOffset.Y,
                };

                //Create new IO
                CreateVO(iobjSrc, iObjPos);

                //Write cloning data to the new IO, registry new IO as a cloned
                var iobjNew = IODataBase.Last();
                iobjNew.OldID = iobjSrc.ID;
                iobjNew.LinksIn = new List<int>(iobjSrc.LinksIn);
                iobjNew.LinksOut = new List<int>(iobjSrc.LinksOut);
                clonedIObjs.Add(iobjNew);
            }

            //Generate links (ch: clonedIObjs; x.OldID
            GenerateLinks();
            CloneVOsFinish();
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
            if (vobj == null)
            {
                return;
            }

            Label label = (vobj.Tag as ItemObject).TagLabel;
            if (highLight)
            {
                label.BackColor = VisualObject.Constructor.HighlightColor;
            }
            else
            {
                label.BackColor = VisualObject.Constructor.DefaultColor;
            }
        }

        private static void GenerateLinks()//Generates cross-links by IDs
        {
            List<ItemObject> iObjList;
            if (Cloning)
            {
                iObjList = clonedIObjs;
            }
            else
            {
                iObjList = IODataBase;
            }

            //Generate cross-links by IDs (or old IDs)
            foreach (ItemObject iobjBeg in iObjList) //Get the beginning iobj
            {
                for (int i = iobjBeg.LinksOut.Count - 1; i > -1; i--)  //Get id of the ending iobj
                {
                    int idEnd = iobjBeg.LinksOut[i];

                    //Get the ending iobj
                    ItemObject iobjEnd;
                    if (Cloning) 
                    {
                        iobjEnd = IODataBase.Find(x => x.OldID == idEnd);
                    }
                    else
                    {
                        iobjEnd = IODataBase.Find(x => x.ID == idEnd);
                    }
                    
                    //if ID is defined & valid - create cross-link, else - delete this ID
                    if (iobjEnd == null || idEnd == -1)
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

            if (Cloning)
            {
                //Update links IDs
                foreach (ItemObject iobjBeg in clonedIObjs)
                {
                    //Remove old IDs
                    iobjBeg.LinksIn.Clear();
                    iobjBeg.LinksOut.Clear();

                    //Generate new IDs
                    foreach (ItemObject link in iobjBeg.LinkOutTags)
                    {
                        iobjBeg.LinksOut.Add(link.ID);
                    }
                    foreach (ItemObject link in iobjBeg.LinkInTags)
                    {
                        iobjBeg.LinksIn.Add(link.ID);
                    }
                }

                //Reset OldID records
                foreach (ItemObject iobj in clonedIObjs)
                {
                    iobj.OldID = -1;
                }
            }
        }

        private static Rectangle SelectedVOsBox()//inside the area
        {
            int xl = -1, xr = -1, yt = -1, yb = -1;

            foreach (ItemObject iobj in selectedIObjs)
            {
                PictureBox icon = iobj.TagIcon;
                Label label = iobj.TagLabel;

                //get VO bound box
                var box = GetVOBox(icon, label);

                //Extend the bounds
                if (box.Left < xl || xl == -1)
                {
                    xl = box.Left;
                }
                if (box.Right > xr || xr == -1)
                {
                    xr = box.Right;
                }
                if (box.Top < yt || yt == -1)
                {
                    yt = box.Top;
                }
                if (box.Bottom > yb || yb == -1)
                {
                    yb = box.Bottom;
                }
            }

            return new Rectangle(xl, yt, xr - xl, yb - yt);
        }

        private static Rectangle GetVOBox(VisualObject.Container ct)
        {
            return GetVOBox(ct.Icon, ct.Label);
        }

        private static Rectangle GetVOBox(PictureBox icon, Label label)
        {
            Point lt = new Point(0, icon.Top);
            if (label.Left < icon.Left)
            {
                lt.X = label.Left;
            }
            else
            {
                lt.X = icon.Left;
            }

            Point rb = new Point(0, label.Bottom);
            if (label.Right > icon.Right)
            {
                rb.X = label.Right;
            }
            else
            {
                rb.X = icon.Right;
            }

            return new Rectangle(lt.X - 2, lt.Y - 2, rb.X - lt.X + 4, rb.Y - lt.Y + 4);
        }

        private static void SelectBoxShow()
        {
            selBox = SelectedVOsBox();
            selBoxStartPos = selBox.Location;
            selBoxEnable = true;
        }

        private static void SelectBoxHide()
        {
            selBoxEnable = false;
            RetraceArea();
        }

        private static Rectangle RelocationLimiter(ref int dx, ref int dy, Rectangle box, Point boxRefPos)
        {
            return RelocationLimiter(ref dx, ref dy, box, boxRefPos, Area.ClientRectangle);
        }

        private static Rectangle RelocationLimiter(ref int dx, ref int dy, Rectangle box, Point boxRefPos, Rectangle bounds)
        {
            //limits dx|dy (relative to boxRefPos) if the resulting box are out of the bounds
            Rectangle boxBuf = box;
            boxBuf.Location = GetOffsetPoint(boxRefPos, dx, dy);

            if (boxBuf.Left < 0)
            {
                dx -= boxBuf.Left;
            }
            else if (boxBuf.Right > bounds.Width && bounds.Width > 0)
            {
                dx -= boxBuf.Right - bounds.Width;
            }
            if (boxBuf.Top < 0)
            {
                dy -= boxBuf.Top;
            }
            else if (boxBuf.Bottom > bounds.Height && bounds.Height > 0)
            {
                dy -= boxBuf.Bottom - bounds.Height;
            }

            box.Location = GetOffsetPoint(boxRefPos, dx, dy);

            return box;
        }

        private static Rectangle RelocationLimiterAbs(Point locationNew, Rectangle box, Rectangle bounds)
        {
            //limits dx|dy (relative to boxRefPos) if the resulting box are out of the bounds
            Rectangle boxBuf = box;
            boxBuf.Location = locationNew;

            if (boxBuf.Left < 0)
            {
                locationNew.X -= boxBuf.Left;
            }
            else if (boxBuf.Right > bounds.Width && bounds.Width > 0)
            {
                locationNew.X -= boxBuf.Right - bounds.Width;
            }
            if (boxBuf.Top < 0)
            {
                locationNew.Y -= boxBuf.Top;
            }
            else if (boxBuf.Bottom > bounds.Height && bounds.Height > 0)
            {
                locationNew.Y -= boxBuf.Bottom - bounds.Height;
            }

            box.Location = locationNew;

            return box;
        }

        private static Point GetOffsetPoint(Point reference, int dx, int dy)
        {
            reference.Offset(dx, dy);
            return reference;
        }

        private static void CloneVOST(Point center, bool externData, Clip clip)
        {
            //do the normal insert
            if (!externData && selectedIObjs.Count == 1)
            {
                InsertItem = selectedIObjs[0].Item;
                Cloning = false;
                return;
            }

            //get the bounds of cloning massive
            if (externData)
            {
                selectedIObjs = clip.IOs;
                cloneRect = clip.Box;
                cloneOffset = clip.Offset;
                cloneCenter = clip.Center;
            }
            else
            {
                cloneRect = SelectedVOsBox();
                cloneOffset = new Point(cloneRect.X - center.X, cloneRect.Y - center.Y);
                cloneCenter = center;
            }

            if (selectedIObjs.Count == 0)
            {
                Cloning = false;
                return;
            }

            Cloning = true;
            CloneBoxDraw(center);
        }

        private static void SelectedBufferAdd(ItemObject iobj)
        {
            if (!selectedIObjs.Contains(iobj))
            {
                selectedIObjs.Add(iobj);
            }
        }
    }
}
