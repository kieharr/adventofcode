namespace AdventOfCode.Solutions._2020;

public class Day05: ASolution
{
    public override object Part1()
    {
        return Input.Max(GetSeatId);
    }

    public override object Part2()
    {
        var seatIds = Input.AsParallel().Select(GetSeatId).ToHashSet();
        return Enumerable.Range(0, int.MaxValue)
            .FirstOrDefault(x => !seatIds.Contains(x) && seatIds.Contains(x - 1) && seatIds.Contains(x + 1));
    }

    private int GetSeatId(string line)
    {
        return 8 * ExtractPosition(line, 0,128) + ExtractPosition(line, 7, 8);
    }

    private int ExtractPosition(string line, int startPosition, int range)
    {
        var position = 0;
        for (; range > 1; startPosition++)
        {
            range /= 2;
            if (line[startPosition] == 'B' || line[startPosition] == 'R')
            {
                position += range;
            }
        }
        return position;
    }
}