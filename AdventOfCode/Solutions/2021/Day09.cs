using System.Drawing;

namespace AdventOfCode.Solutions._2021;

public class Day09: ASolution
{
    public override object Part1()
    {
        var map = BuildMap(Input);
        return map.Where(x => IsLowPoint(map, x.Key)).Select(x => x.Value + 1).Sum();
    }
    
    public override object Part2()
    {
        var map = BuildMap(Input);
        return map.Where(x => IsLowPoint(map, x.Key)).Select(x => GetBasinSize(map, x.Key))
            .OrderByDescending(x => x).Take(3).Aggregate((a, b) => a * b);
    }
    
    private static Dictionary<Point, int> BuildMap(string[] input)
    {
        Dictionary<Point, int> map = new();

        for (var i = 0; i < input.First().Length; i++)
        {
            for (var j = 0; j < input.Length; j++)
            {
                map.Add(new Point(i,j), (int)char.GetNumericValue(input[j][i]));
            }
        }

        return map;
    }

    private static bool IsLowPoint(Dictionary<Point, int> map, Point point)
    {
        var height = map[point];
        
        if(map.TryGetValue(new Point(point.X, point.Y - 1), out var up))
        {
            if (height >= up)
            {
                return false;
            }
        }
        
        if(map.TryGetValue(new Point(point.X, point.Y + 1), out var down))
        {
            if (height >= down)
            {
                return false;
            }
        }
        
        if(map.TryGetValue(new Point(point.X - 1, point.Y), out var left))
        {
            if (height >= left)
            {
                return false;
            }
        }
        
        if(map.TryGetValue(new Point(point.X + 1, point.Y), out var right))
        {
            if (height >= right)
            {
                return false;
            }
        }

        return true;
    }

    private static List<Point> GetAdjacentPoints(Dictionary<Point, int> map, Point point)
    {
        List<Point> points = new();
        
        var pt = new Point(point.X, point.Y - 1);
        if(map.TryGetValue(pt, out var up))
        {
            if (up != 9)
            {
                points.Add(pt);
            }

        }

        pt = new Point(point.X, point.Y + 1);
        if(map.TryGetValue(pt, out var down))
        {
            if (down != 9)
            {
                points.Add(pt);
            }
        }

        pt = new Point(point.X - 1, point.Y);
        if(map.TryGetValue(pt, out var left))
        {
            if (left != 9)
            {
                points.Add(pt);
            }
        }

        pt = new Point(point.X + 1, point.Y);
        if(map.TryGetValue(new Point(point.X + 1, point.Y), out var right))
        {
            if (right != 9)
            {
                points.Add(pt);
            }
        }

        return points;
    }

    private static int GetBasinSize(Dictionary<Point, int> map, Point point, HashSet<Point>? results = null)
    {
        results ??= new HashSet<Point>{point};
        
        foreach (var point1 in GetAdjacentPoints(map, point).Where(x=> !results.Contains(x)))
        {
            results.Add(point1);
            GetBasinSize(map, point1, results);
        }

        return results.Count;
    }
}