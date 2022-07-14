// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.220608.4

#pragma once
#ifndef WINRT_Windows_System_UserProfile_H
#define WINRT_Windows_System_UserProfile_H
#include "winrt/base.h"
static_assert(winrt::check_version(CPPWINRT_VERSION, "2.0.220608.4"), "Mismatched C++/WinRT headers.");
#define CPPWINRT_VERSION "2.0.220608.4"
#include "winrt/Windows.System.h"
#include "winrt/impl/Windows.Foundation.2.h"
#include "winrt/impl/Windows.Foundation.Collections.2.h"
#include "winrt/impl/Windows.Globalization.2.h"
#include "winrt/impl/Windows.Storage.2.h"
#include "winrt/impl/Windows.System.2.h"
#include "winrt/impl/Windows.System.UserProfile.2.h"
namespace winrt::impl
{
    template <typename D> auto consume_Windows_System_UserProfile_IAdvertisingManagerForUser<D>::AdvertisingId() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAdvertisingManagerForUser)->get_AdvertisingId(&value));
        return hstring{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAdvertisingManagerForUser<D>::User() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAdvertisingManagerForUser)->get_User(&value));
        return winrt::Windows::System::User{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAdvertisingManagerStatics<D>::AdvertisingId() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAdvertisingManagerStatics)->get_AdvertisingId(&value));
        return hstring{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAdvertisingManagerStatics2<D>::GetForUser(winrt::Windows::System::User const& user) const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAdvertisingManagerStatics2)->GetForUser(*(void**)(&user), &value));
        return winrt::Windows::System::UserProfile::AdvertisingManagerForUser{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAssignedAccessSettings<D>::IsEnabled() const
    {
        bool value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAssignedAccessSettings)->get_IsEnabled(&value));
        return value;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAssignedAccessSettings<D>::IsSingleAppKioskMode() const
    {
        bool value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAssignedAccessSettings)->get_IsSingleAppKioskMode(&value));
        return value;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAssignedAccessSettings<D>::User() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAssignedAccessSettings)->get_User(&value));
        return winrt::Windows::System::User{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAssignedAccessSettingsStatics<D>::GetDefault() const
    {
        void* result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAssignedAccessSettingsStatics)->GetDefault(&result));
        return winrt::Windows::System::UserProfile::AssignedAccessSettings{ result, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IAssignedAccessSettingsStatics<D>::GetForUser(winrt::Windows::System::User const& user) const
    {
        void* result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IAssignedAccessSettingsStatics)->GetForUser(*(void**)(&user), &result));
        return winrt::Windows::System::UserProfile::AssignedAccessSettings{ result, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IDiagnosticsSettings<D>::CanUseDiagnosticsToTailorExperiences() const
    {
        bool value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IDiagnosticsSettings)->get_CanUseDiagnosticsToTailorExperiences(&value));
        return value;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IDiagnosticsSettings<D>::User() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IDiagnosticsSettings)->get_User(&value));
        return winrt::Windows::System::User{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IDiagnosticsSettingsStatics<D>::GetDefault() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IDiagnosticsSettingsStatics)->GetDefault(&value));
        return winrt::Windows::System::UserProfile::DiagnosticsSettings{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IDiagnosticsSettingsStatics<D>::GetForUser(winrt::Windows::System::User const& user) const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IDiagnosticsSettingsStatics)->GetForUser(*(void**)(&user), &value));
        return winrt::Windows::System::UserProfile::DiagnosticsSettings{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IFirstSignInSettingsStatics<D>::GetDefault() const
    {
        void* result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IFirstSignInSettingsStatics)->GetDefault(&result));
        return winrt::Windows::System::UserProfile::FirstSignInSettings{ result, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::User() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_User(&value));
        return winrt::Windows::System::User{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::Calendars() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_Calendars(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::Clocks() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_Clocks(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::Currencies() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_Currencies(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::Languages() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_Languages(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::HomeGeographicRegion() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_HomeGeographicRegion(&value));
        return hstring{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesForUser<D>::WeekStartsOn() const
    {
        winrt::Windows::Globalization::DayOfWeek value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser)->get_WeekStartsOn(reinterpret_cast<int32_t*>(&value)));
        return value;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics<D>::Calendars() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics)->get_Calendars(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics<D>::Clocks() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics)->get_Clocks(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics<D>::Currencies() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics)->get_Currencies(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics<D>::Languages() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics)->get_Languages(&value));
        return winrt::Windows::Foundation::Collections::IVectorView<hstring>{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics<D>::HomeGeographicRegion() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics)->get_HomeGeographicRegion(&value));
        return hstring{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics<D>::WeekStartsOn() const
    {
        winrt::Windows::Globalization::DayOfWeek value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics)->get_WeekStartsOn(reinterpret_cast<int32_t*>(&value)));
        return value;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics2<D>::TrySetHomeGeographicRegion(param::hstring const& region) const
    {
        bool result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics2)->TrySetHomeGeographicRegion(*(void**)(&region), &result));
        return result;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics2<D>::TrySetLanguages(param::iterable<hstring> const& languageTags) const
    {
        bool result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics2)->TrySetLanguages(*(void**)(&languageTags), &result));
        return result;
    }
    template <typename D> auto consume_Windows_System_UserProfile_IGlobalizationPreferencesStatics3<D>::GetForUser(winrt::Windows::System::User const& user) const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics3)->GetForUser(*(void**)(&user), &value));
        return winrt::Windows::System::UserProfile::GlobalizationPreferencesForUser{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IUserProfilePersonalizationSettings<D>::TrySetLockScreenImageAsync(winrt::Windows::Storage::StorageFile const& imageFile) const
    {
        void* operation{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettings)->TrySetLockScreenImageAsync(*(void**)(&imageFile), &operation));
        return winrt::Windows::Foundation::IAsyncOperation<bool>{ operation, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IUserProfilePersonalizationSettings<D>::TrySetWallpaperImageAsync(winrt::Windows::Storage::StorageFile const& imageFile) const
    {
        void* operation{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettings)->TrySetWallpaperImageAsync(*(void**)(&imageFile), &operation));
        return winrt::Windows::Foundation::IAsyncOperation<bool>{ operation, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IUserProfilePersonalizationSettingsStatics<D>::Current() const
    {
        void* value{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettingsStatics)->get_Current(&value));
        return winrt::Windows::System::UserProfile::UserProfilePersonalizationSettings{ value, take_ownership_from_abi };
    }
    template <typename D> auto consume_Windows_System_UserProfile_IUserProfilePersonalizationSettingsStatics<D>::IsSupported() const
    {
        bool result{};
        check_hresult(WINRT_IMPL_SHIM(winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettingsStatics)->IsSupported(&result));
        return result;
    }
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IAdvertisingManagerForUser> : produce_base<D, winrt::Windows::System::UserProfile::IAdvertisingManagerForUser>
    {
        int32_t __stdcall get_AdvertisingId(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<hstring>(this->shim().AdvertisingId());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_User(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::User>(this->shim().User());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IAdvertisingManagerStatics> : produce_base<D, winrt::Windows::System::UserProfile::IAdvertisingManagerStatics>
    {
        int32_t __stdcall get_AdvertisingId(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<hstring>(this->shim().AdvertisingId());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IAdvertisingManagerStatics2> : produce_base<D, winrt::Windows::System::UserProfile::IAdvertisingManagerStatics2>
    {
        int32_t __stdcall GetForUser(void* user, void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::UserProfile::AdvertisingManagerForUser>(this->shim().GetForUser(*reinterpret_cast<winrt::Windows::System::User const*>(&user)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IAssignedAccessSettings> : produce_base<D, winrt::Windows::System::UserProfile::IAssignedAccessSettings>
    {
        int32_t __stdcall get_IsEnabled(bool* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<bool>(this->shim().IsEnabled());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_IsSingleAppKioskMode(bool* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<bool>(this->shim().IsSingleAppKioskMode());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_User(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::User>(this->shim().User());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IAssignedAccessSettingsStatics> : produce_base<D, winrt::Windows::System::UserProfile::IAssignedAccessSettingsStatics>
    {
        int32_t __stdcall GetDefault(void** result) noexcept final try
        {
            clear_abi(result);
            typename D::abi_guard guard(this->shim());
            *result = detach_from<winrt::Windows::System::UserProfile::AssignedAccessSettings>(this->shim().GetDefault());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall GetForUser(void* user, void** result) noexcept final try
        {
            clear_abi(result);
            typename D::abi_guard guard(this->shim());
            *result = detach_from<winrt::Windows::System::UserProfile::AssignedAccessSettings>(this->shim().GetForUser(*reinterpret_cast<winrt::Windows::System::User const*>(&user)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IDiagnosticsSettings> : produce_base<D, winrt::Windows::System::UserProfile::IDiagnosticsSettings>
    {
        int32_t __stdcall get_CanUseDiagnosticsToTailorExperiences(bool* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<bool>(this->shim().CanUseDiagnosticsToTailorExperiences());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_User(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::User>(this->shim().User());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IDiagnosticsSettingsStatics> : produce_base<D, winrt::Windows::System::UserProfile::IDiagnosticsSettingsStatics>
    {
        int32_t __stdcall GetDefault(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::UserProfile::DiagnosticsSettings>(this->shim().GetDefault());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall GetForUser(void* user, void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::UserProfile::DiagnosticsSettings>(this->shim().GetForUser(*reinterpret_cast<winrt::Windows::System::User const*>(&user)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IFirstSignInSettings> : produce_base<D, winrt::Windows::System::UserProfile::IFirstSignInSettings>
    {
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IFirstSignInSettingsStatics> : produce_base<D, winrt::Windows::System::UserProfile::IFirstSignInSettingsStatics>
    {
        int32_t __stdcall GetDefault(void** result) noexcept final try
        {
            clear_abi(result);
            typename D::abi_guard guard(this->shim());
            *result = detach_from<winrt::Windows::System::UserProfile::FirstSignInSettings>(this->shim().GetDefault());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser> : produce_base<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser>
    {
        int32_t __stdcall get_User(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::User>(this->shim().User());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Calendars(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Calendars());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Clocks(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Clocks());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Currencies(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Currencies());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Languages(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Languages());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_HomeGeographicRegion(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<hstring>(this->shim().HomeGeographicRegion());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_WeekStartsOn(int32_t* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Globalization::DayOfWeek>(this->shim().WeekStartsOn());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics> : produce_base<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics>
    {
        int32_t __stdcall get_Calendars(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Calendars());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Clocks(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Clocks());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Currencies(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Currencies());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_Languages(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Foundation::Collections::IVectorView<hstring>>(this->shim().Languages());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_HomeGeographicRegion(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<hstring>(this->shim().HomeGeographicRegion());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall get_WeekStartsOn(int32_t* value) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::Globalization::DayOfWeek>(this->shim().WeekStartsOn());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics2> : produce_base<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics2>
    {
        int32_t __stdcall TrySetHomeGeographicRegion(void* region, bool* result) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *result = detach_from<bool>(this->shim().TrySetHomeGeographicRegion(*reinterpret_cast<hstring const*>(&region)));
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall TrySetLanguages(void* languageTags, bool* result) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *result = detach_from<bool>(this->shim().TrySetLanguages(*reinterpret_cast<winrt::Windows::Foundation::Collections::IIterable<hstring> const*>(&languageTags)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics3> : produce_base<D, winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics3>
    {
        int32_t __stdcall GetForUser(void* user, void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::UserProfile::GlobalizationPreferencesForUser>(this->shim().GetForUser(*reinterpret_cast<winrt::Windows::System::User const*>(&user)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettings> : produce_base<D, winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettings>
    {
        int32_t __stdcall TrySetLockScreenImageAsync(void* imageFile, void** operation) noexcept final try
        {
            clear_abi(operation);
            typename D::abi_guard guard(this->shim());
            *operation = detach_from<winrt::Windows::Foundation::IAsyncOperation<bool>>(this->shim().TrySetLockScreenImageAsync(*reinterpret_cast<winrt::Windows::Storage::StorageFile const*>(&imageFile)));
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall TrySetWallpaperImageAsync(void* imageFile, void** operation) noexcept final try
        {
            clear_abi(operation);
            typename D::abi_guard guard(this->shim());
            *operation = detach_from<winrt::Windows::Foundation::IAsyncOperation<bool>>(this->shim().TrySetWallpaperImageAsync(*reinterpret_cast<winrt::Windows::Storage::StorageFile const*>(&imageFile)));
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
#ifndef WINRT_LEAN_AND_MEAN
    template <typename D>
    struct produce<D, winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettingsStatics> : produce_base<D, winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettingsStatics>
    {
        int32_t __stdcall get_Current(void** value) noexcept final try
        {
            clear_abi(value);
            typename D::abi_guard guard(this->shim());
            *value = detach_from<winrt::Windows::System::UserProfile::UserProfilePersonalizationSettings>(this->shim().Current());
            return 0;
        }
        catch (...) { return to_hresult(); }
        int32_t __stdcall IsSupported(bool* result) noexcept final try
        {
            typename D::abi_guard guard(this->shim());
            *result = detach_from<bool>(this->shim().IsSupported());
            return 0;
        }
        catch (...) { return to_hresult(); }
    };
#endif
}
WINRT_EXPORT namespace winrt::Windows::System::UserProfile
{
    inline auto AdvertisingManager::AdvertisingId()
    {
        return impl::call_factory_cast<hstring(*)(IAdvertisingManagerStatics const&), AdvertisingManager, IAdvertisingManagerStatics>([](IAdvertisingManagerStatics const& f) { return f.AdvertisingId(); });
    }
    inline auto AdvertisingManager::GetForUser(winrt::Windows::System::User const& user)
    {
        return impl::call_factory<AdvertisingManager, IAdvertisingManagerStatics2>([&](IAdvertisingManagerStatics2 const& f) { return f.GetForUser(user); });
    }
    inline auto AssignedAccessSettings::GetDefault()
    {
        return impl::call_factory_cast<winrt::Windows::System::UserProfile::AssignedAccessSettings(*)(IAssignedAccessSettingsStatics const&), AssignedAccessSettings, IAssignedAccessSettingsStatics>([](IAssignedAccessSettingsStatics const& f) { return f.GetDefault(); });
    }
    inline auto AssignedAccessSettings::GetForUser(winrt::Windows::System::User const& user)
    {
        return impl::call_factory<AssignedAccessSettings, IAssignedAccessSettingsStatics>([&](IAssignedAccessSettingsStatics const& f) { return f.GetForUser(user); });
    }
    inline auto DiagnosticsSettings::GetDefault()
    {
        return impl::call_factory_cast<winrt::Windows::System::UserProfile::DiagnosticsSettings(*)(IDiagnosticsSettingsStatics const&), DiagnosticsSettings, IDiagnosticsSettingsStatics>([](IDiagnosticsSettingsStatics const& f) { return f.GetDefault(); });
    }
    inline auto DiagnosticsSettings::GetForUser(winrt::Windows::System::User const& user)
    {
        return impl::call_factory<DiagnosticsSettings, IDiagnosticsSettingsStatics>([&](IDiagnosticsSettingsStatics const& f) { return f.GetForUser(user); });
    }
    inline auto FirstSignInSettings::GetDefault()
    {
        return impl::call_factory_cast<winrt::Windows::System::UserProfile::FirstSignInSettings(*)(IFirstSignInSettingsStatics const&), FirstSignInSettings, IFirstSignInSettingsStatics>([](IFirstSignInSettingsStatics const& f) { return f.GetDefault(); });
    }
    inline auto GlobalizationPreferences::Calendars()
    {
        return impl::call_factory_cast<winrt::Windows::Foundation::Collections::IVectorView<hstring>(*)(IGlobalizationPreferencesStatics const&), GlobalizationPreferences, IGlobalizationPreferencesStatics>([](IGlobalizationPreferencesStatics const& f) { return f.Calendars(); });
    }
    inline auto GlobalizationPreferences::Clocks()
    {
        return impl::call_factory_cast<winrt::Windows::Foundation::Collections::IVectorView<hstring>(*)(IGlobalizationPreferencesStatics const&), GlobalizationPreferences, IGlobalizationPreferencesStatics>([](IGlobalizationPreferencesStatics const& f) { return f.Clocks(); });
    }
    inline auto GlobalizationPreferences::Currencies()
    {
        return impl::call_factory_cast<winrt::Windows::Foundation::Collections::IVectorView<hstring>(*)(IGlobalizationPreferencesStatics const&), GlobalizationPreferences, IGlobalizationPreferencesStatics>([](IGlobalizationPreferencesStatics const& f) { return f.Currencies(); });
    }
    inline auto GlobalizationPreferences::Languages()
    {
        return impl::call_factory_cast<winrt::Windows::Foundation::Collections::IVectorView<hstring>(*)(IGlobalizationPreferencesStatics const&), GlobalizationPreferences, IGlobalizationPreferencesStatics>([](IGlobalizationPreferencesStatics const& f) { return f.Languages(); });
    }
    inline auto GlobalizationPreferences::HomeGeographicRegion()
    {
        return impl::call_factory_cast<hstring(*)(IGlobalizationPreferencesStatics const&), GlobalizationPreferences, IGlobalizationPreferencesStatics>([](IGlobalizationPreferencesStatics const& f) { return f.HomeGeographicRegion(); });
    }
    inline auto GlobalizationPreferences::WeekStartsOn()
    {
        return impl::call_factory_cast<winrt::Windows::Globalization::DayOfWeek(*)(IGlobalizationPreferencesStatics const&), GlobalizationPreferences, IGlobalizationPreferencesStatics>([](IGlobalizationPreferencesStatics const& f) { return f.WeekStartsOn(); });
    }
    inline auto GlobalizationPreferences::TrySetHomeGeographicRegion(param::hstring const& region)
    {
        return impl::call_factory<GlobalizationPreferences, IGlobalizationPreferencesStatics2>([&](IGlobalizationPreferencesStatics2 const& f) { return f.TrySetHomeGeographicRegion(region); });
    }
    inline auto GlobalizationPreferences::TrySetLanguages(param::iterable<hstring> const& languageTags)
    {
        return impl::call_factory<GlobalizationPreferences, IGlobalizationPreferencesStatics2>([&](IGlobalizationPreferencesStatics2 const& f) { return f.TrySetLanguages(languageTags); });
    }
    inline auto GlobalizationPreferences::GetForUser(winrt::Windows::System::User const& user)
    {
        return impl::call_factory<GlobalizationPreferences, IGlobalizationPreferencesStatics3>([&](IGlobalizationPreferencesStatics3 const& f) { return f.GetForUser(user); });
    }
    inline auto UserProfilePersonalizationSettings::Current()
    {
        return impl::call_factory_cast<winrt::Windows::System::UserProfile::UserProfilePersonalizationSettings(*)(IUserProfilePersonalizationSettingsStatics const&), UserProfilePersonalizationSettings, IUserProfilePersonalizationSettingsStatics>([](IUserProfilePersonalizationSettingsStatics const& f) { return f.Current(); });
    }
    inline auto UserProfilePersonalizationSettings::IsSupported()
    {
        return impl::call_factory_cast<bool(*)(IUserProfilePersonalizationSettingsStatics const&), UserProfilePersonalizationSettings, IUserProfilePersonalizationSettingsStatics>([](IUserProfilePersonalizationSettingsStatics const& f) { return f.IsSupported(); });
    }
}
namespace std
{
#ifndef WINRT_LEAN_AND_MEAN
    template<> struct hash<winrt::Windows::System::UserProfile::IAdvertisingManagerForUser> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IAdvertisingManagerStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IAdvertisingManagerStatics2> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IAssignedAccessSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IAssignedAccessSettingsStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IDiagnosticsSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IDiagnosticsSettingsStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IFirstSignInSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IFirstSignInSettingsStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IGlobalizationPreferencesForUser> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics2> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IGlobalizationPreferencesStatics3> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::IUserProfilePersonalizationSettingsStatics> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::AdvertisingManager> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::AdvertisingManagerForUser> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::AssignedAccessSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::DiagnosticsSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::FirstSignInSettings> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::GlobalizationPreferences> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::GlobalizationPreferencesForUser> : winrt::impl::hash_base {};
    template<> struct hash<winrt::Windows::System::UserProfile::UserProfilePersonalizationSettings> : winrt::impl::hash_base {};
#endif
#ifdef __cpp_lib_format
#endif
}
#endif
