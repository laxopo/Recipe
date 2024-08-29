
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonImport = new System.Windows.Forms.Button();
            this.listBoxLibItems = new System.Windows.Forms.ListBox();
            this.pictureBoxItemIcon = new System.Windows.Forms.PictureBox();
            this.buttonItemInsert = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.checkBoxOpa = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // treeLibDir
            // 
            this.treeLibDir.Location = new System.Drawing.Point(12, 25);
            this.treeLibDir.Name = "treeLibDir";
            this.treeLibDir.Size = new System.Drawing.Size(194, 212);
            this.treeLibDir.TabIndex = 0;
            this.treeLibDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeLibDir_AfterSelect);
            this.treeLibDir.Click += new System.EventHandler(this.treeLibDir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item explorer";
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(412, 214);
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
            this.listBoxLibItems.Size = new System.Drawing.Size(194, 212);
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
            this.labelPath.Location = new System.Drawing.Point(12, 240);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(29, 13);
            this.labelPath.TabIndex = 7;
            this.labelPath.Text = "Path";
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(412, 124);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(64, 23);
            this.buttonRename.TabIndex = 8;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(412, 153);
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
            this.checkBoxOpa.Checked = true;
            this.checkBoxOpa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpa.Location = new System.Drawing.Point(415, 5);
            this.checkBoxOpa.Name = "checkBoxOpa";
            this.checkBoxOpa.Size = new System.Drawing.Size(62, 17);
            this.checkBoxOpa.TabIndex = 10;
            this.checkBoxOpa.Text = "Opacity";
            this.checkBoxOpa.UseVisualStyleBackColor = true;
            // 
            // FormLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 258);
            this.Controls.Add(this.checkBoxOpa);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.buttonItemInsert);
            this.Controls.Add(this.pictureBoxItemIcon);
            this.Controls.Add(this.listBoxLibItems);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeLibDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(502, 1000);
            this.MinimumSize = new System.Drawing.Size(502, 297);
            this.Name = "FormLibrary";
            this.Opacity = 0.5D;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ListBox listBoxLibItems;
        private System.Windows.Forms.PictureBox pictureBoxItemIcon;
        private System.Windows.Forms.Button buttonItemInsert;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.CheckBox checkBoxOpa;
    }
}