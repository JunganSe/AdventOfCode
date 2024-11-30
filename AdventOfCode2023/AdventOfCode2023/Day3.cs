namespace AdventOfCode2023;

internal class Day3
{
    public class Number
    {
        public int Value { get; set; }
        public int Line { get; set; }
        public int Location { get; set; }
        public int Length => Value.ToString().Length;
    }

    public class Symbol
    {
        public int Line { get; set; }
        public int Location { get; set; }
    }

    public void Run()
    {
        var input = Helper.ReadTextFile("3");
        Part1(input);
        Part2(input);
    }

    public void Part1(string[] input)
    {
        var numbers = GetNumbers(input);
        var symbols = GetSymbols(input);
        var numbersAdjacentToSymbols = numbers.Where(number => IsNumberAdjacentToSymbol(number, symbols));
        int numbersSum = numbersAdjacentToSymbols.Select(number => number.Value).Sum();
        Console.WriteLine("1 - Sum of numbers: " + numbersSum);
    }

    public void Part2(string[] input)
    {
        var numbers = GetNumbers(input);
        var symbols = GetSymbols(input);

        int gearRatiosSum = 0;
        foreach (var symbol in symbols)
        {
            var adjacentNumbers = GetNumbersAdjacentToSymbol(symbol, numbers);
            if (adjacentNumbers.Count() == 2)
                gearRatiosSum += MultiplyNumbers(adjacentNumbers);
        }
        Console.WriteLine("2 - Sum of gear ratios: " + gearRatiosSum);
    }



    private List<Number> GetNumbers(string[] input)
    {
        var numbers = new List<Number>();
        for (int iLine = 0; iLine < input.Length; iLine++)
        {
            var lineNumbers = ParseNumbers(input[iLine]);
            lineNumbers.ForEach(number => number.Line = iLine);
            numbers.AddRange(lineNumbers);
        }
        return numbers;
    }

    private List<Number> ParseNumbers(string line)
    {
        var numbers = new List<Number>();
        string digits = "";

        for (int iChar = 0; iChar < line.Length; iChar++)
        {
            char c = line[iChar];
            if (char.IsDigit(c))
                digits += c;

            if (digits.Length == 0)
                continue;

            if (!char.IsDigit(c))
            {
                numbers.Add(new Number() { Value = int.Parse(digits), Location = iChar - digits.Length });
                digits = "";
            }
            else if (iChar == line.Length - 1)
                numbers.Add(new Number() { Value = int.Parse(digits), Location = iChar - digits.Length + 1 });
        }

        return numbers;
    }

    private List<Symbol> GetSymbols(string[] input)
    {
        var symbols = new List<Symbol>();
        for (int iLine = 0; iLine < input.Length; iLine++)
        {
            var lineSymbols = ParseSymbols(input[iLine]);
            lineSymbols.ForEach(symbol => symbol.Line = iLine);
            symbols.AddRange(lineSymbols);
        }
        return symbols;
    }

    private List<Symbol> ParseSymbols(string line)
    {
        var symbols = new List<Symbol>();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c != '.' && !char.IsDigit(c))
                symbols.Add(new Symbol() { Location = i });
        }
        return symbols;
    }

    private bool IsNumberAdjacentToSymbol(Number number, IEnumerable<Symbol> symbols)
    {
        return symbols
            .Where(symbol => 
                (symbol.Line >= number.Line - 1)
                && (symbol.Line <= number.Line + 1)
                && (symbol.Location >= number.Location - 1) 
                && (symbol.Location <= number.Location + number.Length))
            .Any();
    }

    private IEnumerable<Number> GetNumbersAdjacentToSymbol(Symbol symbol, IEnumerable<Number> numbers)
    {
        return numbers
            .Where(number =>
                (symbol.Line >= number.Line - 1)
                && (symbol.Line <= number.Line + 1)
                && (symbol.Location >= number.Location - 1)
                && (symbol.Location <= number.Location + number.Length));
    }

    private int MultiplyNumbers(IEnumerable<Number> numbers)
        => numbers.Select(n => n.Value).Aggregate((a, b) => a * b);
}
