using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace EventsAndCommandsDemo
{
    public static class MyCustomCommands
    {
        private static RoutedUICommand StandUpCommand = new RoutedUICommand("Stand up!", "StandUp", typeof(MyCustomCommands));
        public static RoutedUICommand StandUp { get { return StandUpCommand; } }
    }
}
