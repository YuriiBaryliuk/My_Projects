#include <QGuiApplication>
#include <QQmlApplicationEngine>
#include <QQmlContext>
#include <QWindow>
#include <QIcon>
#include <random>
#include <vector>
#include <QObject>
#include "figureclass.h"
#include "eventhandler.h"

struct FigureStructure{
    Q_GADGET
public:
    QString m_firstFigure;
    QString m_secondFigure;
    QString m_thirdFigure;
    Q_PROPERTY(QString firstFigure MEMBER m_firstFigure)
    Q_PROPERTY(QString secondFigure MEMBER m_secondFigure)
    Q_PROPERTY(QString thirdFigure MEMBER m_thirdFigure)
};

template<class T>
void vectorRandomizer(std::vector<T>& vec){
    for(int i = 0; i < 10; ++i)
        std::swap(vec.at(rand() % vec.size()), vec.at(rand() % vec.size()));
}

/*
    To add data
    1. add images to resources.qrc
    2. add a vector QString with 3 names of images
    3. add this vector to another vector with packs
*/

int main(int argc, char *argv[])
{

    QGuiApplication app(argc, argv);

    QQmlApplicationEngine engine;

    app.setWindowIcon(QIcon(":/images/Carrot.png"));

    srand(time(nullptr));

    // Data initialization
    Eventhandler myEvent;
    QString imagePath = "qrc:/images/";
    std::vector<QString> vegetables {"Carrot", "onion", "potato"};
    std::vector<QString> animals {"bunny", "wolf", "bear"};
    std::vector<QString> vehicles {"car", "bus", "truck"};
    std::vector<QString> transportTypes {"train", "ship", "plane"};
    std::vector<QString> planets {"earth", "moon", "sun"};
    std::vector<QString> countries {"Italy", "Ukraine", "USA"};
    std::vector<QString> trees {"oak", "pine", "birch"};
    std::vector<QString> musicalInstruments {"guitar", "violin", "piano"};
    std::vector<QString> fruits {"apple", "plum", "pear"};
    std::vector<QString> berries {"strawberry", "blackberry", "grape"};
    std::vector<std::vector<QString>> packsVec { vegetables, animals, vehicles, transportTypes, planets, countries, trees, musicalInstruments, fruits, berries };

    // Packs randomizer (to choose which pack to use)
    vectorRandomizer<std::vector<QString>>(packsVec);

    // Structure initializetion of randomly chosen pack
    FigureStructure myStructure {packsVec.at(0).at(0), packsVec.at(0).at(1), packsVec.at(0).at(2)};

    // FigureClass objects init and randomize
    FigureClass *figureFirst = new FigureClass(0, imagePath+myStructure.m_firstFigure+"_white.png");
    FigureClass *figureSecond = new FigureClass (1, imagePath+myStructure.m_secondFigure+"_white.png");
    FigureClass *figureThird = new FigureClass(2, imagePath+myStructure.m_thirdFigure+"_white.png");

    std::vector<FigureClass*>figureVector;
    figureVector.push_back(figureFirst);
    figureVector.push_back(figureSecond);
    figureVector.push_back(figureThird);

    vectorRandomizer<FigureClass*>(figureVector);

    // Register singleton objects
    qmlRegisterSingletonInstance<FigureClass>("DragFigures", 1, 0, "FirstElement", figureVector.at(0));
    qmlRegisterSingletonInstance<FigureClass>("DragFigures", 1, 0, "SecondElement", figureVector.at(1));
    qmlRegisterSingletonInstance<FigureClass>("DragFigures", 1, 0, "ThirdElement", figureVector.at(2));
    //qmlRegisterSingletonInstance<FigureStructure>("DragFigures", 1, 0, "MyStructure", myStructure);

    // Set the context property
    engine.rootContext()->setContextProperty("myValueImagePath", QVariant::fromValue(imagePath));
    engine.rootContext()->setContextProperty("myValueStructure", QVariant::fromValue(myStructure));
    engine.rootContext()->setContextProperty("myValueRestartButton", &myEvent);

    // Engine settings
    const QUrl url(QStringLiteral("qrc:/Main.qml"));
    QObject::connect(
        &engine,
        &QQmlApplicationEngine::objectCreationFailed,
        &app,
        []() { QCoreApplication::exit(-1); },
        Qt::QueuedConnection);



    engine.load(url);
    return app.exec();
}

#include "main.moc"     //for structure (Q_GADGET)
