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
    }
}
