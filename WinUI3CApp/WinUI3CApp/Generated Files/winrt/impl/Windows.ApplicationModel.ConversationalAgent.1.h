// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.220608.4

#pragma once
#ifndef WINRT_Windows_ApplicationModel_ConversationalAgent_1_H
#define WINRT_Windows_ApplicationModel_ConversationalAgent_1_H
#include "winrt/impl/Windows.ApplicationModel.ConversationalAgent.0.h"
WINRT_EXPORT namespace winrt::Windows::ApplicationModel::ConversationalAgent
{
    struct __declspec(empty_bases) IConversationalAgentSession :
        winrt::Windows::Foundation::IInspectable,
        impl::consume_t<IConversationalAgentSession>
    {
        IConversationalAgentSession(std::nullptr_t = nullptr) noexcept {}
        IConversationalAgentSession(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::Foundation::IInspectable(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) IConversationalAgentSessionInterruptedEventArgs :
        winrt::Windows::Foundation::IInspectable,
        impl::consume_t<IConversationalAgentSessionInterruptedEventArgs>
    {
        IConversationalAgentSessionInterruptedEventArgs(std::nullptr_t = nullptr) noexcept {}
        IConversationalAgentSessionInterruptedEventArgs(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::Foundation::IInspectable(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) IConversationalAgentSessionStatics :
        winrt::Windows::Foundation::IInspectable,
        impl::consume_t<IConversationalAgentSessionStatics>
    {
        IConversationalAgentSessionStatics(std::nullptr_t = nullptr) noexcept {}
        IConversationalAgentSessionStatics(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::Foundation::IInspectable(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) IConversationalAgentSignal :
        winrt::Windows::Foundation::IInspectable,
        impl::consume_t<IConversationalAgentSignal>
    {
        IConversationalAgentSignal(std::nullptr_t = nullptr) noexcept {}
        IConversationalAgentSignal(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::Foundation::IInspectable(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) IConversationalAgentSignalDetectedEventArgs :
        winrt::Windows::Foundation::IInspectable,
        impl::consume_t<IConversationalAgentSignalDetectedEventArgs>
    {
        IConversationalAgentSignalDetectedEventArgs(std::nullptr_t = nullptr) noexcept {}
        IConversationalAgentSignalDetectedEventArgs(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::Foundation::IInspectable(ptr, take_ownership_from_abi) {}
    };
    struct __declspec(empty_bases) IConversationalAgentSystemStateChangedEventArgs :
        winrt::Windows::Foundation::IInspectable,
        impl::consume_t<IConversationalAgentSystemStateChangedEventArgs>
    {
        IConversationalAgentSystemStateChangedEventArgs(std::nullptr_t = nullptr) noexcept {}
        IConversationalAgentSystemStateChangedEventArgs(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::Foundation::IInspectable(ptr, take_ownership_from_abi) {}
    };
}
#endif
