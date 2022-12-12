namespace AdventOfCode2022;

internal class Day4
{
    public static void Run()
    {
        var input = Helper.ReadTextFile("4.txt");
        var assignments = ParseInput(input);

        Console.WriteLine(GetContainingCount(assignments));
        Console.WriteLine(GetOverlappingCount(assignments));
        Console.WriteLine(GetOverlappingCountV2(assignments));
    }

    private static List<Assignment> ParseInput(List<string> input)
    {
        // Ordning: StartA, EndA, StartB, EndB
        var output = new List<Assignment>();
        foreach (var line in input)
        {
            string[] parts = line.Split(new char[] { ',', '-' });
            int[] assignmentDetails = parts.Select(int.Parse).ToArray();
            output.Add(new Assignment(assignmentDetails));
        }
        return output;
    }

    private static int GetContainingCount(List<Assignment> assignments)
    {
        int containsCount = 0;
        foreach (var a in assignments)
        {
            if (((a.StartA <= a.StartB) && (a.EndA >= a.EndB))     // A contains B
                || ((a.StartB <= a.StartA) && (a.EndB >= a.EndA))) // B contains A
                containsCount++;
        }
        return containsCount;
    }

    private static int GetOverlappingCount(List<Assignment> assignments)
    {
        int overlappingCount = 0;
        foreach (var a in assignments)
        {
            if (((a.StartA >= a.StartB) && (a.StartA <= a.EndB))    // StartA overlaps B
                || ((a.EndA >= a.StartB) && (a.EndA <= a.EndB))     // EndA overlaps B
                || ((a.StartB >= a.StartA) && (a.StartB <= a.EndA)) // StartB overlaps A
                || ((a.EndB >= a.StartA) && (a.EndB <= a.EndA)))    // EndB overlaps A
                overlappingCount++;
        }
        return overlappingCount;
    }

    private static int GetOverlappingCountV2(List<Assignment> assignments)
    {
        int overlappingCount = 0;
        foreach (var a in assignments)
        {
            // Idé från internet: Kollar om det inte överlappar.
            if (!((a.EndA < a.StartB)    // A ends before B starts
                || (a.EndB < a.StartA))) // B ends before A starts
                overlappingCount++;
        }
        return overlappingCount;
    }
}

internal struct Assignment
{
    public int StartA { get; set; }
    public int EndA { get; set; }
    public int StartB { get; set; }
    public int EndB { get; set; }

    public Assignment(int[] assignmentDetails)
    {
        StartA = assignmentDetails[0];
        EndA = assignmentDetails[1];
        StartB = assignmentDetails[2];
        EndB = assignmentDetails[3];
    }
}