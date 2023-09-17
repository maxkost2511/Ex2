using System.Text;

/// <summary>
/// String extension methods.
/// </summary>
public static class StringExtensions
{
    #region Public Methods

    /// <summary>
    /// Removes special characters.
    /// </summary>
    /// <param name="str">Input string</param>
    /// <returns></returns>
    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Formats json to human readable.
    /// </summary>
    /// <param name="str">Input string</param>
    /// <returns></returns>
    public static string FormatJson(this string str)
    {
        var stringBuilder = new StringBuilder();

        bool escaping = false;
        bool inQuotes = false;
        int indentation = 0;

        foreach (char character in str)
        {
            if (escaping)
            {
                escaping = false;
                stringBuilder.Append(character);
            }
            else
            {
                if (character == '\\')
                {
                    escaping = true;
                    stringBuilder.Append(character);
                }
                else if (character == '\"')
                {
                    inQuotes = !inQuotes;
                    stringBuilder.Append(character);
                }
                else if (!inQuotes)
                {
                    if (character == ',')
                    {
                        stringBuilder.Append(character);
                        stringBuilder.Append("\r\n");
                        stringBuilder.Append('\t', indentation);
                    }
                    else if (character == '[' || character == '{')
                    {
                        stringBuilder.Append(character);
                        stringBuilder.Append("\r\n");
                        stringBuilder.Append('\t', ++indentation);
                    }
                    else if (character == ']' || character == '}')
                    {
                        stringBuilder.Append("\r\n");
                        stringBuilder.Append('\t', --indentation);
                        stringBuilder.Append(character);
                    }
                    else if (character == ':')
                    {
                        stringBuilder.Append(character);
                        stringBuilder.Append('\t');
                    }
                    else if (!char.IsWhiteSpace(character))
                    {
                        stringBuilder.Append(character);
                    }
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }
        }

        return stringBuilder.ToString();
    }

    #endregion Public Methods
}
