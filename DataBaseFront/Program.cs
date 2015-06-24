using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataBaseFront
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //异常处理
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //显示加载窗口
            Splasher.Show();

            //执行加载过程
            Program.DoStartup(args);

            //隐藏加载窗口
            Splasher.Close();
        }

        static void DoStartup(string[] args)
        {
            UI.FrmMain form = new UI.FrmMain();
            Application.Run(form);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            MessageUtil.ShowError(ex.Message);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs ex)
        {
            MessageUtil.ShowError(ex.Exception.Message);
        }
    }
}
