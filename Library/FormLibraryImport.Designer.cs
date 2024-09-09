
namespace Recipe
{
    partial class FormLibraryImport
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
            this.buttonLibraryFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.checkBoxNormName = new System.Windows.Forms.CheckBox();
            this.checkBoxTopDir = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonLibraryFolder
            // 
            this.buttonLibraryFolder.Location = new System.Drawing.Point(12, 43);
            this.buttonLibraryFolder.Name = "buttonLibraryFolder";
            this.buttonLibraryFolder.Size = new System.Drawing.Size(114, 23);
            this.buttonLibraryFolder.TabIndex = 0;
            this.buttonLibraryFolder.Text = "Open Library Folder";
            this.buttonLibraryFolder.UseVisualStyleBackColor = true;
            this.buttonLibraryFolder.Click += new System.EventHandler(this.buttonLibraryFolder_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Note: copy a folder with icon to library folder. After, update the tree and choos" +
    "e the folder in the tree and import it.";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(132, 43);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(141, 163);
            this.treeView1.TabIndex = 2;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(12, 72);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(114, 23);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Update Tree";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(12, 183);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(114, 23);
            this.buttonImport.TabIndex = 4;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // checkBoxNormName
            // 
            this.checkBoxNormName.AutoSize = true;
            this.checkBoxNormName.Checked = true;
            this.checkBoxNormName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormName.Location = new System.Drawing.Point(12, 160);
            this.checkBoxNormName.Name = "checkBoxNormName";
            this.checkBoxNormName.Size = new System.Drawing.Size(108, 17);
            this.checkBoxNormName.TabIndex = 5;
            this.checkBoxNormName.Text = "Normalize Names";
            this.checkBoxNormName.UseVisualStyleBackColor = true;
            // 
            // checkBoxTopDir
            // 
            this.checkBoxTopDir.AutoSize = true;
            this.checkBoxTopDir.Location = new System.Drawing.Point(12, 137);
            this.checkBoxTopDir.Name = "checkBoxTopDir";
            this.checkBoxTopDir.Size = new System.Drawing.Size(101, 17);
            this.checkBoxTopDir.TabIndex = 6;
            this.checkBoxTopDir.Text = "Top Folder Only";
            this.checkBoxTopDir.UseVisualStyleBackColor = true;
            // 
            // FormLibraryImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 218);
            this.Controls.Add(this.checkBoxTopDir);
            this.Controls.Add(this.checkBoxNormName);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLibraryFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormLibraryImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import New Items";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLibraryFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.CheckBox checkBoxNormName;
        private System.Windows.Forms.CheckBox checkBoxTopDir;
    }
}