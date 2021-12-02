using System.Drawing;

namespace AdventOfCode.Solutions._2021;

public class Day02: ASolution
{
    public override object Part1()
    {
        var start = new Point();
        foreach (var line in Input)
        {
            var parts = line.Split(' ');
            var num = int.Parse(parts[1]);
            switch (parts[0])
            {
                case "forward":
                    start.X += num;
                    break;
                case "down":
                    start.Y += num;
                    break;
                case "up":
                    start.Y -= num;
                    break;
                default:
                    throw new Exception();
            }
        }
        return start.X * start.Y;
    }

    public override object Part2()
    {
        var start = new Point();
        var aim = 0;
        foreach (var line in Input)
        {
            var parts = line.Split(' ');
            var num = int.Parse(parts[1]);
            switch (parts[0])
            {
                case "forward":
                    start.X += num;
                    start.Y += aim * num;
                    break;
                case "down":
                    aim += num;
                    break;
                case "up":
                    aim -= num;
                    break;
                default:
                    throw new Exception();
            }
        }
        return start.X * start.Y;
    }
}