
namespace Recipe
{
    partial class PropEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.listBoxLinksInput = new System.Windows.Forms.ListBox();
            this.listBoxLinksOutput = new System.Windows.Forms.ListBox();
            this.groupBoxLinks = new System.Windows.Forms.GroupBox();
            this.buttonLinksDeselectAll = new System.Windows.Forms.Button();
            this.buttonLinksSelectAll = new System.Windows.Forms.Button();
            this.buttonDeleteLinks = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxOpa = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTypes = new System.Windows.Forms.ComboBox();
            this.groupBoxLinks.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(56, 6);
            this.textBoxName.MaxLength = 30;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(127, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyDown);
            // 
            // listBoxLinksInput
            // 
            this.listBoxLinksInput.FormattingEnabled = true;
            this.listBoxLinksInput.Location = new System.Drawing.Point(6, 32);
            this.listBoxLinksInput.Name = "listBoxLinksInput";
            this.listBoxLinksInput.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxLinksInput.Size = new System.Drawing.Size(120, 95);
            this.listBoxLinksInput.TabIndex = 3;
            this.listBoxLinksInput.SelectedIndexChanged += new System.EventHandler(this.listBoxLinksInput_SelectedIndexChanged);
            this.listBoxLinksInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxLinksInput_KeyDown);
            // 
            // listBoxLinksOutput
            // 
            this.listBoxLinksOutput.FormattingEnabled = true;
            this.listBoxLinksOutput.Location = new System.Drawing.Point(132, 32);
            this.listBoxLinksOutput.Name = "listBoxLinksOutput";
            this.listBoxLinksOutput.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxLinksOutput.Size = new System.Drawing.Size(120, 95);
            this.listBoxLinksOutput.TabIndex = 4;
            this.listBoxLinksOutput.SelectedIndexChanged += new System.EventHandler(this.listBoxLinksOutput_SelectedIndexChanged);
            this.listBoxLinksOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxLinksOutput_KeyDown);
            // 
            // groupBoxLinks
            // 
            this.groupBoxLinks.Controls.Add(this.buttonLinksDeselectAll);
            this.groupBoxLinks.Controls.Add(this.buttonLinksSelectAll);
            this.groupBoxLinks.Controls.Add(this.buttonDeleteLinks);
            this.groupBoxLinks.Controls.Add(this.label3);
            this.groupBoxLinks.Controls.Add(this.label2);
            this.groupBoxLinks.Controls.Add(this.listBoxLinksInput);
            this.groupBoxLinks.Controls.Add(this.listBoxLinksOutput);
            this.groupBoxLinks.Location = new System.Drawing.Point(12, 59);
            this.groupBoxLinks.Name = "groupBoxLinks";
            this.groupBoxLinks.Size = new System.Drawing.Size(261, 164);
            this.groupBoxLinks.TabIndex = 5;
            this.groupBoxLinks.TabStop = false;
            this.groupBoxLinks.Text = "Links";
            // 
            // buttonLinksDeselectAll
            // 
            this.buttonLinksDeselectAll.Location = new System.Drawing.Point(177, 133);
            this.buttonLinksDeselectAll.Name = "buttonLinksDeselectAll";
            this.buttonLinksDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonLinksDeselectAll.TabIndex = 9;
            this.buttonLinksDeselectAll.Text = "Deselect All";
            this.buttonLinksDeselectAll.UseVisualStyleBackColor = true;
            this.buttonLinksDeselectAll.Click += new System.EventHandler(this.buttonLinksDeselectAll_Click);
            // 
            // buttonLinksSelectAll
            // 
            this.buttonLinksSelectAll.Location = new System.Drawing.Point(92, 133);
            this.buttonLinksSelectAll.Name = "buttonLinksSelectAll";
            this.buttonLinksSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonLinksSelectAll.TabIndex = 8;
            this.buttonLinksSelectAll.Text = "Select All";
            this.buttonLinksSelectAll.UseVisualStyleBackColor = true;
            this.buttonLinksSelectAll.Click += new System.EventHandler(this.buttonLinksSelectAll_Click);
            // 
            // buttonDeleteLinks
            // 
            this.buttonDeleteLinks.Location = new System.Drawing.Point(6, 133);
            this.buttonDeleteLinks.Name = "buttonDeleteLinks";
            this.buttonDeleteLinks.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteLinks.TabIndex = 6;
            this.buttonDeleteLinks.Text = "Delete";
            this.buttonDeleteLinks.UseVisualStyleBackColor = true;
            this.buttonDeleteLinks.Click += new System.EventHandler(this.buttonDeleteLinks_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input:";
            // 
            // checkBoxOpa
            // 
            this.checkBoxOpa.AutoSize = true;
            this.checkBoxOpa.Checked = true;
            this.checkBoxOpa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpa.Location = new System.Drawing.Point(213, 8);
            this.checkBoxOpa.Name = "checkBoxOpa";
            this.checkBoxOpa.Size = new System.Drawing.Size(62, 17);
            this.checkBoxOpa.TabIndex = 6;
            this.checkBoxOpa.Text = "Opacity";
            this.checkBoxOpa.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Type:";
            // 
            // comboBoxTypes
            // 
            this.comboBoxTypes.FormattingEnabled = true;
            this.comboBoxTypes.Items.AddRange(new object[] {
            "Default",
            "Mechanism",
            "Block",
            "Fluid"});
            this.comboBoxTypes.Location = new System.Drawing.Point(56, 32);
            this.comboBoxTypes.Name = "comboBoxTypes";
            this.comboBoxTypes.Size = new System.Drawing.Size(127, 21);
            this.comboBoxTypes.TabIndex = 8;
            this.comboBoxTypes.Text = "Default";
            this.comboBoxTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypes_SelectedIndexChanged);
            // 
            // PropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 232);
            this.Controls.Add(this.comboBoxTypes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxOpa);
            this.Controls.Add(this.groupBoxLinks);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PropEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Properties Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropEditor_FormClosing);
            this.Load += new System.EventHandler(this.PropEditor_Load);
            this.Move += new System.EventHandler(this.PropEditor_Move);
            this.groupBoxLinks.ResumeLayout(false);
            this.groupBoxLinks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ListBox listBoxLinksInput;
        private System.Windows.Forms.ListBox listBoxLinksOutput;
        private System.Windows.Forms.GroupBox groupBoxLinks;
        private System.Windows.Forms.Button buttonDeleteLinks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxOpa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxTypes;
        private System.Windows.Forms.Button buttonLinksDeselectAll;
        private System.Windows.Forms.Button buttonLinksSelectAll;
    }
}