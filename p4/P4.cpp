/* Summer Xia - cpsc3200
* 5 / 19 / 23
* revision history: 5/17 -> 5/19/2023
*/
#include <iostream>
#include <cstdlib>
#include <vector>
#include <memory>
#include "nova.h"
#include <ctime>

int generateRandom(int min, int max) {
    static bool initialized = false;
    if (!initialized) {
        std::srand(static_cast<unsigned>(std::time(nullptr)));  // Seed the random number generator
        initialized = true;
    }
    return min + std::rand() % (max - min + 1);  // Generate and return the random number
}

//int + lumen, non-destructive
lumen operator+(int num, lumen& l){
    return l + num;
}
//lumen - int, non-destructive
lumen operator-(int num, lumen& l){
    return l - num;
}
//lumen + nova
nova operator+(const lumen l, const nova& n){
    //forward call:n.operator+(lumen)
    return n + l;
}
//lumen - nova
nova operator-(const lumen l, const nova& n){
    //forward call:n.operator-(lumen)
    return n - l;
}

nova getNova() {
    int lumenNum = generateRandom(1, 10);
    std::cout<<"lumenNum:"<<lumenNum<<"\n";
    lumen* array = new lumen[lumenNum];
    for (int i = 0; i < lumenNum; ++i) {
        int lumenBright = generateRandom(1, 100);
        int lumenSize = generateRandom(1, 5);
        array[i] = lumen(lumenBright, lumenSize); // Generate random elements between 0 and 100
    }
    return {array, lumenNum};
}

void printNova(nova n) {
    for (int k = 0; k < n.capacity; k++) {
        std::cout << "nova.glow(" << k << ") = " << n.glow(k) << "\n";
    }
    std::cout << "Max glow for nova: " << n.getMax() << "\n";
    std::cout << "Min glow for nova: " << n.getMin() << "\n";
}

//unique_ptr Nova
std::unique_ptr<nova> getUnNova() {
    int lumenNum = generateRandom(1, 10);
    std::unique_ptr<lumen[]> array(new lumen[lumenNum]);
    for (int i = 0; i < lumenNum; ++i) {
        int lumenBright = generateRandom(1, 100);
        int lumenSize = generateRandom(1, 5);
        array[i] = lumen(lumenBright, lumenSize); // Generate random elements between 0 and 100
    }
    std::unique_ptr<nova> unNova(new nova(array.get(), lumenNum));
    array.release(); // Release ownership of the array from unique_ptr
    return unNova;
}
//shared_ptr Nova
std::shared_ptr<nova> getShNova() {
    int lumenNum = generateRandom(1, 10);
    std::shared_ptr<lumen[]> array(new lumen[lumenNum], std::default_delete<lumen[]>());
    for (int i = 0; i < lumenNum; ++i) {
        int lumenBright = generateRandom(1, 100);
        int lumenSize = generateRandom(1, 5);
        array[i] = lumen(lumenBright, lumenSize); // Generate random elements between 0 and 100
    }
    std::shared_ptr<nova> shNova(new nova(array.get(), lumenNum));
    return shNova;
}

//return one vector contain 5 heap-allocated unique pointed nova objects
std::vector<std::unique_ptr<nova>> getUnNovaVector() {
    std::vector<std::unique_ptr<nova>> novaVector;
// Create a heap-allocated nova object and add it to the vector
    for(int i = 0; i < 6; i++){
        novaVector.push_back(std::make_unique<nova>(getUnNova()));
    }
    // Remove a nova object
    novaVector.pop_back();
    return novaVector;
}

//return one vector contain 5 heap-allocated shared pointed nova objects
std::vector<std::shared_ptr<nova>> getShNovaVector() {
    std::vector<std::shared_ptr<nova>> novaVector;
// Create a heap-allocated nova object and add it to the vector
    for(int i = 0; i< 6; i++){
        novaVector.push_back(std::make_shared<nova>(getShNova()));
    }
    // Remove a nova object
    novaVector.pop_back();
    return novaVector;
}

void printShNova(const std::shared_ptr<nova>& n) {
    for (int k = 0; k < n->capacity; k++) {
        std::cout << "nova.glow(" << k << ") = " << n->glow(k) << "\n";
    }
    std::cout << "Max glow for nova: " << n->getMax() << "\n";
    std::cout << "Min glow for nova: " << n->getMin() << "\n";
}

void printUniqueNova(const std::unique_ptr<nova>& n) {
    for (int k = 0; k < n->capacity; k++) {
        std::cout << "nova.glow(" << k << ") = " << n->glow(k) << "\n";
    }
    std::cout << "Max glow for nova: " << n->getMax() << "\n";
    std::cout << "Min glow for nova: " << n->getMin() << "\n";
}

void testShared(){
    std::vector<std::shared_ptr<nova>> myNovaVector = getShNovaVector();
    for (int i = 0; i < 5;i++) {
        printShNova(myNovaVector[i]);
    }
}

void testUnique(){
    std::vector<std::unique_ptr<nova>> myNovaVector = getUnNovaVector();
    for (int i = 0; i < 5;i++) {
        printUniqueNova(myNovaVector[i]);
    }
}

void testCopy() {
    std::cout<<"__________Coping using call by value__________"<<"\n";
    //coping using call by value
    std::unique_ptr<nova> b = getUnNova();
    std::cout<<"_____Create a nova object 'b':_____"<<"\n";
    printUniqueNova(b);
    std::unique_ptr<nova> a(std::move(b));
    std::cout<<"_____After 'a(b)', 'a' is :_____"<<"\n";
    printUniqueNova(a);
}

void testMoveAssignment() {
    std::cout<<"__________Move Assignment Operator__________"<<"\n";
    std::shared_ptr<nova> lNova = getShNova();
    std::shared_ptr<nova> rNova = getShNova();
    std::cout<<"_____nova lNova:_____"<<"\n";
    printShNova(lNova);
    std::cout<<"_____nova rNova:_____"<<"\n";
    printShNova(rNova);
    lNova = std::move(rNova);  // Move assignment
    std::cout<<"After move assignment operator lNova = rNova: "<<"\n";
    std::cout<<"_____nova lNova:_____"<<"\n";
    printShNova(lNova);
    std::cout<<"_____nova rNova:_____"<<"\n";
    printShNova(rNova);
}

int main(){
    printNova(getNova());
    printUniqueNova(getUnNova());
    testShared();
    testUnique();
    testCopy();
    testMoveAssignment();
    return 0;
}