using System.Drawing;

namespace AdventOfCode.Solutions._2020;

public class Day03: ASolution
{
    public override object Part1()
    {
        return new TreeMap(Input).GetTreeCount(new Point(3, 1));
    }

    public override object Part2()
    {
        var treeMap = new TreeMap(Input);
        return new List<Point>
            {
                new Point(1, 1),
                new Point(3, 1),
                new Point(5, 1),
                new Point(7, 1),
                new Point(1, 2)
            }
            .Select(treeMap.GetTreeCount)
            .Aggregate((res, item) => res * item);
    }
    
    private class TreeMap
    {
        private readonly HashSet<Point> _trees;
        private readonly int _height;
        private readonly int _width;

        public TreeMap(string[] input)
        {

            _trees = new HashSet<Point>();
            _height = input.Length;
            _width = input[0].Length;
                
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (input[y][x] == '#')
                    {
                        _trees.Add(new Point(x, y));
                    }
                }
            }
        }

        public long GetTreeCount(Point slope)
        {
            var treeCount = 0;
            for (int x = 0, y = 0; y < _height; x+=slope.X, y+=slope.Y)
            {
                if (_trees.Contains(new Point(x % _width, y)))
                {
                    treeCount++;
                }
            }
            return treeCount;
        }
    }
}