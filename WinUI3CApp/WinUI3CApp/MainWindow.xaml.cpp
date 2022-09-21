#include "pch.h"
#include "MainWindow.xaml.h"
#if __has_include("MainWindow.g.cpp")
#include "MainWindow.g.cpp"
#endif

using namespace winrt;
using namespace Microsoft::UI::Xaml;
using namespace Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::WinUI3CApp::implementation
{
    std::wstring const this_app_name{ L"ToastAndCallback" };

    MainWindow::MainWindow()
    {
        InitializeComponent();
    }

    void create_toast()
    {
        XmlDocument xml;

        std::wstring toastPayload
        {
            LR"(
    <toast>
    <visual>
    <binding template='ToastGeneric'>
      <text>
This is a test notification
      </text>
    </binding>
  </visual>
</toast>)" };
        xml.LoadXml(toastPayload);

        ToastNotification toast{ xml };
        ToastNotifier notifier{ ToastNotificationManager::CreateToastNotifier(this_app_name) };
        notifier.Show(toast);
        ::Sleep(50); // Give the callback chance to display.
    }

    int32_t MainWindow::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void MainWindow::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
    {
        myButton().Content(box_value(L"Clicked"));


        // Right now does not work, I am missing something (see WpfApp1 sample)
        create_toast();
    }
}
