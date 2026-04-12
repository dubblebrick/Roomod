using HarmonyLib;
using Roomod;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace Roomod_TR3;
internal class TR3Patches
{
    // Patch that intercepts localization queries to use custom localization data.
    [HarmonyPatch(typeof(Localization), nameof(Localization.Get))]
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

    // Reverse patch that acts as BoxGameStateManager.LoadLevel() but switches the gamemode to story instead of freeplay.
    // Story mode enables saving and has some currently unexplored implications regarding inventory items.
    [HarmonyPatch(typeof(BoxGameStateManager), "LoadLevel")]
    [HarmonyReversePatch(HarmonyReversePatchType.Original)]
    internal static void LoadLevelStory(BoxGameStateManager instance, int level)
    {
        IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Stfld && codes[i].operand.ToString().Contains("GameMode"))
                {
                    codes[i - 1].opcode = OpCodes.Ldc_I4_0;
                    break;
                }
            }
            return codes.AsEnumerable();
        }

        // compiler gets mad if it thinks the transpiler is unused even though it ends up getting called externally
        _ = Transpiler(null);
    }

    // Transpiler that inserts a method call to query custom hint timers
    [HarmonyPatch(typeof(HintManager), "ChooseNewHint")]
    [HarmonyTranspiler]
    internal static IEnumerable<CodeInstruction> NewHintCustomTimeHook(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        CodeInstruction load = null;
        for (int i = 0; i < codes.Count; i++)
        {
            var code = codes[i];
            if (load == null && code.opcode == OpCodes.Ldfld && code.operand.ToString().Contains("CurrentHintItem"))
            {
                load = code;
            }

            if (load != null && code.opcode == OpCodes.Ldelem_R4)
            {
                i++;
                codes.Insert(i, new CodeInstruction(OpCodes.Ldarg_0));
                codes.Insert(i + 1, new CodeInstruction(load));
                codes.Insert(i + 2, CodeInstruction.LoadField((load.operand as FieldInfo).FieldType, "Owner"));
                codes.Insert(i + 3, CodeInstruction.Call(typeof(RoomodTR3), nameof(RoomodTR3.CheckCustomHintTimes)));
                RoomodBase.Log("Injected method call in ChooseNewHint()");
                break;
            }
        }
        return codes.AsEnumerable();
    }

    // Same as above for a different method that sets the hint timer
    [HarmonyPatch(typeof(HintManager), nameof(HintManager.HintButtonPressed))]
    [HarmonyTranspiler]
    internal static IEnumerable<CodeInstruction> NextHintCustomTimeHook(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        CodeInstruction load = null;
        for (int i = 0; i < codes.Count; i++)
        {
            var code = codes[i];
            if (load == null && code.opcode == OpCodes.Ldfld && code.operand.ToString().Contains("CurrentHintItem"))
            {
                load = code;
            }

            if (load != null && code.opcode == OpCodes.Ldelem_R4)
            {
                i++;
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldarg_0));
                codes.Insert(i + 2, new CodeInstruction(load));
                codes.Insert(i + 3, CodeInstruction.LoadField((load.operand as FieldInfo).FieldType, "Owner"));
                codes.Insert(i + 4, CodeInstruction.Call(typeof(RoomodTR3), nameof(RoomodTR3.CheckCustomHintTimes)));
                RoomodBase.Log("Injected method call in HintButtonPressed()");
                break;
            }
        }

        return codes.AsEnumerable();
    }

    // Debug output
    [HarmonyPatch(typeof(HintManager), "SetTimeUntilNextHint")]
    [HarmonyPostfix]
    internal static void SetHintTimePostfix(float time)
    {
        RoomodBase.Log($"Set hint time to {time} seconds");
    }
}
