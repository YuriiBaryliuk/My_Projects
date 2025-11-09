#ifndef FIGURECLASS_H
#define FIGURECLASS_H

#include <QObject>
#include <QString>
#include <QProcess>
#include <QCoreApplication>
#include <QDir>
#include <QDebug>

class FigureClass : public QObject{
    Q_OBJECT
public:
    FigureClass(int value, QString figureDropPath, QObject* parent = nullptr);
public slots:
    int getValue();
    QString getfigureDropPath();
private:
    int m_value;
    QString m_figureDropPath;
};

#endif // FIGURECLASS_H
