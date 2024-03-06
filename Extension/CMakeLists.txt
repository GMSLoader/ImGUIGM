cmake_minimum_required(VERSION 3.8)

set(CMAKE_CXX_STANDARD 17)

add_library(Extension MODULE
    src/extension.cpp
    vendor/imgui/backends/imgui_impl_dx11.cpp
    vendor/imgui/backends/imgui_impl_win32.cpp
    vendor/imgui/imgui_demo.cpp
    vendor/imgui/imgui_draw.cpp
    vendor/imgui/imgui_tables.cpp
    vendor/imgui/imgui_widgets.cpp
    vendor/imgui/imgui.cpp
    vendor/imgui/misc/cpp/imgui_stdlib.cpp
)

target_include_directories(Extension PRIVATE vendor/imgui)

set(THREADS_PREFER_PTHREAD_FLAG ON)
find_package(Threads REQUIRED)
target_link_libraries(Extension PRIVATE "d3d11.lib" "dxgi.lib" "d3dcompiler.lib" "dwmapi.lib" "Comctl32.lib" Threads::Threads)

add_custom_command(TARGET Extension POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:Extension> "${OutDir}/"
)

add_compile_definitions(NDEBUG)