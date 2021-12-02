namespace AdventOfCode.Solutions._2020;

public class Day01: ASolution
{
    public override object Part1()
    {
        var nums = Input.Select(int.Parse).ToList();
        var num = nums.First(x => nums.Contains(2020 - x));
        return (2020 - num) * num;
    }

    public override object Part2()
    {
        var nums = Input.Select(int.Parse).ToList();

        for(var i = 0; i < nums.Count; i++)
        {
            for(var j = i + 1; j < nums.Count; j++)
            {
                var target = 2020 - nums[i] - nums[j];
                if (nums.Contains(target))
                {
                    return (target * nums[i] * nums[j]).ToString();
                }
            }
        }
        throw new NotImplementedException();
    }
}