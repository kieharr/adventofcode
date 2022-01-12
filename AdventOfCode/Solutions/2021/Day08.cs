using System.Text;

namespace AdventOfCode.Solutions._2021;

public class Day08: ASolution
{
    public override object Part1()
    {
        return Input.Sum(GetCount);
    }

    public override object Part2()
    {
        return Input.Select(GetOutput).Sum();
    }

    private static int GetCount(string inputLine)
    {
        return inputLine.Substring(inputLine.IndexOf('|') + 1)
            .Split(' ').Count(x => x.Length is 2 or 3 or 4 or 7);
    }
    private int GetOutput(string inputLine)
    {
        Dictionary<string, char> map = new();
        var parts = inputLine.Split(" | ").Select(x => x.Split(' ')).ToList();
        var lhs = parts[0].GroupBy(x => x.Length).ToList();
    
        var one = lhs.First(x => x.Key == 2).First();
        map.Add(one, '1');
        var four = lhs.First(x => x.Key == 4).First();
        map.Add(four, '4');
        var seven = lhs.First(x => x.Key == 3).First();
        map.Add(seven, '7');
        var eight = lhs.First(x => x.Key == 7).First();
        map.Add(eight, '8');
    
        var twoThreeFive = lhs.First(x => x.Key == 5).ToList();
        var zeroSixNine = lhs.First(x => x.Key == 6).ToList();
    
        var three = twoThreeFive.First(x => one.All(x.Contains));
        map.Add(three, '3');
        twoThreeFive.Remove(three);
    
        var nine = zeroSixNine.First(x => three.All(x.Contains));
        map.Add(nine, '9');
        zeroSixNine.Remove(nine);
    
        var five = twoThreeFive.First(x => x.All(nine.Contains));
        map.Add(five, '5');
        twoThreeFive.Remove(five);
        
        var two = twoThreeFive.First();
        map.Add(two, '2');
    
        var six = zeroSixNine.First(x => five.All(x.Contains));
        map.Add(six, '6');
        zeroSixNine.Remove(six);
        var zero = zeroSixNine.First();
        map.Add(zero, '0');
    
        var sb = new StringBuilder();
        foreach (var str in parts[1])
        {
            var res = map.First(m => str.Length == m.Key.Length && str.All(m.Key.Contains)).Value;
            sb.Append(res);
        }
    
        return int.Parse(sb.ToString());
    }
}