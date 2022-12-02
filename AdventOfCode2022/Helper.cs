namespace AdventOfCode2022;

internal static class Helper
{
    public static List<string> ReadTextFile(string fileName)
    {
        return File.ReadAllLines($"../../../Inputs/{fileName}").ToList();
    }
}
