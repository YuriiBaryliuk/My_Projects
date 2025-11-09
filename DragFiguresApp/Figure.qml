import QtQuick
import QtQuick.Controls

Image{

    MyConstants{id:myConstants}

    id: firstRectangle
    property point backPoint: Qt.point(myConstants.figure_Xposition, myConstants.figure_YpositionFirst)
    property point newPoint: Qt.point(myConstants.dropFigure_XPosition, myConstants.figure_YpositionFirst)
    property bool caught: false
    property int value: 0
    property int gradeValue: 0
    x:myConstants.figure_Xposition
    y:myConstants.figure_YpositionFirst
    source: "qrc:/images/Carrot.png"
    width: myConstants.figure_width
    height: myConstants.figure_height
    Drag.hotSpot.x: firstRectangle.width/2
    Drag.hotSpot.y: firstRectangle.height/2
    z: firstDragArea.drag.active || firstDragArea.pressed ? 2 : 1

    Drag.active: firstDragArea.drag.active

    MouseArea{
        id: firstDragArea
        anchors.fill: parent
        drag.target: parent

        onReleased:
            if(!firstRectangle.caught){
                firstRectangle.x = firstRectangle.backPoint.x
                firstRectangle.y = firstRectangle.backPoint.y
            }
            else{
                firstRectangle.x = firstRectangle.newPoint.x
                firstRectangle.y = firstRectangle.newPoint.y
                if(!firstRectangle.gradeValue)
                    firstRectangle.gradeValue+=1
            }
    }
}
