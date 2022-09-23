#include "pch.h"
#include "MainWindow.xaml.h"
#if __has_include("MainWindow.g.cpp")
#include "MainWindow.g.cpp"
#endif


using namespace winrt;
using namespace Microsoft::UI::Xaml;
using namespace Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;
using namespace Windows::Globalization;
using namespace Windows::Media::Ocr;
using namespace Windows::Foundation;
using namespace Windows::Storage;
using namespace Windows::Storage::Pickers;
using namespace Windows::Storage::Streams;
using namespace Windows::Graphics::Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::WinUI3CApp::implementation
{
    std::wstring const this_app_name{ L"ToastAndCallback" };

    // Language for OCR.
    Windows::Globalization::Language ocrLanguage{ Windows::Globalization::Language(L"en") };


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

    fire_and_forget MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
    {
        myButton().Content(box_value(L"Clicked"));

        Calendar calendar;

        std::wstring_view strv = std::wstring_view(calendar.MonthAsSoloString());

        OutputTextBox().Text(strv);


        FileOpenPicker openPicker;
        openPicker.SuggestedStartLocation(PickerLocationId::DocumentsLibrary);
        openPicker.FileTypeFilter().Append(L".png");

        HWND hwnd = GetActiveWindow();
        openPicker.as<IInitializeWithWindow>()->Initialize(hwnd);

        StorageFile file = co_await openPicker.PickSingleFileAsync();
        if (file == nullptr)
        {
            co_return;
        }

        IRandomAccessStream fileStream = co_await file.OpenAsync(FileAccessMode::Read);
        BitmapDecoder decoder = co_await BitmapDecoder::CreateAsync(fileStream);
        SoftwareBitmap bitmap = co_await decoder.GetSoftwareBitmapAsync();


        OcrEngine ocrEngine = OcrEngine::TryCreateFromLanguage(ocrLanguage);

        if (ocrEngine != nullptr)
        {

            OcrResult result = co_await ocrEngine.RecognizeAsync(bitmap);
            OutputTextBox().Text(result.Text());
        }
        

        /*
        we need to copy Registry for AMID
            c# (Windows Community Toolkit)
            Win32AppInfo win32AppInfo = null;

            // If sparse
            if (DesktopBridgeHelpers.HasIdentity())
            {
                _win32Aumid = new ManifestHelper().GetAumidFromPackageManifest();
            }
            else
            {
                win32AppInfo = Win32AppInfo.Get();
                _win32Aumid = win32AppInfo.Aumid;
            }

            // Create and register activator
            var activatorType = CreateAndRegisterActivator();
        
        */
        co_return;
        // Right now does not work, I am missing something (see WpfApp1 sample)
        //create_toast();
    }
}
