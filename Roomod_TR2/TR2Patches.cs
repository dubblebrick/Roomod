using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Roomod;

namespace Roomod_TR2
{
    internal class TR2Patches
    {
        [HarmonyPatch(typeof(Localization), "Get")]
        [HarmonyPrefix]
        internal static bool InjectCustomLocalization(Localization __instance, ref string __result, string key)
        {
            if (RoomodBase.TryGetCustomLocalization(__instance.currentLanguage, key, out string value))
            {
                RoomodTR2.Log($"Overwrote localization key {key}");
                __result = value;
                return false;
            }
            else
                return true;
        }

        [HarmonyPatch(typeof(PuzzleItemTypewriter), "GetAIResponse")]
        [HarmonyPrefix]
        internal static bool InjectCustomTypewriterResponse(PuzzleItemTypewriter __instance, ref PuzzleItemTypewriter.TypewriterResponse __result, string playerText, out int responseNumber)
        {
            responseNumber = -1;
            foreach (PuzzleItemTypewriter.TypewriterResponse response in RoomodTR2.customResponses)
            {
                string targetText = Localization.instance.Get(response.PlayerText);
                RoomodTR2.Log($"Typewriter: Comparing {playerText} with target string {targetText}");
                if (targetText == playerText)
                {
                    RoomodTR2.Log("Match found!");
                    __result = response;
                    return false;
                }
            }
            RoomodTR2.Log("No match found; falling through to original method.");
            return true;
        }
    }
}
