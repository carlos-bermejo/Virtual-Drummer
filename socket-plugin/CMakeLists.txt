cmake_minimum_required(VERSION 3.14.0)

option(SMTG_ENABLE_VST3_PLUGIN_EXAMPLES "Enable VST 3 Plug-in Examples" OFF)
option(SMTG_ENABLE_VST3_HOSTING_EXAMPLES "Enable VST 3 Hosting Examples" OFF)

set(CMAKE_OSX_DEPLOYMENT_TARGET 10.13 CACHE STRING "")

set(vst3sdk_SOURCE_DIR "D:/Archivos de Programa 2/vst3 sdk steinberg/VST_SDK/vst3sdk")
if(NOT vst3sdk_SOURCE_DIR)
    message(FATAL_ERROR "Path to VST3 SDK is empty!")
endif()

project(socket_plugin
    # This is your plug-in version number. Change it here only.
    # Version number symbols usable in C++ can be found in
    # source/version.h and ${PROJECT_BINARY_DIR}/projectversion.h.
    VERSION 1.0.0.0 
    DESCRIPTION "socket_plugin VST 3 Plug-in"
)

set(SMTG_VSTGUI_ROOT "${vst3sdk_SOURCE_DIR}")

add_subdirectory(${vst3sdk_SOURCE_DIR} ${PROJECT_BINARY_DIR}/vst3sdk)
smtg_enable_vst3_sdk()

smtg_add_vst3plugin(socket_plugin
    source/version.h
    source/plug_cids.h
    source/plug_processor.h
    source/plug_processor.cpp
    source/plug_controller.h
    source/plug_controller.cpp
    source/plug_entry.cpp
)

#- VSTGUI Wanted ----
if(SMTG_ENABLE_VSTGUI_SUPPORT)
    target_sources(socket_plugin
        PRIVATE
            resource/plug_editor.uidesc
    )
    target_link_libraries(socket_plugin
        PRIVATE
            vstgui_support
    )
    smtg_target_add_plugin_resources(socket_plugin
        RESOURCES
            "resource/plug_editor.uidesc"
    )
endif(SMTG_ENABLE_VSTGUI_SUPPORT)
# -------------------

smtg_target_add_plugin_snapshots (socket_plugin
    RESOURCES
        resource/E14C8B5BA50159B6AF44B15F5E925D59_snapshot.png
        resource/E14C8B5BA50159B6AF44B15F5E925D59_snapshot_2.0x.png
)

target_link_libraries(socket_plugin
    PRIVATE
        sdk
)

smtg_target_configure_version_file(socket_plugin)

if(SMTG_MAC)
    smtg_target_set_bundle(socket_plugin
        BUNDLE_IDENTIFIER com.carlos.socket-plugin
        COMPANY_NAME "Carlos Bermejo"
    )
    smtg_target_set_debug_executable(socket_plugin
        "/Applications/VST3PluginTestHost.app"
        "--pluginfolder;$(BUILT_PRODUCTS_DIR)"
    )
elseif(SMTG_WIN)
    target_sources(socket_plugin PRIVATE 
        resource/win32resource.rc
    )
    if(MSVC)
        set_property(DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR} PROPERTY VS_STARTUP_PROJECT socket_plugin)

        smtg_target_set_debug_executable(socket_plugin
            "$(ProgramW6432)/Steinberg/VST3PluginTestHost/VST3PluginTestHost.exe"
            "--pluginfolder \"$(OutDir)/\""
        )
    endif()
endif(SMTG_MAC)
