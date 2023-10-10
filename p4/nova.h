/*
* Summer Xia - cpsc3200
* 5 / 19 / 23
* revision history: 5/17 -> 5/19/2023
*/

#ifndef P4_CLION_NOVA_H
#define P4_CLION_NOVA_H
#include "lumen.h"
#include <cstdio>
#include <memory>

class nova
{
private:
    const unsigned HIGHEST_BOUND = 1000;
    lumen* arr{};
    void fixLumen();
    void copy(const nova& src);
    int const fixThreshold = 8;
    //int totalGlow = 0;
public:
    int totalGlow;
    nova(lumen* handle, int arrSize);
    nova();
    nova(std::shared_ptr<nova> a);
    ~nova();
    //operator overloading
    void operator=(const nova& rhs);
    lumen& operator[](int index);
    nova operator+(const nova& n) const;
    nova operator-(const nova& n) const;
    nova& operator+=(nova& n);
    nova& operator-=(nova& n);
    nova operator++(int x);
    nova& operator++();
    nova operator--(int x);
    nova& operator--();
    bool operator==(const nova& n);
    bool operator!=(const nova& n);
    //symmetry addition
    nova operator+(lumen l) const;
    nova operator-(lumen l) const;
    //move semantic
    nova(nova&& source) noexcept ;
    void operator=(nova&& rhs);
    int capacity;
    int glow(int pos);
    unsigned getMin();
    unsigned getMax();
    unsigned min{},max{};
    void replace(unsigned num);

};

#endif //P4_CLION_NOVA_H