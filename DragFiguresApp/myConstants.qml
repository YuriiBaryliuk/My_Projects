import QtQuick
import QtQuick.Window

QtObject{
    id:myConstants
    readonly property int window_width: Screen.width
    readonly property int window_height: Screen.height
    readonly property int spacing: 30
    readonly property int figure_Xposition: Screen.width/4 - myConstants.figure_width/2
    readonly property int figure_YpositionFirst: myConstants.spacing
    readonly property int figure_YpositionSecond: myConstants.spacing+myConstants.figure_height
    readonly property int figure_YpositionThird: myConstants.spacing+(myConstants.figure_height)*2
    readonly property int dropFigure_XPosition: myConstants.figure_Xposition + Screen.width/2
    readonly property int figure_width: 200
    readonly property int figure_height: Screen.height/3 - myConstants.spacing*2
}
