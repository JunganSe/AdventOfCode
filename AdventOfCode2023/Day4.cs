namespace AdventOfCode2023;

internal class Day4
{
    public class Card
    {
        public List<int> WinningNumbers { get; set; } = new();
        public List<int> OwnNumbers { get; set; } = new();
    }

    public void Run()
    {
        var input = Helper.ReadTextFile("4");
        Part1(input);
    }

    public void Part1(string[] input)
    {
        var cards = input.Select(ParseCard);
        var pointsSum = cards.Select(GetCardPoints).Sum();
        Console.WriteLine("1 - Sum of card points: " + pointsSum);
    }



    private Card ParseCard(string line)
    {
        var parts = line.Split(':', '|');
        return new Card
        {
            WinningNumbers = ExtractNumbers(parts[1]),
            OwnNumbers = ExtractNumbers(parts[2])
        };
    }

    private List<int> ExtractNumbers(string input)
        => input
            .Split(' ')
            .Where(x => x.Length > 0)
            .Select(n => int.Parse(n.Trim()))
            .ToList();

    private int GetCardPoints(Card card)
    {
        int matches = GetCardMatchesCount(card);
        return (int)(Math.Pow(2, matches) / 2);
    }

    private int GetCardMatchesCount(Card card)
        => card.OwnNumbers
            .Where(n => card.WinningNumbers.Contains(n))
            .Count();
}
