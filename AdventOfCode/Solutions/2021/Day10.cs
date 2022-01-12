namespace AdventOfCode.Solutions._2021;

public class Day10: ASolution
{
    public override object Part1()
    {
        var errorScore = 0;
        foreach (var s in Input)
        {
            try
            {
                ParseLine(s);
            }
            catch (SyntaxErrorException e)
            {
                errorScore += e.Score;
            }
        }

        return errorScore;
    }

    public override object Part2()
    {
        var scores = new List<long>();
        foreach (var s in Input)
        {
            try
            {
                scores.Add(ParseLine(s));
            }
            catch (SyntaxErrorException)
            {
            }
        }

        return scores.OrderBy(x => x).ToList()[scores.Count / 2];
    }

    private long ParseLine(string line)
    {
        var st = new Stack<char>();
        foreach (var character in line)
        {
            switch (character)
            {
                case '(':
                    st.Push(')');
                    break;
                case '[':
                    st.Push(']');
                    break;
                case '{':
                    st.Push('}');
                    break;
                case '<':
                    st.Push('>');
                    break;
                case ')':
                case ']':
                case '}':
                case '>':
                    if (character != st.Pop())
                    {
                        throw new SyntaxErrorException(character switch
                        {
                            ')' => 3,
                            ']' => 57,
                            '}' => 1197,
                            '>' => 25137
                        });
                    }
                    break;
                default:
                    throw new Exception($"illegal {character}");
                    
            }
        }

        return GetCompletionScore(st);
    }

    private long GetCompletionScore(IEnumerable<char> completionChars)
    {
        var score = 0L;

        foreach (var completionChar in completionChars)
        {
            score *= 5;
            score += completionChar switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4
            };
        }
        
        return score;
    }
}

public class SyntaxErrorException : Exception
{
    public readonly int Score;

    public SyntaxErrorException(int score)
    {
        Score = score;
    }
}