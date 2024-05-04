//------------------------------------------------------------------------
// Copyright(c) 2024 Carlos Bermejo.
//------------------------------------------------------------------------

#pragma once

#include "pluginterfaces/base/funknown.h"
#include "pluginterfaces/vst/vsttypes.h"

namespace MyCompanyName {
//------------------------------------------------------------------------
static const Steinberg::FUID ksocket_pluginProcessorUID (0xE14C8B5B, 0xA50159B6, 0xAF44B15F, 0x5E925D59);
static const Steinberg::FUID ksocket_pluginControllerUID (0xACB17582, 0xB95F5A5A, 0xABAB5F0D, 0xBF0E4E1F);

#define socket_pluginVST3Category "Instrument"

//------------------------------------------------------------------------
} // namespace MyCompanyName
