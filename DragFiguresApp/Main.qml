import QtQuick
import QtQuick.Window
import QtQuick.Controls
import QtQuick.Layouts
import DragFigures 1.0

Window {

    // Constants implementation
    MyConstants{
        id:myConstants
    }

    // Window properties

    width: myConstants.window_width
    height: myConstants.window_height
    visible: true
    title: qsTr("Drag Figures")

    // End Screen

    Image{
        id:endScreen
        width:myConstants.window_width
        height:myConstants.window_height
        source: myValueImagePath+ "endScreenBackground.jpg"
        z: 0
    }

    Text{
        id:endScreenText
        x:myConstants.window_width/2-endScreenText.width/2
        y:myConstants.window_height*0.3
        text: "Вітаємо! Всі фігури були правильно розставлені!"
        z:-1

        color:"#000B58"
        font.pixelSize: 40
        font.family: "Comic Sans MS"
        style: Text.Outline
        styleColor: "#FDEB9E"
    }

    EndScreenButton{
        id: quitButton
        innerText: "Quit"
        onClicked: Qt.quit()
    }

    EndScreenButton{
        id: restartButton
        y: quitButton.y - restartButton.height - 10
        innerText: "Restart"
        onClicked: {
            myValueRestartButton.restartProgram()
        }
    }

    // Side objects

    Timer{
        interval: 1000
        running: true
        repeat: true

        onTriggered: {
            if(firstFigure.gradeValue && secondFigure.gradeValue && thirdFigure.gradeValue) {
                firstFigure.visible = false
                secondFigure.visible = false
                thirdFigure.visible = false

                endScreen.z = 2
                endScreenText.z = 2
                quitButton.visible = true
                quitButton.z = 2
                restartButton.visible = true
                restartButton.z = 2
            }
        }
    }

    Item{
        id:search
        function searchForIndex(value, a = FirstElement, b = SecondElement, c = ThirdElement){
            if(a.getValue() === value)
                return 0
            else if(b.getValue() === value)
                return 1
            else if(c.getValue() === value)
                return 2
        }
    }

    // Main game screen

    // Text{
    //     x: 10
    //     y: 10
    //     id:gradeText
    //     text: myValueStructure.firstFigure + ": " + firstFigure.gradeValue
    //           + "\n" + myValueStructure.secondFigure + ": " + secondFigure.gradeValue
    //           + "\n" + myValueStructure.thirdFigure + ": " + thirdFigure.gradeValue

    //     color:"#000B58"
    //     font.pixelSize: 20
    //     font.family: "Comic Sans MS"
    //     style: Text.Outline
    //     styleColor: "#FDEB9E"
    // }

    Image{
        width:myConstants.window_width
        height:myConstants.window_height
        source: myValueImagePath+ "backgroundImage.jpg"
        z: 0
    }

    Figure{
        id:firstFigure
        value: 0
        y: myConstants.figure_YpositionFirst
        backPoint: Qt.point(myConstants.figure_Xposition, myConstants.figure_YpositionFirst)
        newPoint.x: myConstants.dropFigure_XPosition
        newPoint.y:{
            (search.searchForIndex(firstFigure.value) * myConstants.figure_height) + myConstants.spacing
        }
        source: myValueImagePath+ myValueStructure.firstFigure+".png"
    }
    FigureShade{
        y:myConstants.figure_YpositionFirst
        source: myValueImagePath+ myValueStructure.firstFigure+"_silver.png"
    }
    DropAreas{
        value:FirstElement.getValue()
        y:myConstants.figure_YpositionFirst
        source:FirstElement.getfigureDropPath()
    }

    Figure{
        id:secondFigure
        value: 1
        y: myConstants.figure_YpositionSecond
        backPoint: Qt.point(myConstants.figure_Xposition, myConstants.figure_YpositionSecond)
        newPoint.x: myConstants.dropFigure_XPosition
        newPoint.y:{
            (search.searchForIndex(secondFigure.value) * myConstants.figure_height) + myConstants.spacing
        }
        source:myValueImagePath+myValueStructure.secondFigure+".png"
    }
    FigureShade{
        y:myConstants.figure_YpositionSecond
        source:myValueImagePath+myValueStructure.secondFigure+"_silver.png"
    }
    DropAreas{
        value:SecondElement.getValue()
        y:myConstants.figure_YpositionSecond
        source:SecondElement.getfigureDropPath()
    }

    Figure{
        id:thirdFigure
        value: 2
        y: myConstants.figure_YpositionThird
        backPoint: Qt.point(myConstants.figure_Xposition, myConstants.figure_YpositionThird)
        newPoint.x: myConstants.dropFigure_XPosition
        newPoint.y:{
            (search.searchForIndex(thirdFigure.value) * myConstants.figure_height) + myConstants.spacing
        }
        source:myValueImagePath+myValueStructure.thirdFigure+".png"
    }
    FigureShade{
        y:myConstants.figure_YpositionThird
        source:myValueImagePath+myValueStructure.thirdFigure+"_silver.png"
    }
    DropAreas{
        value:ThirdElement.getValue()
        y:myConstants.figure_YpositionThird
        source:ThirdElement.getfigureDropPath()
    }
 }
