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
        private List<TreeNode> treeNodes = new List<TreeNode>();
        private bool searchDirBeg = false;
        private bool searchDirEnd = false;
        private bool filtered = false;
        private Config config;

        private bool loaded = false;
        private object lastCtrl;
        private int sOffset;
        private int sHeight;
        private OpacityForm opacity; //autonomic form opacity handler

        public FormLibrary(Config config)
        {
            InitializeComponent();
            this.config = config;
            sOffset = groupBoxSearch.Top - listBoxLibItems.Bottom;
            sHeight = ClientRectangle.Height - groupBoxSearch.Top;
            opacity = new OpacityForm(this, checkBoxOpa);
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
            textBoxItemSearch.Text = "";
            Explorer(null, false);
        }

        private void Explorer(TreeNode node, bool topOnly)
        {
            DeselectItem();
            listBoxLibItems.Items.Clear();
            labelPath.Text = "";

            Library.Directory dir;
            Library.Exp exp = null;
            var text = textBoxItemSearch.Text.ToUpper();
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

            treeNodes.Clear();
            NodesListUpdate(treeLibDir.Nodes);
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

        private void SelectFirstItem()
        {
            foreach (TreeNode node in treeNodes)
            {
                var exp = node.Tag as Library.Exp;
                if (exp.Items.Count > 0)
                {
                    treeLibDir.SelectedNode = node;
                    listBoxLibItems.SelectedItem = exp.Items.First();
                    break;
                }
            }
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
            pictureBoxItemIcon.Image = Routine.ImageNB(Image.FromStream(fs), pictureBoxItemIcon.Size, 0, 0);
            fs.Close();
        }

        private void DeselectItem()
        {
            pictureBoxItemIcon.Image = null;
            selectedItem = null;
            Editor.Engine.InsertItem = null;
            listBoxLibItems.SelectedIndex = -1;
        }

        private void SaveLibrary()
        {
            var data = JsonConvert.SerializeObject(library, Formatting.Indented);
            File.WriteAllText(Routine.Files.Library, data);
        }

        private void SearchDirectory(bool forward)
        {
            int index;
            if (treeLibDir.SelectedNode == null)
            {
                index = 0;
            }
            else
            {
                index = treeNodes.FindIndex(x => x == treeLibDir.SelectedNode);
            }

            seek:
            if (forward)
            {
                for (index++; index < treeNodes.Count; index++)
                {
                    if (treeNodes[index].Text.ToUpper().Contains(textBoxDirSearch.Text.ToUpper()))
                    {
                        break;
                    }
                }
            }
            else
            {
                for (index--; index >= 0; index--)
                {
                    if (treeNodes[index].Text.ToUpper().Contains(textBoxDirSearch.Text.ToUpper()))
                    {
                        break;
                    }
                }
            }

            if (index >= treeNodes.Count)
            {
                if (searchDirEnd)
                {
                    searchDirBeg = false;
                    searchDirEnd = false;
                    index = 0;
                    goto seek;
                }
                else
                {
                    MessageBox.Show("End of the tree.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    searchDirEnd = true;
                }
            }
            else if (index < 0)
            {
                if (searchDirBeg)
                {
                    searchDirBeg = false;
                    searchDirEnd = false;
                    index = treeNodes.Count - 1;
                    goto seek;
                }
                else
                {
                    MessageBox.Show("Beginning of the tree.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    searchDirBeg = true;
                }
            }
            else
            {
                treeLibDir.SelectedNode = treeNodes[index];
            }
        }

        private void NodesListUpdate(TreeNodeCollection nodes)
        {
            foreach (TreeNode subNode in nodes)
            {
                treeNodes.Add(subNode);
                NodesListUpdate(subNode.Nodes);
            }
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

        private void treeLibDir_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Bounds.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                treeLibDir.SelectedNode = null;
                treeLibDir_AfterSelect(null, null);
            }
        }

        private void listBoxLibItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxLibItems.SelectedIndex == -1)
            {
                DeselectItem();
                return;
            }

            Editor.Engine.InsertItem = null;
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
            Editor.Engine.Replacing = false;
            Editor.Engine.InsertItem = selectedItem;
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            Editor.Engine.Replacing = true;
            Editor.Engine.InsertItem = selectedItem;
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
                int index = listBoxLibItems.SelectedIndex;
                listBoxLibItems.Items[index] = ren.ItemName;
                (listBoxLibItems.Tag as Library.Exp).Items[index].Name = ren.ItemName;
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
                    if (Editor.Engine.IODataBase.Find(x => x.Item.IconPath == selectedItem.IconPath) != null)
                    {
                        if (MessageBox.Show("This item is used in the project. Remove it from the library?", 
                            "Item Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //remove the file from disk
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    //remove the item from library
                    var exp = (listBoxLibItems.Tag as Library.Exp);
                    exp.Directory.Items.Remove(selectedItem);

                    //remove the item from explorer
                    exp.Items.RemoveAt(listBoxLibItems.SelectedIndex);
                    listBoxLibItems.Items.Remove(listBoxLibItems.SelectedItem);
                    DeselectItem();

                    SaveLibrary();
                }
            }
            else if (lastCtrl == treeLibDir)
            {
                if (treeLibDir.SelectedNode == null)
                {
                    return;
                }

                text += "directory \"" + treeLibDir.SelectedNode.Text + "\" and its items?";

                if (MessageBox.Show(text, "Directory Delete",
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
                            if (Editor.Engine.IODataBase.Find(x => x.Item.IconPath == item) != null)
                            {
                                if (MessageBox.Show("This directory contains the items used in the project. " +
                                    "Continue deleting?", "Directory Delete", 
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
                    var exp = treeLibDir.SelectedNode.Tag as Library.Exp;
                    exp.Node.Remove();
                    exp.Parent.SubNodes.Remove(exp);

                    SaveLibrary();
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
                Height = config.ToolPosition.Library_Height;
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
        
        private void FormLibrary_Resize(object sender, EventArgs e)
        {
            listBoxLibItems.Height = ClientRectangle.Height - treeLibDir.Top - sHeight;
            treeLibDir.Height = listBoxLibItems.Height;
            groupBoxSearch.Top = listBoxLibItems.Bottom + sOffset;

            if (loaded)
            {
                config.ToolPosition.Library_Height = Height;
            }
        }

        private void buttonItemSearch_Click(object sender, EventArgs e)
        {
            if (textBoxItemSearch.Text == "")
            {
                return;
            }
            Explorer(treeLibDir.SelectedNode, !checkBoxIncSubDir.Checked);
            SelectFirstItem();
        }

        private void buttonItemSearchClear_Click(object sender, EventArgs e)
        {
            var node = treeLibDir.SelectedNode;
            var item = listBoxLibItems.Text;
            ExplorerUpdate();
            SelectNode(node);
            listBoxLibItems.SelectedItem = item;
        }

        private void textBoxItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonItemSearch_Click(null, null);
            }
        }

        private void buttonDirSearchFw_Click(object sender, EventArgs e)
        {
            SearchDirectory(true);
        }

        private void buttonDirSearchBack_Click(object sender, EventArgs e)
        {
            SearchDirectory(false);
        }

        private void buttonDirSearchBeg_Click(object sender, EventArgs e)
        {
            if (treeNodes.Count == 0)
            {
                return;
            }
            treeLibDir.SelectedNode = treeNodes.First();
        }

        private void buttonDirSearchEnd_Click(object sender, EventArgs e)
        {
            if (treeNodes.Count == 0)
            {
                return;
            }
            treeLibDir.SelectedNode = treeNodes.Last();
        }

        private void textBoxDirSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter)
            {
                SearchDirectory(false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                 SearchDirectory(true);
            }
        }

        private void textBoxDirSearch_TextChanged(object sender, EventArgs e)
        {
            searchDirBeg = false;
            searchDirEnd = false;
        }
    }
}
