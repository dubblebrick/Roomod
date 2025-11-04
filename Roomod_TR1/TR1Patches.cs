using HarmonyLib;
using Roomod;

namespace Roomod_TR1
{
    internal class TR1Patches
    {
        [HarmonyPatch(typeof(Localization), "Get")]
        [HarmonyPrefix]
        public static bool InjectCustomLocalization(ref string __result, string key)
        {
            if (RoomodBase.TryGetCustomLocalization(Localization.language, key, out string value))
            {
                RoomodTR1.Log($"Overwrote localization key {key}.");
                __result = value;
                return false;
            }
            return true;
        }
    }
}
