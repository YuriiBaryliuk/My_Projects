#include "figureclass.h"

FigureClass::FigureClass(int value, QString figureDropPath, QObject* parent) :
    QObject(parent), m_value(value), m_figureDropPath(figureDropPath) {}

int FigureClass::getValue() {return this->m_value;}

QString FigureClass::getfigureDropPath() {return this->m_figureDropPath;}
