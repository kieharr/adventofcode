using System.Drawing;

namespace AdventOfCode.Solutions._2021;

public class Day04: ASolution
{
    public override object Part1()
    {
        foreach (var num in GetNumbersFromInput(Input))
        {
            foreach (var bingoCard in GetCardsFromInput(Input))
            {
                if (bingoCard.CheckNumber(num, out var score))
                {
                    return score;
                }
            }
        }

        throw new Exception();
    }

    public override object Part2()
    {
        var nums = GetNumbersFromInput(Input);
        var cards = GetCardsFromInput(Input);

        foreach (var num in nums)
        {
            foreach (var bingoCard in cards.ToList())
            {
                if (bingoCard.CheckNumber(num, out var score))
                {
                    cards.Remove(bingoCard);
                    if (cards.Count == 0)
                    {
                        return score;
                    }
                }
            }
        }

        throw new Exception();
    }
    
    private static List<int> GetNumbersFromInput(IEnumerable<string> input)
    {
        return input.First().Split(',').Select(int.Parse).ToList();
    }

    private static List<BingoCard> GetCardsFromInput(IEnumerable<string> input)
    {
        BingoCard card = new();
        var cards = new List<BingoCard>();
        
        foreach (var line in input.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                card = new BingoCard();
                cards.Add(card);
                continue;
            }
            
            card.AddRow(line.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse).ToList());
        }

        return cards;
    }
    
    private class BingoCard
    {
        private readonly Dictionary<Point, bool> _marked = new();
        private readonly Dictionary<int, Point> _numbers = new();
        private int _xSize;
        private int _ySize;

        public void AddRow(IReadOnlyList<int> values)
        {
            if (_ySize == 0)
            {
                _xSize = values.Count;
            }

            for (var i = 0; i < _xSize; i++)
            {
                var point = new Point(i, _ySize);
                _marked.Add(point, false);
                _numbers.Add(values[i], point);
            }
            _ySize++;
        }

        public bool CheckNumber(int num, out int score)
        {
            score = 0;
            if (!_numbers.TryGetValue(num, out var location)) return false;
            
            _marked[location] = true;
            if (CheckRow(location.Y) || CheckColumn(location.X))
            {
                score = GetScore(num);
                return true;
            }
            return false;
        }

        private int GetScore(int calledNumber)
        {
            return _marked.Where(x => !x.Value)
                .Select(x => _numbers.First(number => number.Value == new Point(x.Key.X, x.Key.Y)))
                .Sum(x => x.Key)
                   * calledNumber;
        }

        private bool CheckColumn(int x)
        {
            for (var y = 0; y < _ySize; y++)
            {
                if (!_marked[new Point(x, y)])
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckRow(int y)
        {
            for (var x = 0; x < _xSize; x++)
            {
                if (!_marked[new Point(x, y)])
                {
                    return false;
                }
            }
            return true;
        }
    }
}