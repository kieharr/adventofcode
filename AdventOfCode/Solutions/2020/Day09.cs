namespace AdventOfCode.Solutions._2020;

public class Day09: ASolution
{
    public override object Part1()
    {
        var data = Input.Select(long.Parse).ToList();
        return FindFirstInvalidNumber(data);
    }

    public override object Part2()
    {
        var data = Input.Select(long.Parse).ToList();
        var target = FindFirstInvalidNumber(data);
            
        for (var i = 0; i < data.Count; i++)
        {
            var result = GetEncryptionWeakness(target, data.Skip(i));
            if (result == null) continue;

            return (result.Min() + result.Max()).ToString();
        }
            
        return null;
    }
    
    private long FindFirstInvalidNumber(List<long> data)
    {
        var checkSize = 25;
        for (var i = checkSize; i < data.Count; i++)
        {
            var subList = data.Skip(i - checkSize).Take(checkSize).ToList();
            var target = data[i];
            if(!IsValid(subList, target))
            {
                return target;
            }
        }
        throw new Exception();
    }

    private bool IsValid(List<long> list, long target)
    {
        foreach (var i in list.Skip(1))
        {
            var subTarget = target - i;
            if(subTarget == i)
                continue;
            if (list.Contains(subTarget))
                return true;
        }

        return false;
    }
    
    private List<long> GetEncryptionWeakness(long target, IEnumerable<long> nums)
    {
        long sum = 0;
        var checkedList = new List<long>();
        foreach (var num in nums)
        {
            checkedList.Add(num);
            sum += num;
            if (sum > target)
                return null;
            if (sum == target)
                return checkedList;
        }

        return null;
    }
}