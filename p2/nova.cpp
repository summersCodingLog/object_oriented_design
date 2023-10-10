/*
* Summer Xia - cpsc3200
* 4 / 14 / 23
* revision history: 4/13 -> 4/14/2023

* Class Invariant :

1)fixLumen(): check if sub object could be internal trigger recharge() or replace() by state of the subjects;
2)void copy(const nova& src): realized the deep copy.
3)nova(int bright, int size, int num):constructor, error handling for invalid size is to automatically set sub objects size to at least 2. Initialize object and sub objects.
4) nova():no-argument constructor, used for allocating objects before initializing to complex parameters. default to have nullptr and no size.
5)nova(const nova& a):copy constructor, support deep copy by calling private copy function.
6)~nova():destructor will automatically be called when an object is out of scope, deallocate the heap object and free memory.
7)void operator=(const nova& rhs): overloading assignment operator,expect client to not make lhs = rhs’s address.
8)nova(nova&& source):move semantic, make lhs equal to rhs.
9)void operator=(nova&& rhs):move assignment,make lhs equal to rhs.
10)int glow(int pos): triggers lumen(subobject) encapsulated in nova(object). Expect client input valid index value.
11)unsigned getMin():Expect object is not empty. Value of glow() will be compared in a loop to get the minimum value among all subobjects.
12)unsigned getMax():Expect object is not empty. Value of glow() will be compared in loop to get the minimum value among all subobjects
13)void replace(unsigned num): replace all sub objects at once when sub objects at unwanted state are over some limits.
14)unsigned int getCapacity():public accessor, used for get the number of subObj inorder to call glow(x);
 */

#include "nova.h"
#include <iostream>

//Pre-condition: must at least have 2 subObject
nova::nova(int bright, int size, int num)
{
    if(num <= 1){
        if(num == 0) num += 2;
        num += 1;
    }
    capacity = 2 + num % 10;
    arr = new lumen[capacity];
    arr[0] = lumen(bright, size);
    int newBright, newSize;
    newBright = bright;
    newSize = size;
    for(int i = 1; i < capacity; i++)
    {
        if(i % 2 == 0){
            newBright = int(newBright * alterBright_1);
            newSize = int(newSize * alterSize_1);
        }
        else{
            newBright = int(newBright * alterBright_2);
            newSize = int(newSize * alterSize_2);
        }
        arr[i] = lumen(newBright,newSize);
    }
}

unsigned int nova::getCapacity() const{
    return  capacity;
}

nova::nova():capacity(0), arr(nullptr) {}

void nova::fixLumen() {
    int powerless = 0;
    int broken = 0;
    for (int k = 0; k < capacity; k++) {
        if (!arr[k].isActive()) powerless++;
        if (!arr[k].isStable()) broken++;
    }
    if (broken > 8) {
        std::cout<<"replace!\n";
        replace(capacity);
    }
    if (powerless > capacity / 2) {
        for (int k = 0; k < capacity; k++) {
            arr[k].recharge();
        }
    }
}

void nova::copy(const nova& src)
{
    capacity = src.capacity;
    arr = new lumen[capacity];
    for(int i = 0; i < capacity;i++)
    {
        arr[i] = src.arr[i];
    }
}

//copy constructor: support deep copy
nova::nova(const nova& a)
{   copy(a);    }

nova::~nova() {   delete[] arr;   }

//overload assignment operator
//Pre-condition: rhs should not be the same address as lhs
void nova::operator=(const nova& rhs)
{
    //avoid self-assign
    if(this == &rhs) return;
    delete[] arr;
    copy(rhs);
}
//Post-condition: lhs will be equal to rhs

//move semantic
nova::nova(nova&& source)
{
    capacity = source.capacity;
    arr = source.arr;
    source.capacity = 0;
    source.arr = nullptr;
}

//move assignment operator
void nova::operator=(nova&& rhs){
    std::swap(capacity,rhs.capacity);
    std::swap(arr,rhs.arr);
}
//Post-condition: lhs will be equal to rhs

//Pre-condition: input number must be within range
int nova::glow(int range) {
    fixLumen();
    int totalGlow = 0;
    if(range > capacity) {
        range = range % capacity;
    }
    for(int i = 0; i < range;i++) {
        totalGlow += arr[i].glow();
    }
    return totalGlow;
}
//Post-condition: all subobject within range will call glow

//Pre-condition: object can't be empty
unsigned nova::getMin() {
    if (capacity == 0) return 0;
    min = HIGHEST_BOUND;
    for (int i = 0; i < capacity - 1; i++) {
        if (arr[i].glowNum < min) {
            min = arr[i].glowNum;
        }
    }
    return min;
}
//Post-condition: min glow value among sub objects will be returned

//Pre-condition: object can't be empty
unsigned nova::getMax() {
    if (capacity == 0) return 0;
    max = 0;
    for (int i = 0; i < capacity - 1; i++) {
        if (arr[i].glowNum > max) {
            max = arr[i].glowNum;
        }
    }
    return max;
}
//Post-condition: max glow value among sub objects will be returned

//Pre-condition: unstable object must be more than certain number
void nova::replace(unsigned num) {
    delete[] arr;
    if (num != capacity) capacity = num % 10;
    arr = new lumen[capacity];
}
//Post-condition: Everything from previous object will be replaced


/* implementation invariant:
*Variable:
1)HIGHEST_BOUND: set a very large number as initial value of min;loop to compare and update.
2)capacity: sub object lumen array’s size, determine how many lumen objects.
3)lumen* arr: internal ptr to an array of sub objects and modify them through this ptr.

*Functions:
1)void copy(const nova& src): private copy function for deep copy, not only to avoid aliasing, but to provide information hiding and code reuse for many other functions.
2)move semantics: efficient & safe way to transfer ownership. Save a pair of construct and destruct when the copy object is an expiring temp.
3)move assignment: transfer ownership by swapping,use when the copy object is an expiring temp.
4)overloading assignment operator: reuse private copy function to avoid aliasing.
5)getMin/getMax:keep update by loop through ptr to sub objects glow value
6)glow(x):call fixLumen as the first step to check an object's state. Get the glow value from the heap by using ptr to the index’s address.
7)replace(): delete the old objects. And allocate new sub objects with the same size on the heap.
*/
