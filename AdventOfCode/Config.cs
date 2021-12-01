namespace AdventOfCode;

public static class Config
{
    public static readonly string BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
    public static readonly string Token = File.ReadAllText(Path.Combine(BasePath, "token"));
}