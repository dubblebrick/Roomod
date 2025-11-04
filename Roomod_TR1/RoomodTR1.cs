using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Roomod;

namespace Roomod_TR1
{
    [BepInPlugin("com.dubblebrick.roomod.tr1", "Roomod TR1", MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("TheRoom.exe")]
    [BepInDependency("com.dubblebrick.roomod.base")]
    public class RoomodTR1 : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            Harmony.CreateAndPatchAll(typeof(TR1Patches));

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        // debug log method
        internal static void Log(string msg)
        {
            if (RoomodBase.debugEnable.Value)
                Logger.LogInfo(msg);
        }
    }
}
