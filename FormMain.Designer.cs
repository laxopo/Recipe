
namespace Recipe
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pictureBoxArea = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeyHandlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.buttonAreaResize = new System.Windows.Forms.Button();
            this.contextMenuStripEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogProj = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogProj = new System.Windows.Forms.SaveFileDialog();
            this.hScrollBarEditor = new System.Windows.Forms.HScrollBar();
            this.vScrollBarEditor = new System.Windows.Forms.VScrollBar();
            this.toolTipBAreaResz = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialogSnapshot = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArea)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.contextMenuStripEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxArea
            // 
            this.pictureBoxArea.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxArea.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxArea.Name = "pictureBoxArea";
            this.pictureBoxArea.Size = new System.Drawing.Size(724, 381);
            this.pictureBoxArea.TabIndex = 0;
            this.pictureBoxArea.TabStop = false;
            this.pictureBoxArea.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBoxArea_LoadCompleted);
            this.pictureBoxArea.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxArea_Paint);
            this.pictureBoxArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxArea_MouseDown);
            this.pictureBoxArea.MouseEnter += new System.EventHandler(this.pictureBoxArea_MouseEnter);
            this.pictureBoxArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxArea_MouseMove);
            this.pictureBoxArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxArea_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.libraryToolStripMenuItem,
            this.sheetToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.hotkeyHandlerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(795, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportImageToolStripMenuItem
            // 
            this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
            this.exportImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportImageToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportImageToolStripMenuItem.Text = "Export Graphics";
            this.exportImageToolStripMenuItem.Click += new System.EventHandler(this.exportImageToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.deselectToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // deselectToolStripMenuItem
            // 
            this.deselectToolStripMenuItem.Name = "deselectToolStripMenuItem";
            this.deselectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deselectToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deselectToolStripMenuItem.Text = "Deselect";
            this.deselectToolStripMenuItem.Click += new System.EventHandler(this.deselectToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // libraryToolStripMenuItem
            // 
            this.libraryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemPropertiesToolStripMenuItem,
            this.libraryToolStripMenuItem1});
            this.libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            this.libraryToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.libraryToolStripMenuItem.Text = "Tools";
            // 
            // itemPropertiesToolStripMenuItem
            // 
            this.itemPropertiesToolStripMenuItem.Name = "itemPropertiesToolStripMenuItem";
            this.itemPropertiesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.itemPropertiesToolStripMenuItem.Text = "Properties Editor";
            this.itemPropertiesToolStripMenuItem.Click += new System.EventHandler(this.itemPropertiesToolStripMenuItem_Click);
            // 
            // libraryToolStripMenuItem1
            // 
            this.libraryToolStripMenuItem1.Name = "libraryToolStripMenuItem1";
            this.libraryToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.libraryToolStripMenuItem1.Text = "Library";
            this.libraryToolStripMenuItem1.Click += new System.EventHandler(this.libraryToolStripMenuItem1_Click);
            // 
            // sheetToolStripMenuItem
            // 
            this.sheetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeToolStripMenuItem,
            this.fitToolStripMenuItem,
            this.appToolStripMenuItem});
            this.sheetToolStripMenuItem.Name = "sheetToolStripMenuItem";
            this.sheetToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.sheetToolStripMenuItem.Text = "Sheet";
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.resizeToolStripMenuItem.Text = "Resize";
            this.resizeToolStripMenuItem.Click += new System.EventHandler(this.resizeToolStripMenuItem_Click);
            // 
            // fitToolStripMenuItem
            // 
            this.fitToolStripMenuItem.Name = "fitToolStripMenuItem";
            this.fitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.fitToolStripMenuItem.Text = "Fit";
            this.fitToolStripMenuItem.Click += new System.EventHandler(this.fitToolStripMenuItem_Click);
            // 
            // appToolStripMenuItem
            // 
            this.appToolStripMenuItem.Name = "appToolStripMenuItem";
            this.appToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.appToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.appToolStripMenuItem.Text = "Appearance";
            this.appToolStripMenuItem.Click += new System.EventHandler(this.appToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showHelpToolStripMenuItem
            // 
            this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
            this.showHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showHelpToolStripMenuItem.Text = "Show help";
            this.showHelpToolStripMenuItem.Click += new System.EventHandler(this.showHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // hotkeyHandlerToolStripMenuItem
            // 
            this.hotkeyHandlerToolStripMenuItem.Name = "hotkeyHandlerToolStripMenuItem";
            this.hotkeyHandlerToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.hotkeyHandlerToolStripMenuItem.Text = "hotkeyHandler";
            this.hotkeyHandlerToolStripMenuItem.Visible = false;
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelEditor.Controls.Add(this.buttonAreaResize);
            this.panelEditor.Controls.Add(this.pictureBoxArea);
            this.panelEditor.Location = new System.Drawing.Point(0, 27);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(769, 411);
            this.panelEditor.TabIndex = 2;
            // 
            // buttonAreaResize
            // 
            this.buttonAreaResize.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.buttonAreaResize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAreaResize.Location = new System.Drawing.Point(728, 383);
            this.buttonAreaResize.Name = "buttonAreaResize";
            this.buttonAreaResize.Size = new System.Drawing.Size(8, 8);
            this.buttonAreaResize.TabIndex = 1;
            this.buttonAreaResize.TabStop = false;
            this.toolTipBAreaResz.SetToolTip(this.buttonAreaResize, "Resize sheet");
            this.buttonAreaResize.UseVisualStyleBackColor = true;
            this.buttonAreaResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonAreaResize_MouseDown);
            this.buttonAreaResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonAreaResize_MouseMove);
            this.buttonAreaResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonAreaResize_MouseUp);
            // 
            // contextMenuStripEditor
            // 
            this.contextMenuStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuEditor,
            this.pasteToolStripMenuEditor});
            this.contextMenuStripEditor.Name = "contextMenuStripIObj";
            this.contextMenuStripEditor.Size = new System.Drawing.Size(138, 48);
            this.contextMenuStripEditor.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripEditor_Opening);
            // 
            // insertToolStripMenuEditor
            // 
            this.insertToolStripMenuEditor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.insertToolStripMenuEditor.Name = "insertToolStripMenuEditor";
            this.insertToolStripMenuEditor.Size = new System.Drawing.Size(137, 22);
            this.insertToolStripMenuEditor.Text = "Insert Item";
            this.insertToolStripMenuEditor.Click += new System.EventHandler(this.insertToolStripMenuEditor_Click);
            // 
            // pasteToolStripMenuEditor
            // 
            this.pasteToolStripMenuEditor.Name = "pasteToolStripMenuEditor";
            this.pasteToolStripMenuEditor.Size = new System.Drawing.Size(137, 22);
            this.pasteToolStripMenuEditor.Text = "Paste";
            this.pasteToolStripMenuEditor.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // openFileDialogProj
            // 
            this.openFileDialogProj.Filter = "JSON|*.json|All files|*.*";
            this.openFileDialogProj.Title = "Open The Project";
            // 
            // saveFileDialogProj
            // 
            this.saveFileDialogProj.Filter = "JSON|*.json|All files|*.*";
            this.saveFileDialogProj.Title = "Save The Project As";
            // 
            // hScrollBarEditor
            // 
            this.hScrollBarEditor.LargeChange = 100;
            this.hScrollBarEditor.Location = new System.Drawing.Point(2, 438);
            this.hScrollBarEditor.Name = "hScrollBarEditor";
            this.hScrollBarEditor.Size = new System.Drawing.Size(769, 17);
            this.hScrollBarEditor.SmallChange = 20;
            this.hScrollBarEditor.TabIndex = 3;
            this.hScrollBarEditor.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEditor_Scroll);
            // 
            // vScrollBarEditor
            // 
            this.vScrollBarEditor.LargeChange = 100;
            this.vScrollBarEditor.Location = new System.Drawing.Point(769, 27);
            this.vScrollBarEditor.Name = "vScrollBarEditor";
            this.vScrollBarEditor.Size = new System.Drawing.Size(17, 411);
            this.vScrollBarEditor.SmallChange = 20;
            this.vScrollBarEditor.TabIndex = 4;
            this.vScrollBarEditor.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBarEditor_Scroll);
            // 
            // saveFileDialogSnapshot
            // 
            this.saveFileDialogSnapshot.Filter = "PNG|*.png|BMP|*.bmp|JPEG|*.jpg; *.jpeg|GIF|*.gif|TIFF|*.tiff|EXIF|*.exif|EMF|*.em" +
    "f|WMF|*.wmf";
            this.saveFileDialogSnapshot.Title = "Save The Snapshot As";
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 465);
            this.Controls.Add(this.vScrollBarEditor);
            this.Controls.Add(this.hScrollBarEditor);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recipe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResizeEnd += new System.EventHandler(this.FormMain_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArea)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelEditor.ResumeLayout(false);
            this.contextMenuStripEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxArea;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem libraryToolStripMenuItem;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEditor;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuEditor;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogProj;
        private System.Windows.Forms.SaveFileDialog saveFileDialogProj;
        private System.Windows.Forms.HScrollBar hScrollBarEditor;
        private System.Windows.Forms.VScrollBar vScrollBarEditor;
        private System.Windows.Forms.ToolTip toolTipBAreaResz;
        private System.Windows.Forms.Button buttonAreaResize;
        private System.Windows.Forms.ToolStripMenuItem sheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem itemPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSnapshot;
        private System.Windows.Forms.ToolStripMenuItem fitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotkeyHandlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuEditor;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

