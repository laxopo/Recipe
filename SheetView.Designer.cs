
namespace Recipe
{
    partial class SheetView
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
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonOpenImg = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxLayout = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxBL = new System.Windows.Forms.CheckBox();
            this.checkBoxBI = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(6, 19);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(99, 13);
            this.labelPath.TabIndex = 0;
            this.labelPath.Text = "(open an image file)";
            // 
            // buttonOpenImg
            // 
            this.buttonOpenImg.Location = new System.Drawing.Point(6, 35);
            this.buttonOpenImg.Name = "buttonOpenImg";
            this.buttonOpenImg.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenImg.TabIndex = 1;
            this.buttonOpenImg.Text = "Open";
            this.buttonOpenImg.UseVisualStyleBackColor = true;
            this.buttonOpenImg.Click += new System.EventHandler(this.buttonOpenImg_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(87, 35);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(351, 98);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Picture files|*.png; *.bmp; *.gif; *.jpeg; *.jpg";
            // 
            // comboBoxLayout
            // 
            this.comboBoxLayout.Location = new System.Drawing.Point(285, 37);
            this.comboBoxLayout.Name = "comboBoxLayout";
            this.comboBoxLayout.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLayout.TabIndex = 4;
            this.comboBoxLayout.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayout_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Image size mode:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonOpenImg);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelPath);
            this.groupBox1.Controls.Add(this.comboBoxLayout);
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxBL);
            this.groupBox2.Controls.Add(this.checkBoxBI);
            this.groupBox2.Location = new System.Drawing.Point(12, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 41);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Borders";
            // 
            // checkBoxBL
            // 
            this.checkBoxBL.AutoSize = true;
            this.checkBoxBL.Location = new System.Drawing.Point(87, 18);
            this.checkBoxBL.Name = "checkBoxBL";
            this.checkBoxBL.Size = new System.Drawing.Size(52, 17);
            this.checkBoxBL.TabIndex = 1;
            this.checkBoxBL.Text = "Label";
            this.checkBoxBL.UseVisualStyleBackColor = true;
            this.checkBoxBL.CheckedChanged += new System.EventHandler(this.checkBoxBL_CheckedChanged);
            // 
            // checkBoxBI
            // 
            this.checkBoxBI.AutoSize = true;
            this.checkBoxBI.Location = new System.Drawing.Point(9, 18);
            this.checkBoxBI.Name = "checkBoxBI";
            this.checkBoxBI.Size = new System.Drawing.Size(47, 17);
            this.checkBoxBI.TabIndex = 0;
            this.checkBoxBI.Text = "Icon";
            this.checkBoxBI.UseVisualStyleBackColor = true;
            this.checkBoxBI.CheckedChanged += new System.EventHandler(this.checkBoxBI_CheckedChanged);
            // 
            // SheetView
            // 
            this.AcceptButton = this.buttonApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 132);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SheetView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sheet Appearance";
            this.Load += new System.EventHandler(this.SheetView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonOpenImg;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox comboBoxLayout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxBL;
        private System.Windows.Forms.CheckBox checkBoxBI;
    }
}