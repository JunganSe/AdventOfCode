﻿namespace AdventOfCode2023;
internal static class Helper
{
    public static List<string> ReadTextFile(string fileName)
    {
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory.Split(@"\bin")[0];
        string filePath = $"{projectDirectory}/Inputs/{fileName}.txt";
        return File.ReadAllLines(filePath).ToList();
    }
}
