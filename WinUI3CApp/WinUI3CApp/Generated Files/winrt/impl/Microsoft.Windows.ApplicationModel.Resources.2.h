// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.220608.4

#pragma once
#ifndef WINRT_Microsoft_Windows_ApplicationModel_Resources_2_H
#define WINRT_Microsoft_Windows_ApplicationModel_Resources_2_H
#include "winrt/impl/Microsoft.Windows.ApplicationModel.Resources.1.h"
WINRT_EXPORT namespace winrt::Microsoft::Windows::ApplicationModel::Resources
{
    struct KnownResourceQualifierName
    {
        KnownResourceQualifierName() = delete;
        [[nodiscard]] static auto Contrast();
        [[nodiscard]] static auto Custom();
        [[nodiscard]] static auto DeviceFamily();
        [[nodiscard]] static auto HomeRegion();
        [[nodiscard]] static auto Language();
        [[nodiscard]] static auto LayoutDirection();
        [[nodiscard]] static auto Scale();
        [[nodiscard]] static auto TargetSize();
        [[nodiscard]] static auto Theme();
    };
    struct __declspec(empty_bases) ResourceCandidate : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceCandidate
    {
        ResourceCandidate(std::nullptr_t) noexcept {}
        ResourceCandidate(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceCandidate(ptr, take_ownership_from_abi) {}
        ResourceCandidate(winrt::Microsoft::Windows::ApplicationModel::Resources::ResourceCandidateKind const& kind, param::hstring const& data);
        explicit ResourceCandidate(array_view<uint8_t const> data);
    };
    struct __declspec(empty_bases) ResourceContext : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceContext,
        impl::require<ResourceContext, winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceContext2>
    {
        ResourceContext(std::nullptr_t) noexcept {}
        ResourceContext(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceContext(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) ResourceLoader : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceLoader
    {
        ResourceLoader(std::nullptr_t) noexcept {}
        ResourceLoader(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceLoader(ptr, take_ownership_from_abi) {}
        ResourceLoader();
        explicit ResourceLoader(param::hstring const& fileName);
        ResourceLoader(param::hstring const& fileName, param::hstring const& resourceMap);
        static auto GetDefaultResourceFilePath();
    };
    struct __declspec(empty_bases) ResourceManager : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceManager,
        impl::require<ResourceManager, winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceManager2>
    {
        ResourceManager(std::nullptr_t) noexcept {}
        ResourceManager(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceManager(ptr, take_ownership_from_abi) {}
        ResourceManager();
        explicit ResourceManager(param::hstring const& fileName);
    };
    struct __declspec(empty_bases) ResourceMap : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceMap
    {
        ResourceMap(std::nullptr_t) noexcept {}
        ResourceMap(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceMap(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) ResourceNotFoundEventArgs : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceNotFoundEventArgs
    {
        ResourceNotFoundEventArgs(std::nullptr_t) noexcept {}
        ResourceNotFoundEventArgs(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Microsoft::Windows::ApplicationModel::Resources::IResourceNotFoundEventArgs(ptr, take_ownership_from_abi) {}
    };
}
#endif
