using HarmonyLib;
using Roomod;

namespace Roomod_TR2;

internal class TR2Patches
{
    [HarmonyPatch(typeof(Localization), "Get")]
    [HarmonyPrefix]
    internal static bool InjectCustomLocalization(Localization __instance, ref string __result, string key)
    {
        if (RoomodBase.TryGetCustomLocalization(Roomod.Languages.ParseLanguage(__instance.currentLanguage), key, out string value))
        {
            RoomodBase.Log($"Overwrote localization key {key}");
            __result = value;
            return false;
        }
        else
        {
            return true;
        }
    }

    [HarmonyPatch(typeof(PuzzleItemTypewriter), "GetAIResponse")]
    [HarmonyPrefix]
    internal static bool InjectCustomTypewriterResponse(PuzzleItemTypewriter __instance, ref PuzzleItemTypewriter.TypewriterResponse __result, string playerText, out int responseNumber)
    {
        responseNumber = -1;
        foreach (PuzzleItemTypewriter.TypewriterResponse response in RoomodTR2.customResponses)
        {
            string targetText = Localization.instance.Get(response.PlayerText).ToUpper();
            RoomodBase.Log($"Typewriter: Comparing {playerText} with target string {targetText}");
            if (targetText == playerText)
            {
                RoomodBase.Log("Match found!");
                __result = response;
                return false;
            }
        }
        RoomodBase.Log("No match found; falling through to original method.");
        return true;
    }
}
