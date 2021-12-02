namespace AdventOfCode.Solutions._2021;

public class Day01: ASolution
{
    public override object Part1()
    {
        int? prev = null;
        var count = 0;
        foreach (var s in Input.Select(int.Parse))
        {
            if (s > prev)
            {
                count++;
            }
            prev = s;
        }

        return count;
    }

    public override object Part2()
    {
        var input = Input.Select(int.Parse).ToList();
        int? prev = null;
        var count = 0;
        
        for (var i = 2; i < input.Count; i++)
        {
            var sum = input[i - 2] + input[i - 1] + input[i];
            
            if (sum > prev)
            {
                count++;
            }
            prev = sum;
        }

        return count;
    }
}