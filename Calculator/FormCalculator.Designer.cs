
namespace Recipe
{
    partial class FormCalculator
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
            this.listBoxTrees = new System.Windows.Forms.ListBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.vScrollBarInputs = new System.Windows.Forms.VScrollBar();
            this.panelInputsWindow = new System.Windows.Forms.Panel();
            this.panelInputsContainer = new System.Windows.Forms.Panel();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.vScrollBarOutputs = new System.Windows.Forms.VScrollBar();
            this.panelOutputsWindow = new System.Windows.Forms.Panel();
            this.panelOutputsContainer = new System.Windows.Forms.Panel();
            this.radioButtonCalcIn = new System.Windows.Forms.RadioButton();
            this.radioButtonCalcOut = new System.Windows.Forms.RadioButton();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelEnergy = new System.Windows.Forms.Label();
            this.listBoxEnergy = new System.Windows.Forms.ListBox();
            this.checkBoxPerItem = new System.Windows.Forms.CheckBox();
            this.groupBoxInput.SuspendLayout();
            this.panelInputsWindow.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.panelOutputsWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxTrees
            // 
            this.listBoxTrees.FormattingEnabled = true;
            this.listBoxTrees.Location = new System.Drawing.Point(12, 12);
            this.listBoxTrees.Name = "listBoxTrees";
            this.listBoxTrees.Size = new System.Drawing.Size(120, 95);
            this.listBoxTrees.TabIndex = 1;
            this.listBoxTrees.SelectedIndexChanged += new System.EventHandler(this.listBoxTrees_SelectedIndexChanged);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(12, 136);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(120, 23);
            this.buttonCalculate.TabIndex = 4;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.vScrollBarInputs);
            this.groupBoxInput.Controls.Add(this.panelInputsWindow);
            this.groupBoxInput.Location = new System.Drawing.Point(138, 12);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(376, 245);
            this.groupBoxInput.TabIndex = 5;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // vScrollBarInputs
            // 
            this.vScrollBarInputs.Enabled = false;
            this.vScrollBarInputs.LargeChange = 80;
            this.vScrollBarInputs.Location = new System.Drawing.Point(356, 19);
            this.vScrollBarInputs.Name = "vScrollBarInputs";
            this.vScrollBarInputs.Size = new System.Drawing.Size(17, 219);
            this.vScrollBarInputs.SmallChange = 20;
            this.vScrollBarInputs.TabIndex = 1;
            this.vScrollBarInputs.ValueChanged += new System.EventHandler(this.vScrollBarInputs_ValueChanged);
            // 
            // panelInputsWindow
            // 
            this.panelInputsWindow.Controls.Add(this.panelInputsContainer);
            this.panelInputsWindow.Location = new System.Drawing.Point(6, 19);
            this.panelInputsWindow.Name = "panelInputsWindow";
            this.panelInputsWindow.Size = new System.Drawing.Size(347, 219);
            this.panelInputsWindow.TabIndex = 0;
            // 
            // panelInputsContainer
            // 
            this.panelInputsContainer.BackColor = System.Drawing.SystemColors.Control;
            this.panelInputsContainer.Location = new System.Drawing.Point(0, 0);
            this.panelInputsContainer.Name = "panelInputsContainer";
            this.panelInputsContainer.Size = new System.Drawing.Size(347, 42);
            this.panelInputsContainer.TabIndex = 0;
            this.panelInputsContainer.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panelInputsContainer_ControlAdded);
            this.panelInputsContainer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.panelInputsContainer_ControlRemoved);
            this.panelInputsContainer.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelInputsContainer_MouseWheel);
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.vScrollBarOutputs);
            this.groupBoxOutput.Controls.Add(this.panelOutputsWindow);
            this.groupBoxOutput.Location = new System.Drawing.Point(520, 12);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(253, 245);
            this.groupBoxOutput.TabIndex = 6;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // vScrollBarOutputs
            // 
            this.vScrollBarOutputs.Enabled = false;
            this.vScrollBarOutputs.LargeChange = 80;
            this.vScrollBarOutputs.Location = new System.Drawing.Point(233, 16);
            this.vScrollBarOutputs.Name = "vScrollBarOutputs";
            this.vScrollBarOutputs.Size = new System.Drawing.Size(17, 222);
            this.vScrollBarOutputs.SmallChange = 20;
            this.vScrollBarOutputs.TabIndex = 2;
            this.vScrollBarOutputs.ValueChanged += new System.EventHandler(this.vScrollBarOutputs_ValueChanged);
            // 
            // panelOutputsWindow
            // 
            this.panelOutputsWindow.Controls.Add(this.panelOutputsContainer);
            this.panelOutputsWindow.Location = new System.Drawing.Point(6, 19);
            this.panelOutputsWindow.Name = "panelOutputsWindow";
            this.panelOutputsWindow.Size = new System.Drawing.Size(224, 219);
            this.panelOutputsWindow.TabIndex = 0;
            // 
            // panelOutputsContainer
            // 
            this.panelOutputsContainer.Location = new System.Drawing.Point(0, 0);
            this.panelOutputsContainer.Name = "panelOutputsContainer";
            this.panelOutputsContainer.Size = new System.Drawing.Size(224, 42);
            this.panelOutputsContainer.TabIndex = 1;
            this.panelOutputsContainer.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panelOutputsContainer_ControlAdded);
            this.panelOutputsContainer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.panelOutputsContainer_ControlRemoved);
            this.panelOutputsContainer.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelOutputsContainer_MouseWheel);
            // 
            // radioButtonCalcIn
            // 
            this.radioButtonCalcIn.AutoSize = true;
            this.radioButtonCalcIn.Location = new System.Drawing.Point(12, 113);
            this.radioButtonCalcIn.Name = "radioButtonCalcIn";
            this.radioButtonCalcIn.Size = new System.Drawing.Size(49, 17);
            this.radioButtonCalcIn.TabIndex = 7;
            this.radioButtonCalcIn.Text = "Input";
            this.radioButtonCalcIn.UseVisualStyleBackColor = true;
            this.radioButtonCalcIn.CheckedChanged += new System.EventHandler(this.radioButtonCalcIn_CheckedChanged);
            // 
            // radioButtonCalcOut
            // 
            this.radioButtonCalcOut.AutoSize = true;
            this.radioButtonCalcOut.Checked = true;
            this.radioButtonCalcOut.Location = new System.Drawing.Point(75, 113);
            this.radioButtonCalcOut.Name = "radioButtonCalcOut";
            this.radioButtonCalcOut.Size = new System.Drawing.Size(57, 17);
            this.radioButtonCalcOut.TabIndex = 8;
            this.radioButtonCalcOut.TabStop = true;
            this.radioButtonCalcOut.Text = "Output";
            this.radioButtonCalcOut.UseVisualStyleBackColor = true;
            this.radioButtonCalcOut.CheckedChanged += new System.EventHandler(this.radioButtonCalcOut_CheckedChanged);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(9, 162);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(33, 13);
            this.labelTime.TabIndex = 9;
            this.labelTime.Text = "Time:";
            // 
            // labelEnergy
            // 
            this.labelEnergy.AutoSize = true;
            this.labelEnergy.Location = new System.Drawing.Point(9, 175);
            this.labelEnergy.Name = "labelEnergy";
            this.labelEnergy.Size = new System.Drawing.Size(112, 13);
            this.labelEnergy.TabIndex = 10;
            this.labelEnergy.Text = "Energy Consumptions:";
            // 
            // listBoxEnergy
            // 
            this.listBoxEnergy.FormattingEnabled = true;
            this.listBoxEnergy.Location = new System.Drawing.Point(12, 191);
            this.listBoxEnergy.Name = "listBoxEnergy";
            this.listBoxEnergy.Size = new System.Drawing.Size(120, 43);
            this.listBoxEnergy.TabIndex = 11;
            // 
            // checkBoxPerItem
            // 
            this.checkBoxPerItem.AutoSize = true;
            this.checkBoxPerItem.Location = new System.Drawing.Point(12, 240);
            this.checkBoxPerItem.Name = "checkBoxPerItem";
            this.checkBoxPerItem.Size = new System.Drawing.Size(83, 17);
            this.checkBoxPerItem.TabIndex = 12;
            this.checkBoxPerItem.Text = "EU Per Item";
            this.checkBoxPerItem.UseVisualStyleBackColor = true;
            this.checkBoxPerItem.CheckedChanged += new System.EventHandler(this.checkBoxPerItem_CheckedChanged);
            // 
            // FormCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 262);
            this.Controls.Add(this.checkBoxPerItem);
            this.Controls.Add(this.listBoxEnergy);
            this.Controls.Add(this.labelEnergy);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.radioButtonCalcOut);
            this.Controls.Add(this.radioButtonCalcIn);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.listBoxTrees);
            this.Icon = global::Recipe.Properties.Resources.pumpkin_pie;
            this.MaximizeBox = false;
            this.Name = "FormCalculator";
            this.Text = "Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCalculator_FormClosing);
            this.Load += new System.EventHandler(this.FormCalculator_Load);
            this.MouseEnter += new System.EventHandler(this.FormCalculator_MouseEnter);
            this.groupBoxInput.ResumeLayout(false);
            this.panelInputsWindow.ResumeLayout(false);
            this.groupBoxOutput.ResumeLayout(false);
            this.panelOutputsWindow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxTrees;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.VScrollBar vScrollBarInputs;
        private System.Windows.Forms.Panel panelInputsWindow;
        private System.Windows.Forms.VScrollBar vScrollBarOutputs;
        private System.Windows.Forms.Panel panelOutputsWindow;
        private System.Windows.Forms.Panel panelInputsContainer;
        private System.Windows.Forms.Panel panelOutputsContainer;
        private System.Windows.Forms.RadioButton radioButtonCalcIn;
        private System.Windows.Forms.RadioButton radioButtonCalcOut;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelEnergy;
        private System.Windows.Forms.ListBox listBoxEnergy;
        private System.Windows.Forms.CheckBox checkBoxPerItem;
    }
}