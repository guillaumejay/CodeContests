
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

        public Position(Position pos)
        {
            X = pos.X;
            Y = pos.Y;
            ComingFrom = pos.ComingFrom;
        }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public string ComingFrom { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", X, Y);
        }


        internal void GoLeft()
        {
            X--;
            ComingFrom = "RIGHT";
        }
        internal void GoRight()
        {
            X++;
            ComingFrom = "LEFT";
        }

        internal void GoDown()
        {
            Y++;
            ComingFrom = "TOP";
        }
    }

    private static int[,] roomTypes;
    private static int width, height, xExit;

    private static void AnswerChallenge()
    {
        var initData = ReadIntList(-1);
        width = initData[0];
        height = initData[1];
        roomTypes = new int[height, width];
        Console.Error.WriteLine(width + ' ' + height);
        for (int i = 0; i < height; i++)
        {
            var line = ReadIntList(-1);
            for (int x = 0; x < width; x++)
            {
                roomTypes[i, x] = line[x];
                Console.Error.Write(roomTypes[i, x] + " ");
            }
            Console.Error.WriteLine();
        }
        xExit = ReadInt();
        Console.Error.WriteLine(xExit);
        while (true)
        {
            string line = myReader.ReadLine();

            string[] currentTurn = line.Split(' ');
            Console.Error.WriteLine("Turn: " + line);
            int currentX = Convert.ToInt32(currentTurn[0]), currentY = Convert.ToInt32(currentTurn[1]);
            string comingFrom = currentTurn[2];
            int nbRocks = ReadInt();
            Position currentPos = new Position(currentX, currentY) { ComingFrom = comingFrom };
            Position nextPos = null;
            string action = GetAction(currentPos, ref nextPos);
            Position lastPos = new Position(nextPos);
            while (action == "WAIT")
            {



                if (roomTypes[nextPos.Y, nextPos.X] == 0)
                {
                    action = GetRotationValid(lastPos);
                    break;
                }
                lastPos = new Position(nextPos);
                action = GetAction(nextPos, ref nextPos);
                if (action == "WAIT")
                {

                }

            }
            if (action == "WON")
            {
                action = "WAIT";
            }
            myWriter.WriteLine(action);
        }
    }

    private static string GetRotationValid(Position lastPos)
    {
        int currentType = roomTypes[lastPos.Y, lastPos.X];
        int newType = LeftRotate(currentType);
        if (IsPosValid(GetNextPosition(newType, new Position(lastPos))))
        {
            roomTypes[lastPos.Y, lastPos.X] = newType;
            return lastPos + " LEFT";
        }
        roomTypes[lastPos.Y, lastPos.X] = RightRotate(currentType); ;
        return lastPos + " RIGHT";
    }

    private static string GetAction(Position current, ref Position next)
    {
        string action = "WAIT";
        next = GetNextPosition(current.X, current.Y, current.ComingFrom);
        int roomType = roomTypes[next.Y, next.X];

        bool isValid = IsPosValid(next.ComingFrom, roomType);
        if (!isValid)
        {

            int newRoomType;
            newRoomType= RightRotate(roomType);
            //  Position accordingToNewRT = GetNextPosition(newRoomType, new Position(next));
            if (IsPosValid(next.ComingFrom, newRoomType))// && IsPosValid(accordingToNewRT))
            {
                action = next.ToString() + " RIGHT";
            }
            else
            {
                newRoomType = LeftRotate(roomType);
                if (IsPosValid(next.ComingFrom, newRoomType))
                {
                    action = next.ToString() + " LEFT";
                }
            }
            roomTypes[next.Y, next.X] = newRoomType;
        }
        if (next.Y == height - 1 && next.X == xExit)
            action = "WON";
        return action;
    }
    private static int LeftRotate(int roomType)
    {
        int newRoomType = 0;
        if (roomType < 0)
            return roomType;
        switch (Math.Abs(roomType))
        {
            case 0:
                return 0;
            case 1:
                newRoomType = 1;
                break;
            case 2:
                newRoomType = 3;
                break;
            case 3:
                newRoomType = 2;
                break;
            case 4:
                newRoomType = 5;
                break;
            case 5:
                newRoomType = 4;
                break;
            case 6:
                newRoomType = 9;
                break;
            case 7:
                newRoomType = 6;
                break;
            case 8:
                newRoomType = 7;
                break;
            case 9:
                newRoomType = 8;
                break;
            case 10:
                newRoomType = 13;
                break;
            case 11:
                newRoomType = 10;
                break;
            case 12:
                newRoomType = 11;
                break;
            case 13:
                newRoomType = 12;
                break;
        }
        return newRoomType;
    }
    private static int RightRotate(int roomType)
    {
        int newRoomType = 0;
        if (roomType < 0)
            return roomType;
        switch (Math.Abs(roomType))
        {
            case 0:
                return 0;
            case 1:
                newRoomType = 1;
                break;
            case 2:
                newRoomType = 3;
                break;
            case 3:
                newRoomType = 2;
                break;
            case 4:
                newRoomType = 5;
                break;
            case 5:
                newRoomType = 4;
                break;
            case 6:
                newRoomType = 7;
                break;
            case 7:
                newRoomType = 8;
                break;
            case 8:
                newRoomType = 9;
                break;
            case 9:
                newRoomType = 6;
                break;
            case 10:
                newRoomType = 11;
                break;
            case 11:
                newRoomType = 12;
                break;
            case 12:
                newRoomType = 13;
                break;
            case 13:
                newRoomType = 10;
                break;
        }
        return newRoomType;
    }
    private static bool IsPosValid(string comingFrom, int currentType)
    {
        bool valid = false;
        switch (Math.Abs(currentType))
        {
            case 0:
                return false;
            case 1:
                valid = true;
                break;
            case 2:
                valid = comingFrom != "TOP";
                break;
            case 3:
            case 6:
            case 10:
            case 11:
                valid = comingFrom == "TOP";
                break;
            case 4:
                valid = comingFrom == "TOP" || comingFrom == "RIGHT";
                break;
            case 5:
                valid = comingFrom == "TOP" || comingFrom == "LEFT";
                break;

            case 7:
                valid = comingFrom == "TOP" || comingFrom == "RIGHT";
                break;
            case 8:
                valid = comingFrom == "LEFT" || comingFrom == "RIGHT";
                break;
            case 9:
                valid = comingFrom == "LEFT" || comingFrom == "TOP";
                break;
            case 12:
                valid = comingFrom == "RIGHT";

                break;
            case 13:
                valid = comingFrom == "LEFT";

                break;
        }
        return valid;
    }
    private static bool IsPosValid(Position pos)
    {
        return IsPosValid(pos.ComingFrom, roomTypes[pos.Y, pos.X]);

    }
    private static Position GetNextPosition(int currentX, int currentY, string comingFrom)
    {
        int currentType = roomTypes[currentY, currentX];
        Position pos = new Position(currentX, currentY) { ComingFrom = comingFrom };
        return GetNextPosition(currentType, pos);
    }

    private static Position GetNextPosition(int currentType, Position pos)
    {

        switch (Math.Abs(currentType))
        {
            case 0:
                Console.Error.WriteLine("Invalid RT " + pos);
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
                if (pos.ComingFrom == "LEFT")
                    pos.GoRight();
                else
                {
                    pos.GoLeft();
                }
                break;
            case 4:
                if (pos.ComingFrom == "TOP")
                    pos.GoLeft();
                else
                {
                    pos.GoDown();
                }
                break;
            case 5:
                if (pos.ComingFrom == "TOP")
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
        myReader = new StreamReader("..\\..\\input3.txt");
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



