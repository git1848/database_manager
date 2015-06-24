using System;
using System.Threading;
using System.Windows.Forms;
using DataBaseFront.UI;

namespace DataBaseFront
{
    public class Splasher
    {
        static FrmSplash splashForm = null;
        static Thread splashThread = null;

        static void ShowThread()
        {
            splashForm = new FrmSplash();

            Application.Run(splashForm);
        }

        public static void Show()
        {
            if (splashThread != null)
                return;

            splashThread = new Thread(new ThreadStart(Splasher.ShowThread));
            splashThread.IsBackground = true;
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();
        }

        public static void Close()
        {
            if (splashThread == null) return;
            if (splashForm == null) return;

            try
            {
                splashForm.Invoke(new MethodInvoker(splashForm.Close));
            }
            catch (Exception)
            {
            }
            splashThread = null;
            splashForm = null;
        }

        public static string Status
        {
            get
            {
                if (splashForm == null)
                {
                    throw new InvalidOperationException("Splash Form not on screen");
                }
                return splashForm.StatusInfo;
            }
            set
            {
                if (splashForm == null)
                {
                    return;
                }

                splashForm.StatusInfo = value;
            }
        }
    }
}
