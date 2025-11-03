using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Roomod;
using System.Security.Permissions;

namespace Roomod_TR3
{
    [BepInPlugin("com.dubblebrick.roomod.tr3", "Roomod TR3", MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("TheRoomThree.exe")]
    [BepInDependency("com.dubblebrick.roomod.base")]
    public class RoomodTR3 : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private void Awake()
        {
            Logger = base.Logger;

            // Patches specific to TR3
            Harmony.CreateAndPatchAll(typeof(TR3Patches));

            Logger.LogInfo($"Successfully loaded Roomod TR3");
        }

        internal static void Log(string msg)
        {
            Logger.LogInfo(msg);
        }

        public static void CreateMessageBox(string msg)
        {
            HudManager.Instance.DoMessageBox(msg, null, 0, MessageBox.BoxPosition.Bottom);
        }
    }
}
