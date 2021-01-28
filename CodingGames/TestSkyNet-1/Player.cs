// http://www.codingame.com/ide/?target=clogin&s=1&id=308880198fd2c3d8aea9f3b171ac04a1da7abc&nickname=GuillaumeJ#!test:334823:true:%2523!list
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player
{
    static bool IsGapDone;
    static int currentSpeed;
    static int currentPosition;
    static void MainCG(String[] args)
    {
        int beforeGap = Convert.ToInt32(Console.ReadLine());

        int gap = Convert.ToInt32(Console.ReadLine());
        int afterGap = Convert.ToInt32(Console.ReadLine());
        Console.Error.WriteLine(beforeGap + "-" + gap + "-" + afterGap);
        int neededSpeed = gap + 1;
        int toReach = beforeGap + gap;
        int acceleration = (((neededSpeed + 1) * neededSpeed) / 2 - 1);
        int accelerateOn = (toReach - acceleration) % neededSpeed - 1;

        while (true)
        {
            currentSpeed = Convert.ToInt32(Console.ReadLine());
            currentPosition = Convert.ToInt32(Console.ReadLine());
            Console.Error.WriteLine("JumpOn :" + (toReach - neededSpeed));
            Console.Error.WriteLine("ToReachOn :" + toReach);
            Console.Error.WriteLine("Acceleration" + acceleration + "AccelerateOn :" + accelerateOn);
            IsGapDone = currentPosition >= toReach;
            string action = Decision(currentPosition, accelerateOn, toReach - neededSpeed, neededSpeed);


            Console.WriteLine(action);
        }
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