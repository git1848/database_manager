using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DataBaseFront
{
    public class FileUtil
    {
        public static string Open()
        {
            try
            {
                string sql = string.Empty;
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    Filter = "数据库脚本文件（*.sql）|*.sql|文本文件|*.txt|所有文件|*.*",
                    RestoreDirectory = true,
                    FilterIndex = 1
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8))
                    {
                        sql = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                return sql;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Save(string data)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "数据库脚本文件（*.sql）|*.sql",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        sw.Write(data);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
