namespace AdventOfCode2022;

internal class Day2
{
    public void Run()
    {
        var input = Helper.ReadTextFile("2.txt");

        int pointsA = 0;
        foreach (string line in input)
            pointsA += GetShapePoints(line[2]) + GetRoundPoints(line[0], line[2]);

        int pointsB = 0;
        foreach (string line in input)
            pointsB += GetPointsB(line[0], line[2]);

        Console.WriteLine($"Points A: {pointsA}\nPoints B: {pointsB}");
    }

    private int GetShapePoints(char shape)
    {
        if (IsRock(shape))
            return 1;
        if (IsPaper(shape))
            return 2;
        if (IsScissors(shape))
            return 3;

        throw new ArgumentException();
    }

    private int GetRoundPoints(char theirShape, char myShape)
    {
        if ((IsRock(theirShape) && IsPaper(myShape))
            || (IsPaper(theirShape) && IsScissors(myShape))
            || (IsScissors(theirShape) && IsRock(myShape)))
            return 6;
        if ((IsRock(theirShape) && IsRock(myShape))
            || (IsPaper(theirShape) && IsPaper(myShape))
            || (IsScissors(theirShape) && IsScissors(myShape)))
            return 3;
        if ((IsRock(theirShape) && IsScissors(myShape))
            || (IsPaper(theirShape) && IsRock(myShape))
            || (IsScissors(theirShape) && IsPaper(myShape)))
            return 0;
        
        throw new ArgumentException();
    }

    private bool IsRock(char shape) => "AX".Contains(shape);
    private bool IsPaper(char shape) => "BY".Contains(shape);
    private bool IsScissors(char shape) => "CZ".Contains(shape);

    private int GetPointsB(char theirShape, char strategy)
    {
        char myShape = GetMyShape(theirShape, strategy);
        return GetShapePoints(myShape) + GetRoundPoints(theirShape, myShape);
    }

    private char GetMyShape(char theirShape, char strategy)
    {
        if ((IsRock(theirShape) && WantToDraw(strategy))
            || (IsPaper(theirShape) && WantToLose(strategy))
            || (IsScissors(theirShape) && WantToWin(strategy)))
            return 'X';
        if ((IsRock(theirShape) && WantToWin(strategy))
            || (IsPaper(theirShape) && WantToDraw(strategy))
            || (IsScissors(theirShape) && WantToLose(strategy)))
            return 'Y';
        if ((IsRock(theirShape) && WantToLose(strategy))
            || (IsPaper(theirShape) && WantToWin(strategy))
            || (IsScissors(theirShape) && WantToDraw(strategy)))
            return 'Z';

        throw new ArgumentException();
    }

    private bool WantToLose(char strategy) => strategy == 'X';
    private bool WantToDraw(char strategy) => strategy == 'Y';
    private bool WantToWin(char strategy) => strategy == 'Z';
}