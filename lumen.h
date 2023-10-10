/*
* Summer Xia - cpsc3200
* 4 / 14 / 23
* revision history: 4/13 -> 4/14/2023
*/
#ifndef P2_CLION_LUMEN_H
#define P2_CLION_LUMEN_H
#include <cstdio>

class lumen
{
private:
    int threshold;
    int brightness, power;
    int size;
    int glowRequest;
    int initialBrnts, initialPwr;
    bool Active = true;

public:
    lumen();
    lumen(int aBrightness, int aSize);
    bool isStable() const;
    bool isActive();
    bool reset();
    int glow();
    bool recharge();
    int glowNum{};
    int unActiveCount = 0;
    int unstableCount = 0;

};

#endif //P2_CLION_LUMEN_H
