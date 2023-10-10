/*
* Summer Xia - cpsc3200
* 5 / 19 / 23
* revision history: 5/17 -> 5/19/2023
*/
#ifndef P4_CLION_LUMEN_H
#define P4_CLION_LUMEN_H

#include <cstdio>

class lumen
{
private:
    int threshold;
    int brightness, power;
    int size;
    int glowRequest;
    int maxResetNum;
    int resetCounter;
    int initialBrnts, initialPwr;
    bool Active = true;
    void copy(const lumen& src);
    const double dimmer = 0.2;
    const double alterPwr = 0.5;
    const double decrementer = 0.8;
    const double powerDecrementer = 0.9;
    const int stableThreshold = 10;
    const int chargeNum = 100;
public:
    lumen();
    lumen(int aBrightness, int aSize);
    //operator overloading
    lumen& operator=(const lumen& l);
    lumen operator+(const lumen& l) const;
    lumen operator-(const lumen& l) const;
    lumen& operator+=(lumen& l);
    lumen& operator-=(lumen& l);
    lumen operator+(int num);
    lumen operator-(int num);
    lumen operator++(int x);
    lumen operator++();
    lumen operator--(int x);
    lumen operator--();
    bool operator==(const lumen& l) const;
    bool operator!=(const lumen& l) const;

    bool isStable() const;
    bool isActive();
    bool reset();
    int glow();
    bool recharge();
    int glowNum{};
    int unActiveCount = 0;
    int unstableCount = 0;

};

#endif //P4_CLION_LUMEN_H