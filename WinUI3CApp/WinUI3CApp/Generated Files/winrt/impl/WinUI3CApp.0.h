// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.210806.1

#pragma once
#ifndef WINRT_WinUI3CApp_0_H
#define WINRT_WinUI3CApp_0_H
WINRT_EXPORT namespace winrt::Microsoft::UI::Xaml::Markup
{
    struct IXamlMetadataProvider;
}
WINRT_EXPORT namespace winrt::WinUI3CApp
{
    struct IMainWindow;
    struct MainWindow;
    struct XamlMetaDataProvider;
}
namespace winrt::impl
{
    template <> struct category<winrt::WinUI3CApp::IMainWindow>{ using type = interface_category; };
    template <> struct category<winrt::WinUI3CApp::MainWindow>{ using type = class_category; };
    template <> struct category<winrt::WinUI3CApp::XamlMetaDataProvider>{ using type = class_category; };
    template <> inline constexpr auto& name_v<winrt::WinUI3CApp::MainWindow> = L"WinUI3CApp.MainWindow";
    template <> inline constexpr auto& name_v<winrt::WinUI3CApp::XamlMetaDataProvider> = L"WinUI3CApp.XamlMetaDataProvider";
    template <> inline constexpr auto& name_v<winrt::WinUI3CApp::IMainWindow> = L"WinUI3CApp.IMainWindow";
    template <> inline constexpr guid guid_v<winrt::WinUI3CApp::IMainWindow>{ 0x04D11C1A,0x3E80,0x572F,{ 0xBA,0x2A,0x12,0xC9,0xE8,0xE9,0xEA,0x8B } }; // 04D11C1A-3E80-572F-BA2A-12C9E8E9EA8B
    template <> struct default_interface<winrt::WinUI3CApp::MainWindow>{ using type = winrt::WinUI3CApp::IMainWindow; };
    template <> struct default_interface<winrt::WinUI3CApp::XamlMetaDataProvider>{ using type = winrt::Microsoft::UI::Xaml::Markup::IXamlMetadataProvider; };
    template <> struct abi<winrt::WinUI3CApp::IMainWindow>
    {
        struct __declspec(novtable) type : inspectable_abi
        {
            virtual int32_t __stdcall get_MyProperty(int32_t*) noexcept = 0;
            virtual int32_t __stdcall put_MyProperty(int32_t) noexcept = 0;
        };
    };
    template <typename D>
    struct consume_WinUI3CApp_IMainWindow
    {
        [[nodiscard]] WINRT_IMPL_AUTO(int32_t) MyProperty() const;
        WINRT_IMPL_AUTO(void) MyProperty(int32_t value) const;
    };
    template <> struct consume<winrt::WinUI3CApp::IMainWindow>
    {
        template <typename D> using type = consume_WinUI3CApp_IMainWindow<D>;
    };
}
#endif
