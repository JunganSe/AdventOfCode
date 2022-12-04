namespace AdventOfCode2022;

internal class Day2v2
{
    public static void Run()
    {
        // Metoden hämtad från nätet, egen kod.

        // ax rock 1, by paper 2, cz scissors 3
        // x lose, y draw, z win
        var input = Helper.ReadTextFile("2.txt");
        var dictA = new Dictionary<string, int>()
        {
            { "A X", 4 }, { "A Y", 8 }, { "A Z", 3 },
            { "B X", 1 }, { "B Y", 5 }, { "B Z", 9 },
            { "C X", 7 }, { "C Y", 2 }, { "C Z", 6 }
        };
        int pointsA = 0;
        foreach (var line in input)
            pointsA += dictA[line];

        var dictB = new Dictionary<string, int>()
        {
            { "A X", 3 }, { "A Y", 4 }, { "A Z", 8 },
            { "B X", 1 }, { "B Y", 5 }, { "B Z", 9 },
            { "C X", 2 }, { "C Y", 6 }, { "C Z", 7 }
        };
        int pointsB = 0;
        foreach (var line in input)
            pointsB += dictB[line];

        Console.WriteLine($"A: {pointsA}\nB: {pointsB}");
    }
}
