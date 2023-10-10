/* 
 * * Summer Xia - cpsc3200
* 4 / 4 / 23
* revision history: 3/31 -> 4/3 -> 4/4/2023
* 
* Class Invariant :
* IsStable(): test if the power is not too big to burned out. Return T if power is not too big; return F otherwise.
* IsActive(): test if have enough power to still be active. Return T if has active level of power; return F otherwise.
* Glow(): Will return a number based on object's state(3):
*     1)Inactive will return a dimmed brightness value;
*     2)Stable will return brightness magnified by its size;
*     3) Neither will return erratically number with its power
* Reset(): if call Glow() method(glow request) exceeds some value,object will be reset to it's original level, see as fully charged.
* if condition not met, brightness and power will decreace.
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace p1
{
    public class Lumen
    //Attributes
    {
        private uint Threshold = 50;
        private readonly uint Size;
        private uint Brightness, Power, GlowRequest;
        private uint InitialBrtns, InitialPwr;

        //Constructor access private data to encapsulate
        public Lumen(uint aBrightness, uint aSize)
        {
            Brightness = InitialBrtns = aBrightness;
            Size = aSize;
            Power = InitialPwr = aBrightness * aSize;
        }

        //public uint GetBrtns
        //{
        //    get { return Brightness; }
        //}

        //public uint GetSize
        //{
        //    get { return Size; }
        //}

        //public uint GetPower
        //{
        //    get { return Power; }
        //    //set { Power = value; }
        //}

        //Methods
        //Pre-Condition: power is valid
        
        public bool IsStable()//public
        {
            //state not stable if request too big
            return Power <= 400;
        }
        //Post-Condition: T/F

        //Pre-Condition: power is valid
        public bool IsActive()//public
        {
            return Power >= Threshold;
            //Post-Condition: T/F
        }


        //Pre-Condition:
        public uint Glow()
        {
            GlowRequest++;
            //power reduce with each glow request
            //inactive -> dimness
            if (Power == 0)
            {
                throw new Exception("Lumen is out of power!");
            }
            //(50,400)
            //1:Power <= 50
            if (!IsActive())
            {
                //stable -> brightness magnified by its size
                //2:Power <= 400
                //if (IsStable())
                //{
                //    Power = Convert.ToUInt32(Power * 0.9);
                //    return Brightness *= Size;
                //}
                Power = Convert.ToUInt32(Power * 0.9);
                return Brightness = Convert.ToUInt32(Brightness * 0.2);
            }
            if (IsStable())
            {
                Power = Convert.ToUInt32(Power * 0.9);
                return Brightness *= Size;
            }
            else if (IsStable())
            {
                Power = Convert.ToUInt32(Power * 0.9);
                return Brightness *= Size;
            }
            //Neither: erratically with its power
            //Power > 400
            else
            {
                if (Power % 2 != 0)
                {
                    Power = Convert.ToUInt32(Power * 0.9);
                    return Brightness = 0;
                }
                //lucky
                else
                {
                    Power = Convert.ToUInt32(Power * 0.9);
                    return Brightness = Convert.ToUInt32(Brightness * 0.8);
                }
            }
        }
        //Post-Condition: Return a new brightness value based on object's states(3)

        //required public methods 
        public bool Reset()
        {
            if (GlowRequest % 3 == 0 && Power > 0)
            {
                Brightness = InitialBrtns;
                Power = InitialPwr;
                return true;
            }
            else
            {
                Power = Convert.ToUInt32(Power * 0.8);
                Brightness = Convert.ToUInt32(Brightness * 0.8);
                return false;
            }
        }
        //Post-Condition: T/F

    }
}


//implementation invariant:
/*
 * Attributes:
 * power = brtns * size realized the bigger the object, bigger the power.
 * ALL member varible are set as private, getter and setter is for unit testing to modify the test value. Driver don't have the access of it.
 * Threshold: private value for test if power is at active level
 * Brightness/Size: Clinnt will entry Brightness(50,100) and Size(1,5) with limit shown in parentheses. 
 * So the initial will not be lower than threshold.
 * Initial: Store the initial value of Brightness and power for Reset()
 * GlowRequest: Record time of calling Glow()
 * 
 * Method:
 * All state methods are set as public(IsActive, IsStable), so client can use as 
 * IsStable() will return F if power value greater than 400, 20% chance the power could get in-stable state.
 * Since Active(power) >= 50 && stable(power) <= 400, in Neither state is when power > 400
 * Reset() is also public method for client, but the internal design and values are hidden and encapsulated from client.
 */

