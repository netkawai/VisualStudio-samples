﻿using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;
using simpleView.Infrastructure;
namespace simpleView
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel(INavigationService navigator)
        {
            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
                this.IsMenuVisible = false;
            });
        }


        [Reactive] public bool IsMenuVisible { get; private set; }
        public ICommand Navigate { get; }
    }
}

