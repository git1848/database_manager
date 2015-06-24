using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataBaseFront
{
    public class AppInit
    {
        #region Folder
        public static string S_RootFolder = Application.StartupPath;
        public static string S_TemplateFolder = Path.Combine(S_RootFolder, "Template");
        public static string S_TemplateDocFolder = Path.Combine(S_TemplateFolder, "Doc");
        public static string S_TemplateModelFolder = Path.Combine(S_TemplateFolder, "Model");
        public static string S_TemplateBuildFolder = Path.Combine(S_RootFolder, "Build");
        public static string S_HistoryFolder = Path.Combine(S_RootFolder, "History");
        public static string S_ConfigFolder = Path.Combine(S_RootFolder, "Config");
        #endregion

        #region File
        public static string S_HistoryPath = Path.Combine(S_HistoryFolder, "History.Bin");
        public static string S_DataTypeMapPath = Path.Combine(S_ConfigFolder, "DataTypeMap.xml");
        #endregion

        /// <summary>
        /// 初始化工程需要的文件夹
        /// </summary>
        public static void InitProjectFolder()
        {
            FormUtil.CreateFolder(S_TemplateFolder);
            FormUtil.CreateFolder(S_TemplateDocFolder);
            FormUtil.CreateFolder(S_TemplateModelFolder);
            FormUtil.CreateFolder(S_TemplateBuildFolder);
            FormUtil.CreateFolder(S_HistoryFolder);
            FormUtil.CreateFolder(S_ConfigFolder);
        }
    }
}
