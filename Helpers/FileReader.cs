namespace Helpers;

public static class FileReader
{
    public static string[] ReadTextFile(string fileName)
    {
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory.Split(@"\bin")[0];
        string filePath = $"{projectDirectory}/Inputs/{fileName}.txt";
        return File.ReadAllLines(filePath);
    }
}