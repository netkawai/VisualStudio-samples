// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.220608.4

#pragma once
#ifndef WINRT_Microsoft_UI_Xaml_Markup_2_H
#define WINRT_Microsoft_UI_Xaml_Markup_2_H
#include "winrt/impl/Microsoft.UI.Xaml.1.h"
#include "winrt/impl/Windows.Foundation.1.h"
#include "winrt/impl/Windows.Foundation.Collections.1.h"
#include "winrt/impl/Windows.Storage.Streams.1.h"
#include "winrt/impl/Windows.UI.Xaml.Interop.1.h"
#include "winrt/impl/Microsoft.UI.Xaml.Markup.1.h"
WINRT_EXPORT namespace winrt::Microsoft::UI::Xaml::Markup
{
    struct XamlBinaryWriterErrorInformation
    {
        uint32_t InputStreamIndex;
        uint32_t LineNumber;
        uint32_t LinePosition;
    };
    inline bool operator==(XamlBinaryWriterErrorInformation const& left, XamlBinaryWriterErrorInformation const& right) noexcept
    {
        return left.InputStreamIndex == right.InputStreamIndex && left.LineNumber == right.LineNumber && left.LinePosition == right.LinePosition;
    }
    inline bool operator!=(XamlBinaryWriterErrorInformation const& left, XamlBinaryWriterErrorInformation const& right) noexcept
    {
        return !(left == right);
    }
    struct XmlnsDefinition
    {
        hstring XmlNamespace;
        hstring Namespace;
    };
    inline bool operator==(XmlnsDefinition const& left, XmlnsDefinition const& right) noexcept
    {
        return left.XmlNamespace == right.XmlNamespace && left.Namespace == right.Namespace;
    }
    inline bool operator!=(XmlnsDefinition const& left, XmlnsDefinition const& right) noexcept
    {
        return !(left == right);
    }
    struct __declspec(empty_bases) MarkupExtension : winrt::Microsoft::UI::Xaml::Markup::IMarkupExtension,
        impl::require<MarkupExtension, winrt::Microsoft::UI::Xaml::Markup::IMarkupExtensionOverrides>
    {
        MarkupExtension(std::nullptr_t) noexcept {}
        MarkupExtension(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::UI::Xaml::Markup::IMarkupExtension(ptr, take_ownership_from_abi) {}
        MarkupExtension();
    };
    struct __declspec(empty_bases) ProvideValueTargetProperty : winrt::Microsoft::UI::Xaml::Markup::IProvideValueTargetProperty
    {
        ProvideValueTargetProperty(std::nullptr_t) noexcept {}
        ProvideValueTargetProperty(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::UI::Xaml::Markup::IProvideValueTargetProperty(ptr, take_ownership_from_abi) {}
        ProvideValueTargetProperty();
    };
    struct __declspec(empty_bases) XamlBinaryWriter : winrt::Microsoft::UI::Xaml::Markup::IXamlBinaryWriter
    {
        XamlBinaryWriter(std::nullptr_t) noexcept {}
        XamlBinaryWriter(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::UI::Xaml::Markup::IXamlBinaryWriter(ptr, take_ownership_from_abi) {}
        static auto Write(param::vector<winrt::Windows::Storage::Streams::IRandomAccessStream> const& inputStreams, param::vector<winrt::Windows::Storage::Streams::IRandomAccessStream> const& outputStreams, winrt::Microsoft::UI::Xaml::Markup::IXamlMetadataProvider const& xamlMetadataProvider);
    };
    struct __declspec(empty_bases) XamlBindingHelper : winrt::Microsoft::UI::Xaml::Markup::IXamlBindingHelper
    {
        XamlBindingHelper(std::nullptr_t) noexcept {}
        XamlBindingHelper(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::UI::Xaml::Markup::IXamlBindingHelper(ptr, take_ownership_from_abi) {}
        [[nodiscard]] static auto DataTemplateComponentProperty();
        static auto GetDataTemplateComponent(winrt::Microsoft::UI::Xaml::DependencyObject const& element);
        static auto SetDataTemplateComponent(winrt::Microsoft::UI::Xaml::DependencyObject const& element, winrt::Microsoft::UI::Xaml::Markup::IDataTemplateComponent const& value);
        static auto SuspendRendering(winrt::Microsoft::UI::Xaml::UIElement const& target);
        static auto ResumeRendering(winrt::Microsoft::UI::Xaml::UIElement const& target);
        static auto ConvertValue(winrt::Windows::UI::Xaml::Interop::TypeName const& type, winrt::Windows::Foundation::IInspectable const& value);
        static auto SetPropertyFromString(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, param::hstring const& value);
        static auto SetPropertyFromBoolean(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, bool value);
        static auto SetPropertyFromChar16(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, char16_t value);
        static auto SetPropertyFromDateTime(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::DateTime const& value);
        static auto SetPropertyFromDouble(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, double value);
        static auto SetPropertyFromInt32(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, int32_t value);
        static auto SetPropertyFromUInt32(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, uint32_t value);
        static auto SetPropertyFromInt64(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, int64_t value);
        static auto SetPropertyFromUInt64(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, uint64_t value);
        static auto SetPropertyFromSingle(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, float value);
        static auto SetPropertyFromPoint(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::Point const& value);
        static auto SetPropertyFromRect(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::Rect const& value);
        static auto SetPropertyFromSize(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::Size const& value);
        static auto SetPropertyFromTimeSpan(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::TimeSpan const& value);
        static auto SetPropertyFromByte(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, uint8_t value);
        static auto SetPropertyFromUri(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::Uri const& value);
        static auto SetPropertyFromObject(winrt::Windows::Foundation::IInspectable const& dependencyObject, winrt::Microsoft::UI::Xaml::DependencyProperty const& propertyToSet, winrt::Windows::Foundation::IInspectable const& value);
    };
    struct __declspec(empty_bases) XamlMarkupHelper : winrt::Microsoft::UI::Xaml::Markup::IXamlMarkupHelper
    {
        XamlMarkupHelper(std::nullptr_t) noexcept {}
        XamlMarkupHelper(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::UI::Xaml::Markup::IXamlMarkupHelper(ptr, take_ownership_from_abi) {}
        static auto UnloadObject(winrt::Microsoft::UI::Xaml::DependencyObject const& element);
    };
    struct __declspec(empty_bases) XamlReader : winrt::Microsoft::UI::Xaml::Markup::IXamlReader
    {
        XamlReader(std::nullptr_t) noexcept {}
        XamlReader(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::UI::Xaml::Markup::IXamlReader(ptr, take_ownership_from_abi) {}
        static auto Load(param::hstring const& xaml);
        static auto LoadWithInitialTemplateValidation(param::hstring const& xaml);
    };
    template <typename D>
    class IMarkupExtensionOverridesT
    {
        D& shim() noexcept { return *static_cast<D*>(this); }
        D const& shim() const noexcept { return *static_cast<const D*>(this); }
    public:
        using IMarkupExtensionOverrides = winrt::Microsoft::UI::Xaml::Markup::IMarkupExtensionOverrides;
        auto ProvideValue() const;
        auto ProvideValue(winrt::Microsoft::UI::Xaml::IXamlServiceProvider const& serviceProvider) const;
    };
}
#endif
