using Helpers;

namespace AdventOfCode2023;

internal class Day4
{
    public class Card
    {
        public int Id { get; set; }
        public List<int> WinningNumbers { get; set; } = new();
        public List<int> OwnNumbers { get; set; } = new();
    }

    public void Run()
    {
        var input = FileReader.ReadTextFile("4");
        Part1(input);
        Part2(input);
    }

    public void Part1(string[] input)
    {
        var cards = input.Select(ParseCard);
        var pointsSum = cards.Select(GetCardPoints).Sum();
        Console.WriteLine("1 - Sum of card points: " + pointsSum);
    }

    public void Part2(string[] input)
    {
        var cards = input.Select(ParseCard).ToList();
        int highestCardId = cards.Select(c => c.Id).Max();
        for (int i = 1; i <= highestCardId; i++)
        {
            var currentSet = cards.Where(c => c.Id == i);
            var bonusCards = currentSet.SelectMany(c => GetBonusCards(c, cards)).ToList();
            cards.AddRange(bonusCards);
        }
        Console.WriteLine("2 - Number of cards: " + cards.Count);
    }



    private Card ParseCard(string line)
    {
        var parts = line.Split(':', '|');
        return new Card
        {
            Id = ExtractId(parts[0]),
            WinningNumbers = ExtractNumbers(parts[1]),
            OwnNumbers = ExtractNumbers(parts[2])
        };
    }

    private int ExtractId(string input)
        => int.Parse(input.Replace("Card", "").Trim());

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

    private IEnumerable<Card> GetBonusCards(Card card, IEnumerable<Card> baseCards)
    {
        var bonusCards = new List<Card>();
        int bonusCardsCount = GetCardMatchesCount(card);
        for (int i = 1; i <= bonusCardsCount; i++)
        {
            var cardToCopy = baseCards.First(c => c.Id == card.Id + i);
            bonusCards.Add(cardToCopy);
        }
        return bonusCards;
    }
}
