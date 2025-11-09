import QtQuick
import QtQuick.Controls

Button{
    id:endScreenButton
    x:myConstants.window_width/2-endScreenButton.width/2
    y:myConstants.window_height*0.5
    width:100
    height:50
    visible: false
    z:0
    hoverEnabled: false

    background: Rectangle{
        color: endScreenButton.down ? "#FDEB9E" : "#000B58"
        border.color: "black"

        Text{
            id: buttonText
            anchors.centerIn: parent
            text:"Вийти"
            color:"#FDEB9E"
            font.pixelSize: 15
            font.family: "Comic Sans MS"
        }
    }

    property alias innerText: buttonText.text
}
