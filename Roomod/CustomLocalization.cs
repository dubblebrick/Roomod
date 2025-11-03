using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomod
{
    internal class CustomLocalization
    {
        public Languages.Language language { get; }
        private Dictionary<string, string> localizationData;

        internal CustomLocalization(Languages.Language language, Dictionary<string, string> localizationData)
        {
            this.language = language;
            this.localizationData = localizationData;
        }

        internal bool TryGetValue(string lang, string key, out string value)
        {
            if (Languages.ParseLanguage(lang) == language)
            {
                return localizationData.TryGetValue(key, out value);
            }
            else
            {
                value = "";
                return false;
            }
        }

        internal bool TryGetValue(string key, out string value)
        {
            return localizationData.TryGetValue(key, out value);
        }
    }
}
