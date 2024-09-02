
namespace Recipe
{
    partial class FormLibrary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeLibDir = new System.Windows.Forms.TreeView();
            this.buttonImport = new System.Windows.Forms.Button();
            this.listBoxLibItems = new System.Windows.Forms.ListBox();
            this.pictureBoxItemIcon = new System.Windows.Forms.PictureBox();
            this.buttonItemInsert = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.checkBoxOpa = new System.Windows.Forms.CheckBox();
            this.textBoxItemSearch = new System.Windows.Forms.TextBox();
            this.buttonItemSearch = new System.Windows.Forms.Button();
            this.checkBoxIncSubDir = new System.Windows.Forms.CheckBox();
            this.buttonItemSearchClear = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDirSearchHome = new System.Windows.Forms.Button();
            this.buttonDirSearchFw = new System.Windows.Forms.Button();
            this.buttonDirSearchBack = new System.Windows.Forms.Button();
            this.textBoxDirSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).BeginInit();
            this.groupBoxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeLibDir
            // 
            this.treeLibDir.HideSelection = false;
            this.treeLibDir.Location = new System.Drawing.Point(12, 25);
            this.treeLibDir.Name = "treeLibDir";
            this.treeLibDir.Size = new System.Drawing.Size(194, 225);
            this.treeLibDir.TabIndex = 0;
            this.treeLibDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeLibDir_AfterSelect);
            this.treeLibDir.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeLibDir_NodeMouseClick);
            this.treeLibDir.Click += new System.EventHandler(this.treeLibDir_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(412, 227);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(64, 23);
            this.buttonImport.TabIndex = 2;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // listBoxLibItems
            // 
            this.listBoxLibItems.FormattingEnabled = true;
            this.listBoxLibItems.Location = new System.Drawing.Point(212, 25);
            this.listBoxLibItems.Name = "listBoxLibItems";
            this.listBoxLibItems.Size = new System.Drawing.Size(194, 225);
            this.listBoxLibItems.TabIndex = 3;
            this.listBoxLibItems.Click += new System.EventHandler(this.listBoxLibItems_Click);
            this.listBoxLibItems.SelectedIndexChanged += new System.EventHandler(this.listBoxLibItems_SelectedIndexChanged);
            this.listBoxLibItems.DoubleClick += new System.EventHandler(this.listBoxLibItems_DoubleClick);
            // 
            // pictureBoxItemIcon
            // 
            this.pictureBoxItemIcon.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxItemIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxItemIcon.Location = new System.Drawing.Point(412, 25);
            this.pictureBoxItemIcon.Name = "pictureBoxItemIcon";
            this.pictureBoxItemIcon.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxItemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxItemIcon.TabIndex = 4;
            this.pictureBoxItemIcon.TabStop = false;
            // 
            // buttonItemInsert
            // 
            this.buttonItemInsert.Location = new System.Drawing.Point(412, 95);
            this.buttonItemInsert.Name = "buttonItemInsert";
            this.buttonItemInsert.Size = new System.Drawing.Size(64, 23);
            this.buttonItemInsert.TabIndex = 5;
            this.buttonItemInsert.Text = "Insert";
            this.buttonItemInsert.UseVisualStyleBackColor = true;
            this.buttonItemInsert.Click += new System.EventHandler(this.buttonItemInsert_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 9);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(29, 13);
            this.labelPath.TabIndex = 7;
            this.labelPath.Text = "Path";
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(412, 169);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(64, 23);
            this.buttonRename.TabIndex = 8;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(412, 198);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(64, 23);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // checkBoxOpa
            // 
            this.checkBoxOpa.AutoSize = true;
            this.checkBoxOpa.Location = new System.Drawing.Point(415, 5);
            this.checkBoxOpa.Name = "checkBoxOpa";
            this.checkBoxOpa.Size = new System.Drawing.Size(62, 17);
            this.checkBoxOpa.TabIndex = 10;
            this.checkBoxOpa.Text = "Opacity";
            this.checkBoxOpa.UseVisualStyleBackColor = true;
            // 
            // textBoxItemSearch
            // 
            this.textBoxItemSearch.Location = new System.Drawing.Point(231, 17);
            this.textBoxItemSearch.Name = "textBoxItemSearch";
            this.textBoxItemSearch.Size = new System.Drawing.Size(163, 20);
            this.textBoxItemSearch.TabIndex = 11;
            this.textBoxItemSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemSearch_KeyDown);
            // 
            // buttonItemSearch
            // 
            this.buttonItemSearch.Location = new System.Drawing.Point(402, 15);
            this.buttonItemSearch.Name = "buttonItemSearch";
            this.buttonItemSearch.Size = new System.Drawing.Size(64, 23);
            this.buttonItemSearch.TabIndex = 12;
            this.buttonItemSearch.Text = "Search";
            this.buttonItemSearch.UseVisualStyleBackColor = true;
            this.buttonItemSearch.Click += new System.EventHandler(this.buttonItemSearch_Click);
            // 
            // checkBoxIncSubDir
            // 
            this.checkBoxIncSubDir.AutoSize = true;
            this.checkBoxIncSubDir.Location = new System.Drawing.Point(65, 19);
            this.checkBoxIncSubDir.Name = "checkBoxIncSubDir";
            this.checkBoxIncSubDir.Size = new System.Drawing.Size(131, 17);
            this.checkBoxIncSubDir.TabIndex = 13;
            this.checkBoxIncSubDir.Text = "Include Subdirectories";
            this.checkBoxIncSubDir.UseVisualStyleBackColor = true;
            // 
            // buttonItemSearchClear
            // 
            this.buttonItemSearchClear.Location = new System.Drawing.Point(202, 15);
            this.buttonItemSearchClear.Name = "buttonItemSearchClear";
            this.buttonItemSearchClear.Size = new System.Drawing.Size(23, 23);
            this.buttonItemSearchClear.TabIndex = 14;
            this.buttonItemSearchClear.Text = "X";
            this.buttonItemSearchClear.UseVisualStyleBackColor = true;
            this.buttonItemSearchClear.Click += new System.EventHandler(this.buttonItemSearchClear_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(412, 124);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(64, 23);
            this.buttonReplace.TabIndex = 15;
            this.buttonReplace.Text = "Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.label2);
            this.groupBoxSearch.Controls.Add(this.label1);
            this.groupBoxSearch.Controls.Add(this.buttonDirSearchHome);
            this.groupBoxSearch.Controls.Add(this.buttonDirSearchFw);
            this.groupBoxSearch.Controls.Add(this.buttonDirSearchBack);
            this.groupBoxSearch.Controls.Add(this.textBoxDirSearch);
            this.groupBoxSearch.Controls.Add(this.buttonItemSearchClear);
            this.groupBoxSearch.Controls.Add(this.checkBoxIncSubDir);
            this.groupBoxSearch.Controls.Add(this.buttonItemSearch);
            this.groupBoxSearch.Controls.Add(this.textBoxItemSearch);
            this.groupBoxSearch.Location = new System.Drawing.Point(10, 258);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(470, 70);
            this.groupBoxSearch.TabIndex = 16;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Dirs:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Items:";
            // 
            // buttonDirSearchHome
            // 
            this.buttonDirSearchHome.Location = new System.Drawing.Point(202, 41);
            this.buttonDirSearchHome.Name = "buttonDirSearchHome";
            this.buttonDirSearchHome.Size = new System.Drawing.Size(23, 23);
            this.buttonDirSearchHome.TabIndex = 18;
            this.buttonDirSearchHome.Text = "↑";
            this.buttonDirSearchHome.UseVisualStyleBackColor = true;
            this.buttonDirSearchHome.Click += new System.EventHandler(this.buttonDirSearchHome_Click);
            // 
            // buttonDirSearchFw
            // 
            this.buttonDirSearchFw.Location = new System.Drawing.Point(437, 41);
            this.buttonDirSearchFw.Name = "buttonDirSearchFw";
            this.buttonDirSearchFw.Size = new System.Drawing.Size(29, 23);
            this.buttonDirSearchFw.TabIndex = 17;
            this.buttonDirSearchFw.Text = ">";
            this.buttonDirSearchFw.UseVisualStyleBackColor = true;
            this.buttonDirSearchFw.Click += new System.EventHandler(this.buttonDirSearchFw_Click);
            // 
            // buttonDirSearchBack
            // 
            this.buttonDirSearchBack.Location = new System.Drawing.Point(402, 41);
            this.buttonDirSearchBack.Name = "buttonDirSearchBack";
            this.buttonDirSearchBack.Size = new System.Drawing.Size(29, 23);
            this.buttonDirSearchBack.TabIndex = 16;
            this.buttonDirSearchBack.Text = "<";
            this.buttonDirSearchBack.UseVisualStyleBackColor = true;
            this.buttonDirSearchBack.Click += new System.EventHandler(this.buttonDirSearchBack_Click);
            // 
            // textBoxDirSearch
            // 
            this.textBoxDirSearch.Location = new System.Drawing.Point(231, 43);
            this.textBoxDirSearch.Name = "textBoxDirSearch";
            this.textBoxDirSearch.Size = new System.Drawing.Size(163, 20);
            this.textBoxDirSearch.TabIndex = 15;
            // 
            // FormLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 340);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.checkBoxOpa);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.buttonItemInsert);
            this.Controls.Add(this.pictureBoxItemIcon);
            this.Controls.Add(this.listBoxLibItems);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.treeLibDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(502, 10000);
            this.MinimumSize = new System.Drawing.Size(502, 379);
            this.Name = "FormLibrary";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Library";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLibrary_FormClosing);
            this.Load += new System.EventHandler(this.FormLibrary_Load);
            this.MouseEnter += new System.EventHandler(this.FormLibrary_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.FormLibrary_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLibrary_MouseMove);
            this.Move += new System.EventHandler(this.FormLibrary_Move);
            this.Resize += new System.EventHandler(this.FormLibrary_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).EndInit();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeLibDir;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ListBox listBoxLibItems;
        private System.Windows.Forms.PictureBox pictureBoxItemIcon;
        private System.Windows.Forms.Button buttonItemInsert;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.CheckBox checkBoxOpa;
        private System.Windows.Forms.TextBox textBoxItemSearch;
        private System.Windows.Forms.Button buttonItemSearch;
        private System.Windows.Forms.CheckBox checkBoxIncSubDir;
        private System.Windows.Forms.Button buttonItemSearchClear;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox textBoxDirSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDirSearchHome;
        private System.Windows.Forms.Button buttonDirSearchFw;
        private System.Windows.Forms.Button buttonDirSearchBack;
    }
}