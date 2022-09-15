using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAppTemplatePractice
{
    public class DialogTest : INotifyPropertyChanged
    {
        /// <summary>
        /// The content to host inside the dialog
        /// </summary>
        public Control Content { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
