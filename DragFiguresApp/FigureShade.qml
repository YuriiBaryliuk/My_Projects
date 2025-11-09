import QtQuick

Image{

    MyConstants{id:myConstants}

    x:myConstants.figure_Xposition
    y:myConstants.figure_YpositionFirst
    width:myConstants.figure_width
    height:myConstants.figure_height
    source:"qrc:/images/Carrot_silver.png"
    z:0
}
