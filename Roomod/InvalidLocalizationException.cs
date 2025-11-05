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

        public InvalidLocalizationException(string message, ErrorCode err, string[] sources) : base(message)
        {
            switch(err)
            {
                case ErrorCode.InvalidFormat:
                    Data.Add("Exception type", "Invalid data format");
                    Data.Add("Source file", sources[0]);
                    Data.Add("First invalid line", sources[1]);
                    break;
                case ErrorCode.EmptyHintSet:
                    Data.Add("Exception type", "No hints in set");
                    Data.Add("Hint root", sources[0]);
                    break;
            }
        }

        public enum ErrorCode
        {
            Other = -1,
            InvalidFormat,
            EmptyHintSet
        }
    }
}
