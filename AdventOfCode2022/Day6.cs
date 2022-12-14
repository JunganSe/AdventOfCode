using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day6
{
    public static void Run()
    {
        var input = Helper.ReadTextFile("6.txt");
        string message = input[0];

        int startOfPacket = IndexOfUniquePart(message, 4) + 4;
        Console.WriteLine("Message snippet: " + message[(startOfPacket-8)..(startOfPacket + 4)]);
        Console.WriteLine("First unique 4:      " + message[(startOfPacket-4)..(startOfPacket + 0)]);
        Console.WriteLine("First letter of packet:  " + message[startOfPacket]);
        Console.WriteLine("Letters before packet starts: " + startOfPacket);

        int startOfMessage = IndexOfUniquePart(message, 14) + 14;
        Console.WriteLine("\nLetters before message starts: " + startOfMessage);
    }

    private static int IndexOfUniquePart(string message, int partSize)
    {
        for (int i = 0; i < message.Length-partSize; i++)
        {
            if (HasOnlyUniqueCharacters(message[i..(i+partSize)]))
                return i;
        }
        return -1;
    }

    public static bool HasOnlyUniqueCharacters(string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (word.LastIndexOf(word[i]) != i)
                return false;
        }
        return true;
    }

    public static bool HasOnlyUniqueCharactersRegex(string word)
    {
        return Regex.IsMatch(word, @"^(?:(.)(?!.*\1))*$");
    }
}
