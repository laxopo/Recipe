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
        private Library.Exp explorerBuff = new Library.Exp();
        private bool filtered = false;
        private Config config;

        private bool loaded = false;
        private object lastCtrl;
        private int sOffset;
        private int sHeight;

        public FormLibrary(Config config)
        {
            InitializeComponent();
            this.config = config;
            sOffset = buttonSearch.Top - listBoxLibItems.Bottom;
            sHeight = ClientRectangle.Height - buttonSearch.Top;
        }

        public void OpenItem(Library.Item item)
        {
            Show();

            //reset search
            if (filtered)
            {
                ExplorerUpdate();
            }

            //Select node
            var lpath = Library.Directory.DeserializePath(Path.GetDirectoryName(item.IconPath));
            SelectNode(lpath);

            if (treeLibDir.SelectedNode == null)
            {
                return;
            }

            //Show item
            var exp = treeLibDir.SelectedNode.Tag as Library.Exp;
            int index = exp.Items.FindIndex(x => x.IconPath == item.IconPath);
            listBoxLibItems.SelectedIndex = index; 
        }

        /**/

        private void ExplorerUpdate()
        {
            textBoxSearch.Text = "";
            Explorer(null, false);
        }

        private void Explorer(TreeNode node, bool topOnly)
        {
            DeselectItem();
            listBoxLibItems.Items.Clear();
            labelPath.Text = "";

            Library.Directory dir;
            Library.Exp exp = null;
            var text = textBoxSearch.Text.ToUpper();
            filtered = !(text == "");

            if (node == null)
            {
                dir = library;
            }
            else
            {
                exp = node.Tag as Library.Exp;
                dir = exp.Directory;
            }

            //Clear all nodes
            treeLibDir.Nodes.Clear();
            explorerBuff = new Library.Exp();

            //create selected node tree
            var lpath = dir.GetDirectoryLPath();
            exp = explorerBuff;
            foreach (var name in lpath)
            {
                exp.SubNodes.Add(new Library.Exp(name));
                exp = exp.SubNodes.Last();
            }

            //explore
            Explore(dir, exp, text, topOnly);

            //show results
            TreeUpdate(null, explorerBuff);
        }

        private void Explore(Library.Directory dir, Library.Exp exp, string searchText, bool topOnly)
        {
            //setup node
            exp.Directory = dir;

            //items
            foreach (var item in exp.Directory.Items)
            {
                if (item.Name.ToUpper().Contains(searchText))
                {
                    exp.Items.Add(item);
                }
            }

            if (topOnly)
            {
                return;
            }

            //directories
            foreach (var subDir in exp.Directory.Directories)
            {
                exp.SubNodes.Add(new Library.Exp() { 
                    Parent = exp,
                    Node = new TreeNode(subDir.Name),
                    Directory = subDir,
                });

                Library.Exp last = exp.SubNodes.Last();

                Explore(subDir, last, searchText, false);

                if (last.Items.Count == 0 && last.SubNodes.Count == 0)
                {
                    exp.SubNodes.Remove(last);
                }
            }
        }

        private void TreeUpdate(TreeNode node, Library.Exp exp)
        {
            TreeNodeCollection nodeColl;
            if (node == null)
            {
                nodeColl = treeLibDir.Nodes;
            }
            else
            {
                nodeColl = node.Nodes;
                node.Tag = exp;
            }

            foreach (Library.Exp subExp in exp.SubNodes)
            {
                nodeColl.Add(subExp.Node);
                TreeNode last = nodeColl[nodeColl.Count - 1];
                TreeUpdate(last, subExp);
            }
        }

        private void SelectNode(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            var lpath = (node.Tag as Library.Exp).Directory.GetDirectoryLPath();
            SelectNode(lpath);
        }

        private void SelectNode(List<string> lpath)
        {
            var nodeColl = treeLibDir.Nodes;
            TreeNode curNode = null;

            foreach (var dirName in lpath)
            {
                curNode = null;
                foreach (TreeNode nd in nodeColl)
                {
                    if (nd.Text == dirName)
                    {
                        curNode = nd;
                        break;
                    }
                }

                if (curNode == null)
                {
                    return;
                }
                nodeColl = curNode.Nodes;
            }

            treeLibDir.SelectedNode = curNode;
        }

        private void ItemListUpdate(TreeNode node)
        {
            Library.Exp exp;

            if (node == null)
            {
                exp = explorerBuff;
            }
            else
            {
                exp = node.Tag as Library.Exp;
            }

            listBoxLibItems.Items.Clear();
            foreach (Library.Item item in exp.Items)
            {
                listBoxLibItems.Items.Add(item.Name);
            }
            listBoxLibItems.Tag = exp;
        }

        private void ItemPreview()
        {
            if (selectedItem == null)
            {
                return;
            }

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

        private void DeselectItem()
        {
            pictureBoxItemIcon.Image = null;
            selectedItem = null;
            Editor.Editor.InsertItem = null;
            listBoxLibItems.SelectedIndex = -1;
        }

        private void SaveLibrary()
        {
            var data = JsonConvert.SerializeObject(library, Formatting.Indented);
            File.WriteAllText(Routine.Files.Library, data);
        }

        //  //  /////     ///    //  //    //  //     ////
                //       // //   //  //    //  //    //  //
          //    /////   ///////  //  //    //  //    //  //
          //    //  //  //   //  ////////  ////////  //  //
          //    /////   //   //        //        //   ////

        private void treeLibDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DeselectItem();
            ItemListUpdate(treeLibDir.SelectedNode);

            if (treeLibDir.SelectedNode == null)
            {
                labelPath.Text = "<root>";
            }
            else
            {
                labelPath.Text = treeLibDir.SelectedNode.FullPath;
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

            Editor.Editor.InsertItem = null;
            var exp = listBoxLibItems.Tag as Library.Exp;
            selectedItem = exp.Items[listBoxLibItems.SelectedIndex];
            ItemPreview();
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
                    if (Editor.Editor.IODataBase.Find(x => x.Item.IconPath == selectedItem.IconPath) != null)
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
                            if (Editor.Editor.IODataBase.Find(x => x.Item.IconPath == item) != null)
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
            listBoxLibItems.Height = ClientRectangle.Height - treeLibDir.Top - sHeight;
            treeLibDir.Height = listBoxLibItems.Height;
            int searchTop = listBoxLibItems.Bottom + sOffset;
            textBoxSearch.Top = searchTop + 2;
            buttonSearchClear.Top = searchTop;
            buttonSearch.Top = searchTop;
            checkBoxIncSubDir.Top = searchTop + 4;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var node = treeLibDir.SelectedNode;
            Explorer(treeLibDir.SelectedNode, !checkBoxIncSubDir.Checked);
            SelectNode(node);
        }

        private void treeLibDir_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Bounds.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                treeLibDir.SelectedNode = null;
                labelPath.Text = "<root>";
                treeLibDir_AfterSelect(null, null);
            }
        }

        private void buttonSearchClear_Click(object sender, EventArgs e)
        {
            var node = treeLibDir.SelectedNode;
            var item = listBoxLibItems.Text;
            ExplorerUpdate();
            SelectNode(node);
            listBoxLibItems.SelectedItem = item;
        }
    }
}
