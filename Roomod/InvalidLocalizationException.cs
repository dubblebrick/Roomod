using System.IO;

namespace Roomod;

/// <summary>
/// Exception thrown for errors related to localization files
/// </summary>
public class InvalidLocalizationException : IOException
{
    public InvalidLocalizationException(string message, string source) : base(message)
    {
        Source = source;
    }
}
