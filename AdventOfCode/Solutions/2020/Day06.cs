namespace AdventOfCode.Solutions._2020;

public class Day06: ASolution
{
    public override object Part1()
    {
        return string.Join('\n', Input).Split("\n\n").Sum(x => x.Replace("\n", string.Empty).Distinct().Count());
    }

    public override object Part2()
    {
        return string.Join('\n', Input).Split("\n\n")
            .Sum(groupInput => groupInput.Replace("\n", string.Empty).Distinct()
                .Count(groupAnswer => groupInput.Split('\n').All(x => x.Contains(groupAnswer))
                )
            ).ToString();
    }
}