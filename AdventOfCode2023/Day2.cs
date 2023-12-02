namespace AdventOfCode2023;

internal class Day2
{
    internal class Game
    {
        public int Id { get; set; }
        public List<Round> Rounds { get; set; } = new();
    }

    internal struct Round
    {
        public int RedCount { get; set; }
        public int GreenCount { get; set; }
        public int BlueCount { get; set; }
    }

    public void Run()
    {
        Part1();
        Part2();
    }

    public void Part1()
    {
        var input = Helper.ReadTextFile("2");
        var games = ParseGames(input);

        var possibleGames = new List<Game>();
        foreach (var game in games)
        {
            if (IsGamePossible(game))
                possibleGames.Add(game);
        }

        int idSum = possibleGames.Select(g => g.Id).Sum();
        Console.WriteLine("1 - Sum of ids: " + idSum);
    }

    public void Part2()
    {
        var input = Helper.ReadTextFile("2");
        var games = ParseGames(input);

        int powerSum = games.Select(game => GetMinimumRed(game) * GetMinimumGreen(game) * GetMinimumBlue(game)).Sum();
        Console.WriteLine("2 - Sum of cube power: " + powerSum);
    }

    private IEnumerable<Game> ParseGames(string[] input)
        => input.Select(line => ParseGame(line));

    private Game ParseGame(string line)
    {
        var game = new Game();
        var idAndResults = line.Split(':');
        game.Id = int.Parse(idAndResults[0].Split(' ')[1]);
        var rounds = idAndResults[1].Split(';');

        foreach (var round in rounds)
        {
            var gameRound = new Round();
            var colors = round.Split(',');
            foreach (var color in colors)
            {
                var parts = color.Trim().Split(' ');
                int count = int.Parse(parts[0]);
                if (parts[1] == "red")
                    gameRound.RedCount = count;
                else if (parts[1] == "green")
                    gameRound.GreenCount = count;
                else if (parts[1] == "blue")
                    gameRound.BlueCount = count;
            }
            game.Rounds.Add(gameRound);
        }

        return game;
    }

    private bool IsGamePossible(Game game)
    {
        foreach (var round in game.Rounds)
        {
            if (round.RedCount > 12
                || round.GreenCount > 13
                || round.BlueCount > 14)
                return false;
        }
        return true;
    }

    private int GetMinimumRed(Game game)
        => game.Rounds.Select(r => r.RedCount).Max();

    private int GetMinimumGreen(Game game)
        => game.Rounds.Select(r => r.GreenCount).Max();

    private int GetMinimumBlue(Game game)
        => game.Rounds.Select(r => r.BlueCount).Max();
}
