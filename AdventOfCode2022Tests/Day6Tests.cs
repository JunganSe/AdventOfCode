using AdventOfCode2022;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Tests;

[TestClass()]
public class Day6Tests
{
    [TestMethod()]
    [DataRow(true, "abc")]
    [DataRow(true, "abc1")]
    [DataRow(true, "abc1.")]
    [DataRow(false, "aba")]
    [DataRow(false, "ab1cd1")]
    [DataRow(false, "a-b1cd-e")]
    public void HasUniqueCharactersTest(bool expected, string word)
    {
        bool actual = Day6.HasOnlyUniqueCharacters(word);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DataRow(true, "abc")]
    [DataRow(true, "abc1")]
    [DataRow(true, "abc1.")]
    [DataRow(false, "aba")]
    [DataRow(false, "ab1cd1")]
    [DataRow(false, "a-b1cd-e")]
    public void HasUniqueCharactersRegexTest(bool expected, string word)
    {
        bool actual = Day6.HasOnlyUniqueCharactersRegex(word);
        Assert.AreEqual(expected, actual);
    }
}