using DataBaseFront.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DataBaseFront
{
    public static class FormUtil
    {
        public static void CreateFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
        }

        public static List<string> GetTemplateNames(string path)
        {
            List<string> list = new List<string>();
            string[] files = Directory.GetFiles(path, "*.tm");
            FileInfo fileInfo;
            foreach (string file in files)
            {
                fileInfo = new FileInfo(file);
                list.Add(fileInfo.Name);
            }
            return list;
        }

        public static void CopyData(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                FrmMain.MAIN.ShowTip = "复制的文本为空";
            }
            else
            {
                Clipboard.SetDataObject(text, true);
                FrmMain.MAIN.ShowTip = "已复制到粘贴板";
            }
        }

        public static int InRange(int x, int lo, int hi)
        {
            return x < lo ? lo : (x > hi ? hi : x);
        }

        public static bool IsInRange(int x, int lo, int hi)
        {
            return (x >= lo) && (x <= hi);
        }

        public static Color HalfMix(Color one, Color two)
        {
            return Color.FromArgb(
                (one.A + two.A) >> 1,
                (one.R + two.R) >> 1,
                (one.G + two.G) >> 1,
                (one.B + two.B) >> 1);
        }

        public static bool IsNull(object obj)
        {
            return obj == null || obj == DBNull.Value;
        }
    }
}
