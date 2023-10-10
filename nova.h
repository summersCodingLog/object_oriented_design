/*
* Summer Xia - cpsc3200
* 4 / 14 / 23
* revision history: 4/13 -> 4/14/2023
*/

#ifndef P2_CLION_NOVA_H
#define P2_CLION_NOVA_H
#include "lumen.h"
#include <stdio.h>

class nova
{
private:
    const double alterBright_1 = 1.5;
    const double alterSize_1 = 0.9;
    const double alterBright_2 = 2.0;
    const double alterSize_2 = 0.7;
    const unsigned HIGHEST_BOUND = 1000;
    unsigned int capacity{};
    lumen* arr{};
    void fixLumen();
    void copy(const nova& src);

public:
    nova(int bright, int size, int num);
    nova();
    nova(const nova& a);
    ~nova();
    unsigned int getCapacity() const;
    void operator=(const nova& rhs);
    nova(nova&& source);
    void operator=(nova&& rhs);
    int glow(int pos);
    unsigned getMin();
    unsigned getMax();
    unsigned min{},max{};
    void replace(unsigned num);

};


#endif //P2_CLION_NOVA_H
