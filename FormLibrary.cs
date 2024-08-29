using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Drawing2D;

namespace Recipe
{
    public partial class FormLibrary : Form
    {
        public Library.Item selectedItem;

        private Library.Directory library = null;
        private Config config;

        private bool loaded = false;
        private object lastCtrl;

        private int lbPthPos;

        public FormLibrary(Config config)
        {
            InitializeComponent();
            this.config = config;
            lbPthPos = labelPath.Top - listBoxLibItems.Height - listBoxLibItems.Top;
        }

        private void ExplorerUpdate()
        {
            treeLibDir.Nodes.Clear();
            listBoxLibItems.Items.Clear();
            DeselectItem();

            LibDirScan(library, treeLibDir.Nodes);
        }

        private void LibDirScan(Library.Directory directory, TreeNodeCollection nodes)
        {
            foreach (Library.Directory dir in directory.Directories)
            {
                nodes.Add(dir.Name);
                nodes[nodes.Count - 1].Tag = dir;
                LibDirScan(dir, nodes[nodes.Count - 1].Nodes);
            }
        }

        private void SaveLibrary()
        {
            var data = JsonConvert.SerializeObject(library, Formatting.Indented);
            File.WriteAllText(Routine.Files.Library, data);
        }

        private void DeselectItem()
        {
            pictureBoxItemIcon.Image = null;
            selectedItem = null;
        }

        //  //  /////     ///    //  //    //  //     ////
                //       // //   //  //    //  //    //  //
          //    /////   ///////  //  //    //  //    //  //
          //    //  //  //   //  ////////  ////////  //  //
          //    /////   //   //        //        //   ////

        private void treeLibDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Library.Directory dir = treeLibDir.SelectedNode.Tag as Library.Directory;
            labelPath.Text = treeLibDir.SelectedNode.FullPath;

            listBoxLibItems.Items.Clear();
            DeselectItem();

            foreach (Library.Item item in dir.Items)
            {
                listBoxLibItems.Items.Add(item.Name);
            }
        }

        private void treeLibDir_Click(object sender, EventArgs e)
        {
            lastCtrl = sender;
        }

        private void listBoxLibItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLibItems.SelectedIndex == -1)
            {
                DeselectItem();
                return;
            }

            Library.Directory dir = treeLibDir.SelectedNode.Tag as Library.Directory;
            selectedItem = dir.Items[listBoxLibItems.SelectedIndex];

            string file = Path.Combine(Routine.Directories.Library, selectedItem.IconPath);
            
            if (!File.Exists(file))
            {
                pictureBoxItemIcon.Image = pictureBoxItemIcon.ErrorImage;
                selectedItem = null;
                return;
            }

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            pictureBoxItemIcon.Image = Routine.ImageNB(Image.FromStream(fs), pictureBoxItemIcon.Size);
            fs.Close();
        }

        private void listBoxLibItems_Click(object sender, EventArgs e)
        {
            lastCtrl = sender;
        }

        private void listBoxLibItems_DoubleClick(object sender, EventArgs e)
        {
            buttonItemInsert_Click(null, null);
        }

        private void buttonItemInsert_Click(object sender, EventArgs e)
        {
            Editor.Editor.InsertItem = selectedItem;
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                return;
            }

            FormRename ren = new FormRename(selectedItem.Name);
            if (ren.ShowDialog() == DialogResult.OK)
            {
                selectedItem.Name = ren.ItemName;
                listBoxLibItems.Items[listBoxLibItems.SelectedIndex] = ren.ItemName;
                SaveLibrary();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string text = "Are you sure you want to delete ";

            if (lastCtrl == listBoxLibItems)
            {
                if (listBoxLibItems.SelectedIndex == -1)
                {
                    return;
                }

                text += "item \"" + selectedItem.Name + "\"?";

                if (MessageBox.Show(text, "Item Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var path = Path.Combine(Routine.Directories.Library, selectedItem.IconPath);

                    //check if the item is used in the current project
                    if (Editor.Editor.ItemObjects.Find(x => x.Item.IconPath == selectedItem.IconPath) != null)
                    {
                        if (MessageBox.Show("This item is used in the project. Remove it from the library?", 
                            "Item Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    var dir = treeLibDir.SelectedNode.Tag as Library.Directory;
                    dir.Items.Remove(selectedItem);
                    listBoxLibItems.Items.Remove(listBoxLibItems.SelectedItem);
                    listBoxLibItems.SelectedIndex = -1;
                    SaveLibrary();
                }
            }
            else if (lastCtrl == treeLibDir)
            {
                if (treeLibDir.SelectedNode == null)
                {
                    return;
                }

                text += "branch \"" + treeLibDir.SelectedNode.Text + "\" and its items?";

                if (MessageBox.Show(text, "Branch Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    DeselectItem();
                    var path = Path.Combine(Routine.Directories.Library, treeLibDir.SelectedNode.FullPath);
                    if (Directory.Exists(path))
                    {
                        var files = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
                        foreach (var file in files)
                        {
                            var item = Library.Directory.TrimPath(file);
                            if (Editor.Editor.ItemObjects.Find(x => x.Item.IconPath == item) != null)
                            {
                                if (MessageBox.Show("This branch contains the items used in the project. " +
                                    "Continue deleting branch?", "Branch Delete", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    return;
                                }
                                break;
                            }
                        }

                        Directory.Delete(path, true);
                    }
                    library.DeleteDirectory(treeLibDir.SelectedNode.FullPath);
                    SaveLibrary();
                    ExplorerUpdate();
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (new FormLibraryImport(library, ExplorerUpdate).ShowDialog() == DialogResult.OK)
            {
                SaveLibrary();
            }
        }

        private void FormLibrary_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void FormLibrary_Move(object sender, EventArgs e)
        {
            if (loaded)
            {
                config.ToolPosition.Library = Location;
            }
        }

        private void FormLibrary_Load(object sender, EventArgs e)
        {
            if (config.Loaded)
            {
                Location = config.ToolPosition.Library;
            }

            if (File.Exists(Routine.Files.Library))
            {
                string libSerial = File.ReadAllText(Routine.Files.Library);
                library = JsonConvert.DeserializeObject<Library.Directory>(libSerial);
                library.GenerateRelations();
                ExplorerUpdate();
            }
            else
            {
                library = new Library.Directory("ROOT", null);
                SaveLibrary();
            }

            loaded = true;
        }

        private void FormLibrary_MouseEnter(object sender, EventArgs e)
        {
            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                Opacity = 1.0;
            }
        }

        private void FormLibrary_MouseLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)) && checkBoxOpa.Checked)
            {
                Opacity = 0.5;
            }
        }

        private void FormLibrary_MouseMove(object sender, MouseEventArgs e)
        {
            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                Opacity = 1.0;
            }
            else if (checkBoxOpa.Checked)
            {
                Opacity = 0.5;
            }
        }
        
        private void FormLibrary_Resize(object sender, EventArgs e)
        {
            listBoxLibItems.Height = ClientRectangle.Height - treeLibDir.Top - labelPath.Height;
            treeLibDir.Height = listBoxLibItems.Height;
            labelPath.Top = listBoxLibItems.Height + listBoxLibItems.Top + lbPthPos;
            buttonImport.Top = listBoxLibItems.Height + listBoxLibItems.Top - buttonImport.Height;
        }
    }
}
