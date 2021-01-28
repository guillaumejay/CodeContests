
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;


internal class Player
{
    private class BatMan
    {
        public BatMan(int x, int y)
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

    public class Bomb
    {
        public Bomb(int xMax, int yMax)
        {
            YMax = yMax;
            XMax = xMax;
        }

        public int XMin { get; set; }
        public int XMax { get; set; }
        public int YMin { get; set; }
        public int YMax { get; set; }
    }
    private static void AnswerChallenge()
    {
        var initData = ReadIntList(-1);
        int width = initData[0];
        int height = initData[1];
        Bomb bomb=new Bomb(width, height);
        int maxJump = ReadInt();
        initData = ReadIntList(-1);
        BatMan batman = new BatMan(initData[0], initData[1]);
        
        while(true)
        {
            string bombDirection=ReadLine();

       //     DisplayInputs();

            GetNewPosition(batman, bomb,bombDirection);
            #if DEBUG
                Debug.Assert(batman.X<width);
#endif
            myWriter.WriteLine(batman);
        }
       
    }

    private static void GetNewPosition(BatMan batman, Bomb bomb,string bombDirection)
    {
        if (bombDirection.StartsWith("U"))
        {
            bomb.YMax = Math.Min(batman.Y,bomb.YMax);
            //batman.Y = (bomb.YMax + bomb.YMin)/2;
        }
        if (bombDirection.StartsWith("D"))
        {
            bomb.YMin = Math.Max(batman.Y, bomb.YMin);
            //batman.Y = (bomb.YMin + bomb.YMax) / 2;
        }        
        
        if (bombDirection.EndsWith("R"))
        {
            bomb.XMin = Math.Max(batman.X, bomb.XMin);
        }
        if (bombDirection.EndsWith("L"))
        {
            bomb.XMax = Math.Min(batman.X, bomb.XMax);
        }
        batman.Y = (bomb.YMax + bomb.YMin) / 2;
        batman.X = (bomb.XMax + bomb.XMin) / 2;
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



