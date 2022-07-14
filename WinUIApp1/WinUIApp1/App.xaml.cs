using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIApp1
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
#if false
        private static Window startupWindow;
        // Get the initial window created for this app
        // On UWP, this is simply Window.Current
        // On Desktop, multiple Windows may be created, and the StartupWindow may have already
        // been closed.
        public static Window StartupWindow
        {
            get
            {
                return startupWindow;
            }
        }

        public static UIElement appTitleBar = null;

        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        public void EnableSound(bool withSpatial = false)
        {
            ElementSoundPlayer.State = ElementSoundPlayerState.On;

            if (!withSpatial)
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
            else
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            IdleSynchronizer.Init();

            startupWindow = WindowHelper.CreateWindow();

#if DEBUG
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    this.DebugSettings.EnableFrameRateCounter = true;
            //}

            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.BindingFailed += DebugSettings_BindingFailed;
            }
#endif
            //draw into the title bar


            // args.UWPLaunchActivatedEventArgs throws an InvalidCastException in desktop apps.
            EnsureWindow();
        }

        private void DebugSettings_BindingFailed(object sender, BindingFailedEventArgs e)
        {

        }


        private async void EnsureWindow(IActivatedEventArgs args = null)
        {
            // No matter what our destination is, we're going to need control data loaded - let's knock that out now.
            // We'll never need to do this again.
            await ControlInfoDataSource.Instance.GetGroupsAsync();

            Frame rootFrame = GetRootFrame();

            ThemeHelper.Initialize();

            Type targetPageType = typeof(NewControlsPage);
            string targetPageArguments = string.Empty;

            if (args != null)
            {
                if (args.Kind == ActivationKind.Launch)
                {
                    if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                    {
                        try
                        {
                            await SuspensionManager.RestoreAsync();
                        }
                        catch (SuspensionManagerException)
                        {
                            //Something went wrong restoring state.
                            //Assume there is no state and continue
                        }
                    }

                    targetPageArguments = ((Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)args).Arguments;
                }
                else if (args.Kind == ActivationKind.Protocol)
                {
                    Match match;

                    string targetId = string.Empty;

                    switch (((ProtocolActivatedEventArgs)args).Uri?.AbsoluteUri)
                    {
                        case string s when IsMatching(s, "(/*)category/(.*)"):
                            targetId = match.Groups[2]?.ToString();
                            if (targetId == "AllControls")
                            {
                                targetPageType = typeof(AllControlsPage);
                            }
                            else if (targetId == "NewControls")
                            {
                                targetPageType = typeof(NewControlsPage);
                            }
                            else if (ControlInfoDataSource.Instance.Groups.Any(g => g.UniqueId == targetId))
                            {
                                targetPageType = typeof(SectionPage);
                            }
                            break;

                        case string s when IsMatching(s, "(/*)item/(.*)"):
                            targetId = match.Groups[2]?.ToString();
                            if (ControlInfoDataSource.Instance.Groups.Any(g => g.Items.Any(i => i.UniqueId == targetId)))
                            {
                                targetPageType = typeof(ItemPage);
                            }
                            break;
                    }

                    targetPageArguments = targetId;

                    bool IsMatching(string parent, string expression)
                    {
                        match = Regex.Match(parent, expression);
                        return match.Success;
                    }
                }
            }

            NavigationRootPage rootPage = StartupWindow.Content as NavigationRootPage;
            rootPage.Navigate(targetPageType, targetPageArguments);

            if (targetPageType == typeof(NewControlsPage))
            {
                ((Microsoft.UI.Xaml.Controls.NavigationViewItem)((NavigationRootPage)App.StartupWindow.Content).NavigationView.MenuItems[0]).IsSelected = true;
            }
            else if (targetPageType == typeof(ItemPage))
            {
                NavigationRootPage.GetForElement(this).EnsureNavigationSelection(targetPageArguments);
            }

            // Ensure the current window is active
            StartupWindow.Activate();
        }

        private Frame GetRootFrame()
        {
            Frame rootFrame;
            NavigationRootPage rootPage = StartupWindow.Content as NavigationRootPage;
            if (rootPage == null)
            {
                rootPage = new NavigationRootPage();
                rootFrame = (Frame)rootPage.FindName("rootFrame");
                if (rootFrame == null)
                {
                    throw new Exception("Root frame not found");
                }
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                rootFrame.NavigationFailed += OnNavigationFailed;

                StartupWindow.Content = rootPage;
            }
            else
            {
                rootFrame = (Frame)rootPage.FindName("rootFrame");
            }

            return rootFrame;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

#endif
    }
}
