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

            Logger.LogInfo($"Successfully loaded Roomod TR3!");
        }

        private void Update()
        {
            if (RoomodBase.debugEnable.Value && RoomodBase.debugFastHintsKeybind.Value.IsDown())
            {
                HintManager.Instance.UseDebugAcceleratedHints();
                Log("Accelerated hints activated.");
            }
        }

        internal static void Log(string msg)
        {
            Logger.LogDebug(msg);
        }

        /// <summary>
        /// Creates a message box on the screen.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="pos">The position of the message box on the screen.</param>
        public static void CreateMessageBox(string message, MessageBox.BoxPosition pos = MessageBox.BoxPosition.Bottom)
        {
            // maybe add support for callbacks later
            HudManager.Instance.DoMessageBox(message, null, 0, pos);
        }

        /// <summary>
        /// Creates a tutorial popup on the screen.
        /// </summary>
        /// <param name="text">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="time">The amount of time in seconds that the popup will be displayed for.</param>
        public static void CreateTutorialPopup(string text, float time = 8f, string title = "HUD_HINT_TUTORIAL")
        {
            if (title == string.Empty)
                title = " ";
            HintTextManager.Instance.ShowText(text, title, time);
        }

        /// <summary>
        /// Registers a set of hints to be displayed.
        /// </summary>
        /// <param name="hintRoot">The root of the localization keys for all elements of the hint set.</param>
        /// <param name="speed">The amount of time it takes to display each hint.</param>
        public static void RegisterHintSet(string hintRoot, HintManager.eHintSpeed speed = HintManager.eHintSpeed.Medium)
        {
            HintManager.Instance.AddHintItem(new HintProxy(hintRoot, speed));
            // Look into potentially patching the game to make arbitrary hint times possible
        }
    }
}
