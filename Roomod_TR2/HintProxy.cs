using System.Collections.Generic;
using Roomod;
using UnityEngine;

namespace Roomod_TR2;

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

        if (hintCount == 0)
        {
            throw new InvalidLocalizationException(
                $"Hint root \"{root}\" contains no hints.",
                root
            );
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
