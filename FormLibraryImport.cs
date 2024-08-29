using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Recipe
{
    public partial class FormLibraryImport : Form
    {
        public FormLibraryImport(Library.Directory library, Action viewUpdate)
        {
            InitializeComponent();

            this.library = library;
            buttonUpdate_Click(null, null);
            formCaption = Text;
            ViewUpdate = viewUpdate;
        }

        /**/

        private Library.Directory library;
        private string formCaption;
        private Action ViewUpdate;

        private void TreeUpdate(TreeNode root)
        {
            string path;
            if (root.Text == Path.GetFileName(Routine.Directories.Library))
            {
                path = Routine.Directories.Library;
            }
            else
            {
                path = Path.Combine(Routine.Directories.Root, root.FullPath);
            }

            var folders = Directory.GetDirectories(path);

            foreach (var folder in folders)
            {
                root.Nodes.Add(Path.GetFileName(folder));
            }


            foreach (TreeNode node in root.Nodes)
            {
                TreeUpdate(node);
            }
        }

        private void Import()
        {
            var tempLib = library.Clone() as Library.Directory;
            DialogResult = new ImportProgress(tempLib, treeView1.SelectedNode, checkBoxNormName.Checked).ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                library = tempLib;
                ViewUpdate();
            }
        }

        /**/

        private void buttonLibraryFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Routine.Directories.Library);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(Path.GetFileName(Routine.Directories.Library));
            TreeUpdate(treeView1.Nodes[0]);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            var caption = (sender as Button).Text;
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("First, select a directory in the tree.", caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                return;
            }

            Import();
        }
    }
}
