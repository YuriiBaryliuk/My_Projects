#ifndef EVENTHANDLER_H
#define EVENTHANDLER_H

#include <QObject>
#include <QProcess>
#include <QCoreApplication>

class Eventhandler : public QObject
{
    Q_OBJECT
public:
    Eventhandler(QObject *parent = nullptr);
    Q_INVOKABLE void restartProgram();
};

#endif// EVENTHANDLER_H
