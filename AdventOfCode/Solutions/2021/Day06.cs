using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Solutions._2021;

public class Day06: ASolution
{
    public override object Part1()
    {
        return GetNumberOfFish(80);
    }
    
    public override object Part2()
    {
        return GetNumberOfFish(256);
    }

    private long GetNumberOfFish(int days)
    {
        var nums = Enumerable.Range(0, 9).Select(x => (long)0).ToList();

        foreach (var num in Input.First().Split(',').Select(int.Parse))
        {
            nums[num]++;
        }

        foreach (var _ in Enumerable.Range(0, days))
        {
            nums.Add(nums[0]);
            nums.RemoveAt(0);
            nums[6] += nums[8];
        }
        return nums.Sum();
    }
}