// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.210806.1

#pragma once
#ifndef WINRT_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_H
#define WINRT_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_H
#include "winrt/base.h"
static_assert(winrt::check_version(CPPWINRT_VERSION, "2.0.210806.1"), "Mismatched C++/WinRT headers.");
#define CPPWINRT_VERSION "2.0.210806.1"
#include "winrt/impl/Windows.Foundation.2.h"
#include "winrt/impl/Microsoft.Windows.ApplicationModel.WindowsAppRuntime.2.h"
namespace winrt::impl
{
    template <typename D> WINRT_IMPL_AUTO(bool) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentInitializeOptions<D>::ForceDeployment() const
    {
        bool value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentInitializeOptions)->get_ForceDeployment(&value));
        return value;
    }
    template <typename D> WINRT_IMPL_AUTO(void) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentInitializeOptions<D>::ForceDeployment(bool value) const
    {
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentInitializeOptions)->put_ForceDeployment(value));
    }
    template <typename D> WINRT_IMPL_AUTO(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentManagerStatics<D>::GetStatus() const
    {
        void* result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics)->GetStatus(&result));
        return winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult{ result, take_ownership_from_abi };
    }
    template <typename D> WINRT_IMPL_AUTO(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentManagerStatics<D>::Initialize() const
    {
        void* result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics)->Initialize(&result));
        return winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult{ result, take_ownership_from_abi };
    }
    template <typename D> WINRT_IMPL_AUTO(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentManagerStatics2<D>::Initialize(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentInitializeOptions const& deploymentInitializeOptions) const
    {
        void* result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics2)->Initialize(*(void**)(&deploymentInitializeOptions), &result));
        return winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult{ result, take_ownership_from_abi };
    }
    template <typename D> WINRT_IMPL_AUTO(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentStatus) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentResult<D>::Status() const
    {
        winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentStatus value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResult)->get_Status(reinterpret_cast<int32_t*>(&value)));
        return value;
    }
    template <typename D> WINRT_IMPL_AUTO(winrt::hresult) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentResult<D>::ExtendedError() const
    {
        winrt::hresult value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResult)->get_ExtendedError(put_abi(value)));
        return value;
    }
    template <typename D> WINRT_IMPL_AUTO(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult) consume_Microsoft_Windows_ApplicationModel_WindowsAppRuntime_IDeploymentResultFactory<D>::CreateInstance(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentStatus const& status, winrt::hresult const& extendedError) const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResultFactory)->CreateInstance(static_cast<int32_t>(status), impl::bind_in(extendedError), &value));
        return winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult{ value, take_ownership_from_abi };
    }
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentInitializeOptions> : produce_base<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentInitializeOptions>
    {
        int32_t __stdcall get_ForceDeployment(bool* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<bool>(this->shim().ForceDeployment());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall put_ForceDeployment(bool value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            this->shim().ForceDeployment(value);
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics> : produce_base<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics>
    {
        int32_t __stdcall GetStatus(void** result) noexcept final try
        {
            clear_abi(result);
            typename D::abi_guard guard(this->shim());
            *result = detach_from<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult>(this->shim().GetStatus());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall Initialize(void** result) noexcept final try
        {
            clear_abi(result);
            typename D::abi_guard guard(this->shim());
            *result = detach_from<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult>(this->shim().Initialize());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics2> : produce_base<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics2>
    {
        int32_t __stdcall Initialize(void* deploymentInitializeOptions, void** result) noexcept final try
        {
            clear_abi(result);
            typename D::abi_guard guard(this->shim());
            *result = detach_from<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult>(this->shim().Initialize(*reinterpret_cast<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentInitializeOptions const*>(&deploymentInitializeOptions)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResult> : produce_base<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResult>
    {
        int32_t __stdcall get_Status(int32_t* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentStatus>(this->shim().Status());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_ExtendedError(winrt::hresult* value) noexcept final try
        {
            zero_abi<winrt::hresult>(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::hresult>(this->shim().ExtendedError());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResultFactory> : produce_base<D, winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResultFactory>
    {
        int32_t __stdcall CreateInstance(int32_t status, winrt::hresult extendedError, void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult>(this->shim().CreateInstance(*reinterpret_cast<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentStatus const*>(&status), *reinterpret_cast<winrt::hresult const*>(&extendedError)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
}
WINRT_EXPORT namespace winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime
{
    inline DeploymentInitializeOptions::DeploymentInitializeOptions() :
        DeploymentInitializeOptions(impl::call_factory_cast<DeploymentInitializeOptions(*)(winrt::Windows::Foundation::IActivationFactory const&), DeploymentInitializeOptions>([](winrt::Windows::Foundation::IActivationFactory const& f) { return f.template ActivateInstance<DeploymentInitializeOptions>(); }))
    {
    }
    inline auto DeploymentManager::GetStatus()
    {
        return impl::call_factory_cast<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult(*)(IDeploymentManagerStatics const&), DeploymentManager, IDeploymentManagerStatics>([](IDeploymentManagerStatics const& f) { return f.GetStatus(); });
    }
    inline auto DeploymentManager::Initialize()
    {
        return impl::call_factory_cast<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult(*)(IDeploymentManagerStatics const&), DeploymentManager, IDeploymentManagerStatics>([](IDeploymentManagerStatics const& f) { return f.Initialize(); });
    }
    inline auto DeploymentManager::Initialize(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentInitializeOptions const& deploymentInitializeOptions)
    {
        return impl::call_factory<DeploymentManager, IDeploymentManagerStatics2>([&](IDeploymentManagerStatics2 const& f) { return f.Initialize(deploymentInitializeOptions); });
    }
    inline DeploymentResult::DeploymentResult(winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentStatus const& status, winrt::hresult const& extendedError) :
        DeploymentResult(impl::call_factory<DeploymentResult, IDeploymentResultFactory>([&](IDeploymentResultFactory const& f) { return f.CreateInstance(status, extendedError); }))
    {
    }
}
namespace std
{
#ifndef WINRT_LEAN_AND_MEAN
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentInitializeOptions> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentManagerStatics2> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResult> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::IDeploymentResultFactory> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentInitializeOptions> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentManager> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::DeploymentResult> : winrt::impl::hash_base {};
#endif
}
#endif
