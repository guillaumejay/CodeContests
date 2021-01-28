using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

public class Solver
{
    public void Solve()
    {
        int r = ReadInt() - 1;
        int g = ReadInt();
        int l = ReadInt();

        while (true)
        {
            int s = ReadInt();
            int x = ReadInt();

            if (s == 0)
                Console.WriteLine("SPEED");
            else if (x < r)
            {
                int speedToGain = g + 1;
                int dist = r;
                if (speedToGain >= s)
                {
                    while (speedToGain > s)
                    {
                        dist -= speedToGain;
                        speedToGain--;
                    }
                    if (dist == x)
                        Console.WriteLine("SPEED");
                    else
                        Console.WriteLine("WAIT");
                }
                else
                {
                    Console.WriteLine("SLOW");
                }

            }
            else if (x == r)
                Console.WriteLine("JUMP");
            else
                Console.WriteLine("SLOW");
        }
    }

    #region Main

    protected static TextReader reader;
    protected static TextWriter writer;

    static void Maintest()
    {
#if DEBUG
        reader = Console.In;
        reader = new StreamReader("..\\..\\input.txt");
        writer = Console.Out;
        //writer = new StreamWriter("..\\..\\output.txt");
#else
        reader = Console.In;
        writer = Console.Out;
#endif
        try
        {
            new Solver().Solve();
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine(ex);
#else
            throw;
#endif
        }
        reader.Close();
        writer.Close();
    }

    #endregion

    #region Read/Write

    private static Queue<string> currentLineTokens = new Queue<string>();

    private static string[] ReadAndSplitLine()
    {
        return reader.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string ReadToken()
    {
        if (currentLineTokens.Count == 0)
            currentLineTokens = new Queue<string>(ReadAndSplitLine());
        return currentLineTokens.Dequeue();
    }

    public static int ReadInt()
    {
        return int.Parse(ReadToken());
    }

    public static long ReadLong()
    {
        return long.Parse(ReadToken());
    }

    public static double ReadDouble()
    {
        return double.Parse(ReadToken(), CultureInfo.InvariantCulture);
    }

    public static int[] ReadIntArray()
    {
        return ReadAndSplitLine().Select(int.Parse).ToArray();
    }

    public static long[] ReadLongArray()
    {
        return ReadAndSplitLine().Select(long.Parse).ToArray();
    }

    public static double[] ReadDoubleArray()
    {
        return ReadAndSplitLine().Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
    }

    public static int[][] ReadIntMatrix(int numberOfRows)
    {
        int[][] matrix = new int[numberOfRows][];
        for (int i = 0; i < numberOfRows; i++)
            matrix[i] = ReadIntArray();
        return matrix;
    }

    public static string[] ReadLines(int quantity)
    {
        string[] lines = new string[quantity];
        for (int i = 0; i < quantity; i++)
            lines[i] = reader.ReadLine().Trim();
        return lines;
    }

    public static void WriteArray<T>(IEnumerable<T> array)
    {
        writer.WriteLine(string.Join(" ", array));
    }

    #endregion
}