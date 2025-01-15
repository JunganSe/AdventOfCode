namespace AdventOfCode2022;

internal static class Helper
{
    public static List<string> ReadTextFile(string fileName)
    {
        return System.IO.File.ReadAllLines($"../../../Inputs/{fileName}").ToList();
    }
}
