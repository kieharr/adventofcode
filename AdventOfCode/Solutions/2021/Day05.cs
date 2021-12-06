using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions._2021;

public class Day05: ASolution
{
    private static readonly Regex Regex = new (@"^(\d{1,}),(\d{1,}) -> (\d{1,}),(\d{1,})$");
    public override object Part1()
    {
        Dictionary<Point, int> grid = new();
        foreach (var (start, end) in Input.Select(ParseInputLine).Where(x => x.start.X == x.end.X || x.start.Y == x.end.Y))
        {
            PlotLine(start, end, grid);
        }
        return grid.Count(x => x.Value > 1);
    }

    public override object Part2()
    {
        Dictionary<Point, int> grid = new();
        foreach (var (start, end) in Input.Select(ParseInputLine))
        {
            PlotLine(start, end, grid);
        }
        return grid.Count(x => x.Value > 1);
    }
    
    private static (Point start, Point end) ParseInputLine(string line)
    {
        var nums = Regex.Split(line).Skip(1).Take(4).Select(int.Parse).ToList();
        return (new Point(nums[0], nums[1]), new Point(nums[2], nums[3]));
    }
    
    private static void PlotLine(Point start, Point end, IDictionary<Point, int> grid)
    {
        var count = Math.Max(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y));
        var xAdjust = end.X.CompareTo(start.X);
        var yAdjust = end.Y.CompareTo(start.Y);
            
        for (var i = 0; i <= count; i++)
        {
            var point = new Point(start.X + i * xAdjust, start.Y + i * yAdjust);
            if (grid.ContainsKey(point))
            {
                grid[point]++;
            }
            else
            {
                grid.Add(point, 1);
            }
        }
    }
}