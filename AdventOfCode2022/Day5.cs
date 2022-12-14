namespace AdventOfCode2022;

internal class Day5
{
    public void Run()
    {
        var input = Helper.ReadTextFile("5.txt");
        int splitAt = input.FindIndex(line => string.IsNullOrEmpty(line));
        var rawDrawing = input.GetRange(0, splitAt);
        rawDrawing.Reverse();
        var rawInstructions = input.GetRange(splitAt + 1, input.Count - splitAt - 1);

        var instructions = ParseInstructions(rawInstructions);

        var stacks1 = ParseDrawing(rawDrawing);
        ExecuteInstructions1(stacks1, instructions);
        Console.WriteLine(GetTopOfStacks(stacks1));

        var stacks2 = ParseDrawing(rawDrawing);
        ExecuteInstructions2(stacks2, instructions);
        Console.WriteLine(GetTopOfStacks(stacks2));
    }

    private List<Stack<char>> ParseDrawing(List<string> rawDrawing)
    {
        string reference = rawDrawing[0];

        var stacks = new List<Stack<char>>();
        for (int i = 0; i < reference.Length; i++)
        {
            if (char.IsDigit(reference[i]))
            {
                var stack = new Stack<char>();
                foreach (string line in rawDrawing)
                {
                    if (char.IsLetter(line[i]))
                        stack.Push(line[i]);
                }
                stacks.Add(stack);
            }
        }
        return stacks;
    }

    private List<Instruction> ParseInstructions(List<string> rawInstructions)
    {
        var instructions = new List<Instruction>();
        foreach (string line in rawInstructions)
        {
            var parts = line.Split(' ').ToList();
            parts.RemoveAll(part => !int.TryParse(part, out _));
            var parsedParts = parts.Select(int.Parse).ToArray();
            instructions.Add(new Instruction(parsedParts));
        }
        return instructions;
    }

    private void ExecuteInstructions1(List<Stack<char>> stacks, List<Instruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.MoveCount; i++)
            {
                var crate = stacks[instruction.From].Pop();
                stacks[instruction.To].Push(crate);
            }
        }
    }

    private void ExecuteInstructions2(List<Stack<char>> stacks, List<Instruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            var buffer = new Stack<char>();
            for (int i = 0; i < instruction.MoveCount; i++)
                buffer.Push(stacks[instruction.From].Pop());
            for (int i = 0; i < instruction.MoveCount; i++)
                stacks[instruction.To].Push(buffer.Pop());
        }
    }

    private string GetTopOfStacks(List<Stack<char>> stacks)
    {
        string output = "";
        foreach (var stack in stacks)
            output += stack.Peek();
        return output;
    }
}

internal struct Instruction
{
    public int MoveCount { get; set; }
    public int From { get; set; }
    public int To { get; set; }

    public Instruction(int[] instructionDetails)
    {
        MoveCount = instructionDetails[0];
        From = instructionDetails[1] - 1;
        To = instructionDetails[2] - 1;
    }
}