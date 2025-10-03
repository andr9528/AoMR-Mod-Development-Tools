using System.Text;

namespace Tools.Abstraction.Extensions;

public static class StringExtensions
{
    public static string ToScreamingSnake(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var result = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (char.IsUpper(c))
            {
                // Add underscore if not the first character and the previous wasn't an underscore
                if (i > 0 && input[i - 1] != '_')
                {
                    result.Append('_');
                }
            }

            result.Append(char.ToUpperInvariant(c));
        }

        return result.ToString();
    }

    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        string[] parts = input.Split('_');
        var result = new StringBuilder();

        foreach (string part in parts)
        {
            if (part.Length == 0)
            {
                continue;
            }

            result.Append(char.ToUpperInvariant(part[0]));
            if (part.Length > 1)
            {
                result.Append(part[1..].ToLowerInvariant());
            }
        }

        return result.ToString();
    }
}
