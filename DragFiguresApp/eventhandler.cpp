#include "eventhandler.h"

Eventhandler::Eventhandler(QObject *parent) : QObject(parent) {
}

void Eventhandler::restartProgram(){
    QString program = QCoreApplication::applicationFilePath();
    QStringList args = QCoreApplication::arguments();
    QProcess::startDetached(program, args);
    QCoreApplication::quit();
}
