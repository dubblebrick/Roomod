using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomod
{
    /// <summary>
    /// Exception thrown for errors related to localization files
    /// </summary>
    public class InvalidLocalizationException : Exception
    {

        public InvalidLocalizationException(string message, string source) : base(message)
        {
            Source = source;
        }
    }
}
