/*
 * 
* Summer Xia - cpsc3200
* 6 / 2 / 23
* revision history: 5/29 -> 6/2/2023
* 
* Class Invariant :
* 1)FightGuard(int[] w, uint choice = 1): Multiple inhereted object FightGuard constctructor
* 2)bool IsActive(): Expect Figher is Armed; return Fighter IsActive
* 3)bool IsAlive(): Expect Figher is Armed; return Fighter IsAlive
* 4)bool HasDefense(): return subObject's IsAlive()
* 5)bool Attack(int x,int y, int q): return Fighter's Target;
* 6)void Hurt(int pos, int points): strength will be deducted based on subObject's block
* 7)void Revive(): can only be internal trigered by Hyrt(); strength will be reset back to original if condition meet
*/
using System;
namespace P5
{
    public class FighterGuard : Fighter
    {
        private int OriginalStrength;
        private Guard g;
        private int ReviveCount;
        public Type GuardType { get { return g.GetType(); } }

        public FighterGuard(int[] w, uint choice = 1) : base(w)
        {
            if (choice == 1) g = new Guard(w);
            else if (choice == 2) g = new skipGuard(w);
            else g = new quirkyGuard(w);
            ReviveCount = 0;
            OriginalStrength = Strength;
        }

        public override bool IsActive()
        { return base.IsActive(); }

        public override bool IsAlive()
        { return base.IsAlive(); }

        public bool HasDefense()
        { return g.IsAlive(); }

        public bool Attack(int x, int y, int q)
        {
            return base.Target(x, y, q);
        }

        public void Hurt(int pos, int points)
        {
            //bolock->hurt
            g.block(pos);
            //shield exist
            if (g.DurabilityGetter[pos] != 0)
            { return; }
            Strength -= points;
            Artillery[pos] = 0;
            //almost died
            if (Strength > 0 && !IsAlive())
            {
                Revive();
            }
        }

        public void Revive()
        {
            if (ReviveCount <= 3)
            {
                ReviveCount++;
                Strength = OriginalStrength;
            }
        }
    }
}

/*
 * Implementation invariant:
 * 
 * Attribute: 
 * int OriginalStrength: saved original strength
 * Guard g:based on constructor will triger to call different guard subobject
 * int ReviveCount: counter of reset
 * Type GuardType: inorder to check it indeed create different guard subobject
 * 
 * Methods:
* 1)FightGuard(int[] w, uint choice = 1):choice have default to create guard subobject.
* 2)bool IsActive(): redirect based on fighter base class, show attack weapon force
* 3)bool IsAlive(): redirect based on fighter base class, show attack strength force
* 4)bool HasDefense():redirect based on internal guard object's IsAlive()
* 5)bool Attack(int x,inty, int q): redirect based on fighter's Target()
* 6)void Hurt(int pos, int points): position will call guard object's block, if blocked then strength won't decrease;otherwise decrease
* 7)void Revive():is object is no longer alive but is not yet 0, it can have three times to reset back to original strength
* 
 */