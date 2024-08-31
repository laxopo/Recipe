using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace Recipe
{
    public partial class FormMain : Form
    {
        private static Config config;
        private static FormLibrary formLibrary;
        private static PropEditor propEditor;

        private static FormatTable graphicsExport = new FormatTable( new List<FormatTable.Coll>()
        {
            new FormatTable.Coll("PNG", new string[]{ "png" } , ImageFormat.Png),
            new FormatTable.Coll("BMP", new string[]{ "bmp" } , ImageFormat.Bmp),
            new FormatTable.Coll("JPEG", new string[]{ "jpg", "jpeg" } , ImageFormat.Jpeg),
            new FormatTable.Coll("GIF", new string[]{ "gif" } , ImageFormat.Gif),
            new FormatTable.Coll("TIFF", new string[]{ "tiff" } , ImageFormat.Tiff),
            new FormatTable.Coll("EXIF", new string[]{ "exif" } , ImageFormat.Exif),
            new FormatTable.Coll("EMF", new string[]{ "emf" } , ImageFormat.Emf),
            new FormatTable.Coll("WMF", new string[]{ "wmf" } , ImageFormat.Wmf)
        });

        public FormMain()
        {
            InitializeComponent();

            config = Config.Load(Routine.Files.Config);

            formLibrary = new FormLibrary(config);
            formLibrary.Owner = this;
            propEditor = new PropEditor(config);
            propEditor.Owner = this;

            Editor.Editor.Initialize(this, propEditor, panelEditor, config);

            FitTheControls();

            saveFileDialogSnapshot.Filter = graphicsExport.GetExtFilter();
        }

        private void ResizeSheetDialog()
        {
            SheetResize rsz = new SheetResize(pictureBoxArea.Size);
            if (rsz.ShowDialog() == DialogResult.OK)
            {
                Editor.Editor.AreaResize(rsz.newSize, rsz.shift);
                FitTheControls();
            }
        }

        private void FitTheControls()
        {
            //Fit & Set position
            panelEditor.Width = ClientRectangle.Width - panelEditor.Left - vScrollBarEditor.Width - 1;
            panelEditor.Height = ClientRectangle.Height - panelEditor.Top - hScrollBarEditor.Height - 1;

            hScrollBarEditor.Left = panelEditor.Left;
            hScrollBarEditor.Top = panelEditor.Top + panelEditor.Height;
            hScrollBarEditor.Width = panelEditor.Width;

            vScrollBarEditor.Left = panelEditor.Left + panelEditor.Width;
            vScrollBarEditor.Top = panelEditor.Top;
            vScrollBarEditor.Height = panelEditor.Height;

            int adx = 0, ady = 0;

            int offset = pictureBoxArea.Left + pictureBoxArea.Width - panelEditor.Width + 10;
            if (offset < 0)
            {
                adx = -offset;
            }

            offset = pictureBoxArea.Top + pictureBoxArea.Height - panelEditor.Height + 10;
            if (offset < 0)
            {
                ady = -offset;
            }

            if (adx != 0 || ady != 0)
            {
                AreaMove(adx, ady, true);
            }
            else
            {
                ScrollBarsUpdate();
            }
        }

        private bool UnsavedFile(string caption)
        {
            DialogResult dr = DialogResult.OK;
            if (Editor.Editor.Changed)
            {
                string text = " is changed. Do you want to save it?";

                if (Editor.Editor.FilePath != null)
                {
                    text = "File \"" + Editor.Editor.FilePath + "\"" + text;
                }
                else
                {
                    text = "Current file" + text;
                }

                dr = MessageBox.Show(text, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            }

            switch (dr)
            {
                case DialogResult.Yes:
                    saveToolStripMenuItem_Click(null, null);
                    if (Editor.Editor.Changed)
                    {
                        return false;
                    }
                    break;

                case DialogResult.Cancel:
                    return false;
            }

            return true;
        }

        /*ToolStripMenu*/
        //File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UnsavedFile("New File"))
            {
                return;
            }

            Editor.Editor.NewSheet();
            AreaSizeUpdate();
            FitTheControls();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UnsavedFile("Open File"))
            {
                return;
            }

            if (openFileDialogProj.ShowDialog() == DialogResult.OK)
            {
                Editor.Editor.LoadSheet(openFileDialogProj.FileName);
            }
            AreaSizeUpdate();
            FitTheControls();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.Editor.Changed)
            {
                if (Editor.Editor.FilePath != null)
                {
                    if (File.Exists(Editor.Editor.FilePath))
                    {
                        File.Delete(Editor.Editor.FilePath);
                    }

                    Editor.Editor.SaveSheet(Editor.Editor.FilePath);
                }
                else
                {
                    if (saveFileDialogProj.ShowDialog() == DialogResult.OK)
                    {
                        Editor.Editor.SaveSheet(saveFileDialogProj.FileName);
                    }
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogProj.ShowDialog() == DialogResult.OK)
            {
                Editor.Editor.SaveSheet(saveFileDialogProj.FileName);
            }
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Editor.Editor.FilePath != null)
            {
                saveFileDialogSnapshot.FileName = Path.GetFileNameWithoutExtension(Editor.Editor.FilePath);
            }
            else
            {
                saveFileDialogSnapshot.FileName = "";
            }
            

            if (saveFileDialogSnapshot.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Bitmap bmp = new Bitmap(pictureBoxArea.ClientSize.Width,
                               pictureBoxArea.ClientSize.Height);
            Graphics gpx = Graphics.FromImage(bmp);

            gpx.Clear(pictureBoxArea.BackColor);

            if (pictureBoxArea.ImageLocation != null)
            {
                Size imgsz = pictureBoxArea.Image.Size;

                Rectangle imgRegion = new Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Size = imgsz
                };

                switch (pictureBoxArea.SizeMode)
                {
                    case PictureBoxSizeMode.StretchImage:
                        imgRegion.Size = bmp.Size;
                        break;

                    case PictureBoxSizeMode.CenterImage:
                        imgRegion.Location = new Point()
                        {
                            X = bmp.Width / 2 - imgsz.Width / 2,
                            Y = bmp.Height / 2 - imgsz.Height / 2,
                        };
                        break;

                    case PictureBoxSizeMode.Zoom:
                        double ki = (double)imgsz.Width / imgsz.Height;
                        double kb = (double)bmp.Width / bmp.Height;

                        if (ki > kb)
                        {
                            imgRegion.Size = new Size(bmp.Width, Convert.ToInt32(bmp.Width / ki));
                        }
                        else
                        {
                            imgRegion.Size = new Size(Convert.ToInt32(ki * bmp.Height), bmp.Height);
                        }

                        imgRegion.Location = new Point()
                        {
                            X = bmp.Width / 2 - imgRegion.Size.Width / 2,
                            Y = bmp.Height / 2 - imgRegion.Size.Height / 2,
                        };

                        break;
                }

                gpx.DrawImage(pictureBoxArea.Image, imgRegion);
            }
            

            Editor.Editor.Tracing(gpx, true);
            Editor.Editor.DeselectVOs();

            foreach (Control ctrl in pictureBoxArea.Controls)
            {
                var bc = new Bitmap(ctrl.ClientSize.Width, ctrl.ClientSize.Height);
                ctrl.DrawToBitmap(bc, ctrl.ClientRectangle);

                gpx.DrawImage(bc, ctrl.Location);
            }
            
            bmp.Save(saveFileDialogSnapshot.FileName, 
                graphicsExport.GetImageFormat(saveFileDialogSnapshot.FilterIndex));

            Editor.Editor.RetraceArea();
        }

        //Edit
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Editor.Editor.RemoveVOs();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        //Tools
        private void libraryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Show Library
            formLibrary.Show();
        }

        private void itemPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show property editor
            propEditor.Show();
        }

        //Sheet
        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResizeSheetDialog();
        }

        private void fitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sheet will fit to the object array.", "Sheet Resize",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                Editor.Editor.AreaResize(new Size(0, 0), true);
                FitTheControls();
            }
        }

        private void appToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SheetView sb = new SheetView(pictureBoxArea, config);
            sb.ShowDialog();
        }

        //About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        //Hotkey handler
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        /*Editor*/
        bool insert;
        bool move;
        bool rectSel;
        bool rescSelEv;
        private Cursor GetCursor()//Changes the editor cursor (not vobj)
        {
            if (move)
            {
                return Cursors.SizeAll;
            }
            else if (insert)
            {
                return Cursors.Cross;
            }

            return Cursors.Default;
        }

        private void AreaMove(int x, int y, bool relative)
        {
            //Define the rsz button position, after set the area position

            //Area & window size; Reset the area location (start position)
            bool rstX = false, rstY = false;
            if (pictureBoxArea.Width + 10 < panelEditor.Width)
            {
                buttonAreaResize.Left = pictureBoxArea.Width;
                rstX = true;
            }
            if (pictureBoxArea.Height + 10 < panelEditor.Height)
            {
                buttonAreaResize.Top = pictureBoxArea.Height;
                rstY = true;
            }

            Point rszPos = buttonAreaResize.Location;
            if (!rstX)
            {
                //Start border check
                if (relative)
                {
                    rszPos.X += x;
                }
                else
                {
                    rszPos.X = x;
                }
                
                if (rszPos.X > pictureBoxArea.Width)
                {
                    rszPos.X = pictureBoxArea.Width;
                }

                //End border check
                if (rszPos.X < panelEditor.Width - 10)
                {
                    rszPos.X = panelEditor.Width - 10;
                }
            }
            if (!rstY)
            {
                //Start border check
                if (relative)
                {
                    rszPos.Y += y;
                }
                else
                {
                    rszPos.Y = y;
                } 
                if (rszPos.Y > pictureBoxArea.Height)
                {                             
                    rszPos.Y = pictureBoxArea.Height;
                }

                //End border check
                if (rszPos.Y < panelEditor.Height - 10)
                {
                    rszPos.Y = panelEditor.Height - 10;
                }
            }

            buttonAreaResize.Location = rszPos;

            //Define area position
            pictureBoxArea.Left = rszPos.X - pictureBoxArea.Width;
            pictureBoxArea.Top = rszPos.Y - pictureBoxArea.Height;

            ScrollBarsUpdate();
            Editor.Editor.RetraceArea();
        }

        private void AreaSizeUpdate()
        {
            pictureBoxArea.Width = buttonAreaResize.Left - pictureBoxArea.Left;
            pictureBoxArea.Height = buttonAreaResize.Top - pictureBoxArea.Top;
            panelEditor.Update();
        }

        private void ScrollBarsUpdate()
        {
            int h = pictureBoxArea.Width - panelEditor.Width;
            if (h > 0)
            {
                hScrollBarEditor.Enabled = true;
                hScrollBarEditor.Maximum = h + hScrollBarEditor.LargeChange + 10;

                int val = -pictureBoxArea.Left;
                if (val <= h)
                {
                    if (val > 0)
                    {
                        hScrollBarEditor.Value = val;
                    }
                    else
                    {
                        hScrollBarEditor.Value = 0;
                    }
                }
                else
                {
                    hScrollBarEditor.Value = h + 10;
                }
            }
            else
            {
                hScrollBarEditor.Enabled = false;
            }

            int v = pictureBoxArea.Height - panelEditor.Height;
            if (v > 0)
            {
                vScrollBarEditor.Enabled = true;
                vScrollBarEditor.Maximum = v + vScrollBarEditor.LargeChange + 10;

                int val = -pictureBoxArea.Top;
                if (val <= v)
                {
                    if (val > 0)
                    {
                        vScrollBarEditor.Value = val;
                    }
                    else
                    {
                        vScrollBarEditor.Value = 0;
                    }
                }
                else
                {
                    vScrollBarEditor.Value = v + 10;
                }
            }
            else
            {
                vScrollBarEditor.Enabled = false;
            }
        }

        private void pictureBoxArea_MouseEnter(object sender, EventArgs e)
        {
            //Cursor style
            if (Editor.Editor.InsertItem != null || Editor.Editor.Cloning)
            {
                insert = true;
                pictureBoxArea.Cursor = GetCursor();
            }
        }

        private Point mouseDown;
        private Point posDown;
        private void pictureBoxArea_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: //rectSelect
                    if (!Editor.Editor.linkProcess && !insert)
                    {
                        mouseDown = e.Location;
                        rectSel = true;
                    }
                    
                    break;

                case MouseButtons.Middle: //area move
                    mouseDown = Cursor.Position;
                    posDown = buttonAreaResize.Location;
                    move = true;
                    pictureBoxArea.Cursor = GetCursor();
                    break;
            }
        }

        private void pictureBoxArea_MouseMove(object sender, MouseEventArgs e)
        {
            labelCoordinates.Text = "X:" + e.X + " Y:" + e.Y;
            switch (e.Button)
            {
                case MouseButtons.Left: //rectSelect
                    if (rectSel)
                    {
                        int dx = Math.Abs(mouseDown.X - e.Location.X);
                        int dy = Math.Abs(mouseDown.Y - e.Location.Y);
                        if (dx > 5 || dy > 5)
                        {
                            rescSelEv = true;
                            Editor.Editor.RectangleSelectDraw(mouseDown, e.Location);
                        }
                    }
                        
                    break;

                case MouseButtons.Middle: //area move
                    int x = posDown.X + Cursor.Position.X - mouseDown.X;
                    int y = posDown.Y + Cursor.Position.Y - mouseDown.Y;
                    AreaMove(x, y, false);
                    break;

                case MouseButtons.None: //clone massive
                    if (insert && Editor.Editor.Cloning)
                    {
                        Editor.Editor.CloneBoxDraw(e.Location);
                    }
                    break;
            }
        }

        private void pictureBoxArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (!Editor.Editor.linkProcess && !move && !insert)
            {
                Editor.Editor.DeselectVOs();
            }

            switch (e.Button)
            {
                case MouseButtons.Left: //rectSelect
                    if (Editor.Editor.InsertItem != null || Editor.Editor.Cloning) //insert new item from the library
                    {
                        Editor.Editor.CreateVisualObject(e.Location);

                        insert = false;
                        pictureBoxArea.Cursor = GetCursor();
                    }
                    if (rescSelEv)
                    {
                        Editor.Editor.RectangleSelectVOs();
                        rectSel = false;
                        rescSelEv = false;
                    }
                    break;

                case MouseButtons.Middle: //area move
                    move = false;
                    pictureBoxArea.Cursor = GetCursor();
                    break;

                case MouseButtons.Right:
                    if (Editor.Editor.linkProcess) //Cancel linking
                    {
                        Editor.Editor.LinkingDisable();
                    }
                    else if (Editor.Editor.InsertItem != null || Editor.Editor.Cloning) //Cancel inserting
                    {
                        Editor.Editor.InsertItem = null;
                        Editor.Editor.CloneVOsFinish();
                        insert = false;
                        pictureBoxArea.Cursor = GetCursor();
                    }
                    else //Editor context menu
                    {
                        Point loc = e.Location;
                        loc.X += Left;
                        loc.Y += Top;
                        contextMenuStripEditor.Show(Cursor.Position);
                    }
                    break;
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            FitTheControls();
            ScrollBarsUpdate();
        }

        //Scroll Bars
        private void hScrollBarEditor_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBoxArea.Left = -hScrollBarEditor.Value;
            buttonAreaResize.Left = pictureBoxArea.Left + pictureBoxArea.Width;
            Editor.Editor.RetraceArea();
        }

        private void vScrollBarEditor_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBoxArea.Top = -vScrollBarEditor.Value;
            buttonAreaResize.Top = pictureBoxArea.Top + pictureBoxArea.Height;
            Editor.Editor.RetraceArea();
        }

        //Resize Area button
        Point startPos;
        private void buttonAreaResize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = Cursor.Position;
                startPos = buttonAreaResize.Location;
            }
        }

        private void buttonAreaResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = Cursor.Position.X - mouseDown.X;
                int dy = Cursor.Position.Y - mouseDown.Y;

                Point pos = Routine.Limiter(
                    new Point(startPos.X + dx, startPos.Y + dy),
                    pictureBoxArea.Location,
                    Editor.Editor.AreaSizeMin,
                    Editor.Editor.AreaSizeMax
                    );

                buttonAreaResize.Location = pos;

                Editor.Editor.Changed = true;

                AreaSizeUpdate();
            }
        }

        private void buttonAreaResize_MouseUp(object sender, MouseEventArgs e)
        {
            Editor.Editor.AreaResize(pictureBoxArea.Size, false); //limit a new size
            FitTheControls();
        }

        //Area update
        private void pictureBoxArea_Paint(object sender, PaintEventArgs e)
        {
            Editor.Editor.Tracing(e.Graphics, false);
        }

        private void pictureBoxArea_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Editor.Editor.RetraceArea();
        }

        //Editor context menu
        private void contextMenuStripEditor_Opening(object sender, CancelEventArgs e)
        {
            bool en = Clipboard.ContainsData(Editor.Editor.DataFormat);
            pasteToolStripMenuItem.Enabled = en;
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show Library
            formLibrary.Show();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Editor.Paste();
        }

        /*Form*/

        private void FormMain_Load(object sender, EventArgs e)
        {
            /*TEST*/
            string[] args = { 
            "",
            @"C:\Users\Barii\Desktop\schematics\Titanium.json"
            };

            //string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                if (File.Exists(args[1]))
                {
                    Editor.Editor.LoadSheet(args[1]);
                    if (Editor.Editor.FilePath == null)
                    {
                        Close();
                    }

                    AreaSizeUpdate();
                    FitTheControls();
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UnsavedFile("Closing The Program"))
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                config.Save(Routine.Files.Config);
            }
        }        

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            if (!UnsavedFile("Open File"))
            {
                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Editor.Editor.LoadSheet(files[0]);

            AreaSizeUpdate();
            FitTheControls();
        }

        bool minimized = false;
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                minimized = true;
                return;
            }

            if ((WindowState == FormWindowState.Maximized || 
                WindowState == FormWindowState.Normal) && minimized)
            {
                minimized = false;
                Editor.Editor.RetraceArea();
            }
        }
    }
}