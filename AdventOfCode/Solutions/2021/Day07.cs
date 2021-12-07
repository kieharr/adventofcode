using System.Collections.Concurrent;

namespace AdventOfCode.Solutions._2021;

public class Day07: ASolution
{
    public override object Part1()
    {
        var numbers = Input.First().Split(',').Select(int.Parse).ToList();

        return Enumerable.Range(numbers.Min(), numbers.Max() - numbers.Min()).AsParallel()
            .Select(x => numbers.Sum(y => Math.Abs(y - x))).Min();
    }

    public override object Part2()
    {
        _fuelRequiredCache.TryAdd(0, 0);
        _fuelRequiredCache.TryAdd(1, 1);
        
        var numbers = Input.First().Split(',').Select(int.Parse).ToList();
        
        return Enumerable.Range(numbers.Min(), numbers.Max() - numbers.Min()).AsParallel()
            .Select(x => numbers.Sum(y => FuelRequired(Math.Abs(y - x)))).Min();
    }

    private readonly ConcurrentDictionary<long, long> _fuelRequiredCache = new();

    private long FuelRequired(long steps)
    {
        if (_fuelRequiredCache.TryGetValue(steps, out var result))
        {
            return result;
        } 

        var results = steps + FuelRequired(steps - 1);
        _fuelRequiredCache.TryAdd(steps, results);
        return results;
    }
}