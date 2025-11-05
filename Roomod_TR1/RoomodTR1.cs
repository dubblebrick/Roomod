using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Roomod;
using UnityEngine;

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

        private void Update()
        {
            if (RoomodBase.debugEnable.Value && RoomodBase.debugFastHintsKeybind.Value.IsDown())
            {
                HintManager.Instance.UseDebugAcceleratedHints();
                Log("Accelerated hints activated");
            }
        }

        public static void CreateMessageBox(string text, MessageBox.BoxPosition position = MessageBox.BoxPosition.Bottom)
        {
            // maybe add support for callbacks later
            MessageBox.Instance.DoMessageBox(text, null, 0, position);
        }

        public static void CreateTutorialPopup(string text, float time)
        {
            TextManager.Instance.SetText(text, "", time);
        }

        public static void RegisterHintSet(string hintRoot, HintManager.eHintSpeed speed = HintManager.eHintSpeed.Medium)
        {
            // Hint querying works a bit differently in TR1, requiring a GameObject to which it will send the "GetHintInfo" message.
            GameObject proxyObject = new($"HintProxy_{hintRoot}");
            proxyObject.AddComponent<HintProxy>().PrepareHintQuery(hintRoot, speed);
            HintManager.Instance.AddHintItem(proxyObject);
        }

        // debug log method
        internal static void Log(string msg)
        {
            if (RoomodBase.debugEnable.Value)
                Logger.LogInfo(msg);
        }
    }
}
