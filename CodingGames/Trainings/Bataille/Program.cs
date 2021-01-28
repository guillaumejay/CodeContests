using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

class Solution
{
    protected static TextReader myReader;
    protected static TextWriter myWriter;
    static void Main(string[] args)
    {
#if DEBUG
        myReader = Console.In;
             myReader = new StreamReader("input5.txt");
        myWriter = Console.Out;
        //myWriter = new StreamWriter("..\\..\\output.txt");
#else
        myReader = Console.In;
        myWriter = Console.Out;
#endif


        AnswerChallenge();


    }

    private static void AnswerChallenge()
    {
        List<int>[] cards = new List<int>[2];
        int nbPlayer1 = ReadInt();
        cards[0] = ReadCards(nbPlayer1);
        int nbPlayer2 = ReadInt();
        cards[1] = ReadCards(nbPlayer2);
        int nbManche = 0;
        List<int>[] stacks = new List<int>[2] { new List<int>(), new List<int>() };
        while (cards[0].Count > 0 && cards[1].Count > 0)
        {
            stacks[0].AddRange( Take(cards[0], 1));
            stacks[1].AddRange(Take(cards[1], 1));;
            if (stacks[0].Last() == stacks[1].Last())
            {
                if (stacks[0].Count < 4 || stacks[1].Count < 4)
                {
                    Console.WriteLine("PAT");
                    return;
                }
                stacks[0].AddRange(Take(cards[0],3));
                stacks[1].AddRange(Take(cards[1], 3));
                
                continue;
            }
            if (stacks[0].Last() > stacks[1].Last())
            {
                SetWinner(0);
            }
            else
            {
                SetWinner(1);
            }
        }

        int winner = cards[0].Count == 0 ? 2 : 1;
        Console.WriteLine($"{winner} {nbManche}");
        Debug.WriteLine($"{winner} {nbManche}");

        void SetWinner(int currentWinner)
        {
            nbManche++;
            cards[currentWinner].AddRange(stacks[0]);
            cards[currentWinner].AddRange(stacks[1]);
            stacks[0].Clear();
            stacks[1].Clear();
        }
    }

    private static List<int> Take(List<int> cards,int nb)
    {
        List<int> taken = new List<int>();
        for (int i = 0; i < nb; i++)
        {
            int card = cards[0];
            cards.RemoveAt(0);
            taken.Add(card);
        }

        return taken;
    }
    
    private static List<int> ReadCards(int nbCards)
    {
        List<int> cards = new List<int>();
        for (int i = 0; i < nbCards; i++)
        {
            string card = ReadLine();
            card = card.Substring(0, card.Length - 1);
            if (!int.TryParse(card, out int value))
            {
                value = card switch
                {
                    "J" => 11,
                    "Q" => 12,
                    "K" => 13,
                    "A" => 14,
                    _ => value
                };
            }
            cards.Add(value);
        }

        return cards;
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

