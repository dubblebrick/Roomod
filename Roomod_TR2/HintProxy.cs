using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Roomod
{
    internal class HintProxy : Component, IHintInfo
    {
        private List<string> hints;
        private HintManager.eHintSpeed hintSpeed;
        internal HintProxy(string root, HintManager.eHintSpeed speed)
        {
            hints = new();
            hintSpeed = speed;

            int hintCount = 0;
            string hintKey = root + "_H1";
            while (Localization.instance.Get(hintKey) != hintKey)
            {
                hints.Add(hintKey);
                hintKey = root + "_H" + (++hintCount + 1).ToString();
            }
        }

        public void GetHintInfo(HintManager.HintInfoQuery query)
        {
            query.Hints = hints;
            query.Speed = this.hintSpeed;
            query.Priority = 1;
            query.HasBeenFilledIn = true;
        }
    }
}
