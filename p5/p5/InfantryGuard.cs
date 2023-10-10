/*
 * 
* Summer Xia - cpsc3200
* 6 / 2 / 23
* revision history: 5/29 -> 6/2/2023
* 
* Class Invariant :
* 1)InfantryGuard(int[] w, uint choice = 1): Multiple inhereted object InfantryGuard constctructor
* 2)bool IsActive(): Expect Infantry is Armed; return Infantry IsActive
* 3)bool IsAlive(): Expect Infantry is Armed; return Infantry IsAlive
* 4)bool HasDefense(): return subObject's IsAlive()
* 5)bool Attack(int x,int y, int q): return Infantry's Target;
* 6)void Hurt(int pos, int points): strength will be deducted based on subObject's block 
*/
using System;
namespace P5
{
    public class InfantryGuard : Infantry
    {
        private Guard g;
        private int ReviveCount;
        //getters for unit test
        public Guard GuardGetter { get { return g; } }

        public InfantryGuard(int[] w, uint choice = 1) : base(w)
        {
            if (choice == 1) g = new Guard(w);
            else if (choice == 2) g = new skipGuard(w);
            else g = new quirkyGuard(w);
            ReviveCount = 0;
        }

        public override bool IsActive()
        { return base.IsActive(); }

        public override bool IsAlive()
        { return base.IsAlive(); }

        public bool HasDefense()
        { return g.IsAlive(); }

        public void Attack(int x, int y, int q)
        { base.Target(x, y, q); }

        public void Hurt(int x, int points)
        {
            //bolock->hurt
            g.block(x);
            //shield exist
            if (g.DurabilityGetter[x] != 0)
            { return; }
            Strength -= points;
            //almost died
            if (Strength > 0 && !IsAlive() && ReviveCount <= 3)
            { Reset(); }
        }
    }
}

/*
 * Implementation invariant:
 * 
 * Attribute: 
 * Guard GuardGetter: for unit test for type checking
 * Guard g: based on constructor will triger to call different guard subobject
 * int ReviveCount: counter of reset
 * 
 * Methods:
* 1)TurretGuard(int[] w, uint choice = 1):choice have default to create guard subobject.
* 2)bool IsActive(): redirect based on turret class, show attack weapon force
* 3)bool IsAlive(): redirect based on turret class, show attack strength force
* 4)bool HasDefense():redirect based on internal guard object's IsAlive()
* 5)bool Attack(int x,inty, int q): redirect based on turret's Target()
* 6)void Hurt(int pos, int points): position will call guard object's block, if blocked then strength won't decrease; otherwise decrease
 */