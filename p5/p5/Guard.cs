/*
 * 
* Summer Xia - cpsc3200
* 6 / 2 / 23
* revision history: 5/29 -> 6/2/2023
* 
* Class Invariant :
* 1) Guard(int[] w): constructor of Guard class, encapsulate an int array as durability of the shields.
* 2) bool IsAlive(): return true only when at least half its shields are viable
* 3) virtual void block(int x): 
* - expect x to be valid
* - if in up mode, and shield x viable, blocks attack, reducing shield x’s durability
* - if in down mode, loses a shield
*/
using System;
namespace P5
{
    public class Guard
    {
        private int[] Durability;
        private bool IsUp;
        private bool IsArmed = true;
        private double decreaser = 0.8;
        protected int shield;
        protected int averageDurability;
        protected int totalD;
        //getters for unit test
        public bool ArmedGetter { get { return IsArmed; } }
        public bool UpGetter { get { return IsUp; } }
        public int AverageGetter { get { return averageDurability; } }
        public int[] DurabilityGetter { get { return Durability; } }
        public int ShieldGetter { get { return shield; } }

        public Guard(int[] w)
        {
            if (w == null || w.Length == 0)
            {
                IsArmed = false;
            }
            else
            {
                shield = w.Length;
                Durability = new int[shield];
                for (int i = 0; i < shield; i++)
                {
                    Durability[i] = w[i];
                    totalD += w[i];
                }
                averageDurability = totalD / shield;
                IsUp = totalD % 3 != 0;
            }
        }

        public bool IsAlive()
        {
            if (IsArmed)
            {
                int viableCount = 0;
                for (int i = 0; i < shield; i++)
                {
                    if (Durability[i] >= averageDurability)
                    {
                        viableCount++;
                    }
                }
                return viableCount > shield / 2;
            }
            else return false;
        }

        public virtual void block(int x)
        {
            if (x > shield || x < 0 || !IsArmed || Durability[x] == 0) return;

            if (IsUp)
            {
                Durability[x] = Convert.ToInt32(Math.Floor(Durability[x] * decreaser));
            }
            else
            {
                Durability[x] = 0;
            }
            UpdateIsUp();
        }

        private void UpdateIsUp()
        {
            totalD = Durability.Sum();
            averageDurability = totalD / shield;
            IsUp = totalD % 3 != 0;
        }
    }
}
/*
 * Implementation invariant:
 * 
 * Attribute: 
 * int[] Durability: array size = shield; array value = durability level; lost an shield means when shield at position x has durability = 0;
 * bool IsUp: default Up mode, will return false if total Durability value can divisible evenly by 3.
 * bool IsArmed = true: examine if the injected array is null or empty
 * double decreaser: private const; limit magic number
 * int shield: size of the array represent number of shield
 * protected int averageDurability:one shield is viable if it's durability is above average shield durability
 * protected int totalD: used for getting average
 * //getters for unit test
 * 
 * Methods:
 * 1) Guard(int[] w): constructor check if passed in value if value, else IsArmed will toogle to false, object stuck at compromise state
 * 2) IsAlive(): check if over half of the shields are equal or above to average durability.
 *    shield should not be alive if only some of the shield are taking most durability.
 * 3) block(int x): x is the position of one shield, so x must within range; not at compromise stat(!IsArmed); not lost this shield already.
 * IsUp == up mode: decrease durability of shield at x position. 
 * !IsUp == down mode: lost an shield means when shield at position x has durability = 0;
 * 4) UpdateIsUp(): helper function to update the data member after called block(x). same rules as constructor
 * 
 */