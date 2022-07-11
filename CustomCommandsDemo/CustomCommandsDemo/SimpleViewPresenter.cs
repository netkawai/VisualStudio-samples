using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CustomCommandsDemo
{
    public class SimpleViewPresenter
    {
        public SimpleViewPresenter()
        {
            Model = new SimpleViewPresentationModel();
        }

        public SimpleViewPresentationModel Model { get; set; }

        public void AddCommandHandler()
        {
            Model.CookDinnerCommand.CanExecuteTargets += CanExecuteHandler;
            Model.CookDinnerCommand.ExecuteTargets += ExecuteHandler;
        }

        bool CanExecuteHandler()
        {
            return true;
        }

        void ExecuteHandler(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
