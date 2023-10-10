//* Summer Xia - cpsc3200
//* 4 / 14 / 23
//* revision history: 4/13 -> 4/14/2023
//*/

#include <iostream>
#include <cstdlib>
#include <ctime>
#include "nova.h"

int generateRandom(int lowerBound, int upperBound) {
    return rand() % (upperBound - lowerBound + 1) + lowerBound;
}

nova getNova() {
    int lumenNum = generateRandom(1, 10);
    int firstBright = generateRandom(1, 100);
    int firstSize = generateRandom(1, 5);
    return {firstBright, firstSize, lumenNum};
}

void printNova(nova n, int numCalls = 1) {
    for (int i = 0; i < numCalls; i++) {
        for (int k = 0; k < n.getCapacity(); k++) {
            std::cout << "nova.glow(" << k << ") = " << n.glow(k) << "\n";
        }
        std::cout << "Max glow for nova: " << n.getMax() << "\n";
        std::cout << "Min glow for nova: " << n.getMin() << "\n";
    }
}

void createNovaArray() {
    int const novaNum = 10;
    nova novaArr[novaNum];
    srand(static_cast<unsigned int>(time(nullptr)));
    for (int i = 0; i < novaNum; i++) {
        std::cout << "__________" << i + 1 << "__________" << "\n";
        nova local = getNova();
        novaArr[i] = local;
        printNova(novaArr[i], 2);
    }
}

void testMoveAssignment() {
    std::cout<<"__________Move Assignment Operator__________"<<"\n";
    nova lNova = getNova();
    nova rNova = getNova();
    std::cout<<"_____nova lNova:_____"<<"\n";
    printNova(lNova);
    std::cout<<"_____nova rNova:_____"<<"\n";
    printNova(rNova);
    lNova = rNova;
    std::cout<<"After move assignment operator lNova = rNova: "<<"\n";
    std::cout<<"_____nova lNova:_____"<<"\n";
    printNova(lNova);
    std::cout<<"_____nova rNova:_____"<<"\n";
    printNova(rNova);
}

void testCopy() {
    std::cout<<"__________Coping using call by value__________"<<"\n";
    //coping using call by value
    nova b = getNova();
    std::cout<<"_____Create a nova object 'b':_____"<<"\n";
    printNova(b);
    nova a(b);
    std::cout<<"_____After 'a(b)', 'a' is :_____"<<"\n";
    printNova(a);
}

int main() {
    createNovaArray();
    testMoveAssignment();
    testCopy();
    return 0;
}
