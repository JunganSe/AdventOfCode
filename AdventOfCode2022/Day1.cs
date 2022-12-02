namespace AdventOfCode2022;

internal class Day1
{
    public static void Run()
    {
        var input = Helper.ReadTextFile("1.txt");
        var bags = new List<List<int>>();

        var bag = new List<int>();
        foreach (string line in input)
        {
            if (line != "")
                bag.Add(int.Parse(line));
            else
            {
                bags.Add(bag);
                bag = new();
            }
        }

        var bagSums = bags.Select(b => b.Sum());
        int highest = bagSums.Max();
        int highest3 = bagSums.OrderByDescending(b => b).Take(3).Sum();
        Console.WriteLine($"Highest: {highest}\nHighest 3: {highest3}");
    }
}
