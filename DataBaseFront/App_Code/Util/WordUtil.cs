using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Word = Microsoft.Office.Interop.Word;

namespace DataBaseFront.Util
{
    public class WordUtil
    {
        #region - 属性 -
        private Word._Application oWord = null;
        private Word._Document oDoc { get; set; }
        private object Nothing = System.Reflection.Missing.Value;
        public enum Orientation
        {
            横板,
            竖板
        }
        public enum Alignment
        {
            左对齐,
            居中,
            右对齐
        }
        #endregion

        #region - 添加文档 -

        #region - 创建并打开一个空的word文档进行编辑 -
        public void OpenNewWordFileToEdit()
        {
            oDoc = oWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        }
        #endregion

        #endregion

        #region - 创建新Word -
        public bool CreateWord(bool isVisible)
        {
            try
            {
                oWord = new Word.Application();
                oWord.Visible = isVisible;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CreateWord()
        {
            return CreateWord(false);
        }
        #endregion

        #region - 打开文档 -
        public bool Open(string filePath, bool isVisible)
        {
            try
            {
                oWord.Visible = isVisible;

                object path = filePath;
                oDoc = oWord.Documents.Open(ref path,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 插入表格 -
        public bool InsertTable(DataTable dt, bool haveBorder, double[] colWidths)
        {
            try
            {
                object Nothing = System.Reflection.Missing.Value;

                int lenght = oDoc.Characters.Count - 1;
                object start = lenght;
                object end = lenght;

                //表格起始坐标
                Word.Range tableLocation = oDoc.Range(ref start, ref end);

                //添加Word表格     
                Word.Table table = oDoc.Tables.Add(tableLocation, dt.Rows.Count, dt.Columns.Count, ref Nothing, ref Nothing);

                if (colWidths != null)
                {
                    for (int i = 0; i < colWidths.Length; i++)
                    {
                        table.Columns[i + 1].Width = (float)(28.5F * colWidths[i]);
                    }
                }

                ///设置TABLE的样式
                table.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAtLeast;
                table.Rows.Height = oWord.CentimetersToPoints(float.Parse("0.8"));
                table.Range.Font.Size = 10.5F;
                table.Range.Font.Name = "宋体";
                table.Range.Font.Bold = 0;
                table.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                table.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                if (haveBorder == true)
                {
                    //设置外框样式
                    table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    //样式设置结束
                }

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        table.Cell(row + 1, col + 1).Range.Text = dt.Rows[row][col].ToString();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {

            }
        }
        public bool InsertTable(DataTable dt, bool haveBorder)
        {
            return InsertTable(dt, haveBorder, null);
        }
        public bool InsertTable(DataTable dt)
        {
            return InsertTable(dt, false, null);
        }
        #endregion

        #region - 插入文本 -
        public bool InsertText(string strText, System.Drawing.Font font, Alignment alignment, bool isAftre)
        {
            try
            {
                Word.Range rng = oDoc.Content;

                int lenght = oDoc.Characters.Count - 1;
                object start = lenght;
                object end = lenght;

                rng = oDoc.Range(ref start, ref end);

                if (isAftre == true)
                {
                    strText += "\r\n";
                }

                rng.Text = strText;

                rng.Font.Name = font.Name;
                rng.Font.Size = font.Size;
                if (font.Style == FontStyle.Bold) { rng.Font.Bold = 1; } //设置单元格中字体为粗体

                SetAlignment(rng, alignment);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InsertText(string strText)
        {
            return InsertText(strText, new System.Drawing.Font("宋体", 10.5F, FontStyle.Bold), Alignment.左对齐, false);
        }
        #endregion

        #region - 设置对齐方式 -
        private Word.WdParagraphAlignment SetAlignment(Word.Range rng, Alignment alignment)
        {
            rng.ParagraphFormat.Alignment = SetAlignment(alignment);
            return SetAlignment(alignment);
        }
        private Word.WdParagraphAlignment SetAlignment(Alignment alignment)
        {
            if (alignment == Alignment.居中)
            {
                return Word.WdParagraphAlignment.wdAlignParagraphCenter;
            }
            else if (alignment == Alignment.左对齐)
            {
                return Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }
            else
            { return Word.WdParagraphAlignment.wdAlignParagraphRight; }
        }
        #endregion

        #region - 页面设置 -
        public void SetPage(Orientation orientation, double width, double height, double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            oDoc.PageSetup.PageWidth = oWord.CentimetersToPoints((float)width);
            oDoc.PageSetup.PageHeight = oWord.CentimetersToPoints((float)height);

            if (orientation == Orientation.横板)
            {
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            }

            oDoc.PageSetup.TopMargin = (float)(topMargin * 25);//上边距 
            oDoc.PageSetup.LeftMargin = (float)(leftMargin * 25);//左边距 
            oDoc.PageSetup.RightMargin = (float)(rightMargin * 25);//右边距 
            oDoc.PageSetup.BottomMargin = (float)(bottomMargin * 25);//下边距
        }
        public void SetPage(Orientation orientation, double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            SetPage(orientation, 21, 29.7, topMargin, leftMargin, rightMargin, bottomMargin);
        }
        public void SetPage(double topMargin, double leftMargin, double rightMargin, double bottomMargin)
        {
            SetPage(Orientation.竖板, 21, 29.7, topMargin, leftMargin, rightMargin, bottomMargin);
        }
        #endregion

        #region - 插入分页符 -
        public void InsertBreak()
        {
            Word.Paragraph para;
            para = oDoc.Content.Paragraphs.Add(ref Nothing);
            object pBreak = (int)Word.WdBreakType.wdSectionBreakNextPage;
            para.Range.InsertBreak(ref pBreak);
        }
        #endregion

        #region - 关闭当前文档 -
        public bool CloseDocument()
        {
            try
            {
                object doNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                oDoc.Close(ref doNotSaveChanges, ref Nothing, ref Nothing);
                oDoc = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 关闭程序 -
        public bool Quit()
        {
            try
            {
                object saveOption = Word.WdSaveOptions.wdDoNotSaveChanges;
                oWord.Quit(ref saveOption, ref Nothing, ref Nothing);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 保存文档 -
        public bool Save(string savePath)
        {
            return Save(savePath, false);
        }
        public bool Save(string savePath, bool isClose)
        {
            try
            {
                object fileName = savePath;
                oDoc.SaveAs(ref fileName, ref Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing, ref   Nothing);

                if (isClose)
                {
                    return CloseDocument();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 插入页脚 -
        public bool InsertPageFooter(string text, System.Drawing.Font font, WordUtil.Alignment alignment)
        {
            try
            {
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;//页脚 
                oWord.Selection.InsertAfter(text);
                GetWordFont(oWord.Selection.Font, font);

                SetAlignment(oWord.Selection.Range, alignment);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InsertPageFooterNumber(System.Drawing.Font font, WordUtil.Alignment alignment)
        {
            try
            {
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader;
                oWord.Selection.WholeStory();
                oWord.Selection.ParagraphFormat.Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument;

                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;//页脚 
                oWord.Selection.TypeText("第");

                object page = Word.WdFieldType.wdFieldPage;
                oWord.Selection.Fields.Add(oWord.Selection.Range, ref page, ref Nothing, ref Nothing);

                oWord.Selection.TypeText("页/共");
                object pages = Word.WdFieldType.wdFieldNumPages;

                oWord.Selection.Fields.Add(oWord.Selection.Range, ref pages, ref Nothing, ref Nothing);
                oWord.Selection.TypeText("页");

                GetWordFont(oWord.Selection.Font, font);
                SetAlignment(oWord.Selection.Range, alignment);
                oWord.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region - 字体格式设定 -
        public void GetWordFont(Word.Font wordFont, System.Drawing.Font font)
        {
            wordFont.Name = font.Name;
            wordFont.Size = font.Size;
            if (font.Bold) { wordFont.Bold = 1; }
            if (font.Italic) { wordFont.Italic = 1; }
            if (font.Underline == true)
            {
                wordFont.Underline = Word.WdUnderline.wdUnderlineNone;
            }
            wordFont.UnderlineColor = Word.WdColor.wdColorAutomatic;

            if (font.Strikeout)
            {
                wordFont.StrikeThrough = 1;//删除线
            }
        }
        #endregion

        #region - 获取Word中的颜色 -
        public Word.WdColor GetWordColor(Color c)
        {
            UInt32 R = 0x1, G = 0x100, B = 0x10000;
            return (Word.WdColor)(R * c.R + G * c.G + B * c.B);
        }
        #endregion
    }
}