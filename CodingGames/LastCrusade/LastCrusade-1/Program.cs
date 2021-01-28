
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;

internal class Player
{
    private class Position
    {
        public Position(int x, int y)
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


        internal void GoLeft()
        {
            X--;
        }
        internal void GoRight()
        {
            X++;
        }

        internal void GoDown()
        {
            Y++;
        }
    }

    private static int[,] roomTypes;

    private static void AnswerChallenge()
    {


        var initData = ReadIntList(-1);
        int width = initData[0];
        int height = initData[1];
        roomTypes = new int[height, width];
        //   Console.WriteLine("DATRA "  + width +' ' + height);
        for (int i = 0; i < height; i++)
        {
            //    Console.WriteLine("TEST");
            var line = ReadIntList(-1);
            for (int x = 0; x < width; x++)
            {
                roomTypes[i, x] = line[x];
                //Console.Write  (roomTypes[i, x]);
            }
            //    Console.WriteLine();
        }
        int xExit = ReadInt();
        while (true)
        {
            string line = ReadLine();
            //    Console.WriteLine(line);
            string[] currentTurn = line.Split(' ');

            int currentX = Convert.ToInt32(currentTurn[0]), currentY = Convert.ToInt32(currentTurn[1]);
            string comingFrom = currentTurn[2];

            Position pos = GuessNextPosition(currentX, currentY, comingFrom);
            myWriter.WriteLine(pos);
            DisplayInputs();
        }
    }

    private static Position GuessNextPosition(int currentX, int currentY, string comingFrom)
    {
        int currentType = roomTypes[currentY, currentX];
        Position pos = new Position(currentX, currentY);
        switch (currentType)
        {
            case 0:
                throw new ArgumentException("RoomType 0");
            case 1:
            case 3:
            case 7:
            case 8:
            case 9:
            case 12:
            case 13:
                pos.GoDown();
                break;
            case 2:
            case 6:
                if (comingFrom == "LEFT")
                    pos.GoRight();
                else
                {
                    pos.GoLeft();
                }
                break;
            case 4:
                if (comingFrom == "TOP")
                    pos.GoLeft();
                else
                {
                    pos.GoDown();
                }
                break;
            case 5:
                if (comingFrom == "TOP")
                    pos.GoRight();
                else
                {
                    pos.GoDown();
                }
                break;
            case 10:
                pos.GoLeft();
                break;
            case 11:
                pos.GoRight();
                break;

        }
        return pos;

    }

    protected static TextReader myReader;
    protected static TextWriter myWriter;

    private static void Main(string[] args)
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
        foreach(String input in Inputs)
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



