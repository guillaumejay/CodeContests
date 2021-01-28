
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Collections;
using System.Collections.Generic;



internal class Player
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


    public class Range
    {
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
            Step = 0;
        }

        public int Step { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public string MinResult { get; set; }

        public string MaxResult { get; set; }


        internal void SetMinToAverage()
        {
            MinResult = "";
            Min = (Min + Max) / 2;
          //  MaxResult = "";
            Step = 0;
        }
        internal void SetMaxToAverage()
        {
        //    MinResult = "";
            Max = (Min + Max) / 2;
            MaxResult = "";
            Step = 0;
        }

        public bool IsNew
        { get { return String.IsNullOrEmpty(MinResult) && String.IsNullOrEmpty(MaxResult); } }

        public bool IsGood {
            get { return Min == Max; } 
        }

        public int Coverage { get { return (Max - Min); } }

        internal int CloserValue
        {
               get
    {
     
                   return (MaxResult=="WARMER") ? Max : Min;
    }
    }
    }
    private static Range testRange;
    
    private static int width;
    private static int height;
    private static bool lastMoveWasMin=false;
    private static bool YDone;
    private static int lastIncrement;

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
                DisplayInputs();
            GetNewPosition(batman, bombDirection);

            myWriter.WriteLine(batman);
        }

    }

    private static void GetNewPosition(Coords batman, string bombDirection)
    {
        if (bombDirection == "UNKNOWN")
        {
   
            testRange = new Range(0, height - 1);

return;

        }
        if (YDone == false)
        {
            if (testRange.Step==0)
            {
                MoveTo(batman, 0, testRange.Min, bombDirection);
                testRange.Step = 1;
                return;
            }
            if (testRange.Step==1)
            {
                if (String.IsNullOrEmpty( testRange.MinResult))
                {
                    testRange.MinResult = bombDirection;
                    MoveTo(batman, 0, testRange.Max, bombDirection);
                    lastMoveWasMin = false;
                }
                else
                {

                    if (String.IsNullOrEmpty(testRange.MaxResult))
                    {
                        testRange.MaxResult = bombDirection;
                        MoveTo(batman, 0, testRange.Min, bombDirection);
                        lastMoveWasMin = true;
                    }

                }
                testRange.Step = 2;
                return;
            }
  
                testRange.MaxResult = bombDirection;
 
                if (testRange.Coverage <= 1)
                {
                    YDone = true;
                    batman.Y = testRange.CloserValue;
                    testRange = new Range(0, width - 1);
                }
                else
                {

                    if (testRange.MaxResult == "SAME" && testRange.MinResult == "SAME")
                    {
                        YDone = true;
                        batman.Y = (testRange.Max + testRange.Min)/2;
                        testRange = new Range(0, width - 1);
                    }
                    else
                    {
                        if (lastMoveWasMin && bombDirection == "WARMER")
                        {
                            testRange.SetMaxToAverage();
                        }
                        if (lastMoveWasMin && bombDirection == "COLDER")
                        {
                            testRange.SetMinToAverage();
                        }
                        if (!lastMoveWasMin && bombDirection == "WARMER")
                        {
                            testRange.SetMinToAverage();
                        }
                        if (!lastMoveWasMin && bombDirection == "COLDER")
                        {
                            testRange.SetMaxToAverage();
                        }
                        //if (testRange.MaxResult == "WARMER")
                        //{
                        //    //closer To MAx
                        //    testRange.SetMinToAverage();
                            
                        //}
                        //else
                        //{
                        //    testRange.SetMaxToAverage();
                        //}
                        if (testRange.IsGood)
                        {
                            testRange = new Range(0, width - 1);
                            YDone = true;
                        }
                        else
                        {
                            MoveTo(batman, 0, testRange.Min, bombDirection);
                            return;
                        }
                    }
                


            }

        }
       LookForX(batman, bombDirection);
        



    }

    private static void LookForX(Coords batman, string bombDirection)
    {
  //      Console.Error.WriteLine(batman.Y);
        if (testRange.Step == 0)
        {

            MoveTo(batman, testRange.Min, batman.Y, bombDirection);
            testRange.Step = 1;
            return;
        }
        if (testRange.Step == 1)
        {
            testRange.MinResult = bombDirection;
            MoveTo(batman, testRange.Max, batman.Y, bombDirection);
            testRange.Step = 2;
        }
        else
        {
            testRange.MaxResult = bombDirection;

            if (testRange.Coverage <= 1)
            {
                YDone = true;
                batman.X = testRange.CloserValue;
                testRange = new Range(0, width - 1);
            }
            //if (testRange.MaxResult == "SAME" && testRange.MinResult == "SAME")
            //{
            //    YDone = true;
            //    batman.Y = (testRange.Max + testRange.Min)/2;
            //}
            else
            {


                if (testRange.MaxResult == "WARMER")
                {
                    //closer To MAx
                    testRange.SetMinToAverage();
                }
                else
                {
                    testRange.SetMaxToAverage();
                }


                MoveTo(batman, testRange.Min, batman.Y, bombDirection);
                return;

            }

        }
    }

    private static void MoveTo(Coords batman, int x, int y, string direction)
    {
      
        batman.X = x;
        batman.Y = y;
   
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




