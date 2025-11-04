using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Roomod
{
    internal class HintProxyObject : Component, IHintInfo
    {
        private string hintRoot;
        private int hintCount;
        private HintManager.eHintSpeed hintSpeed;
        internal HintProxyObject(string root, int count, HintManager.eHintSpeed speed)
        {
            hintRoot = root;
            hintCount = count;
            hintSpeed = speed;
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
