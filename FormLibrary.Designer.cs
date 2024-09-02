
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
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkBoxIncSubDir = new System.Windows.Forms.CheckBox();
            this.buttonSearchClear = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // treeLibDir
            // 
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
            this.pictureBoxItemIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxItemIcon.Location = new System.Drawing.Point(412, 25);
            this.pictureBoxItemIcon.Name = "pictureBoxItemIcon";
            this.pictureBoxItemIcon.Size = new System.Drawing.Size(63, 63);
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
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(212, 258);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(194, 20);
            this.textBoxSearch.TabIndex = 11;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(412, 256);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(64, 23);
            this.buttonSearch.TabIndex = 12;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // checkBoxIncSubDir
            // 
            this.checkBoxIncSubDir.AutoSize = true;
            this.checkBoxIncSubDir.Location = new System.Drawing.Point(12, 260);
            this.checkBoxIncSubDir.Name = "checkBoxIncSubDir";
            this.checkBoxIncSubDir.Size = new System.Drawing.Size(131, 17);
            this.checkBoxIncSubDir.TabIndex = 13;
            this.checkBoxIncSubDir.Text = "Include Subdirectories";
            this.checkBoxIncSubDir.UseVisualStyleBackColor = true;
            // 
            // buttonSearchClear
            // 
            this.buttonSearchClear.Location = new System.Drawing.Point(183, 256);
            this.buttonSearchClear.Name = "buttonSearchClear";
            this.buttonSearchClear.Size = new System.Drawing.Size(23, 23);
            this.buttonSearchClear.TabIndex = 14;
            this.buttonSearchClear.Text = "X";
            this.buttonSearchClear.UseVisualStyleBackColor = true;
            this.buttonSearchClear.Click += new System.EventHandler(this.buttonSearchClear_Click);
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
            // FormLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 289);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonSearchClear);
            this.Controls.Add(this.checkBoxIncSubDir);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
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
            this.MinimumSize = new System.Drawing.Size(502, 328);
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
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.CheckBox checkBoxIncSubDir;
        private System.Windows.Forms.Button buttonSearchClear;
        private System.Windows.Forms.Button buttonReplace;
    }
}