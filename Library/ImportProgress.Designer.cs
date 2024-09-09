
namespace Recipe
{
    partial class ImportProgress
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelProc = new System.Windows.Forms.Label();
            this.labelImport = new System.Windows.Forms.Label();
            this.labelSkip = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelFile = new System.Windows.Forms.Label();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.timerCurFileShow = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Processed:";
            this.label2.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Imported:";
            this.label3.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Skipped:";
            this.label4.UseWaitCursor = true;
            // 
            // labelProc
            // 
            this.labelProc.AutoSize = true;
            this.labelProc.Location = new System.Drawing.Point(78, 9);
            this.labelProc.Name = "labelProc";
            this.labelProc.Size = new System.Drawing.Size(64, 13);
            this.labelProc.TabIndex = 4;
            this.labelProc.Text = "(calculating)";
            this.labelProc.UseWaitCursor = true;
            // 
            // labelImport
            // 
            this.labelImport.AutoSize = true;
            this.labelImport.Location = new System.Drawing.Point(78, 22);
            this.labelImport.Name = "labelImport";
            this.labelImport.Size = new System.Drawing.Size(13, 13);
            this.labelImport.TabIndex = 5;
            this.labelImport.Text = "0";
            this.labelImport.UseWaitCursor = true;
            // 
            // labelSkip
            // 
            this.labelSkip.AutoSize = true;
            this.labelSkip.Location = new System.Drawing.Point(78, 35);
            this.labelSkip.Name = "labelSkip";
            this.labelSkip.Size = new System.Drawing.Size(13, 13);
            this.labelSkip.TabIndex = 6;
            this.labelSkip.Text = "0";
            this.labelSkip.UseWaitCursor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 51);
            this.progressBar1.MarqueeAnimationSpeed = 1;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 7;
            this.progressBar1.UseWaitCursor = true;
            // 
            // labelFile
            // 
            this.labelFile.AutoEllipsis = true;
            this.labelFile.Location = new System.Drawing.Point(9, 77);
            this.labelFile.Name = "labelFile";
            this.labelFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelFile.Size = new System.Drawing.Size(262, 13);
            this.labelFile.TabIndex = 8;
            this.labelFile.Text = "...";
            this.labelFile.UseWaitCursor = true;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Location = new System.Drawing.Point(196, 22);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonAbort.TabIndex = 9;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.UseWaitCursor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // timerCurFileShow
            // 
            this.timerCurFileShow.Interval = 50;
            this.timerCurFileShow.Tick += new System.EventHandler(this.timerCurFileShow_Tick);
            // 
            // ImportProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 96);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelSkip);
            this.Controls.Add(this.labelImport);
            this.Controls.Add(this.labelProc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImportProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importing...";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.ImportProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelProc;
        private System.Windows.Forms.Label labelImport;
        private System.Windows.Forms.Label labelSkip;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Timer timerCurFileShow;
    }
}