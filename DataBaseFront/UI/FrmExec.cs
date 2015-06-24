using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using DataBaseFront.Entity;

namespace DataBaseFront.UI
{
    public partial class FrmExec : BaseForm
    {
        const string SharpPadFileFilter = "Sql Files (*.sql)|*.sql|All Files (*.*)|*.*";

        ITextEditorProperties _editorSettings;

        Link targetLink;

        public FrmExec(Link link, string sql)
        {
            InitializeComponent();

            targetLink = link;
            Sql = sql;
        }

        public string Sql
        {
            get { return ActiveEditor.Text; }
            set { ActiveEditor.Text = value; }
        }

        private TextEditorControl ActiveEditor { get { return this.textEditorControl; } }

        private void FrmExec_Load(object sender, EventArgs e)
        {
            InitForm(sender);
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            this.ClearExecResult();

            this.DoExec();
        }

        private void tsbRunSelect_Click(object sender, EventArgs e)
        {
            this.ClearExecResult();

            this.DoExec(this.GetSelectionText());
        }

        #region 文件
        private void miOpenFile_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = SharpPadFileFilter;
                    dialog.FilterIndex = 0;
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        editor.LoadFile(dialog.FileName);
                    }
                }
            }
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                if (editor.FileName != null)
                {
                    editor.SaveFile(editor.FileName);
                }
                else
                {
                    this.SaveAs();
                }
            }
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SaveAs()
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = SharpPadFileFilter;
                    dialog.FilterIndex = 0;
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        editor.SaveFile(dialog.FileName);
                        editor.FileName = dialog.FileName;
                    }
                }
            }
        }
        #endregion

        #region 编辑

        private void miEditCut_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Cut());
        }

        private void miEditCopy_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Copy());
        }

        private void miEditPaste_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Paste());
        }

        private void miEditDelete_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Delete());
        }

        Base.FindAndReplaceForm _findForm = new Base.FindAndReplaceForm();
        private void miEditFind_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            _findForm.ShowFor(editor, false);
        }

        private void miEditReplace_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            _findForm.ShowFor(editor, true);
        }

        private void miEditFindNext_Click(object sender, EventArgs e)
        {
            _findForm.FindNext(true, false,
                string.Format("没有找到你要查找的内容！", _findForm.LookFor));
        }

        private void miEditFindPrev_Click(object sender, EventArgs e)
        {
            _findForm.FindNext(true, true,
                string.Format("没有找到你要查找的内容！", _findForm.LookFor));
        }

        private void miToggleBookmark_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.ToggleBookmark());
                editor.IsIconBarVisible = editor.Document.BookmarkManager.Marks.Count > 0;
            }
        }

        private void miGoToNextBookmark_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoNextBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
            {
                return true;
            })));
        }

        private void miGoToPrevBookmark_Click(object sender, EventArgs e)
        {
            DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoPrevBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
            {
                return true;
            })));
        }

        private bool HaveSelection()
        {
            TextEditorControl editor = ActiveEditor;
            return editor != null &&
                editor.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected;
        }

        private void DoEditAction(TextEditorControl editor, ICSharpCode.TextEditor.Actions.IEditAction action)
        {
            if (editor != null && action != null)
            {
                TextArea area = editor.ActiveTextAreaControl.TextArea;
                editor.BeginUpdate();
                try
                {
                    lock (editor.Document)
                    {
                        action.Execute(area);
                        if (area.SelectionManager.HasSomethingSelected && area.AutoClearSelection /*&& caretchanged*/)
                        {
                            if (area.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
                            {
                                area.SelectionManager.ClearSelection();
                            }
                        }
                    }
                }
                finally
                {
                    editor.EndUpdate();
                    area.Caret.UpdateCaretPosition();
                }
            }
        }

        private string GetSelectionText()
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null &&
                editor.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
            {
                return editor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
            }
            return string.Empty;
        }
        #endregion

        #region 选项
        private void miSplitWindow_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.Split();
            OnSettingsChanged();
        }

        private void miShowSpacesTabs_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowSpaces = editor.ShowTabs = !editor.ShowSpaces;
            OnSettingsChanged();
        }

        private void miShowEOLMarkers_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowEOLMarkers = !editor.ShowEOLMarkers;
            OnSettingsChanged();
        }

        private void miShowInvalidLines_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowInvalidLines = !editor.ShowInvalidLines;
            OnSettingsChanged();
        }

        private void miShowLineNumbers_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ShowLineNumbers = !editor.ShowLineNumbers;
            OnSettingsChanged();
        }

        private void miHLCurRow_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.LineViewerStyle = editor.LineViewerStyle == LineViewerStyle.None
                ? LineViewerStyle.FullRow : LineViewerStyle.None;
            OnSettingsChanged();
        }

        private void miBracketMatchingStyle_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.BracketMatchingStyle = editor.BracketMatchingStyle == BracketMatchingStyle.After
                ? BracketMatchingStyle.Before : BracketMatchingStyle.After;
            OnSettingsChanged();
        }

        private void miEnableVirtualSpace_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.AllowCaretBeyondEOL = !editor.AllowCaretBeyondEOL;
            OnSettingsChanged();
        }

        private void miConvertTabsToSpaces_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor == null) return;
            editor.ConvertTabsToSpaces = !editor.ConvertTabsToSpaces;
            OnSettingsChanged();
        }

        private void miSetTabSize_Click(object sender, EventArgs e)
        {
            if (ActiveEditor != null)
            {
                string result = _editorSettings.TabIndent.ToString();
                if (MessageUtil.ShowPrompt("制表符大小", "请指定制表符大小：", ref result) == DialogResult.OK)
                {
                    int value;
                    if (result != null && int.TryParse(result, out value) && FormUtil.IsInRange(value, 1, 32))
                    {
                        ActiveEditor.TabIndent = value;
                    }
                }
            }
        }

        private void miSetFont_Click(object sender, EventArgs e)
        {
            TextEditorControl editor = ActiveEditor;
            if (editor != null)
            {
                fontDialog1.Font = editor.Font;
                if (fontDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    editor.Font = fontDialog1.Font;
                    OnSettingsChanged();
                }
            }
        }

        private void OnSettingsChanged()
        {
            this.miSplitWindow.Checked = ActiveEditor.IsSplited;
            this.miShowSpacesTabs.Checked = _editorSettings.ShowSpaces;
            this.miShowEOLMarkers.Checked = _editorSettings.ShowEOLMarker;
            this.miShowInvalidLines.Checked = _editorSettings.ShowInvalidLines;
            this.miHLCurRow.Checked = _editorSettings.LineViewerStyle == LineViewerStyle.FullRow;
            this.miBracketMatchingStyle.Checked = _editorSettings.BracketMatchingStyle == BracketMatchingStyle.After;
            this.miEnableVirtualSpace.Checked = _editorSettings.AllowCaretBeyondEOL;
            this.miShowLineNumbers.Checked = _editorSettings.ShowLineNumbers;
            this.miConvertTabsToSpaces.Checked = ActiveEditor.ConvertTabsToSpaces;
        }

        #endregion

        #region 方法

        private void InitForm(object sender)
        {
            ActiveEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");

            if (_editorSettings == null)
            {
                _editorSettings = ActiveEditor.TextEditorProperties;
                OnSettingsChanged();
            }
            else
                ActiveEditor.TextEditorProperties = _editorSettings;
        }

        #region 解析Sql语句
        static int DataIndex = 0;
        public void ShowExecResult(DataTable dt)
        {
            DataIndex++;

            TabPage tabPage = new TabPage();
            tabPage.Name = "TabPage" + DataIndex;
            tabPage.Text = "结果" + DataIndex;

            DataGridView dataGridView = new DataGridView();
            dataGridView.Name = "DataGridView" + DataIndex;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.DataSource = dt;
            tabPage.Controls.Add(dataGridView);

            this.tabControl1.TabPages.Add(tabPage);
            this.tabControl1.SelectedIndex = 1;
        }

        public void ShowExecLog(string log)
        {
            this.tabControl1.SelectedIndex = 0;
            this.txtLOG.AppendText(log);
        }

        public void ClearExecResult()
        {
            DataIndex = 0;

            if (this.tabControl1.TabCount > 1)
            {
                for (int i = 1; i <= this.tabControl1.TabCount; i++)
                {
                    this.tabControl1.TabPages.RemoveAt(1);
                }
            }

            this.txtLOG.Clear();
        }

        public void DoExec()
        {
            if (string.IsNullOrEmpty(this.Sql))
                return;

            if (this.Sql.Contains(";"))
            {
                string[] sqls = this.Sql.Split(';');
                foreach (string sql in sqls)
                {
                    DoExec(sql);
                }
            }
            else
            {
                DoExec(this.Sql);
            }
        }

        private void DoExec(string sql)
        {
            sql = sql.Trim();
            if (string.IsNullOrEmpty(sql))
                return;

            try
            {
                if (sql.ToLower().StartsWith("select"))
                {
                    DataTable dt = targetLink.DbOperate.GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ShowExecResult(dt);
                        ShowExecLog(string.Format("[Sql]\r\n{0}\r\n总行数:{1}\r\n\r\n", sql, dt.Rows.Count));
                    }
                    else
                    {
                        ShowExecLog(string.Format("[Sql]\r\n{0}\r\n总行数:{1}\r\n\r\n", sql, 0));
                    }
                }
                else
                {
                    targetLink.DbOperate.ExecuteSql(sql);
                    ShowExecLog(string.Format("[Sql]\r\n{0}\r\n\r\n", sql));
                }

                if (DataIndex > 0)
                    this.tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                ShowExecLog(string.Format("[Sql]\r\n{0}\r\n[错误]\r\n{1}\r\n\r\n", sql, ex.Message));
            }
        }
        #endregion
        #endregion
    }
}
