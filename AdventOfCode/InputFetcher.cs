using System.Net;

namespace AdventOfCode;

public static class InputFetcher
{
    public static string[] LoadInput(int year, int day)
    {
        var path = BuildFilename(year, day);
        if(File.Exists(path))
        {
            return File.ReadAllLines(path);
        }
        
        var inputUrl = $"https://adventofcode.com/{year}/day/{day}/input";

        try
        {
            using var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Cookie, $"session={Token}");
            var input = client.DownloadString(inputUrl).Trim();
            File.WriteAllText(path, input);
        }
        catch(WebException e)
        {
            var statusCode = ((HttpWebResponse)e.Response).StatusCode;
            if(statusCode == HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"Day {day}: 400. Check cookie");
            }
            else if(statusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Day {day}: 404. Not Found.");
            }
            else
            {
                Console.WriteLine(e.ToString());
            }
        }

        return File.ReadAllLines(path);
    }

    private static string BuildFilename(int year, int day)
    {
        return Path.Combine(BasePath, "Inputs", $"{year}-{day:00}.txt");
    }
}