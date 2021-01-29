using System;
using System.Diagnostics;

namespace Guarani
{
    class Program
    {
        static void Main(string[] args)
        {
            for (long s = 11; s < 40; s++)
            {
                var l = s;
                Change m = Solution.OptimalChange(l);
                if (m == null)
                {
                    Console.WriteLine($" {s} impossible");
                    continue;
                }
                Console.Write($"{s} Coin(s)  2€: " + m.coin2);
                Console.Write(" Bill(s)  5€: " + m.bill5);
                Console.WriteLine(" Bill(s) 10€: " + m.bill10);

                long result = m.coin2 * 2 + m.bill5 * 5 + m.bill10 * 10;
                Debug.Assert(result == s);
                //Console.WriteLine($" {s}Change given = " + result);
            }
        }

        class Change
        {
            public long coin2 = 0;
            public long bill5 = 0;
            public long bill10 = 0;
        }

        class Solution
        {

            // Do not modify this method​​​​​​‌​​‌​​​​​​‌‌‌​​​​​‌​‌​‌​​ signature
            public static Change OptimalChange(long s)
            {

                var change = new Change();
                if (s % 5 == 0)
                {
                    change.bill10 = s / 10;
                    change.bill5 = (s % 10) / 5;
                    return change;
                }

                change.bill10 = s / 10;
                s = s % 10;
                if ( s%2==1 & s<5)
                {
                    change.bill10 -= 1;
                    s += 10;
                    
                }


        
                    change.bill5 = s / 5;
                    s = s % 5;
                    if (s % 2 == 1 & s < 5)
                    {
                        change.bill5 -= 1;
                        s += 5;
                    }


                    change.coin2 = s / 2;
                    s = s % 2;
                

                if (s == 1)
                    return null;
                return change;
            }
        }
    }
}
