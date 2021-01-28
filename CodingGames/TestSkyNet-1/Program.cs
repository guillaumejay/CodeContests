using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestSkyNet_1
{
    class Program
    {

        static int beforeGap = 16;
        static int gap = 2;
        static int afterGap = 16;
        static int currentPosition = 1;
        static int currentSpeed ;
        static int maxSpeedAfterGap;
        private static bool IsGapDone = false;
        static void Main(string[] args)
        {
            int neededSpeed = gap + 1;
            int toReach = beforeGap + gap;
            int acceleration = (((neededSpeed + 1) * neededSpeed) / 2 - 1);
            int accelerateOn = (toReach - acceleration) % neededSpeed - 1;
            while (true)
            {
                // Read information from standard input
                

                // Compute logic here

                // Console.Error.WriteLine("Debug messages...");

                // Write action to standard output
              
                IsGapDone = currentPosition >= toReach;
                string action = Decision(currentPosition,accelerateOn,toReach-neededSpeed,neededSpeed);
            

                Console.WriteLine(action);
                if (action == "SPEED")
                    currentSpeed++;
                if (action == "SLOW")
                    currentSpeed--;
                currentPosition += currentSpeed;
                if (currentPosition > beforeGap && currentPosition <= beforeGap + gap)
                {
                    Console.WriteLine("lose");
                    break;
                }
                if (IsGapDone && currentSpeed == 0)
                {
                    Console.WriteLine("win");
                    break;
                }
            }
            Console.ReadLine();
        }


        private static string Decision(int currentPosition, int accelerateOn, int jumpOn, int neededSpeed)
        {
            if (currentSpeed == 0)
                return "SPEED";
            if (currentSpeed > neededSpeed || IsGapDone)
                return "SLOW";

            string action = "WAIT";
            if (jumpOn == currentPosition)
                action = "JUMP";
            else
            {
                if (currentPosition >= accelerateOn)
                {
                    if (currentSpeed < neededSpeed)
                    {
                        action = "SPEED";
                    }
                }
            }
            return action;
        }
    }
}
