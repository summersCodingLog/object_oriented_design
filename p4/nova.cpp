/*
* Summer Xia - cpsc3200
* 5 / 19 / 23
* revision history: 5/17 -> 5/19/2023

* Class Invariant :

1)fixLumen(): check if sub object could be internal trigger recharge() or replace() by state of the subjects;
2)void copy(const nova& src): realized the deep copy.
3)nova():constructor, encapsulated an array of lumen objects by constructor injected; throw exception if passed in data is invalid
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

* Operator Overloading:
lumen& operator[](int index): return reference of the lumen at index position; throw exception is index invalid
nova operator+(const nova& n) const: this->nova + nova
nova operator-(const nova& n) const: this->nova - nova

nova& operator+=(nova& n): short-cut addition of this nova and rhs nova
nova& operator-=(nova& n): short-cut subtraction of this nova and rhs nova

nova operator++(int x): nova++
nova operator++(): ++nova

nova operator--(int x): nova--
nova operator--(): --nova

bool operator==(const nova& n): return true is two nova are equal
bool operator!=(const nova& n): return true is two nova are not equal

nova operator+(lumen l) const: adds a lumen for each lumen elements inside nova
nova operator-(lumen l) const: subtracts a lumen for each lumen elements inside nova

 */

#include "nova.h"
#include <iostream>

//Pre-condition: must at least have 2 subObject
nova::nova(lumen* input, int arrSize) {
    if (input == nullptr) {
        throw std::invalid_argument("Passed in lumen object array can not be null.");
    }
    capacity = arrSize;
    arr = input;
    totalGlow = 0;
}

nova::nova():arr(nullptr) {}

void nova::fixLumen() {
    int powerless = 0;
    int broken = 0;
    for (int k = 0; k < capacity; k++) {
        if (!arr[k].isActive()) powerless++;
        if (!arr[k].isStable()) broken++;
    }
    if (broken > fixThreshold) {
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
nova::nova(std::shared_ptr<nova> a)
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

//access elements return a reference
lumen& nova::operator[](int index){
    if(index > capacity)    throw std::range_error("Invalid index: out of bound.");
    if(index <= 0)          index = abs(index);
    return arr[index];
}
//nova + nova, non-destructive
nova nova::operator+(const nova& n) const{
    nova local(n.arr,n.capacity);
    int minSize = capacity < n.capacity ? capacity: n.capacity;
    for(int i = 0; i < minSize; i++){
        //nova newArr();
        local[i] += arr[i];
    }
    return local;
}
//nova - nova, non-destructive
nova nova::operator-(const nova& n) const{
    nova local(n.arr,n.capacity);
    int minSize = capacity < n.capacity ? capacity: n.capacity;
    for(int i = 0; i < minSize; i++){
        local[i] = arr[i] - local[i];
    }
    return local;
}
//nova + lumen, add lumen to each sub-objects in nova.
nova nova::operator+(lumen l) const{
    nova local(arr,capacity);
    for(int i = 0; i < capacity; i++){
        //lumen + lumen
        local[i] = arr[i] + l;
    }
    return local;
}
nova nova::operator-(lumen l) const{
    nova local(arr,capacity);
    for(int i = 0; i < capacity; i++){
        //lumen - lumen
        local[i] = arr[i] - l;
    }
    return local;
}
//nova += nova, destructive
nova& nova::operator+=(nova& n){
    int minSize = capacity < n.capacity ? capacity: n.capacity;
    for(int i = 0; i < minSize; i++){
        arr[i] += n[i];
    }
    return *this;
}
//nova -= nova, destructive
nova& nova::operator-=(nova& n){
    int minSize = capacity < n.capacity ? capacity: n.capacity;
    for(int i = 0; i < minSize; i++){
        arr[i] -= n[i];
    }
    return *this;
}
//nova++
nova nova::operator++(int x){
    nova oldState(arr, capacity);
    for(int i = 0; i < capacity; i++){
        arr[i]++;
    }
    return oldState;
}
//++nova
nova& nova::operator++(){
    for(int i = 0; i < capacity; i++){
        arr[i]++;
    }
    return *this;
}
//nova--
nova nova::operator--(int x){
    nova oldState(arr,capacity);
    for(int i = 0; i < capacity; i++){
        arr[i]--;
    }
    return oldState;
}
//--nova
nova& nova::operator--(){
    for(int i = 0; i < capacity; i++){
        arr[i]--;
    }
    return *this;
}
//nova == nova
bool nova::operator==(const nova& n) {
    return arr == n.arr;
}

bool nova::operator!=(const nova& n){
    return arr != n.arr;
}

//move semantic
nova::nova(nova&& source) noexcept
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
int nova::glow(int range) {
    fixLumen();
    if(range > capacity) {
        range = range % capacity;
    }
    for(int i = 0; i < range; i++){
        arr[i].glow();
        totalGlow += arr[i].glowNum;
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
    for (int i = 0; i < capacity; i++) {
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

 * Operator Overloading:
lumen& operator[](int index):treat lumen array like array of ints; wrap index check
nova operator+(const nova& n) const: copy the parameter nova object then compare to get the smaller size and make the addition of lumen sub-objects
nova operator-(const nova& n) const: same as the above but subtraction

nova& operator+=(nova& n):compare to get the smaller size and make the addition of lumen sub-objects
nova& operator-=(nova& n):same as the above but subtraction

nova operator++(int x):dummy int as parameter to identify postponement increment; save current state of nova then do the addition on this nova, return saved old state
nova operator++():make increment then return directly

nova operator--(int x):dummy int as parameter to identify postponement decrement; save current state of nova then do the subtraction on this nova, return saved old state
nova operator--():make decrement then return directly

bool operator==(const nova& n): return true is two nova are equal
bool operator!=(const nova& n): return true is two nova are not equal

nova operator+(lumen l) const: adds a lumen for each lumen elements inside nova
nova operator-(lumen l) const: subtracts a lumen for each lumen elements inside nova

 */
