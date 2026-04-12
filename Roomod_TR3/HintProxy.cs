using UnityEngine;
using Roomod;

namespace Roomod_TR3;

internal class HintProxy : Component, IHintInfo
{
    public readonly string hintRoot;
    public readonly int hintCount;
    public readonly float hintSpeed;
    internal HintProxy(string root, float speed)
    {
        hintRoot = root;
        hintSpeed = speed;
        // FindNumberOfHintsForRoot() calls Localization.Get(), so it will see custom keys for hint paths
        hintCount = HintManager.FindNumberOfHintsForRoot(hintRoot);

        if (hintCount == 0)
        {
            throw new InvalidLocalizationException(
                $"Hint root \"{root}\" contains no hints.",
                root
            );
        }
    }

    // GetHintInfo is called by the hint manager when the hint is being registered.
    public void GetHintInfo(HintManager.HintInfoQuery query)
    {
        query.HintRoot = this.hintRoot;
        query.HintCount = this.hintCount;
        query.Speed = HintManager.eHintSpeed.Medium;
        query.Priority = 1;
        query.HasBeenFilledIn = true;
    }
}
