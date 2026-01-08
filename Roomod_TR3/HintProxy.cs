using UnityEngine;

namespace Roomod_TR3;

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
        query.HintRoot = this.hintRoot;
        query.HintCount = this.hintCount;
        query.Speed = this.hintSpeed;
        query.Priority = 1;
        query.HasBeenFilledIn = true;
    }
}
