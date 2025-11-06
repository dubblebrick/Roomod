using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Roomod;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace Roomod_TR2
{
    [BepInPlugin("com.dubblebrick.roomod.tr2", "Roomod TR2", MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("TheRoomTwo.exe")]
    [BepInDependency("com.dubblebrick.roomod.base")]
    public class RoomodTR2 : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        internal static HashSet<PuzzleItemTypewriter.TypewriterResponse> customResponses = new();

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            Harmony.CreateAndPatchAll(typeof(TR2Patches));

            Logger.LogInfo($"Successfully loaded Roomod TR2!");
        }

        private void Update()
        {
            if (RoomodBase.debugEnable.Value && RoomodBase.debugFastHintsKeybind.Value.IsDown())
            {
                HintManager.Instance.UseDebugAcceleratedHints();
                Log("Accelerated hints activated");
            }
        }

        internal static void Log(string msg)
        {
            if (RoomodBase.debugEnable.Value)
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
        /// <param name="time">The amount of time in seconds that the popup will be displayed for.</param>
        public static void CreateTutorialPopup(string text, float time = 8f)
        {
            // could possibly patch HudManager.SetHelpText() to allow for custom titles like in TR3
            TextManager.Instance.SetText(text, "", time);
        }

        /// <summary>
        /// Registers a set of hints to be displayed.
        /// </summary>
        /// <param name="hintRoot">The root of the localization keys for all elements of the hint set.</param>
        /// <param name="speed">The amount of time it takes to display each hint.</param>
        public static void RegisterHintSet(string hintRoot, HintManager.eHintSpeed speed = HintManager.eHintSpeed.Medium)
        {
            HintManager.Instance.AddHintItem(new HintProxy(hintRoot, speed));
        }

        /// <summary>
        /// Attempts to register a new typewriter response for a given keyword. If the keyword is already registered, the response won't be added.
        /// </summary>
        /// <param name="keyword">The key word that triggers the response.</param>
        /// <param name="response">The response that the typewriter will generate.</param>
        /// <returns><c>true</c> if the response was added successfully</returns>
        public static bool RegisterTypewriterResponse(string keyword, string response)
        {
            return customResponses.Add(new PuzzleItemTypewriter.TypewriterResponse
            {
                PlayerText = keyword,
                ResponseText = response
            });
        }
    }
}
