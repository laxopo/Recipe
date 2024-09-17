
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
            this.buttonGenerate = new System.Windows.Forms.Button();
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
            this.groupBoxInput.SuspendLayout();
            this.panelInputsWindow.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.panelOutputsWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(12, 113);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
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
            this.buttonCalculate.Location = new System.Drawing.Point(12, 142);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 23);
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
            this.groupBoxInput.Size = new System.Drawing.Size(376, 184);
            this.groupBoxInput.TabIndex = 5;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // vScrollBarInputs
            // 
            this.vScrollBarInputs.Enabled = false;
            this.vScrollBarInputs.LargeChange = 40;
            this.vScrollBarInputs.Location = new System.Drawing.Point(356, 19);
            this.vScrollBarInputs.Name = "vScrollBarInputs";
            this.vScrollBarInputs.Size = new System.Drawing.Size(17, 162);
            this.vScrollBarInputs.TabIndex = 1;
            this.vScrollBarInputs.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBarInputs_Scroll);
            // 
            // panelInputsWindow
            // 
            this.panelInputsWindow.Controls.Add(this.panelInputsContainer);
            this.panelInputsWindow.Location = new System.Drawing.Point(6, 19);
            this.panelInputsWindow.Name = "panelInputsWindow";
            this.panelInputsWindow.Size = new System.Drawing.Size(347, 159);
            this.panelInputsWindow.TabIndex = 0;
            // 
            // panelInputsContainer
            // 
            this.panelInputsContainer.BackColor = System.Drawing.SystemColors.Control;
            this.panelInputsContainer.Location = new System.Drawing.Point(0, 0);
            this.panelInputsContainer.Name = "panelInputsContainer";
            this.panelInputsContainer.Size = new System.Drawing.Size(347, 42);
            this.panelInputsContainer.TabIndex = 0;
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.vScrollBarOutputs);
            this.groupBoxOutput.Controls.Add(this.panelOutputsWindow);
            this.groupBoxOutput.Location = new System.Drawing.Point(520, 12);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(253, 184);
            this.groupBoxOutput.TabIndex = 6;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // vScrollBarOutputs
            // 
            this.vScrollBarOutputs.Enabled = false;
            this.vScrollBarOutputs.LargeChange = 40;
            this.vScrollBarOutputs.Location = new System.Drawing.Point(233, 16);
            this.vScrollBarOutputs.Name = "vScrollBarOutputs";
            this.vScrollBarOutputs.Size = new System.Drawing.Size(17, 162);
            this.vScrollBarOutputs.TabIndex = 2;
            this.vScrollBarOutputs.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBarOutputs_Scroll);
            // 
            // panelOutputsWindow
            // 
            this.panelOutputsWindow.Controls.Add(this.panelOutputsContainer);
            this.panelOutputsWindow.Location = new System.Drawing.Point(6, 19);
            this.panelOutputsWindow.Name = "panelOutputsWindow";
            this.panelOutputsWindow.Size = new System.Drawing.Size(224, 159);
            this.panelOutputsWindow.TabIndex = 0;
            // 
            // panelOutputsContainer
            // 
            this.panelOutputsContainer.Location = new System.Drawing.Point(0, 0);
            this.panelOutputsContainer.Name = "panelOutputsContainer";
            this.panelOutputsContainer.Size = new System.Drawing.Size(224, 42);
            this.panelOutputsContainer.TabIndex = 1;
            // 
            // FormCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 208);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.listBoxTrees);
            this.Controls.Add(this.buttonGenerate);
            this.Icon = global::Recipe.Properties.Resources.pumpkin_pie;
            this.MaximizeBox = false;
            this.Name = "FormCalculator";
            this.Text = "Calculator";
            this.MouseEnter += new System.EventHandler(this.FormCalculator_MouseEnter);
            this.groupBoxInput.ResumeLayout(false);
            this.panelInputsWindow.ResumeLayout(false);
            this.groupBoxOutput.ResumeLayout(false);
            this.panelOutputsWindow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
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
    }
}