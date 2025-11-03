using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomod
{
    public class Languages
    {
        public enum Language
        {
            None = -1,
            English,
            French,
            German,
            Italian,
            Portuguese,
            Russian,
            Spanish,
            Turkish
        }

        public static Language ParseLanguage(string lang)
        {
            switch (lang)
            {
                case "English":
                    return Language.English;
                case "French":
                    return Language.French;
                case "German":
                    return Language.German;
                case "Italian":
                    return Language.Italian;
                case "Portuguese":
                    return Language.Portuguese;
                case "Russian":
                    return Language.Russian;
                case "Spanish":
                    return Language.Spanish;
                case "Turkish":
                    return Language.Turkish;
                default:
                    return Language.None;
            }
        }
    }
}
