using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Roomod
{
    [BepInPlugin("com.dubblebrick.roomod.base", "Roomod Base", MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("TheRoom.exe")]
    [BepInProcess("TheRoomTwo.exe")]
    [BepInProcess("TheRoomThree.exe")]
    public class RoomodBase : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private static List<CustomLocalization> customLocalizations;

        public static ConfigEntry<bool> debugEnable { get; private set; }
        public static ConfigEntry<KeyboardShortcut> debugFastHintsKeybind { get; private set; }
        private void Awake()
        {
            Logger = base.Logger;
            customLocalizations = new();

            debugEnable = Config.Bind("Debug", "DebugEnable", false, "Whether Roomod's debug features should be enabled.");
            debugFastHintsKeybind = Config.Bind("Debug", "FastHintsKeybind", new KeyboardShortcut(UnityEngine.KeyCode.H), "A keybind to set hint timers to 1 second.");

            Harmony.CreateAndPatchAll(typeof(BasePatches));

            Logger.LogInfo($"Successfully loaded Roomod Base");
        }

        public static void CreateCustomLocalization(Languages.Language lang, string path)
        {
            path = "BepInEx/plugins/" + path;
            if (!File.Exists(path))
                throw new FileNotFoundException($"File \"{path}\" could not be found.");

            StreamReader sr = new(path);
            string nextLine;
            Dictionary<string, string> dict = new();
            while ((nextLine = sr.ReadLine()) != null)
            {
                if (Regex.Match(nextLine, "[A-Z1-9_]+=[^=]+").Value != nextLine)
                    throw new Exception($"One or more localization keys in \"{path}\" is formatted incorrectly.");
                dict.Add(nextLine.Split('=')[0], nextLine.Split('=')[1]);
            }
            sr.Close();
            customLocalizations.Add(new CustomLocalization(lang, dict));
        }

        /// <summary>
        /// Searches all custom localization files with a specified language for a value corresponding to a localization key.
        /// </summary>
        /// <param name="lang">The language to search for matches in</param>
        /// <param name="key">The localization key to search for</param>
        /// <param name="value">When this method returns, <c>value</c> contains the value found, or an empty string if no match was found.</param>
        /// <returns><c>true</c> if a match was found in any custom localization</returns>
        public static bool TryGetCustomLocalization(Languages.Language lang, string key, out string value)
        {
            foreach(CustomLocalization loc in customLocalizations)
            {
                if (loc.language != lang)
                    continue;
                if (loc.TryGetValue(key, out value))
                    return true;
            }
            value = "";
            return false;
        }

        /// <summary>
        /// Searches all custom localization files with a specified language for a value corresponding to a localization key.
        /// </summary>
        /// <param name="lang">The name of the language to search for matches in</param>
        /// <param name="key">The localization key to search for</param>
        /// <param name="value">When this method returns, <c>value</c> contains the value found, or an empty string if no match was found.</param>
        /// <returns><c>true</c> if a match was found in any custom localization</returns>
        public static bool TryGetCustomLocalization(string lang, string key, out string value)
        {
            foreach (CustomLocalization loc in customLocalizations)
            {
                if (loc.language != Languages.ParseLanguage(lang))
                    continue;
                if (loc.TryGetValue(key, out value))
                    return true;
            }
            value = "";
            return false;
        }

        internal static void Log(string msg)
        {
            Logger.LogInfo(msg);
        }
    }
}
