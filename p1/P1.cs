/*
* Summer Xia - cpsc3200
* 4 / 4 / 23
* revision history: 3/31 -> 4/3 -> 4/4/2023
*/
using System;

namespace p1
{
    class Driver
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an array of 10 Lumen objects with seemingly random distribution of lumens, sizes, and initial power
                Lumen[] light = new Lumen[10];
                Random rand = new Random();
                for (int i = 0; i < light.Length; i++)
                {
                    uint brightness = Convert.ToUInt32(rand.Next(50, 100));
                    uint size = Convert.ToUInt32(rand.Next(1, 5));
                    light[i] = new Lumen(brightness, size);
                }

                for (int i = 0; i < light.Length; i++)
                {
                    Console.WriteLine($"----------------------Object {i + 1}:----------------------");

                    for (int j = 0; j < 5; j++)
                    {
                        // Test the IsActive() method for all lumens
                        Console.WriteLine($"Is active? {light[i].IsActive()}");
                        // Test the IsStable() method for all lumens
                        Console.WriteLine($"Is stable? {light[i].IsStable()}");
                        // Test the Glow() method for all lumens
                        Console.WriteLine($"Glow returns: {light[i].Glow()}");

                        // Test the Reset() method for all lumens
                        bool r = light[i].Reset();
                        if (r)
                        {
                            Console.WriteLine("Congrats! Object resetes!");
                        }
                        else
                        {
                            Console.WriteLine($"Was reset successful? {r}");
                        }
                    }

                    Console.WriteLine($"------------------------End {i + 1}------------------------");
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("{0} Argument can not be null.", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} exception caught.", e);
            }


        }
    }

}