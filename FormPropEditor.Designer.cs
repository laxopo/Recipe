
namespace Recipe
{
    partial class FormPropEditor
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
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxQtyIn = new System.Windows.Forms.TextBox();
            this.textBoxQtyOut = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxQuantity = new System.Windows.Forms.GroupBox();
            this.textBoxInjected = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxIO = new System.Windows.Forms.GroupBox();
            this.checkBoxPublic = new System.Windows.Forms.CheckBox();
            this.radioButtonOutput = new System.Windows.Forms.RadioButton();
            this.radioButtonInput = new System.Windows.Forms.RadioButton();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.groupBoxLinks.SuspendLayout();
            this.groupBoxQuantity.SuspendLayout();
            this.groupBoxIO.SuspendLayout();
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
            this.textBoxName.Leave += new System.EventHandler(this.textBoxName_Leave);
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
            this.groupBoxLinks.Location = new System.Drawing.Point(12, 168);
            this.groupBoxLinks.Name = "groupBoxLinks";
            this.groupBoxLinks.Size = new System.Drawing.Size(262, 164);
            this.groupBoxLinks.TabIndex = 5;
            this.groupBoxLinks.TabStop = false;
            this.groupBoxLinks.Text = "Links";
            // 
            // buttonLinksDeselectAll
            // 
            this.buttonLinksDeselectAll.Location = new System.Drawing.Point(92, 133);
            this.buttonLinksDeselectAll.Name = "buttonLinksDeselectAll";
            this.buttonLinksDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonLinksDeselectAll.TabIndex = 9;
            this.buttonLinksDeselectAll.Text = "Deselect All";
            this.buttonLinksDeselectAll.UseVisualStyleBackColor = true;
            this.buttonLinksDeselectAll.Click += new System.EventHandler(this.buttonLinksDeselectAll_Click);
            // 
            // buttonLinksSelectAll
            // 
            this.buttonLinksSelectAll.Location = new System.Drawing.Point(6, 133);
            this.buttonLinksSelectAll.Name = "buttonLinksSelectAll";
            this.buttonLinksSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonLinksSelectAll.TabIndex = 8;
            this.buttonLinksSelectAll.Text = "Select All";
            this.buttonLinksSelectAll.UseVisualStyleBackColor = true;
            this.buttonLinksSelectAll.Click += new System.EventHandler(this.buttonLinksSelectAll_Click);
            // 
            // buttonDeleteLinks
            // 
            this.buttonDeleteLinks.Location = new System.Drawing.Point(177, 133);
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
            this.comboBoxTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypes_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "In:";
            // 
            // textBoxQtyIn
            // 
            this.textBoxQtyIn.Location = new System.Drawing.Point(60, 19);
            this.textBoxQtyIn.MaxLength = 9;
            this.textBoxQtyIn.Name = "textBoxQtyIn";
            this.textBoxQtyIn.Size = new System.Drawing.Size(60, 20);
            this.textBoxQtyIn.TabIndex = 10;
            this.textBoxQtyIn.TextChanged += new System.EventHandler(this.textBoxQtyIn_TextChanged);
            this.textBoxQtyIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxQtyIn_KeyPress);
            // 
            // textBoxQtyOut
            // 
            this.textBoxQtyOut.Location = new System.Drawing.Point(60, 45);
            this.textBoxQtyOut.MaxLength = 9;
            this.textBoxQtyOut.Name = "textBoxQtyOut";
            this.textBoxQtyOut.Size = new System.Drawing.Size(60, 20);
            this.textBoxQtyOut.TabIndex = 11;
            this.textBoxQtyOut.TextChanged += new System.EventHandler(this.textBoxQtyOut_TextChanged);
            this.textBoxQtyOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxQtyOut_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Out:";
            // 
            // groupBoxQuantity
            // 
            this.groupBoxQuantity.Controls.Add(this.textBoxInjected);
            this.groupBoxQuantity.Controls.Add(this.label7);
            this.groupBoxQuantity.Controls.Add(this.label5);
            this.groupBoxQuantity.Controls.Add(this.textBoxQtyOut);
            this.groupBoxQuantity.Controls.Add(this.label6);
            this.groupBoxQuantity.Controls.Add(this.textBoxQtyIn);
            this.groupBoxQuantity.Enabled = false;
            this.groupBoxQuantity.Location = new System.Drawing.Point(12, 59);
            this.groupBoxQuantity.Name = "groupBoxQuantity";
            this.groupBoxQuantity.Size = new System.Drawing.Size(126, 103);
            this.groupBoxQuantity.TabIndex = 13;
            this.groupBoxQuantity.TabStop = false;
            this.groupBoxQuantity.Text = "Quantity";
            // 
            // textBoxInjected
            // 
            this.textBoxInjected.Location = new System.Drawing.Point(60, 71);
            this.textBoxInjected.MaxLength = 9;
            this.textBoxInjected.Name = "textBoxInjected";
            this.textBoxInjected.Size = new System.Drawing.Size(60, 20);
            this.textBoxInjected.TabIndex = 14;
            this.textBoxInjected.TextChanged += new System.EventHandler(this.textBoxInjected_TextChanged);
            this.textBoxInjected.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInjected_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Injected:";
            // 
            // groupBoxIO
            // 
            this.groupBoxIO.Controls.Add(this.checkBoxPublic);
            this.groupBoxIO.Controls.Add(this.radioButtonOutput);
            this.groupBoxIO.Controls.Add(this.radioButtonInput);
            this.groupBoxIO.Controls.Add(this.radioButtonAuto);
            this.groupBoxIO.Enabled = false;
            this.groupBoxIO.Location = new System.Drawing.Point(144, 59);
            this.groupBoxIO.Name = "groupBoxIO";
            this.groupBoxIO.Size = new System.Drawing.Size(130, 103);
            this.groupBoxIO.TabIndex = 14;
            this.groupBoxIO.TabStop = false;
            this.groupBoxIO.Text = "I/O";
            // 
            // checkBoxPublic
            // 
            this.checkBoxPublic.AutoSize = true;
            this.checkBoxPublic.Enabled = false;
            this.checkBoxPublic.Location = new System.Drawing.Point(72, 20);
            this.checkBoxPublic.Name = "checkBoxPublic";
            this.checkBoxPublic.Size = new System.Drawing.Size(55, 17);
            this.checkBoxPublic.TabIndex = 17;
            this.checkBoxPublic.Text = "Public";
            this.checkBoxPublic.UseVisualStyleBackColor = true;
            this.checkBoxPublic.CheckedChanged += new System.EventHandler(this.checkBoxPublic_CheckedChanged);
            // 
            // radioButtonOutput
            // 
            this.radioButtonOutput.AutoSize = true;
            this.radioButtonOutput.Location = new System.Drawing.Point(9, 20);
            this.radioButtonOutput.Name = "radioButtonOutput";
            this.radioButtonOutput.Size = new System.Drawing.Size(57, 17);
            this.radioButtonOutput.TabIndex = 2;
            this.radioButtonOutput.Text = "Output";
            this.radioButtonOutput.UseVisualStyleBackColor = true;
            this.radioButtonOutput.CheckedChanged += new System.EventHandler(this.radioButtonOutput_CheckedChanged);
            this.radioButtonOutput.EnabledChanged += new System.EventHandler(this.radioButtonOutput_EnabledChanged);
            // 
            // radioButtonInput
            // 
            this.radioButtonInput.AutoSize = true;
            this.radioButtonInput.Location = new System.Drawing.Point(9, 46);
            this.radioButtonInput.Name = "radioButtonInput";
            this.radioButtonInput.Size = new System.Drawing.Size(49, 17);
            this.radioButtonInput.TabIndex = 1;
            this.radioButtonInput.Text = "Input";
            this.radioButtonInput.UseVisualStyleBackColor = true;
            this.radioButtonInput.CheckedChanged += new System.EventHandler(this.radioButtonInput_CheckedChanged);
            this.radioButtonInput.EnabledChanged += new System.EventHandler(this.radioButtonInput_EnabledChanged);
            // 
            // radioButtonAuto
            // 
            this.radioButtonAuto.AutoSize = true;
            this.radioButtonAuto.Location = new System.Drawing.Point(9, 72);
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.Size = new System.Drawing.Size(47, 17);
            this.radioButtonAuto.TabIndex = 0;
            this.radioButtonAuto.Text = "Auto";
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            this.radioButtonAuto.CheckedChanged += new System.EventHandler(this.radioButtonAuto_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(210, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "ID:";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(229, 35);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(0, 13);
            this.labelID.TabIndex = 16;
            // 
            // FormPropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 343);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBoxIO);
            this.Controls.Add(this.groupBoxQuantity);
            this.Controls.Add(this.comboBoxTypes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxOpa);
            this.Controls.Add(this.groupBoxLinks);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPropEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Properties Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropEditor_FormClosing);
            this.Load += new System.EventHandler(this.PropEditor_Load);
            this.Move += new System.EventHandler(this.PropEditor_Move);
            this.groupBoxLinks.ResumeLayout(false);
            this.groupBoxLinks.PerformLayout();
            this.groupBoxQuantity.ResumeLayout(false);
            this.groupBoxQuantity.PerformLayout();
            this.groupBoxIO.ResumeLayout(false);
            this.groupBoxIO.PerformLayout();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxQtyIn;
        private System.Windows.Forms.TextBox textBoxQtyOut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxQuantity;
        private System.Windows.Forms.TextBox textBoxInjected;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxIO;
        private System.Windows.Forms.RadioButton radioButtonOutput;
        private System.Windows.Forms.RadioButton radioButtonInput;
        private System.Windows.Forms.RadioButton radioButtonAuto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.CheckBox checkBoxPublic;
    }
}