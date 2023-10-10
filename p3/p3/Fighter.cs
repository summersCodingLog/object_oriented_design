/*
* Summer Xia - cpsc3200
* 6 / 2 / 23
* revision history: 4/27 -> 6/2/2023

* Class Invariant :
* 1)Fighter(int[] w): 
* -Encapsulated weapon array 'w' must be injected by construcor, 
* -null or empty or zero value array will auto generate to limited state(!Armed)
* -weapon under client control
* -no restriction on w length
* 2)bool Move(int x, int y):
* -if move request is within range, the new position will be updated based on the request
* -if request illegal, position won't change
* 3)void Shift(int p):
* -Alter aim according to value p;
* -no effect on base class Fighter
* 4)bool IsActive():
* -Expect Figher is Armed
* -Return if the object is Active
* 5)bool IsAlive():
* -Expect Figher is Armed
* -return if the object is Alive
* 6)Target(int x, int y, int q):
* -Expect Figher is Armed
* -return true if given target of strength q was vanquished; otherwise false;
* 7)int Sum():
* -returns sum total of targets vanquished.
*/
using System;
using System.Linq;

namespace P3
{
    public class Fighter
    {
        private int OriginalStrength;
        private int[] OriginalArtillery;
        protected int[] OriginalArtilleryGetter { get { return OriginalArtillery; } }
        protected int OriginalStrengthGetter { get { return OriginalStrength; } }

        protected bool Armed = true;

        protected int Row, Column;
        protected int[] RowAttkRange, ColAttkRange;
        protected static int[] MoveRange = Enumerable.Range(-100, 201).ToArray();
        protected int Strength;
        protected int[] Artillery;
        private int StrengthThreshold;
        private int ArtilleryThreshold;
        private int KillCount;

        //getters for unit test
        public bool ArmedGetter { get { return Armed; } }
        public int StrengthGetter { get { return Strength; } }
        public int[] ArtilleryGetter { get { return Artillery; } }
        public int[] RowAttkRangeGetter { get { return RowAttkRange; } }
        public int[] ColAttkRangeGetter { get { return ColAttkRange; } }

        //Pre-Condition:Expected injected array is not null
        public Fighter(int[] w)
        {
            Row = Column = 0;
            //exist check
            if (w == null || w.Length == 0)
            {
                Armed = false;
            }

            Artillery = new int[w.Length];
            OriginalArtillery = new int[w.Length];

            for (int i = 0; i < w.Length; i++)
            {
                Artillery[i] = w[i];
                OriginalArtillery[i] = w[i];
            }

            ArtilleryThreshold = Convert.ToInt32(Artillery[Artillery.Length-1] * 0.2);

            for (int j = 0; j < w.Length; j++)
            {
                Strength += w[j] * j;
            }
            if (Strength == 0) { Armed = false; }
            StrengthThreshold = Convert.ToInt32(Strength * 0.2);

            RowAttkRange = ColAttkRange = Enumerable.Range(-w.Length, 2 * w.Length + 1).ToArray();

            OriginalStrength = Strength;
        }
        //Post-Condition:Fighter Object sccefully constructed with other inner attributes

        ///Pre-Condition:Expect request is not out of range
        public virtual bool Move(int x, int y)
        {
            int newRow = Row + x;
            int newCol = Column + y;

            if (MoveRange.Contains(newRow) && MoveRange.Contains(newCol))
            {
                Row = newRow;
                Column = newCol;
                return true;
            }
            else return false;
        }
        //Post-Condition:Return true if move request is within range; otherwise false;

        public virtual void Shift(int p)
        { return; }

        //Pre-Condition:Expect Figher is Armed
        public virtual bool IsActive()
        {
            if (!Armed)
            {
                return false;
            }
            return Artillery.Length > ArtilleryThreshold;
        }
        //Post-Condition:Return if the object is Active

        //Pre-Condition:Expect Figher is Armed
        public virtual bool IsAlive()
        {
            if (!Armed)
            {
                return false;
            }
            return Strength > StrengthThreshold;
        }
        //Post-Condition:Return if the object is Alive

        //Pre-Condition:Expect Figher is Armed
        public bool Target(int x, int y, int q)
        {
            if (Armed)
            {
                if (RowAttkRange.Contains(x) && ColAttkRange.Contains(y) && Artillery.Length > q)
                {
                    Strength = Convert.ToInt32(Math.Floor(Strength * 0.9));
                    KillCount++;
                    return true;
                }
            }
            Strength = Convert.ToInt32(Math.Floor(Strength * 0.9));
            return false;
        }
        //Post-Condition:Return true if given target of strength q was vanquished; otherwise false;

        public int Sum()
        {
            return KillCount;
        }

    }
}

/* implementation invariant:

 * Attributes:
 *private int[] OriginalStrength, OriginalArtillery:
 *-used for saved a backup for derived class to reset and revive, and also unit testing
 *protected int OriginalArtilleryGetter/OriginalStrengthGetter:
 *-decendent can use, client can not. 
 *protected bool Armed:
 *-state check, if the array injected is illegal, !Armed is a compromised state where client can only call Move()
 *protected int Row, Column:
 *-position, protected so encapsulated from client but open for derived class
 *protected int[] RowAttkRange, ColAttkRange:
 *-int array to store the range of Attack range both row and column, easy to check if in range
 *protected static int[] MoveRange = Enumerable.Range(-100, 201).ToArray():
 *-static shared by all object, also int array
 *protected int Strength,int[] Artillery:
 *-generated by injected int array
 *private int StrengthThreshold, ArtilleryThreshold:
 *-provided protected getter so derived class can use while hide the data
 *-used for IsActive & IsAlive check
 *private int KillCount:
 *-internal generated value for store the Target success count, can get from public funtion Sum()
 
 * Method:
* 1)Fighter(int[] w): 
* -Depenency injection is using constructor for lifetime association & share by all classes
* -Strength is index * value, Arrillery is sum of the array's value. 
* 2)bool Move(int x, int y):
* -if move request is within range, the new position will be updated based on the request
* -if request illegal, position won't change
* 3)void Shift(int p)
* -return because contructor has generated, for base class no extra alter
* 4)bool IsActive():
* -Associate with Artillery
* 5)bool IsAlive()
* -Associated with Strength
* 6)Target(int x, int y, int q):
* -Artillery & Strength will decrease no matter True/False
* 7)int Sum(): return Target true time number
*/