using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppBlend2
{
    internal class AppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand TextEnterCommand { get; set; }

        public AppViewModel()
        {
            TextEnterCommand = new DelegateCommand<string>(TextEnterHandler);
        }

        public void TextEnterHandler(string text)
        {
            Console.WriteLine(text); 
        }

    }
}
