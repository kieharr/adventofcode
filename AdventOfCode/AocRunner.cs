using System.Diagnostics;
using System.Reflection;
using AdventOfCode;

public class AocRunner
{
    public static void Run(int year, int day, int part = 0)
    {
        var solution = GetSolution(year, day);

        switch (part)
        {
            case 0:
                RunPart1(solution);
                Console.WriteLine();
                RunPart2(solution);
                break;
            case 1:
                RunPart1(solution);
                break;
            case 2:
                RunPart2(solution);
                break;
        }
    }

    private static void RunPart1(ASolution solution)
    {
        Console.WriteLine("Part 1:");
        var st = Stopwatch.StartNew();
        Console.WriteLine(solution.Part1());
        Console.WriteLine($"In {st.ElapsedMilliseconds} ms ({st.ElapsedTicks}) ticks");
    }
    
    private static void RunPart2(ASolution solution)
    {
        Console.WriteLine("Part 2:");
        var st = Stopwatch.StartNew();
        Console.WriteLine(solution.Part2());
        Console.WriteLine($"In {st.ElapsedMilliseconds} ms ({st.ElapsedTicks}) ticks");
    }

    private static ASolution GetSolution(int year, int day)
    {
        var solution = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => !x.IsAbstract && x.IsAssignableTo(typeof(ASolution)))
            .Where(x => x.Namespace.EndsWith(year.ToString()))
            .First(x => x.Name.Equals($"Day{day:00}"));

        return (ASolution) Activator.CreateInstance(solution);
    }
}