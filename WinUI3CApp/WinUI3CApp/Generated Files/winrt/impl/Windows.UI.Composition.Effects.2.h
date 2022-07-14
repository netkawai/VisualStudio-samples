// WARNING: Please don't edit this file. It was generated by C++/WinRT v2.0.220608.4

#pragma once
#ifndef WINRT_Windows_UI_Composition_Effects_2_H
#define WINRT_Windows_UI_Composition_Effects_2_H
#include "winrt/impl/Windows.Graphics.Effects.1.h"
#include "winrt/impl/Windows.UI.Composition.Effects.1.h"
WINRT_EXPORT namespace winrt::Windows::UI::Composition::Effects
{
    struct __declspec(empty_bases) SceneLightingEffect : winrt::Windows::UI::Composition::Effects::ISceneLightingEffect,
        impl::require<SceneLightingEffect, winrt::Windows::Graphics::Effects::IGraphicsEffectSource, winrt::Windows::Graphics::Effects::IGraphicsEffect, winrt::Windows::UI::Composition::Effects::ISceneLightingEffect2>
    {
        SceneLightingEffect(std::nullptr_t) noexcept {}
        SceneLightingEffect(void* ptr, take_ownership_from_abi_t) noexcept : winrt::Windows::UI::Composition::Effects::ISceneLightingEffect(ptr, take_ownership_from_abi) {}
        SceneLightingEffect();
    };
}
#endif
