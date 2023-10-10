/*
 * 
* Summer Xia - cpsc3200
* 6 / 2 / 23
* revision history: 4/27 -> 6/2/2023
*/
using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using P5;

namespace P5
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

        FighterGuard Get_FG_Obj(int n)
        {
            int[] handle = HandleMaker(); // Common handle array for all cases

            switch (n % 3)
            {
                case 0:
                    // guard
                    return new FighterGuard(handle, 1);

                case 1:
                    // skipGuard
                    return new FighterGuard(handle, 2);

                case 2:
                    // quirkyGuard
                    return new FighterGuard(handle, 3);
            }

            // Handle the case when n is 0 or less than 0
            return new FighterGuard(handle, 1);
        }

        TurretGuard Get_TG_Obj(int n)
        {
            int[] handle = HandleMaker(); // Common handle array for all cases

            switch (n % 3)
            {
                case 0:
                    // guard
                    return new TurretGuard(handle, 1);

                case 1:
                    // skipGuard
                    return new TurretGuard(handle, 2);

                case 2:
                    // quirkyGuard
                    return new TurretGuard(handle, 3);
            }

            // Handle the case when n is 0 or less than 0
            return new TurretGuard(handle, 1);
        }

        InfantryGuard Get_IG_Obj(int n)
        {
            int[] handle = HandleMaker(); // Common handle array for all cases

            switch (n % 3)
            {
                case 0:
                    // guard
                    return new InfantryGuard(handle, 1);

                case 1:
                    // skipGuard
                    return new InfantryGuard(handle, 2);

                case 2:
                    // quirkyGuard
                    return new InfantryGuard(handle, 3);
            }

            // Handle the case when n is 0 or less than 0
            return new InfantryGuard(handle, 1);
        }

        void Test_FG_Hurt(FighterGuard obj)
        {
            int original = obj.StrengthGetter;
            Console.WriteLine($"Original Strength:{original}");
            obj.Hurt(0, 10);
            int first = obj.StrengthGetter;
            Console.WriteLine($"Strength after first hurt:{first}");
            obj.Hurt(0, 10);
            int second = obj.StrengthGetter;
            Console.WriteLine($"Strength after second hurt: {second}");
            obj.Hurt(0, 10);
            int third = obj.StrengthGetter;
            Console.WriteLine($"Strength after third hurt:{third}");
        }

        void Test_TG_Hurt(TurretGuard obj)
        {
            int original = obj.StrengthGetter;
            Console.WriteLine($"Original Strength:{original}");
            obj.Hurt(0, 10);
            int first = obj.StrengthGetter;
            Console.WriteLine($"Strength after first hurt:{first}");
            obj.Hurt(0, 10);
            int second = obj.StrengthGetter;
            Console.WriteLine($"Strength after second hurt: {second}");
            obj.Hurt(0, 10);
            int third = obj.StrengthGetter;
            Console.WriteLine($"Strength after third hurt:{third}");
        }

        void Test_IG_Hurt(InfantryGuard obj)
        {
            int original = obj.StrengthGetter;
            Console.WriteLine($"Original Strength:{original}");
            obj.Hurt(0, 10);
            int first = obj.StrengthGetter;
            Console.WriteLine($"Strength after first hurt:{first}");
            obj.Hurt(0, 10);
            int second = obj.StrengthGetter;
            Console.WriteLine($"Strength after second hurt: {second}");
            obj.Hurt(0, 10);
            int third = obj.StrengthGetter;
            Console.WriteLine($"Strength after third hurt:{third}");
        }

        FighterGuard[] Create_FG_Array()
        {
            FighterGuard[] bigArr = new FighterGuard[10];
            for (int k = 0; k < bigArr.Length; k++)
            {
                bigArr[k] = Get_FG_Obj(k);
            }
            return bigArr;
        }

        TurretGuard[] Create_TG_Array()
        {
            TurretGuard[] bigArr = new TurretGuard[10];
            for (int k = 0; k < bigArr.Length; k++)
            {
                bigArr[k] = Get_TG_Obj(k);
            }
            return bigArr;
        }

        InfantryGuard[] Create_IG_Array()
        {
            InfantryGuard[] bigArr = new InfantryGuard[10];
            for (int k = 0; k < bigArr.Length; k++)
            {
                bigArr[k] = Get_IG_Obj(k);
            }
            return bigArr;
        }

        void FG_HC()
        {
            FighterGuard[] bigArr = Create_FG_Array();
            for (int k = 0; k < bigArr.Length; k++)
            {
                Console.WriteLine($"-------------------{bigArr[k]}[{k}]-------------------");
                if (bigArr[k].GuardType == typeof(skipGuard))
                {
                    Console.WriteLine("This has a skipGuard Sub-Object");
                }
                else if (bigArr[k].GuardType == typeof(quirkyGuard))
                {
                    Console.WriteLine("This has a quirkyGuard Sub-Object");
                }
                else if (bigArr[k].GuardType == typeof(Guard))
                {
                    Console.WriteLine("This has a Guard Sub-Object");
                }
                Test_FG_Hurt(bigArr[k]);
            }
        }

        void TG_HC()
        {
            TurretGuard[] bigArr = Create_TG_Array();
            for (int k = 0; k < bigArr.Length; k++)
            {
                Console.WriteLine($"-------------------{bigArr[k]}[{k}]-------------------");
                Test_TG_Hurt(bigArr[k]);
            }
        }
        void IG_HC()
        {
            InfantryGuard[] bigArr = Create_IG_Array();
            for (int k = 0; k < bigArr.Length; k++)
            {
                Console.WriteLine($"-------------------{bigArr[k]}[{k}]-------------------");
                Test_IG_Hurt(bigArr[k]);
            }
        }

        static void Main(string[] args)
        {
            Driver fighter_guard_obj = new Driver();
            fighter_guard_obj.FG_HC();
            Driver turret_guard_obj = new Driver();
            fighter_guard_obj.TG_HC();
            Driver infantry_guard_obj = new Driver();
            fighter_guard_obj.IG_HC();
        }
    }
}
