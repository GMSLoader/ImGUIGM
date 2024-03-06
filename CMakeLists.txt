cmake_minimum_required(VERSION 3.8)

project("ImGUIGM")

file(MAKE_DIRECTORY ImGUIGM)
set(OutDir "${CMAKE_SOURCE_DIR}/ImGUIGM")

add_subdirectory("Extension")
add_subdirectory("Mod")
