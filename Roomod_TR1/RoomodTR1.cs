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

            Logger.LogInfo($"Successfully loaded Roomod TR1!");
        }

        private void Update()
        {
            if (RoomodBase.debugEnable.Value && RoomodBase.debugFastHintsKeybind.Value.IsDown())
            {
                HintManager.Instance.UseDebugAcceleratedHints();
                Log("Accelerated hints activated");
            }
        }

        // debug log method
        internal static void Log(string msg)
        {
            if (RoomodBase.debugEnable.Value)
                Logger.LogDebug(msg);
        }

        /// <summary>
        /// Creates a message box on the screen.
        /// </summary>
        /// <param name="message">The message to display, or a localization key.</param>
        /// <param name="position">The position of the message box on the screen.</param>
        public static void CreateMessageBox(string message, MessageBox.BoxPosition position = MessageBox.BoxPosition.Bottom)
        {
            // maybe add support for callbacks later
            MessageBox.Instance.DoMessageBox(message, null, 0, position);
        }

        /// <summary>
        /// Creates a tutorial popup on the screen.
        /// </summary>
        /// <param name="text">The message to display, or a localization key.</param>
        /// <param name="time">The amount of time in seconds the popup will be displayed for.</param>
        public static void CreateTutorialPopup(string text, float time)
        {
            TextManager.Instance.SetText(text, "", time);
        }

        /// <summary>
        /// Registers a set of hints to be displayed.
        /// </summary>
        /// <param name="hintRoot">The root of the localization keys for all elements of the hint set.</param>
        /// <param name="speed">The amount of time it takes to display each hint.</param>
        public static void RegisterHintSet(string hintRoot, HintManager.eHintSpeed speed = HintManager.eHintSpeed.Medium)
        {
            // Hint querying works a bit differently in TR1, requiring a GameObject to which it will send the "GetHintInfo" message.
            GameObject proxyObject = new($"HintProxy_{hintRoot}");
            proxyObject.AddComponent<HintProxy>().PrepareHintQuery(hintRoot, speed);
            HintManager.Instance.AddHintItem(proxyObject);
        }
    }
}
