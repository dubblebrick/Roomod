using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Roomod
{
    internal class HintProxy : Component, IHintInfo
    {
        private string hintRoot;
        private int hintCount;
        private HintManager.eHintSpeed hintSpeed;
        internal HintProxy(string root, HintManager.eHintSpeed speed)
        {
            hintRoot = root;
            hintSpeed = speed;
            // FindNumberOfHintsForRoot() calls Localization.Get(), so it will see custom keys for hint paths
            hintCount = HintManager.FindNumberOfHintsForRoot(hintRoot);
        }

        public void GetHintInfo(HintManager.HintInfoQuery query)
        {
            query.HintRoot = this.hintRoot;
            query.HintCount = this.hintCount;
            query.Speed = this.hintSpeed;
            query.Priority = 1;
            query.HasBeenFilledIn = true;
        }
    }
}
