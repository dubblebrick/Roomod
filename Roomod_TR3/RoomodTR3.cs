using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Roomod;
using System.Collections.Generic;

namespace Roomod_TR3;

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
            RoomodBase.Log("Accelerated hints activated.");
        }
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
        {
            title = " ";
        }
        HintTextManager.Instance.ShowText(text, title, time);
    }

    /// <summary>
    /// Registers a set of hints to be displayed. The hints will be displayed after a specified amount of time, with hints after the first taking half as much time.
    /// </summary>
    /// <param name="hintRoot">The root of the localization keys for all elements of the hint set.</param>
    /// <param name="speed">The amount of time in seconds it takes to display the first hint.</param>
    public static void RegisterHintSet(string hintRoot, float speed = 60)
    {
        HintProxy hint = new HintProxy(hintRoot, speed);
        HintManager.Instance.AddHintItem(hint);
    }

    /// <summary>
    /// Changes the current level number, and loads the corresponding map.
    /// </summary>
    /// <param name="level">The level number to switch to.</param>
    /// <remarks>
    /// This method sets the gamemode to story mode, which enables normal game progress and saving. Use <code>BoxGameStateManager.LoadLevel()</code> to change levels while setting freeplay mode.
    /// </remarks>
    public static void ChangeLevel(int level)
    {
        TR3Patches.LoadLevelStory(BoxGameStateManager.Instance, level);
    }

    internal static float CheckCustomHintTimes(float originalTime, UnityEngine.Component hintOwner)
    {
        RoomodBase.Log("Checking for custom hint...");
        // If accelerated hints were activated, fall through and use the standard hint time which is now 1 second.
        if (HintManager.Instance.HintTimes[0] == 1f)
        {
            RoomodBase.Log("Accelerated hints active, falling through to default time.");
            return originalTime;
        }
        else if (hintOwner is HintProxy proxy)
        {
            RoomodBase.Log($"Using custom hint time of {proxy.hintSpeed} seconds.");
            return proxy.hintSpeed;
        }
        else
        {
            RoomodBase.Log("Not a custom hint, falling through to default.");
            return originalTime;
        }
    }
}
