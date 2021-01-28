
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;



internal class Player
{

    public class Elevator
    {
        public Elevator(int floor, int pos)
        {
            Floor = floor;
            Pos = pos;
        }

        public int Floor { get; set; }

        public int Pos { get; set; }
    }

    private static List<Elevator> Elevators = new List<Elevator>();
    private static void AnswerChallenge()
    {

        var inputs = ReadIntList(-1);
        int nbFloors = (inputs[0]); // number of floors
        int width = (inputs[1]); // width of the area
        int nbRounds = (inputs[2]); // maximum number of rounds
        int exitFloor = (inputs[3]); // floor on which the exit is found
        int exitPos = (inputs[4]); // position of the exit on its floor
        int nbTotalClones = (inputs[5]); // number of generated clones
        int nbAdditionalElevators = (inputs[6]); // ignore (always zero)
        int nbElevators = (inputs[7]);
        for (int i = 0; i < nbElevators; i++)
        {
            inputs = ReadIntList(-1);
            Elevators.Add(new Elevator(inputs[0], inputs[1]));

        }
        Elevator current = Elevators.FirstOrDefault();

        int maxFloor = 0;
        while (true)
        {
            int posNeeded = exitPos;
            string action = "WAIT";
            var turnInput = ReadLine().Split(' ');
            int cloneFloor = int.Parse(turnInput[0]); // floor of the leading clone
            int clonePos = int.Parse(turnInput[1]); // position of the leading clone on its floor
            String direction = turnInput[2];
            current = Elevators.SingleOrDefault(x => x.Floor == cloneFloor);
            if (current != null)
            {
                Console.Error.WriteLine("{0} {1}  ", cloneFloor, current.Floor);
                if (cloneFloor <= current.Floor)
                    posNeeded = current.Pos;
            }

            if (clonePos > posNeeded && direction == "RIGHT")
            {
                action = "BLOCK";
            }
            if (clonePos < posNeeded && direction == "LEFT")
            {
                action = "BLOCK";
            }
            Console.Error.WriteLine("{0} {1}  {2}", posNeeded, direction, clonePos);
#if DEBUG

#endif
            myWriter.WriteLine(action);
        }

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



