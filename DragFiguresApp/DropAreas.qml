import QtQuick
import QtQuick.Controls

Image{

    MyConstants{id:myConstants}

    id: firstDropArea
    property int value: 0
    x: myConstants.dropFigure_XPosition
    y: myConstants.figure_YpositionFirst
    source: "qrc:/images/Carrot_white.png"
    width: myConstants.figure_width
    height: myConstants.figure_height

    DropArea{
        anchors.fill: parent
        onEntered: {
            if(drag.source.value === firstDropArea.value)
                drag.source.caught = true
        }
        onExited: {
            if(drag.source.value === firstDropArea.value)
                drag.source.caught = false
        }
    }
}

