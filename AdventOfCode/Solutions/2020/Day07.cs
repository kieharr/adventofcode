using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions._2020;

public class Day07: ASolution
{
    private readonly Regex _regex = new Regex(@"^(\w+\W\w+) bags contain (?:(\d)\W(\w+\W\w+)\W\w+\W+)*(?:no other bags)*(?:.$)*");
    public override object Part1()
    {
        var results = new Dictionary<string, List<string>>();
        foreach (var line in Input)
        {
            var matches = _regex.Match(line);
            var list = matches.Groups[3].Captures.Select(x => x.ToString()).ToList();
            results.Add(matches.Groups[1].Captures.First().ToString(), list);
        }
            
        return results.Count(result => HasTarget(results, "shiny gold", result.Key));
    }

    public override object Part2()
    {
        return CountBags(BuildBagCollection(Input), "shiny gold", 0, new Dictionary<string, int>()).ToString();
    }
    
    private Dictionary<string, List<KeyValuePair<string, int>>> BuildBagCollection(string[] input)
    {
        var bagCollection = new Dictionary<string, List<KeyValuePair<string, int>>>();
        foreach (var line in input)
        {
            var matches = _regex.Match(line);
            var kvpList = new List<KeyValuePair<string, int>>();
            for (var i = 0; i < matches.Groups[3].Captures.Count; i++)
            {
                kvpList.Add(new KeyValuePair<string, int>(matches.Groups[3].Captures[i].ToString(), int.Parse(matches.Groups[2].Captures[i].ToString())));
            }
                
            bagCollection.Add(matches.Groups[1].Captures.First().ToString(), kvpList);
        }

        return bagCollection;
    }
    
    private bool HasTarget(Dictionary<string, List<string>> results, string target, string term)
    {
        return results[term].Any(name => name == target || HasTarget(results, target, name));
    }

    private int CountBags(Dictionary<string, List<KeyValuePair<string, int>>> bagCollection, string term, int count, Dictionary<string, int> countedCache)
    {
        foreach (var (key, value) in bagCollection[term])
        {
            for (var i = 0; i < value; i++)
            {
                count += countedCache.TryGetValue(key, out var counted) ? counted : CountBags(bagCollection, key, 1, countedCache);
            }
        }
        countedCache.Add(term, count);
        return count;
    }
}