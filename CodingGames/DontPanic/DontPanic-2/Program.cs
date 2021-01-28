
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


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
    private static List<int> distances = null;
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
        int nbAdditionalElevators = (inputs[6]);
        int nbElevators = (inputs[7]);
        for (int i = 0; i < nbElevators; i++)
        {
            inputs = ReadIntList(-1);
            Elevators.Add(new Elevator(inputs[0], inputs[1]));

        }
        Elevator current = Elevators.FirstOrDefault();
        int buildRequired = 0;
        for (int i = 0; i < exitFloor; i++)
        {
            if (Elevators.All(x => x.Floor != i))
            {
                buildRequired++;
            }
        }
        int available = nbAdditionalElevators - buildRequired;
        int Worst=int.MinValue; // worst distance on every floor

        string action;
        while (true)
        {

            int posNeeded = exitPos;
            action = "WAIT";
            var turnInput = ReadLine().Split(' ');
            int cloneFloor = int.Parse(turnInput[0]); // floor of the leading clone
            int clonePos = int.Parse(turnInput[1]); // position of the leading clone on its floor
            DisplayInputs();
            if (distances == null)
            {
                 Worst= GetMaxByFloors( clonePos, exitFloor,cloneFloor);
            }

            String direction = turnInput[2];
            if (direction != "NONE")
            {
                int currentDistance ;
                current = null;
                var currents = Elevators.Where(x => x.Floor == cloneFloor);
                if (currents.Count() == 1)
                {
                    current = currents.First();
                    currentDistance = Math.Abs(current.Pos - clonePos);
                }
                else
                {

                    currentDistance = GetDistance(cloneFloor, clonePos, out current);

                }

                if (current != null)
                {


                    if (available > 0)
                    {
                        Console.Error.WriteLine("m{0} w{1}", currentDistance, Worst);
                        if (currentDistance == Worst)
                        {
                            action = "ELEVATOR";
                            current = null;
                        }

                    }
                }
                        if (current != null)
                        {
                            if (cloneFloor <= current.Floor)
                                posNeeded = current.Pos;
                        }
                    

                        else
                        {
                            if (cloneFloor != exitFloor)
                            {
                                action = "ELEVATOR";
                            }
                        }
                        if (action != "ELEVATOR")
                        {
                            if (clonePos > posNeeded && direction == "RIGHT")
                            {
                                action = "BLOCK";
                            }
                            if (clonePos < posNeeded && direction == "LEFT")
                            {
                                action = "BLOCK";
                            }
                        }

                        Console.Error.WriteLine("{0} {1}  {2}", posNeeded, direction, clonePos);
                    }
                

            
            if (action == "ELEVATOR")
            {
                available--;
                Elevators.Add(new Elevator(cloneFloor, clonePos));
                Console.Error.Write("Built F{0} P{1}", cloneFloor, clonePos);
                Worst = GetMaxByFloors(cloneFloor + 1, exitFloor, clonePos);
            }
            myWriter.WriteLine(action);
        }

    }

    private static int GetMaxByFloors( int clonePos, int exitFloor,int currentFloor)
    {
        int worst = int.MinValue;
        distances = new List<int>();
        int startPos = clonePos;
        for (int i = currentFloor; i < exitFloor; i++)
        {
            Elevator e;
            int dist = GetDistance(i, startPos, out e);
            distances.Add(dist);
            if (dist > worst)
                worst = dist;
            
            startPos = (e==null)?startPos:e.Pos;
        }
        return worst;
    }

    private static int GetDistance(int floor, int pos, out Elevator best)
    {
        int max = int.MaxValue;
        best = null;
        var currents = Elevators.Where(x => x.Floor == floor);
        if (currents.Any())
        {
            foreach (var e in currents)
            {
                int distance = Math.Abs(pos - e.Pos);
                if (distance < max)
                {
                    max = distance;
                    best = e;

                }

            }
        }
        return max;
    }


    protected static TextReader myReader;
    protected static TextWriter myWriter;

    private static void Main(string[] args)
    {
#if DEBUG
        myReader = Console.In;
        myReader = new StreamReader("..\\..\\test1.txt");
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



