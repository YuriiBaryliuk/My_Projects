cmake_minimum_required(VERSION 3.16)

project(DragFigures VERSION 0.1 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 REQUIRED COMPONENTS Quick)

qt_standard_project_setup(REQUIRES 6.8)

qt_add_executable(appDragFigures
    main.cpp
    ${QML_RESOURCES}
)

qt_add_resources(QML_RESOURCES resources.qrc)

qt_add_qml_module(appDragFigures
    URI DragFigures
    VERSION 1.0
    QML_FILES
        QML_FILES
            Figure.qml
            DropAreas.qml
            Main.qml
            FigureShade.qml
            MyConstants.qml
            EndScreenButton.qml
        SOURCES
            figureclass.h
            figureclass.cpp
            SOURCES eventhandler.h eventhandler.cpp
)

# Qt for iOS sets MACOSX_BUNDLE_GUI_IDENTIFIER automatically since Qt 6.1.
# If you are developing for iOS or macOS you should consider setting an
# explicit, fixed bundle identifier manually though.
set_target_properties(appDragFigures PROPERTIES
#    MACOSX_BUNDLE_GUI_IDENTIFIER com.example.appDragFigures
    MACOSX_BUNDLE_BUNDLE_VERSION ${PROJECT_VERSION}
    MACOSX_BUNDLE_SHORT_VERSION_STRING ${PROJECT_VERSION_MAJOR}.${PROJECT_VERSION_MINOR}
    MACOSX_BUNDLE TRUE
    WIN32_EXECUTABLE TRUE
)

target_sources(appDragFigures PRIVATE ${QML_RESOURCES})

target_link_libraries(appDragFigures
    PRIVATE Qt6::Quick
)

include(GNUInstallDirs)
install(TARGETS appDragFigures
    BUNDLE DESTINATION .
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
)
