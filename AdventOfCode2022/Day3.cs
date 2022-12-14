namespace AdventOfCode2022;

internal class Day3
{
    public void Run()
    {
        var input = Helper.ReadTextFile("3.txt");

        int totalValueA = 0;
        foreach (var line in input)
        {
            string[] parts = SplitLine(line);
            char common = GetCommonA(parts);
            totalValueA += GetValue(common);
        }
        Console.WriteLine($"Total A: {totalValueA}");

        int totalValueB = 0;
        var groups = GetGroupsOf3(input);
        foreach (var group in groups)
        {
            char common = GetCommonB(group);
            totalValueB += GetValue(common);
        }
        Console.WriteLine($"Total B: {totalValueB}");
    }

    private string[] SplitLine(string line)
    {
        int split = line.Length / 2;
        return new string[] { line[..split], line[split..] };
    }

    private char GetCommonA(string[] input)
    {
        foreach (char c in input[0])
        {
            if (input[1].Contains(c))
                return c;
        }
        throw new ArgumentException();
    }

    private int GetValue(char c)
    {
        if (char.IsLower(c))
            return (int)c - 96;
        if (char.IsUpper(c))
            return (int)c - 38;
        throw new ArgumentException();
    }

    private List<string[]> GetGroupsOf3(List<string> input)
    {
        var groups = new List<string[]>();
        for (int i = 0; i < input.Count; i += 3)
            groups.Add(new string[] { input[i], input[i + 1], input[i + 2] });
        return groups;
    }

    private char GetCommonB(string[] input)
    {
        foreach (char c in input[0])
        {
            if (input[1].Contains(c) && input[2].Contains(c))
                return c;
        }
        throw new ArgumentException();
    }
}
