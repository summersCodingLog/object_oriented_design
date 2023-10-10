/*
* Summer Xia - cpsc3200
* 5 / 19 / 23
* revision history: 5/17 -> 5/19/2023

* Class Invariant :
1)lumen():no-argument constructor, default value all to zero
2)lumen(int aBrightness, int aSize):constructor, pass in bright and size to initialize the object
3)bool isStable():return if the object is stable
4)bool isActive():return if the object is active
5)bool reset():must not reach reset limit && request glow exceed certain number && power must above 0; return if the object can be reset
6)int glow():lumen object must be initialized; Based on object's state, client will get a glow number as return value
7)bool recharge():object must be at stable state; client will be able to know if the object is chargeable
new 8) void lumen::copy(const lumen& src): private deep copy function

* Operator Overload:
 lumen& operator=(const lumen& l): make the lhs lumen object equals to the rhs lumen object
 lumen operator+(const lumen& l) const: lumen + lumen, non-destructive
 lumen operator-(const lumen& l) const: lumen - lumen, non-destructive

 lumen& operator+=(lumen& l): short-cut addition of this lumen and rhs lumen
 lumen& operator-=(lumen& l): short-cut subtraction of this lumen and rhs lumen

 lumen operator+(int num): lumen + num
 lumen operator-(int num): lumen - num

 lumen operator++(int x): lumen++
 lumen operator++(): --lumen

 lumen operator--(int x): lumen--
 lumen operator--(): --lumen

 bool operator==(const lumen& l) const: return true is two lumen are equal
 bool operator!=(const lumen& l) const: return true if two lumen are not equal
*/

#include "lumen.h"

//no-arg constructor
lumen::lumen()
{
    brightness = initialBrnts = 0;
    power = initialPwr = 0;
    size = glowRequest = 0;
    threshold = 0;
}

lumen::lumen(int brightVal, int sizeVal): brightness(brightVal % 101), size(sizeVal % 6)
{
    initialBrnts = brightness;
    power = initialPwr = brightness * size;
    glowRequest = 0;
    threshold = 50;
    resetCounter = 0;
    maxResetNum = size;
}

void lumen::copy(const lumen& src)
{
    brightness = src.brightness;
    power = src.power;
    glowRequest = 0;
    threshold = 50;
}
//Pre-condition: rhs should not be the same address as lhs
lumen& lumen::operator=(const lumen& l){
    if(this == &l) return *this;
    copy(l);
    return *this;
}
//Post-condition: lhs will be equal to rhs

//lumen + lumen, non-destructive
lumen lumen::operator+(const lumen& l) const {
    lumen local(*this);
    local.brightness += l.brightness;
    local.power += l.power;
    local.size += l.size;
    return local;
}
//lumen - lumen, non-destructive
lumen lumen::operator-(const lumen& l) const {
    lumen local(*this);
    local.brightness -= l.brightness;
    if(local.brightness <= 0)   local.brightness = 0;
    local.power -= l.power;
    if(local.power <= 0)   local.power = 0;
    local.size -= l.size;
    if(local.size <= 0)   local.size = 0;
    return local;
}
//lumen += lumen, destructive
lumen& lumen::operator+=(lumen& l) {
    brightness += l.brightness;
    power += l.power;
    size += l.size;
    return *this;
}
//lumen -= lumen, destructive
lumen& lumen::operator-=(lumen& l) {
    brightness -= l.brightness;
    if(brightness <= 0)   brightness = 0;
    power -= l.power;
    if(power <= 0)   power = 0;
    size -= l.size;
    if(size <= 0)   size = 0;
    return *this;
}
//lumen + int, non-destructive
lumen lumen::operator+(int num){
    brightness += num;
    size += num;
    power = brightness * size;
    return *this;
}
//lumen + int, non-destructive
lumen lumen::operator-(int num){
    brightness -= num;
    if(brightness <= 0)   brightness = 0;
    size -= num;
    if(size <= 0)   size = 0;
    power = brightness * size;
    return *this;
}
//++lumen, destructive
lumen lumen::operator++() {
    brightness++;
    size++;
    power = brightness * size;
    return *this;
}
//lumen++, destructive
lumen lumen::operator++(int x) {
    lumen oldState = *this;
    operator++();
    return oldState;
}
//--lumen, destructive
lumen lumen::operator--() {
    brightness--;
    if(brightness <= 0)   brightness = 0;
    size--;
    if(size <= 0)   size = 0;
    power = brightness * size;
    return *this;
}
//lumen--, destructive
lumen lumen::operator--(int x) {
    lumen oldState = *this;
    operator--();
    return oldState;
}

