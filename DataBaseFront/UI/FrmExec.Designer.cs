namespace DataBaseFront.UI
{
    partial class FrmExec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExec));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditFindPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miToggleBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoToNextBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoToPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miOption = new System.Windows.Forms.ToolStripMenuItem();
            this.miSplitWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowSpacesTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowEOLMarkers = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowInvalidLines = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowLineNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.miHLCurRow = new System.Windows.Forms.ToolStripMenuItem();
            this.miBracketMatchingStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.miEnableVirtualSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miConvertTabsToSpaces = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetTabSize = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetFont = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.tsbRunSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFind = new System.Windows.Forms.ToolStripButton();
            this.tsbReplace = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbLog = new System.Windows.Forms.TabPage();
            this.txtLOG = new System.Windows.Forms.TextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.menuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miEdit,
            this.miOption});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenFile,
            this.miSave,
            this.miSaveAs,
            this.toolStripMenuItem1,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(59, 20);
            this.miFile.Text = "文件(&F)";
            // 
            // miOpenFile
            // 
            this.miOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("miOpenFile.Image")));
            this.miOpenFile.Name = "miOpenFile";
            this.miOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpenFile.Size = new System.Drawing.Size(171, 22);
            this.miOpenFile.Text = "打开(&O)...";
            this.miOpenFile.Click += new System.EventHandler(this.miOpenFile_Click);
            // 
            // miSave
            // 
            this.miSave.Image = ((System.Drawing.Image)(resources.GetObject("miSave.Image")));
            this.miSave.Name = "miSave";
            this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSave.Size = new System.Drawing.Size(171, 22);
            this.miSave.Text = "保存(&S)";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(171, 22);
            this.miSaveAs.Text = "另存为(&A)...";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(171, 22);
            this.miExit.Text = "退出(&X)";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditCut,
            this.miEditCopy,
            this.miEditPaste,
            this.miEditDelete,
            this.toolStripMenuItem2,
            this.miEditFind,
            this.miEditReplace,
            this.miEditFindNext,
            this.miEditFindPrev,
            this.toolStripMenuItem3,
            this.miToggleBookmark,
            this.miGoToNextBookmark,
            this.miGoToPrevBookmark});
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(59, 20);
            this.miEdit.Text = "编辑(&E)";
            // 
            // miEditCut
            // 
            this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
            this.miEditCut.Name = "miEditCut";
            this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miEditCut.Size = new System.Drawing.Size(201, 22);
            this.miEditCut.Text = "剪切(&X)";
            this.miEditCut.Click += new System.EventHandler(this.miEditCut_Click);
            // 
            // miEditCopy
            // 
            this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
            this.miEditCopy.Name = "miEditCopy";
            this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miEditCopy.Size = new System.Drawing.Size(201, 22);
            this.miEditCopy.Text = "复制(&C)";
            this.miEditCopy.Click += new System.EventHandler(this.miEditCopy_Click);
            // 
            // miEditPaste
            // 
            this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
            this.miEditPaste.Name = "miEditPaste";
            this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miEditPaste.Size = new System.Drawing.Size(201, 22);
            this.miEditPaste.Text = "粘贴(&V)";
            this.miEditPaste.Click += new System.EventHandler(this.miEditPaste_Click);
            // 
            // miEditDelete
            // 
            this.miEditDelete.Image = ((System.Drawing.Image)(resources.GetObject("miEditDelete.Image")));
            this.miEditDelete.Name = "miEditDelete";
            this.miEditDelete.Size = new System.Drawing.Size(201, 22);
            this.miEditDelete.Text = "删除(&D)";
            this.miEditDelete.Click += new System.EventHandler(this.miEditDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
            // 
            // miEditFind
            // 
            this.miEditFind.Image = ((System.Drawing.Image)(resources.GetObject("miEditFind.Image")));
            this.miEditFind.Name = "miEditFind";
            this.miEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.miEditFind.Size = new System.Drawing.Size(201, 22);
            this.miEditFind.Text = "查找(&F)...";
            this.miEditFind.Click += new System.EventHandler(this.miEditFind_Click);
            // 
            // miEditReplace
            // 
            this.miEditReplace.Name = "miEditReplace";
            this.miEditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.miEditReplace.Size = new System.Drawing.Size(201, 22);
            this.miEditReplace.Text = "替换(&R)...";
            this.miEditReplace.Click += new System.EventHandler(this.miEditReplace_Click);
            // 
            // miEditFindNext
            // 
            this.miEditFindNext.Image = ((System.Drawing.Image)(resources.GetObject("miEditFindNext.Image")));
            this.miEditFindNext.Name = "miEditFindNext";
            this.miEditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.miEditFindNext.Size = new System.Drawing.Size(201, 22);
            this.miEditFindNext.Text = "查找下一个(&N)";
            this.miEditFindNext.Click += new System.EventHandler(this.miEditFindNext_Click);
            // 
            // miEditFindPrev
            // 
            this.miEditFindPrev.Name = "miEditFindPrev";
            this.miEditFindPrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.miEditFindPrev.Size = new System.Drawing.Size(201, 22);
            this.miEditFindPrev.Text = "查找上一个(&P)";
            this.miEditFindPrev.Click += new System.EventHandler(this.miEditFindPrev_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(198, 6);
            // 
            // miToggleBookmark
            // 
            this.miToggleBookmark.Name = "miToggleBookmark";
            this.miToggleBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.miToggleBookmark.Size = new System.Drawing.Size(201, 22);
            this.miToggleBookmark.Text = "设置/取消书签";
            this.miToggleBookmark.Click += new System.EventHandler(this.miToggleBookmark_Click);
            // 
            // miGoToNextBookmark
            // 
            this.miGoToNextBookmark.Name = "miGoToNextBookmark";
            this.miGoToNextBookmark.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miGoToNextBookmark.Size = new System.Drawing.Size(201, 22);
            this.miGoToNextBookmark.Text = "转到下一书签";
            this.miGoToNextBookmark.Click += new System.EventHandler(this.miGoToNextBookmark_Click);
            // 
            // miGoToPrevBookmark
            // 
            this.miGoToPrevBookmark.Name = "miGoToPrevBookmark";
            this.miGoToPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.miGoToPrevBookmark.Size = new System.Drawing.Size(201, 22);
            this.miGoToPrevBookmark.Text = "转到前一书签";
            this.miGoToPrevBookmark.Click += new System.EventHandler(this.miGoToPrevBookmark_Click);
            // 
            // miOption
            // 
            this.miOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSplitWindow,
            this.miShowSpacesTabs,
            this.miShowEOLMarkers,
            this.miShowInvalidLines,
            this.miShowLineNumbers,
            this.miHLCurRow,
            this.miBracketMatchingStyle,
            this.miEnableVirtualSpace,
            this.toolStripMenuItem4,
            this.miConvertTabsToSpaces,
            this.miSetTabSize,
            this.miSetFont});
            this.miOption.Name = "miOption";
            this.miOption.Size = new System.Drawing.Size(59, 20);
            this.miOption.Text = "选项(&O)";
            // 
            // miSplitWindow
            // 
            this.miSplitWindow.Image = ((System.Drawing.Image)(resources.GetObject("miSplitWindow.Image")));
            this.miSplitWindow.Name = "miSplitWindow";
            this.miSplitWindow.Size = new System.Drawing.Size(244, 22);
            this.miSplitWindow.Text = "拆分窗口(&W)";
            this.miSplitWindow.Click += new System.EventHandler(this.miSplitWindow_Click);
            // 
            // miShowSpacesTabs
            // 
            this.miShowSpacesTabs.Name = "miShowSpacesTabs";
            this.miShowSpacesTabs.Size = new System.Drawing.Size(244, 22);
            this.miShowSpacesTabs.Text = "显示空格和制表符(&S)";
            this.miShowSpacesTabs.Click += new System.EventHandler(this.miShowSpacesTabs_Click);
            // 
            // miShowEOLMarkers
            // 
            this.miShowEOLMarkers.Name = "miShowEOLMarkers";
            this.miShowEOLMarkers.Size = new System.Drawing.Size(244, 22);
            this.miShowEOLMarkers.Text = "显示换行标记(&E)";
            this.miShowEOLMarkers.Click += new System.EventHandler(this.miShowEOLMarkers_Click);
            // 
            // miShowInvalidLines
            // 
            this.miShowInvalidLines.Name = "miShowInvalidLines";
            this.miShowInvalidLines.Size = new System.Drawing.Size(244, 22);
            this.miShowInvalidLines.Text = "显示无效行标记(&I)";
            this.miShowInvalidLines.Click += new System.EventHandler(this.miShowInvalidLines_Click);
            // 
            // miShowLineNumbers
            // 
            this.miShowLineNumbers.Name = "miShowLineNumbers";
            this.miShowLineNumbers.Size = new System.Drawing.Size(244, 22);
            this.miShowLineNumbers.Text = "显示行号(&L)";
            this.miShowLineNumbers.Click += new System.EventHandler(this.miShowLineNumbers_Click);
            // 
            // miHLCurRow
            // 
            this.miHLCurRow.Image = ((System.Drawing.Image)(resources.GetObject("miHLCurRow.Image")));
            this.miHLCurRow.Name = "miHLCurRow";
            this.miHLCurRow.Size = new System.Drawing.Size(244, 22);
            this.miHLCurRow.Text = "高亮当前行(&H)";
            this.miHLCurRow.Click += new System.EventHandler(this.miHLCurRow_Click);
            // 
            // miBracketMatchingStyle
            // 
            this.miBracketMatchingStyle.Name = "miBracketMatchingStyle";
            this.miBracketMatchingStyle.Size = new System.Drawing.Size(244, 22);
            this.miBracketMatchingStyle.Text = "高亮匹配括号当光标在其后时(&A)";
            this.miBracketMatchingStyle.Visible = false;
            this.miBracketMatchingStyle.Click += new System.EventHandler(this.miBracketMatchingStyle_Click);
            // 
            // miEnableVirtualSpace
            // 
            this.miEnableVirtualSpace.Name = "miEnableVirtualSpace";
            this.miEnableVirtualSpace.Size = new System.Drawing.Size(244, 22);
            this.miEnableVirtualSpace.Text = "启用虚空格(&V)";
            this.miEnableVirtualSpace.Click += new System.EventHandler(this.miEnableVirtualSpace_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(241, 6);
            // 
            // miConvertTabsToSpaces
            // 
            this.miConvertTabsToSpaces.Name = "miConvertTabsToSpaces";
            this.miConvertTabsToSpaces.Size = new System.Drawing.Size(244, 22);
            this.miConvertTabsToSpaces.Text = "制表符转换为空格(&C)";
            this.miConvertTabsToSpaces.Click += new System.EventHandler(this.miConvertTabsToSpaces_Click);
            // 
            // miSetTabSize
            // 
            this.miSetTabSize.Name = "miSetTabSize";
            this.miSetTabSize.Size = new System.Drawing.Size(244, 22);
            this.miSetTabSize.Text = "设置制表符大小(&T)";
            this.miSetTabSize.Click += new System.EventHandler(this.miSetTabSize_Click);
            // 
            // miSetFont
            // 
            this.miSetFont.Image = ((System.Drawing.Image)(resources.GetObject("miSetFont.Image")));
            this.miSetFont.Name = "miSetFont";
            this.miSetFont.Size = new System.Drawing.Size(244, 22);
            this.miSetFont.Text = "字体(&F)";
            this.miSetFont.Click += new System.EventHandler(this.miSetFont_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRun,
            this.tsbRunSelect,
            this.toolStripSeparator1,
            this.tsbOpen,
            this.tsbSave,
            this.tsbSaveAS,
            this.toolStripSeparator2,
            this.tsbFind,
            this.tsbReplace});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRun
            // 
            this.tsbRun.Image = global::DataBaseFront.Properties.Resources.run;
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(49, 22);
            this.tsbRun.Text = "运行";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // tsbRunSelect
            // 
            this.tsbRunSelect.Image = global::DataBaseFront.Properties.Resources.runselect;
            this.tsbRunSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRunSelect.Name = "tsbRunSelect";
            this.tsbRunSelect.Size = new System.Drawing.Size(73, 22);
            this.tsbRunSelect.Text = "运行选中";
            this.tsbRunSelect.Click += new System.EventHandler(this.tsbRunSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = global::DataBaseFront.Properties.Resources.open;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(49, 22);
            this.tsbOpen.Text = "打开";
            this.tsbOpen.Click += new System.EventHandler(this.miOpenFile_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::DataBaseFront.Properties.Resources.save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(49, 22);
            this.tsbSave.Text = "保存";
            this.tsbSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // tsbSaveAS
            // 
            this.tsbSaveAS.Image = global::DataBaseFront.Properties.Resources.saveas;
            this.tsbSaveAS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAS.Name = "tsbSaveAS";
            this.tsbSaveAS.Size = new System.Drawing.Size(61, 22);
            this.tsbSaveAS.Text = "另存为";
            this.tsbSaveAS.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFind
            // 
            this.tsbFind.Image = global::DataBaseFront.Properties.Resources.find;
            this.tsbFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFind.Name = "tsbFind";
            this.tsbFind.Size = new System.Drawing.Size(49, 22);
            this.tsbFind.Text = "查找";
            this.tsbFind.Click += new System.EventHandler(this.miEditFind_Click);
            // 
            // tsbReplace
            // 
            this.tsbReplace.Image = global::DataBaseFront.Properties.Resources.replace;
            this.tsbReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReplace.Name = "tsbReplace";
            this.tsbReplace.Size = new System.Drawing.Size(49, 22);
            this.tsbReplace.Text = "替换";
            this.tsbReplace.Click += new System.EventHandler(this.miEditReplace_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textEditorControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(792, 502);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 9;
            // 
            // textEditorControl
            // 
            this.textEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(0, 0);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.Size = new System.Drawing.Size(792, 300);
            this.textEditorControl.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(48, 24);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 198);
            this.tabControl1.TabIndex = 1;
            // 
            // tbLog
            // 
            this.tbLog.Controls.Add(this.txtLOG);
            this.tbLog.Location = new System.Drawing.Point(4, 28);
            this.tbLog.Name = "tbLog";
            this.tbLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbLog.Size = new System.Drawing.Size(784, 166);
            this.tbLog.TabIndex = 1;
            this.tbLog.Text = "消息";
            this.tbLog.UseVisualStyleBackColor = true;
            // 
            // txtLOG
            // 
            this.txtLOG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLOG.Location = new System.Drawing.Point(3, 3);
            this.txtLOG.Multiline = true;
            this.txtLOG.Name = "txtLOG";
            this.txtLOG.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLOG.Size = new System.Drawing.Size(778, 160);
            this.txtLOG.TabIndex = 1;
            // 
            // FrmExec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip);
            this.Name = "FrmExec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmExec";
            this.Load += new System.EventHandler(this.FrmExec_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbLog.ResumeLayout(false);
            this.tbLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miOpenFile;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miEditCut;
        private System.Windows.Forms.ToolStripMenuItem miEditCopy;
        private System.Windows.Forms.ToolStripMenuItem miEditPaste;
        private System.Windows.Forms.ToolStripMenuItem miEditDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miEditFind;
        private System.Windows.Forms.ToolStripMenuItem miEditReplace;
        private System.Windows.Forms.ToolStripMenuItem miEditFindNext;
        private System.Windows.Forms.ToolStripMenuItem miEditFindPrev;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miToggleBookmark;
        private System.Windows.Forms.ToolStripMenuItem miGoToNextBookmark;
        private System.Windows.Forms.ToolStripMenuItem miGoToPrevBookmark;
        private System.Windows.Forms.ToolStripMenuItem miOption;
        private System.Windows.Forms.ToolStripMenuItem miSplitWindow;
        private System.Windows.Forms.ToolStripMenuItem miShowSpacesTabs;
        private System.Windows.Forms.ToolStripMenuItem miShowEOLMarkers;
        private System.Windows.Forms.ToolStripMenuItem miShowInvalidLines;
        private System.Windows.Forms.ToolStripMenuItem miShowLineNumbers;
        private System.Windows.Forms.ToolStripMenuItem miHLCurRow;
        private System.Windows.Forms.ToolStripMenuItem miBracketMatchingStyle;
        private System.Windows.Forms.ToolStripMenuItem miEnableVirtualSpace;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miConvertTabsToSpaces;
        private System.Windows.Forms.ToolStripMenuItem miSetTabSize;
        private System.Windows.Forms.ToolStripMenuItem miSetFont;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbLog;
        private System.Windows.Forms.TextBox txtLOG;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbSaveAS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbFind;
        private System.Windows.Forms.ToolStripButton tsbReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.ToolStripButton tsbRunSelect;
    }
}