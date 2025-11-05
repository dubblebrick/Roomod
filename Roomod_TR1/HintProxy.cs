using System.Collections.Generic;
using UnityEngine;

namespace Roomod
{
    /// <summary>
    /// A MonoBehaviour component to be attached to a game object and provide hint set information to the HintManager.
    /// </summary>
    internal class HintProxy : MonoBehaviour
    {
        private List<string> hints;
        private HintManager.eHintSpeed hintSpeed;
        internal void PrepareHintQuery(string root, HintManager.eHintSpeed speed)
        {
            hintSpeed = speed;
            hints = new();
            int hintCount = 0;
            string hintKey = root + "_H1";

            while (Localization.Get(hintKey) != hintKey)
            {
                hints.Add(hintKey);
                hintKey = root + "_H" + (++hintCount + 1).ToString();
            }
        }

        // GetHintInfo will be called by the message sent by HintManager.
        public void GetHintInfo(HintManager.HintInfoQuery query)
        {
            query.HasBeenFilledIn = true;
            query.Hints = hints;
            query.Speed = hintSpeed;
        }
    }
}
