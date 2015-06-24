using System.Windows.Forms;

namespace System
{
    public static class Extensions_Control
    {
        public static void InvokeIfNeeded<T>(this Control control, Action<T> action, T args)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, args);
            }
            else
            {
                action(args);
            }
        }
    }
}