bool lumen::operator==(const lumen& l) const {
    return l.brightness == brightness && l.power == power && l.size == size;
}

bool lumen::operator!=(const lumen& l) const {
    return !operator==(l);
}

bool lumen::isStable() const {
    return brightness > stableThreshold;
}
//Post-condition:client will be able to know if the object is Stable

bool lumen::isActive() {
    if (power > threshold) {
        Active = true;
    } else {
        Active = false;
    }
    return Active;
}
//Post-condition:client will be able to know if the object is Active

//Pre-condition:must not reach reset limit && request glow exceed certain number && power must above 0
bool lumen::reset() {
    if (resetCounter < maxResetNum && glowRequest % 3 == 0 && power > 0) {
        brightness = initialBrnts;
        power = initialPwr;
        resetCounter++;
        return true;
    } else {
        power = int(power * decrementer);
        brightness = int(brightness * decrementer);
        return false;
    }
}
//Post-condition:If call reset() success the object will be reset back to it's initial value; If failed, power & brightness will decrease

//Pre-condition: lumen object must be initialized
int lumen::glow() {
    glowRequest++;
    power = int(power * powerDecrementer);
    // power > 50
    if (isActive()) {
        //stable: bright * size
        //Bright > 30
        if (isStable()) {
            return glowNum = brightness * size;
        }
            // !stable(Bright <= 30) & active (neither) will generate a random machine
        else {
            unstableCount++;
            if (power % 2 == 0) {
                return glowNum = brightness * size;
            } else {
                return glowNum = int(alterPwr * brightness * size);
            }
        }
    }
        //in-active, dimness:reduce bright
    else {
        unActiveCount++;
        return glowNum = int(brightness *= dimmer);
    }
}
//Post-condition: Based on object's state, client will get a glow number as return value

//Pre-condition: object must be at stable state
bool lumen::recharge() {
    if (isStable()) {
        brightness += chargeNum;
        size = 2;
        return true;
    }
    return false;
}
//Post-condition: client will be able to know if the object is chargeable

/* implementation invariant:

 * Variable:
1) threshold: used for check if object is active
2) brightness, power: encapsulated to provide security.Error handling for invalid input is by using % num, to limit it within a range.
3) size: encapsulated & can not be changed.
4) glowRequest : store the number calling glow, as one of the passing condition for reset()
5) initialBrnts/initialPwr:backup for the initial value, once reset() success, use these values to restore the object.
6) Active: set initial state for all objects is active.
7) glowNum: stored glow() returned number for nova object to link.
8) unActiveCount/unstableCount: set initial value to 0, used for nova object to track the replace() and recharge() condition.

 * Function:
1)lumen():provide no-argument constructor to provide portability for assign then initialize.
2)lumen(int aBrightness, int aSize):power is determined by both bright and size
3)bool isStable():determined by brightness
4)bool isActive():determined by power
5)bool reset():reset limit = size; by calling glow every three times, if the power > 0, the object could use the initialBrnts& initialPwr to reset it back to it’s first state.
6)int glow():using isStable() and isActive to modify the return value glowNum, both state and value will be changed here.
7)bool recharge():from nova object,if condition met, this function to change object’s state can be triggered. To make the object with higher value, but not like reset.

  * Operator Overload:
 lumen& operator=(const lumen& l): call copy function to assign the value; limit self-assignment
 lumen operator+(const lumen& l) const: const b/c non-destructive;
 lumen operator-(const lumen& l) const: const b/c non-destructive; check if subtraction make value became negative

 lumen& operator+=(lumen& l): same functionality as operator+(lumen)
 lumen& operator-=(lumen& l): same functionality as operator-(lumen)

 lumen operator+(int num) const: adds an increment to int in lumen
 lumen operator-(int num) const: subtracts an decrement to int in lumen; check if subtraction make value became negative

 lumen operator++(int x): dummy int as parameter to identify postponement increment; save the current state, increment, return current state
 lumen operator++(): increment then return

 lumen operator--(int x): dummy int as parameter to identify postponement decrement; save the current state, decrement, return current state
 lumen operator--(): decrement then return

 bool operator==(const lumen& l) const: const b/c non-destructive;
 bool operator!=(const lumen& l) const: const b/c non-destructive;
 */