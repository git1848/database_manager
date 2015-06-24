using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront
{
    [Serializable]
    public class ListItem
    {
        public ListItem()
        {
        }

        public ListItem(string text, object value, object tag)
        {
            this.Text = text;
            this.Value = value;
            this.Tag = tag;
        }

        public string Text { get; set; }

        public object Value { get; set; }

        public object Tag { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
