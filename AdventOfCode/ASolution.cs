namespace AdventOfCode;

public abstract class ASolution
{
    protected readonly string[] Input;

    protected ASolution()
    {
        var type = GetType();

        var year = int.Parse(type.Namespace?.Substring(type.Namespace.Length - 4) ?? throw new Exception("Error getting year from namespace"));
        var day = int.Parse(type.Name[3..]);

        Input = InputFetcher.LoadInput(year, day);
    }

    public abstract object Part1();
    public abstract object Part2();
}