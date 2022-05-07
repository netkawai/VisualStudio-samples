using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace WinFormsPropertyGrid
{
    public class Stuff
    {
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string MultiLineProperty { get; set; }

        [DefaultValue(true)]
        public bool CheckedMark { get; set; }

        public enum SelectionPart
        {
            Choice1,
            Choice2,
            Choice3
        }

        [DefaultValue(SelectionPart.Choice1)]
        public SelectionPart Choice { get; set; }

    }
}
