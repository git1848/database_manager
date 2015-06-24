using DataBaseFront.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace DataBaseFront
{
    public static class Extensions_BaseForm
    {
        #region 窗口管理
        public static BaseForm GetActivatedForm(DockPanel dockPanel)
        {
            IDockContent[] documents = dockPanel.DocumentsToArray();

            foreach (IDockContent content in documents)
            {
                if (content.DockHandler.IsActivated)
                    return content as BaseForm;
            }
            return null;
        }

        public static BaseForm OpenUniqueWindow(this BaseForm newForm, string title, DockPanel dockPanel)
        {
            foreach (IDockContent content in dockPanel.Documents)
            {
                if (content.GetType().Equals(newForm.GetType()))
                {
                    content.DockHandler.Activate();
                    return content.DockHandler.Form as BaseForm;
                }
            }

            newForm.Text = title;
            newForm.Show(dockPanel);

            return newForm;
        }

        public static BaseForm OpenWindow(this BaseForm newForm, string title, DockPanel dockPanel)
        {
            newForm.Text = title;
            newForm.Show(dockPanel);

            return newForm;
        }
        #endregion
    }
}
