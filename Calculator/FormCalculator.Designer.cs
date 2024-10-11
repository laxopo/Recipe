
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
            this.buttonCalculate.Location = new System.Drawing.Point(12, 182);
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
            this.groupBoxInput.Size = new System.Drawing.Size(376, 199);
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
            this.vScrollBarInputs.Size = new System.Drawing.Size(17, 174);
            this.vScrollBarInputs.SmallChange = 20;
            this.vScrollBarInputs.TabIndex = 1;
            this.vScrollBarInputs.ValueChanged += new System.EventHandler(this.vScrollBarInputs_ValueChanged);
            // 
            // panelInputsWindow
            // 
            this.panelInputsWindow.Controls.Add(this.panelInputsContainer);
            this.panelInputsWindow.Location = new System.Drawing.Point(6, 19);
            this.panelInputsWindow.Name = "panelInputsWindow";
            this.panelInputsWindow.Size = new System.Drawing.Size(347, 174);
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
            this.groupBoxOutput.Size = new System.Drawing.Size(253, 199);
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
            this.vScrollBarOutputs.Size = new System.Drawing.Size(17, 177);
            this.vScrollBarOutputs.SmallChange = 20;
            this.vScrollBarOutputs.TabIndex = 2;
            this.vScrollBarOutputs.ValueChanged += new System.EventHandler(this.vScrollBarOutputs_ValueChanged);
            // 
            // panelOutputsWindow
            // 
            this.panelOutputsWindow.Controls.Add(this.panelOutputsContainer);
            this.panelOutputsWindow.Location = new System.Drawing.Point(6, 19);
            this.panelOutputsWindow.Name = "panelOutputsWindow";
            this.panelOutputsWindow.Size = new System.Drawing.Size(224, 174);
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
            // FormCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 217);
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
    }
}