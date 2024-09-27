using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Recipe
{
    public partial class ImportProgress : Form
    {
        public ImportProgress(Library.Directory library, TreeNode root, bool normNames, bool topDirectory)
        {
            InitializeComponent();

            this.library = library;
            this.root = root;
            this.normNames = normNames;
            this.topDirectory = topDirectory;
            caption = Text;
        }

        private CancellationTokenSource cts = new CancellationTokenSource();

        private string caption;
        private Library.Directory library;
        private TreeNode root;
        private bool normNames;
        private bool topDirectory;
        private int itemAdd = 0;
        private int itemSkip = 0;
        private int amount = 0;
        private int processed = 0;
        private string currentFile = "";

        private int ItemAdd
        {
            get
            {
                return itemAdd;
            }
            set
            {
                itemAdd = value;
                labelImport.Invoke(new MethodInvoker(delegate () {
                    labelImport.Text = value.ToString();
                }));
            }
        }

        private int ItemSkip
        {
            get
            {
                return itemSkip;
            }
            set
            {
                itemSkip = value;
                labelSkip.Invoke(new MethodInvoker(delegate () {
                     labelSkip.Text = value.ToString();
                }));
            }
        }

        private int Progress
        {
            get
            {
                return processed;
            }
            set
            {
                processed = value;
                labelProc.Invoke(new MethodInvoker(delegate () {
                    labelProc.Text = processed + " / " + amount;
                }));

                int proc = Convert.ToInt32((double)processed / amount * 100);

                Invoke(new MethodInvoker(delegate () {
                    Text = caption + proc + "%";
                }));

                progressBar1.Invoke(new MethodInvoker(delegate () {
                    progressBar1.Value = proc;
                }));
            }
        }

        private async Task Import()
        {
            await ImportAsync();

            Sorting(library);

            MessageBox.Show("Imported " + itemAdd + " items, skiped " + itemSkip + ".", "Import Completed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task ImportAsync()
        {
            await Task.Run(() =>
            {
                LibraryImport(root, cts.Token);
            });
        }

        private void Sorting(Library.Directory dir)
        {
            dir.Items.Sort((x, y) => x.Name.CompareTo(y.Name));
            dir.Directories.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (var subDir in dir.Directories)
            {
                Sorting(subDir);
            }
        }

        private void LibraryImport(TreeNode currentNode, CancellationToken token)
        {
            //Get files
            var path = Path.Combine(Routine.Directories.Root, currentNode.FullPath);
            var files = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);

            Library.Directory dir;
            dir = library.GetDirectory(currentNode.FullPath);

            foreach (var file in files)
            {
                AbortRequestCheck(token);

                Progress++;

                var iconPath = Library.Directory.TrimPath(file);

                labelFile.Invoke(new MethodInvoker(delegate () {
                    currentFile = iconPath;
                }));

                //already exists in library
                if (dir != null && dir.Items.Find(x => x.IconPath == iconPath) != null)
                {
                    ItemSkip++;
                    continue;
                }

                //image size isn't quadratic
                Image img = Image.FromFile(file);
                int w = img.Width;
                int h = img.Height;
                double k = (double)w / h;
                img.Dispose();
                if (k != 1 || w > 128)
                {
                    ItemSkip++;
                    File.Delete(file);
                    continue;
                }

                //Import an item
                if (dir == null)
                {
                    library.CreateDirectory(currentNode.FullPath);
                    dir = library.GetDirectory(currentNode.FullPath);
                }

                var name = Path.GetFileNameWithoutExtension(iconPath);

                if (normNames)
                {
                    name = NormalizeName(name);
                }

                dir.Items.Add(new Library.Item()
                {
                    IconPath = iconPath,
                    Name = name,
                    Type = Library.Item.ItemType.Default
                });

                ItemAdd++;
            }

            AbortRequestCheck(token);

            //Get folders
            if (!topDirectory)
            {
                foreach (TreeNode node in currentNode.Nodes)
                {
                    LibraryImport(node, token);
                }
            }

            //Delete empty folder
            int entries = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly).Length;
            entries += Directory.GetDirectories(path).Length;
            if (entries == 0)
            {
                Directory.Delete(path, true);
            }
        }

        private static string NormalizeName(string name)
        {
            string itemName = name;
            int wbeg = 0;
            int wend = 0;
            var words = new List<string>();
            while (wend != -1)
            {
                wend = itemName.IndexOf('_', wbeg);
                if (wend != -1)
                {
                    words.Add(itemName.Substring(wbeg, wend - wbeg));
                    wbeg = wend + 1;
                }
                else
                {
                    words.Add(itemName.Substring(wbeg, itemName.Length - wbeg));
                }
            }

            itemName = "";
            int wrd = 0;
            foreach (string word in words)
            {
                string up = "";
                if (wrd > 0)
                {
                    up = " ";
                }
                wrd++;
                up += word[0].ToString().ToUpper() + word.Substring(1, word.Length - 1);
                itemName += up;
            }

            return itemName;
        }

        private void AbortRequestCheck(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                
                Invoke(new MethodInvoker(delegate () {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }));
               
                token.ThrowIfCancellationRequested();
            }
        }

        /**/

        private void ImportProgress_Load(object sender, EventArgs e)
        {
            itemAdd = 0;
            itemSkip = 0;
            processed = 0;
            SearchOption searchOption = SearchOption.AllDirectories;
            if (topDirectory)
            {
                searchOption = SearchOption.TopDirectoryOnly;
            }

            amount = Directory.GetFiles(Path.Combine(Routine.Directories.Root, root.FullPath), 
                "*.png", searchOption).Length;

            Task task = Import();
            timerCurFileShow.Start();
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void timerCurFileShow_Tick(object sender, EventArgs e)
        {
            labelFile.Text = currentFile;
        }
    }
}
