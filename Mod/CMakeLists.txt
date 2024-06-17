cmake_minimum_required(VERSION 3.8)

add_custom_target(Mod ALL
	COMMAND dotnet build --configuration=${CMAKE_BUILD_TYPE}
	WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
)

add_custom_command(TARGET Mod POST_BUILD 
    COMMAND ${CMAKE_COMMAND} -E copy_directory ${CMAKE_CURRENT_SOURCE_DIR}/bin/${CMAKE_BUILD_TYPE} "${OutDir}/"
	COMMAND ${CMAKE_COMMAND} -E remove "${OutDir}/UndertaleModLib.dll"
	COMMAND ${CMAKE_COMMAND} -E remove "${OutDir}/UndertaleModLib.xml"
	COMMAND ${CMAKE_COMMAND} -E remove "${OutDir}/ImGUIGM.deps.json"
)
