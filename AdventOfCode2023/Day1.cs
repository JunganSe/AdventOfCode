namespace AdventOfCode2023;
internal class Day1
{
    private Dictionary<string, int> _numbersMap;

    public Day1()
    {
        _numbersMap = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };
    }

    public void Run()
    {
        var input = Helper.ReadTextFile("1");

        int sum = 0;
        foreach (var line in input)
        {
            var firstDigit = GetFirstDigit(line);
            var firstNumber = GetFirstNumber(line);
            var firstValue = GetFirstValue(firstDigit, firstNumber);

            var lastDigit = GetLastDigit(line);
            var lastNumber = GetLastNumber(line);
            var lastValue = GetLastValue(lastDigit, lastNumber);
            sum += int.Parse($"{firstValue}{lastValue}");
        }

        Console.WriteLine($"Sum: {sum}");
    }



    private (int? index, int? value) GetFirstDigit(string line)
    {
        int index = line.IndexOf(line.FirstOrDefault(char.IsDigit));
        if (index == -1)
            return (null, null);

        int value = int.Parse(line[index].ToString());
        return (index, value);
    }

    private (int? index, int? value) GetLastDigit(string line)
    {
        int index = line.LastIndexOf(line.LastOrDefault(char.IsDigit));
        if (index == -1)
            return (null, null);

        int value = int.Parse(line[index].ToString());
        return (index, value);
    }

    private (int? index, int? value) GetFirstNumber(string line)
    {
        var hits = new Dictionary<int, int>();
        foreach (var entry in _numbersMap)
        {
            int index = line.IndexOf(entry.Key);
            if (index >= 0)
                hits.Add(index, entry.Value);
        }

        if (hits.Count == 0)
            return (null, null);

        var chosenHit = hits.OrderBy(h => h.Key).First();
        return (chosenHit.Key, chosenHit.Value);
    }

    private (int? index, int? value) GetLastNumber(string line)
    {
        var hits = new Dictionary<int, int>();
        foreach (var entry in _numbersMap)
        {
            int index = line.LastIndexOf(entry.Key);
            if (index >= 0)
                hits.Add(index, entry.Value);
        }

        if (hits.Count == 0)
            return (null, null);

        var chosenHit = hits.OrderBy(h => h.Key).Last();
        return (chosenHit.Key, chosenHit.Value);
    }

    private int? GetFirstValue((int? index, int? value) firstDigit, (int? index, int? value) firstNumber)
    {
        if (firstDigit.index != null && firstNumber.index != null)
            return (firstDigit.index < firstNumber.index) ? firstDigit.value : firstNumber.value;
        return firstDigit.value ?? firstNumber.value;
    }

    private int? GetLastValue((int? index, int? value) firstDigit, (int? index, int? value) firstNumber)
    {
        if (firstDigit.index != null && firstNumber.index != null)
            return (firstDigit.index > firstNumber.index) ? firstDigit.value : firstNumber.value;
        return firstDigit.value ?? firstNumber.value;
    }
}
