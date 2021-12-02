using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions._2020;

public class Day02: ASolution
{
    private readonly Regex _regex = new Regex(@"^([0-9]+)-([0-9]+)\s([a-z]):\s([a-z]+)");
    public override object Part1()
    {
        return Input.Count(line =>
        {
            var splitLine = _regex.Split(line);
            var charCount = splitLine[4].Count(x => x == splitLine[3][0]);
            return charCount >= int.Parse(splitLine[1]) && charCount <= int.Parse(splitLine[2]);
        }).ToString();
    }

    public override object Part2()
    {
        return Input.Count(line =>
        {
            var splitLine = _regex.Split(line);
            return splitLine[4].ElementAtOrDefault(int.Parse(splitLine[1]) - 1) == splitLine[3][0] ^
                   splitLine[4].ElementAtOrDefault(int.Parse(splitLine[2]) - 1) == splitLine[3][0];
        }).ToString();
    }
}