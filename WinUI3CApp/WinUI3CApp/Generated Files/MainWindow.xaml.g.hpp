﻿//------------------------------------------------------------------------------
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//------------------------------------------------------------------------------
#include "pch.h"
#include "MainWindow.xaml.h"

#pragma warning(push)
#pragma warning(disable: 4100) // unreferenced formal parameter

namespace winrt::WinUI3CApp::implementation
{
    using Application = ::winrt::Microsoft::UI::Xaml::Application;
    using ComponentResourceLocation = ::winrt::Microsoft::UI::Xaml::Controls::Primitives::ComponentResourceLocation;
    using DataTemplate = ::winrt::Microsoft::UI::Xaml::DataTemplate;
    using DependencyObject = ::winrt::Microsoft::UI::Xaml::DependencyObject;
    using DependencyProperty = ::winrt::Microsoft::UI::Xaml::DependencyProperty;
    using IComponentConnector = ::winrt::Microsoft::UI::Xaml::Markup::IComponentConnector;
    using Uri = ::winrt::Windows::Foundation::Uri;
    using XamlBindingHelper = ::winrt::Microsoft::UI::Xaml::Markup::XamlBindingHelper;

    template <typename D, typename ... I>
    void MainWindowT<D, I...>::InitializeComponent()
    {
        if (!_contentLoaded)
        {
            _contentLoaded = true;
            ::winrt::Windows::Foundation::Uri resourceLocator{ L"ms-appx:///MainWindow.xaml" };
            ::winrt::Microsoft::UI::Xaml::Application::LoadComponent(*this, resourceLocator, ComponentResourceLocation::Application);
        }
    }

    template <typename D, typename ... I>
    void MainWindowT<D, I...>::Connect(int32_t connectionId, IInspectable const& target)
    {
        switch (connectionId)
        {
        case 2:
            {
                auto targetElement = target.as<::winrt::Microsoft::UI::Xaml::Controls::Button>();
                this->myButton(targetElement);
                auto weakThis = ::winrt::make_weak<class_type>(*this);
                targetElement.Click([weakThis](::winrt::Windows::Foundation::IInspectable const& p0, ::winrt::Microsoft::UI::Xaml::RoutedEventArgs const& p1){
                    if (auto t = weakThis.get())
                    {
                        ::winrt::get_self<D>(t)->myButton_Click(p0, p1);
                    }
                });
            }
            break;
        case 3:
            {
                auto targetElement = target.as<::winrt::Microsoft::UI::Xaml::Controls::TextBox>();
                this->OutputTextBox(targetElement);
            }
            break;
        }
        _contentLoaded = true;
    }

    template <typename D, typename ... I>
    void MainWindowT<D, I...>::DisconnectUnloadedObject(int32_t)
    {
        throw ::winrt::hresult_invalid_argument { L"No unloadable objects to disconnect." };
    }

    template <typename D, typename ... I>
    void MainWindowT<D, I...>::UnloadObject(DependencyObject const&)
    {
        throw ::winrt::hresult_invalid_argument { L"No unloadable objects." };
    }


    template <typename D, typename... I>
    IComponentConnector MainWindowT<D, I...>::GetBindingConnector(int32_t, IInspectable const&)
    {
        return nullptr;
    }

    template struct MainWindowT<struct MainWindow>;
}


#pragma warning(pop)


