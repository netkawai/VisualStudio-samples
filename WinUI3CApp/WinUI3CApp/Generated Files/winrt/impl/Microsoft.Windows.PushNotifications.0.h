// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.210806.1

#pragma once
#ifndef WINRT_Microsoft_Windows_PushNotifications_0_H
#define WINRT_Microsoft_Windows_PushNotifications_0_H
WINRT_EXPORT namespace winrt::Windows::ApplicationModel::Background
{
    struct BackgroundTaskCanceledEventHandler;
    struct BackgroundTaskDeferral;
}
WINRT_EXPORT namespace winrt::Windows::Foundation
{
    struct EventRegistrationToken;
    struct HResult;
    template <typename TResult, typename TProgress> struct __declspec(empty_bases) IAsyncOperationWithProgress;
    template <typename TSender, typename TResult> struct __declspec(empty_bases) TypedEventHandler;
    struct Uri;
}
WINRT_EXPORT namespace winrt::Microsoft::Windows::PushNotifications
{
    enum class PushNotificationChannelStatus : int32_t
    {
        InProgress = 0,
        InProgressRetry = 1,
        CompletedSuccess = 2,
        CompletedFailure = 3,
    };
    struct IPushNotificationChannel;
    struct IPushNotificationCreateChannelResult;
    struct IPushNotificationManager;
    struct IPushNotificationManagerStatics;
    struct IPushNotificationReceivedEventArgs;
    struct PushNotificationChannel;
    struct PushNotificationCreateChannelResult;
    struct PushNotificationManager;
    struct PushNotificationReceivedEventArgs;
    struct PushNotificationCreateChannelStatus;
}
namespace winrt::impl
{
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::IPushNotificationChannel>{ using type = interface_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::IPushNotificationCreateChannelResult>{ using type = interface_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager>{ using type = interface_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManagerStatics>{ using type = interface_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs>{ using type = interface_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::PushNotificationChannel>{ using type = class_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelResult>{ using type = class_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::PushNotificationManager>{ using type = class_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::PushNotificationReceivedEventArgs>{ using type = class_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::PushNotificationChannelStatus>{ using type = enum_category; };
    template <> struct category<winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelStatus>{ using type = struct_category<winrt::Microsoft::Windows::PushNotifications::PushNotificationChannelStatus, winrt::hresult, uint32_t>; };
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::PushNotificationChannel> = L"Microsoft.Windows.PushNotifications.PushNotificationChannel";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelResult> = L"Microsoft.Windows.PushNotifications.PushNotificationCreateChannelResult";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::PushNotificationManager> = L"Microsoft.Windows.PushNotifications.PushNotificationManager";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::PushNotificationReceivedEventArgs> = L"Microsoft.Windows.PushNotifications.PushNotificationReceivedEventArgs";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::PushNotificationChannelStatus> = L"Microsoft.Windows.PushNotifications.PushNotificationChannelStatus";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelStatus> = L"Microsoft.Windows.PushNotifications.PushNotificationCreateChannelStatus";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationChannel> = L"Microsoft.Windows.PushNotifications.IPushNotificationChannel";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationCreateChannelResult> = L"Microsoft.Windows.PushNotifications.IPushNotificationCreateChannelResult";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager> = L"Microsoft.Windows.PushNotifications.IPushNotificationManager";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManagerStatics> = L"Microsoft.Windows.PushNotifications.IPushNotificationManagerStatics";
    template <> inline constexpr auto& name_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs> = L"Microsoft.Windows.PushNotifications.IPushNotificationReceivedEventArgs";
    template <> inline constexpr guid guid_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationChannel>{ 0xDA28BBCB,0x7695,0x5D38,{ 0xAF,0x82,0xF3,0x0B,0x72,0xFE,0xF1,0xF6 } }; // DA28BBCB-7695-5D38-AF82-F30B72FEF1F6
    template <> inline constexpr guid guid_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationCreateChannelResult>{ 0x4DF3717F,0x5D33,0x56E9,{ 0xB3,0x81,0x1B,0x35,0x0C,0x95,0x72,0x2E } }; // 4DF3717F-5D33-56E9-B381-1B350C95722E
    template <> inline constexpr guid guid_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager>{ 0x902F4ABA,0xFF63,0x5DFE,{ 0xA8,0x8F,0x15,0xCC,0x6B,0xED,0x55,0xFF } }; // 902F4ABA-FF63-5DFE-A88F-15CC6BED55FF
    template <> inline constexpr guid guid_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManagerStatics>{ 0x71329470,0x1B55,0x58DC,{ 0xA0,0x0C,0x68,0xC2,0x6F,0x2D,0x8B,0xD9 } }; // 71329470-1B55-58DC-A00C-68C26F2D8BD9
    template <> inline constexpr guid guid_v<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs>{ 0xFBD4EC53,0xBB83,0x5495,{ 0x87,0x77,0xD3,0xCF,0x13,0xE4,0x29,0x9C } }; // FBD4EC53-BB83-5495-8777-D3CF13E4299C
    template <> struct default_interface<winrt::Microsoft::Windows::PushNotifications::PushNotificationChannel>{ using type = winrt::Microsoft::Windows::PushNotifications::IPushNotificationChannel; };
    template <> struct default_interface<winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelResult>{ using type = winrt::Microsoft::Windows::PushNotifications::IPushNotificationCreateChannelResult; };
    template <> struct default_interface<winrt::Microsoft::Windows::PushNotifications::PushNotificationManager>{ using type = winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager; };
    template <> struct default_interface<winrt::Microsoft::Windows::PushNotifications::PushNotificationReceivedEventArgs>{ using type = winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs; };
    template <> struct abi<winrt::Microsoft::Windows::PushNotifications::IPushNotificationChannel>
    {
        struct __declspec(novtable) type : inspectable_abi
        {
            virtual int32_t __stdcall get_Uri(void**) noexcept = 0;
            virtual int32_t __stdcall get_ExpirationTime(int64_t*) noexcept = 0;
            virtual int32_t __stdcall Close() noexcept = 0;
        };
    };
    template <> struct abi<winrt::Microsoft::Windows::PushNotifications::IPushNotificationCreateChannelResult>
    {
        struct __declspec(novtable) type : inspectable_abi
        {
            virtual int32_t __stdcall get_Channel(void**) noexcept = 0;
            virtual int32_t __stdcall get_ExtendedError(winrt::hresult*) noexcept = 0;
            virtual int32_t __stdcall get_Status(int32_t*) noexcept = 0;
        };
    };
    template <> struct abi<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager>
    {
        struct __declspec(novtable) type : inspectable_abi
        {
            virtual int32_t __stdcall Register() noexcept = 0;
            virtual int32_t __stdcall Unregister() noexcept = 0;
            virtual int32_t __stdcall UnregisterAll() noexcept = 0;
            virtual int32_t __stdcall CreateChannelAsync(winrt::guid, void**) noexcept = 0;
            virtual int32_t __stdcall add_PushReceived(void*, winrt::event_token*) noexcept = 0;
            virtual int32_t __stdcall remove_PushReceived(winrt::event_token) noexcept = 0;
        };
    };
    template <> struct abi<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManagerStatics>
    {
        struct __declspec(novtable) type : inspectable_abi
        {
            virtual int32_t __stdcall IsSupported(bool*) noexcept = 0;
            virtual int32_t __stdcall get_Default(void**) noexcept = 0;
        };
    };
    template <> struct abi<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs>
    {
        struct __declspec(novtable) type : inspectable_abi
        {
            virtual int32_t __stdcall get_Payload(uint32_t* __valueSize, uint8_t**) noexcept = 0;
            virtual int32_t __stdcall GetDeferral(void**) noexcept = 0;
            virtual int32_t __stdcall add_Canceled(void*, winrt::event_token*) noexcept = 0;
            virtual int32_t __stdcall remove_Canceled(winrt::event_token) noexcept = 0;
        };
    };
    template <typename D>
    struct consume_Microsoft_Windows_PushNotifications_IPushNotificationChannel
    {
        [[nodiscard]] WINRT_IMPL_AUTO(winrt::Windows::Foundation::Uri) Uri() const;
        [[nodiscard]] WINRT_IMPL_AUTO(winrt::Windows::Foundation::DateTime) ExpirationTime() const;
        WINRT_IMPL_AUTO(void) Close() const;
    };
    template <> struct consume<winrt::Microsoft::Windows::PushNotifications::IPushNotificationChannel>
    {
        template <typename D> using type = consume_Microsoft_Windows_PushNotifications_IPushNotificationChannel<D>;
    };
    template <typename D>
    struct consume_Microsoft_Windows_PushNotifications_IPushNotificationCreateChannelResult
    {
        [[nodiscard]] WINRT_IMPL_AUTO(winrt::Microsoft::Windows::PushNotifications::PushNotificationChannel) Channel() const;
        [[nodiscard]] WINRT_IMPL_AUTO(winrt::hresult) ExtendedError() const;
        [[nodiscard]] WINRT_IMPL_AUTO(winrt::Microsoft::Windows::PushNotifications::PushNotificationChannelStatus) Status() const;
    };
    template <> struct consume<winrt::Microsoft::Windows::PushNotifications::IPushNotificationCreateChannelResult>
    {
        template <typename D> using type = consume_Microsoft_Windows_PushNotifications_IPushNotificationCreateChannelResult<D>;
    };
    template <typename D>
    struct consume_Microsoft_Windows_PushNotifications_IPushNotificationManager
    {
        WINRT_IMPL_AUTO(void) Register() const;
        WINRT_IMPL_AUTO(void) Unregister() const;
        WINRT_IMPL_AUTO(void) UnregisterAll() const;
        WINRT_IMPL_AUTO(winrt::Windows::Foundation::IAsyncOperationWithProgress<winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelResult, winrt::Microsoft::Windows::PushNotifications::PushNotificationCreateChannelStatus>) CreateChannelAsync(winrt::guid const& remoteId) const;
        WINRT_IMPL_AUTO(winrt::event_token) PushReceived(winrt::Windows::Foundation::TypedEventHandler<winrt::Microsoft::Windows::PushNotifications::PushNotificationManager, winrt::Microsoft::Windows::PushNotifications::PushNotificationReceivedEventArgs> const& handler) const;
        using PushReceived_revoker = impl::event_revoker<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager, &impl::abi_t<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager>::remove_PushReceived>;
        [[nodiscard]] PushReceived_revoker PushReceived(auto_revoke_t, winrt::Windows::Foundation::TypedEventHandler<winrt::Microsoft::Windows::PushNotifications::PushNotificationManager, winrt::Microsoft::Windows::PushNotifications::PushNotificationReceivedEventArgs> const& handler) const;
        WINRT_IMPL_AUTO(void) PushReceived(winrt::event_token const& token) const noexcept;
    };
    template <> struct consume<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManager>
    {
        template <typename D> using type = consume_Microsoft_Windows_PushNotifications_IPushNotificationManager<D>;
    };
    template <typename D>
    struct consume_Microsoft_Windows_PushNotifications_IPushNotificationManagerStatics
    {
        WINRT_IMPL_AUTO(bool) IsSupported() const;
        [[nodiscard]] WINRT_IMPL_AUTO(winrt::Microsoft::Windows::PushNotifications::PushNotificationManager) Default() const;
    };
    template <> struct consume<winrt::Microsoft::Windows::PushNotifications::IPushNotificationManagerStatics>
    {
        template <typename D> using type = consume_Microsoft_Windows_PushNotifications_IPushNotificationManagerStatics<D>;
    };
    template <typename D>
    struct consume_Microsoft_Windows_PushNotifications_IPushNotificationReceivedEventArgs
    {
        [[nodiscard]] WINRT_IMPL_AUTO(com_array<uint8_t>) Payload() const;
        WINRT_IMPL_AUTO(winrt::Windows::ApplicationModel::Background::BackgroundTaskDeferral) GetDeferral() const;
        WINRT_IMPL_AUTO(winrt::event_token) Canceled(winrt::Windows::ApplicationModel::Background::BackgroundTaskCanceledEventHandler const& handler) const;
        using Canceled_revoker = impl::event_revoker<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs, &impl::abi_t<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs>::remove_Canceled>;
        [[nodiscard]] Canceled_revoker Canceled(auto_revoke_t, winrt::Windows::ApplicationModel::Background::BackgroundTaskCanceledEventHandler const& handler) const;
        WINRT_IMPL_AUTO(void) Canceled(winrt::event_token const& token) const noexcept;
    };
    template <> struct consume<winrt::Microsoft::Windows::PushNotifications::IPushNotificationReceivedEventArgs>
    {
        template <typename D> using type = consume_Microsoft_Windows_PushNotifications_IPushNotificationReceivedEventArgs<D>;
    };
    struct struct_Microsoft_Windows_PushNotifications_PushNotificationCreateChannelStatus
    {
        int32_t status;
        winrt::hresult extendedError;
        uint32_t retryCount;
    };
    template <> struct abi<Microsoft::Windows::PushNotifications::PushNotificationCreateChannelStatus>
    {
        using type = struct_Microsoft_Windows_PushNotifications_PushNotificationCreateChannelStatus;
    };
}
#endif
