using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataBaseFront
{
    public static class Extensions_CheckedListBox
    {
        public static string GetCheckedItemsText(this CheckedListBox box)
        {
            string result = box.CheckedItems.Cast<object>().Aggregate(string.Empty, (current, checkedItem) => current + (box.GetItemText(checkedItem) + "/"));
            if (result.Length > 0) result = result.TrimEnd(new char[] { '/' });
            return result;
        }

        public static void SetCheckedItmsByNames(this CheckedListBox box, string[] names)
        {
            for (int i = 0; i < box.Items.Count; i++)
            {
                foreach (string name in names)
                {
                    if (box.GetItemText(box.Items[i]) == name)
                    {
                        box.SetItemChecked(i, true);
                    }
                }
            }
        }

        public static void SetCheckedItmsByNames(this CheckedListBox box, string names)
        {
            if (string.IsNullOrEmpty(names)) return;
            string[] name = names.Split(new char[] { '/' });
            SetCheckedItmsByNames(box, name);
        }
    }
}
