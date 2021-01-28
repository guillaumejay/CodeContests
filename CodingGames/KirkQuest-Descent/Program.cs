using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace KirkQuest_Descent
{
    class Player
    {
        private static void AnswerChallenge()
        {
            while (true)
            {
                string action = "HOLD";
                var playerData = ReadIntList(-1);
                int currentPosition = playerData[0], currentAltitude = playerData[1];
                List<int> heights = ReadIntList(8);
                int maxHeight = heights.Max(x => x);
                if (maxHeight > 0 && heights[currentPosition] == maxHeight)
                {
                    action = "FIRE";
                }
                Console.WriteLine(action);
            }
        }

        protected static TextReader myReader;
        protected static TextWriter myWriter;
        static void Main(string[] args)
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
                string[] line = myReader.ReadLine().Split(' ');
                list.AddRange(line.Select(str => Convert.ToInt32(str)));
            }
            return list;
        }

        private static int ReadInt()
        {
            return Convert.ToInt32(myReader.ReadLine());
        }

        #endregion

    }
}

