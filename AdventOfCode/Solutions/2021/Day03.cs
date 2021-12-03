using System.Text;

namespace AdventOfCode.Solutions._2021;

public class Day03: ASolution
{
    public override object Part1()
    {
        var wordLength = Input.First().Length;
        var sb = new StringBuilder();

        foreach (var i in Enumerable.Range(0, wordLength))
        {
            sb.Append(MostCommonAtPosition(Input, i));
        }
        var gammaRate =  Convert.ToInt32(sb.ToString(), 2);
        var epsilonRate = ~gammaRate & ((1 << wordLength) - 1);

        return gammaRate * epsilonRate;
    }
    
    public override object Part2()
    {
        var oxygenGeneratorRating = GetRating(Input, (x, y) => x == y);
        var co2ScrubberRating = GetRating(Input, (x, y) => x != y);
        return oxygenGeneratorRating * co2ScrubberRating;
    }

    private static int GetRating(IEnumerable<string> input, Func<char, char, bool> charComparison)
    {
        var inputList = input.ToList();
        var counter = 0;
        while (inputList.Count > 1)
        {
            var mostCommonAtPosition = MostCommonAtPosition(inputList, counter);
            inputList = inputList.Where(x => charComparison(x[counter], mostCommonAtPosition)).ToList();
            counter++;
        }
        return Convert.ToInt32(new string(inputList.First().ToArray()),2);
    }

    private static char MostCommonAtPosition(IReadOnlyCollection<string> list, int position)
    {
        return list.Count(x => x[position] == '0') > list.Count / 2 ? '0': '1';
    }
}