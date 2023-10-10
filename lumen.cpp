/*
* Summer Xia - cpsc3200
* 4 / 14 / 23
* revision history: 4/13 -> 4/14/2023

* Class Invariant :
1)lumen():no-argument constructor, default value all to zero
2)lumen(int aBrightness, int aSize):constructor, pass in bright and size to initialize the object
3)bool isStable():return if the object is stable
4)bool isActive():return if the object is active
5)bool reset():must request glow exceed certain number && power must above 0; return if the object can be reset
6)int glow():lumen object must be initialized; Based on object's state, client will get a glow number as return value
7)bool recharge():object must be at stable state; client will be able to know if the object is chargeable
*/

#include "lumen.h"

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
}

bool lumen::isStable() const
{
    return brightness > 10;
}
//Post-condition:client will be able to know if the object is Stable

bool lumen::isActive()
{
    if(power > threshold) {
        Active = true;
    }else{
        Active = false;
    }
    return Active;
}
//Post-condition:client will be able to know if the object is Active

//Pre-condition:must request glow exceed certain number && power must above 0
bool lumen::reset()
{
    if(glowRequest % 3 == 0 && power > 0)
    {
        brightness = initialBrnts;
        power = initialPwr;
        return true;
    }
    else
    {
        power = int(power * 0.8);
        brightness = int(brightness * 0.8);
        return false;
    }
}
//Post-condition:If call reset() success the object will be reset back to it's initial value; If failed, power & brightness will decrease

//Pre-condition: lumen object must be initialized
int lumen::glow()
{
    glowRequest++;
    power = int(power * 0.9);
    // power > 50
    if(isActive())
    {
        //stable: bright * size
        //Bright > 30
        if(isStable())
        {
            return glowNum = brightness * size;
        }
        // !stable(Bright <= 30) & active (neither) will generate a random machine
        else
        {
            unstableCount++;
            if(power % 2 == 0){
                return glowNum = brightness * size;
            }else{
                return glowNum = int(0.5 * brightness * size);
            }
        }
    }
    //in-active, dimness:reduce bright
    else
    {
        unActiveCount++;
        return glowNum = int(brightness *= 0.2);
    }
}
//Post-condition: Based on object's state, client will get a glow number as return value

//Pre-condition: object must be at stable state
bool lumen::recharge()
{
    if(isStable()){
        brightness += 100;
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
5)bool reset():by calling glow every three times, if the power > 0, the object could use the initialBrnts& initialPwr to reset it back to it’s first state.
6)int glow():using isStable() and isActive to modify the return value glowNum, both state and value will be changed here.
7)bool recharge():from nova object,if condition met, this function to change object’s state can be triggered. To make the object with higher value, but not like reset.
 */