
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;


internal class PlayerQ1
{
    private class Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", X, Y);
        }
    }

    
    private static int width;
    private static int height;
    private static Coords lastPosition;
    private static bool YDone;
    private static int lastIncrement;
    private static string lastDirection;
    private static void AnswerChallenge()
    {
        var initData = ReadIntList(-1);
       width = initData[0];
         height = initData[1];
      
        int maxJump = ReadInt();
        initData = ReadIntList(-1);
        Coords batman = new Coords(initData[0], initData[1]);
        
        while (true)
        {
            string bombDirection = ReadLine();
     //     DisplayInputs();
            GetNewPosition(batman,  bombDirection);
#if DEBUG
            Debug.Assert(batman.X < width);
#endif
            myWriter.WriteLine(batman);
        }

    }

    private static void GetNewPosition(Coords batman, string bombDirection)
    {
        if (bombDirection == "UNKNOWN")
        {
            //lastPosition = new Coords(batman.X, batman.Y);
            //batman.X = bomb.XMax/2;
            //batman.Y = bomb.YMax/2;
            lastIncrement = FindFirstIncrement(batman.Y, height-1);
            Move(batman, 0, lastIncrement,bombDirection);
            lastDirection = "WARMER";
            return;
         
        }

        if (YDone == false)
        {
            if (bombDirection == "COLDER")
            {
                if (lastDirection == "WARMER")
                { // Found Y
                    batman.Y = lastPosition.Y;
                    YDone = true;
                    lastIncrement =int.MinValue;
                }
                else
                { 
                    lastIncrement = -1*lastIncrement;
                    Console.Error.WriteLine("Two cold");
                    Move(batman, 0, lastIncrement, bombDirection);
                    return;
                }
            }
            else
            { // mvoing warmer
                Move(batman, 0, lastIncrement, bombDirection);
                return;
            }
        }
        if (lastIncrement == int.MinValue)
        {
            lastIncrement = FindFirstIncrement(batman.X,width-1);
            Move(batman, 1, 0, bombDirection);
            lastDirection = "WARMER";
            return;
        }
        if (bombDirection == "COLDER")
        {
            if (lastDirection == "WARMER")
            { // Found X
                batman.X = lastPosition.X;
                lastIncrement = 0;
            }
            else
            {
                lastIncrement = -1 * lastIncrement;
                Console.Error.WriteLine("Two cold X");
                Move(batman, 0, lastIncrement, bombDirection);

                return;
            }
        }
        else
        { // mvoing warmer
            Move(batman, lastIncrement,0,bombDirection);
            return;
        }


    }

    private static int FindFirstIncrement(int pos, int max)
    {
        if (pos == max)
            return -1;
        int remaining = max - pos;
        if (remaining > pos)
        {
            return 1;
            
        }
        return -1;
    }

    private static void Move(Coords batman, int mx, int my,string direction)
    {
        lastPosition = new Coords(batman.X, batman.Y);
        batman.X += mx;
        batman.Y += my;
        lastDirection = direction;
    }

    protected static TextReader myReader;
    protected static TextWriter myWriter;

    private static void Main2(string[] args)
    {
#if DEBUG
        myReader = Console.In;
        myReader = new StreamReader("..\\..\\input1.txt");
        myWriter = Console.Out;
        //myWriter = new StreamWriter("..\\..\\output.txt");
#else
        myReader = Console.In;
        myWriter = Console.Out;
#endif
        AnswerChallenge();

        myReader.Close();
        myWriter.Close();
    }

    #region Common Code
    private static List<int> ReadIntList(int size)
    {
        List<int> list = new List<int>();
        if (size > 0)
        {
            for (int i = 0; i < size; i++)
            {
                list.Add(ReadInt());
            }
        }
        else
        {
            string[] line = ReadLine().Split(' ');
            list.AddRange(line.Select(str => Convert.ToInt32(str)));
        }
        return list;
    }

    private static List<String> Inputs = new List<String>();
    private static string ReadLine()
    {
        string t = myReader.ReadLine();
        Inputs.Add(t);
        return t;
    }

    private static void DisplayInputs()
    {
        Console.Error.WriteLine("--------------");
        foreach (String input in Inputs)
        {
            Console.Error.WriteLine(input);
        }
        Console.Error.WriteLine("--------------");
    }
    private static int ReadInt()
    {
        return Convert.ToInt32(ReadLine());
    }

    #endregion

}



