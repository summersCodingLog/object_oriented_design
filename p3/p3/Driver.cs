/*
* Summer Xia - cpsc3200
* 6 / 2 / 23
* revision history: 4/27 -> 6/2/2023
*/
using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using P3; 

namespace P3
{
    class Driver
    {
        int[] HandleMaker()
        {
            Random rand = new Random();
            int size = rand.Next(1, 11);
            int[] handle = new int[size];
            for (int i = 0; i < size; i++)
            {
                handle[i] = rand.Next(100);
            }
            return handle;
        }

        Fighter GetObj(int n)
        {
            if (n % 2 == 0)
            {
                int[] handle = HandleMaker();
                return new Fighter(handle);
            }
            if (n % 3 == 0)
            {
                int[] handle = HandleMaker();
                return new Turret(handle);
            }
            else
            {
                int[] handle = HandleMaker();
                return new Infantry(handle);
            }
        }

        Fighter[] CreateFighterArray()
        {
            Fighter[] bigArr = new Fighter[30];
            for (int k = 0; k < bigArr.Length; k++)
            {
                bigArr[k] = GetObj(k);
            }
            return bigArr;
        }

        void TestMove(Fighter fighter)
        {
            Random rand = new Random();
            int x = rand.Next(1, 11);
            int y = rand.Next(1, 11);
            bool canMove = fighter.Move(x, y);
            if (canMove)
            {
                Console.WriteLine($"Move({x},{y})");
            }
            else
            {
                Console.WriteLine($"Move({x},{y}) Failed.");
            }
        } 

        void PrintAttackRange(int[] row, int[] col)
        {
            Console.WriteLine($"Row Attack Range:({row[0]},{row[row.Length - 1]})");
            Console.WriteLine($"Col Attack Range:({col[0]},{col[col.Length - 1]})");
        }

        void PrintFighterStatus(Fighter fighter)
        {
            if (fighter.IsActive() && fighter.IsAlive())
            {
                Console.WriteLine("Fighter is both Active and Alive");
            }
            else if (!fighter.IsActive() && fighter.IsAlive())
            {
                Console.WriteLine("Fighter is Out Of Firearms but Alive");
            }
            else if (fighter.IsActive() && !fighter.IsAlive())
            {
                Console.WriteLine("Fighter has Firearms but Dead");
            }
            else
            {
                Console.WriteLine("Fighter is born dead");
            }
        }

        void TargetEnemy(Fighter fighter)
        {
            do
            {
                fighter.Target(0, 0, 0);
            } while (fighter.IsAlive());

            if (!fighter.IsActive() && !fighter.IsAlive())
            {
                Console.WriteLine("Call Target() until Fighter is both Out Of Firearms and Dead");
            }
            else if (fighter.IsActive())
            {
                Console.WriteLine("Call Target() until Fighter is Dead.");
                Console.WriteLine("...Oh wait, there's some leftover Firearms!");
            }
        }

        void PrintSum(Fighter fighter)
        {
            Console.WriteLine($"Sum = {fighter.Sum()}");
        }

        void HC()
        {
            Fighter[] bigArr = CreateFighterArray();

            for (int k = 0; k < bigArr.Length; k++)
            {
                Console.WriteLine($"-------------------{bigArr[k]}[{k}]-------------------");
                TestMove(bigArr[k]);
                //ShiftTurret(bigArr[k]);
                int[] row = bigArr[k].RowAttkRangeGetter;
                int[] col = bigArr[k].ColAttkRangeGetter;
                PrintAttackRange(row, col);
                PrintFighterStatus(bigArr[k]);
                TargetEnemy(bigArr[k]);
                PrintSum(bigArr[k]);
            }
        }

        //void HC()
        //{
        //    //array of base object inorder to use heterogeneous collection
        //    Fighter[] bigArr = CreateFighterArray();

        //    for (int k = 0; k < bigArr.Length; k++)
        //    {
        //        Random rand = new Random();
        //        Console.WriteLine($"-------------------{bigArr[k]}[{k}]-------------------");
        //        //test Move function, expect all Turrest object fail to move
        //        int x = rand.Next(1, 11);
        //        int y = rand.Next(1, 11);
        //        //dynamic behavior realized the polymorphism
        //        bool CanMove = bigArr[k].Move(x, y);
        //        if (CanMove)
        //        {
        //            Console.WriteLine($"Move({x},{y})");
        //        }
        //        else Console.WriteLine($"Move({x},{y}) Failed.");

        //        //Shift(x) Will Alter Turret's row position
        //        bigArr[k].Shift(x);

        //        int[] row = bigArr[k].RowAttkRangeGetter;
        //        int[] col = bigArr[k].ColAttkRangeGetter;
        //        Console.WriteLine($"Row Attack Range:({row[0]},{row[row.Length - 1]})");
        //        Console.WriteLine($"Col Attack Range:({col[0]},{col[col.Length - 1]})");

        //        if (bigArr[k].IsActive() && bigArr[k].IsAlive())
        //        {
        //            Console.WriteLine("Fighter is both Active and Alive");
        //        }
        //        else if (!bigArr[k].IsActive() && bigArr[k].IsAlive())
        //        {
        //            Console.WriteLine("Fighter is Out Of Firearms but Alive");
        //        }
        //        else if (bigArr[k].IsActive() && !bigArr[k].IsAlive())
        //        {
        //            Console.WriteLine("Fighter has Firearms but Dead");
        //        }
        //        else Console.WriteLine("Fighter is born dead");

        //        Console.WriteLine("....Targetting Enemy....");

        //        do
        //        {
        //            bigArr[k].Target(0, 0, 0);
        //        } while (bigArr[k].IsAlive());

        //        if (!bigArr[k].IsActive() && !bigArr[k].IsAlive())
        //        {
        //            Console.WriteLine("Call Target() until Fighter is both Out Of Firearms and Dead");
        //        }
        //        else if (bigArr[k].IsActive())
        //        {
        //            Console.WriteLine("Call Target() until Fighter is Dead.");
        //            Console.WriteLine("...Oh wait, there's some leftover FireArms!");
        //        }
        //        Console.WriteLine($"Sum = {bigArr[k].Sum()}");

        //    }
        //}
        static void Main(string[] args)
        {
            Driver myObj = new Driver();
            myObj.HC();
        }
    }
}