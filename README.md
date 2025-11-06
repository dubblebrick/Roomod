Roomod
===

Roomod is a modular modding framework for The Room, The Room Two, and The Room Three.

Due to significant techincal differences from the first three games, there is no plan to extend support to The Room: Old Sins.

Features
---

With Roomod, you can:

- Override nearly any text
- Trigger custom text boxes and hints
- Create custom typewriter responses in TR2

More features coming soon!

Installation
---

Requires the x86 version of BepInEx 5 to be installed on whichever game you are modding. See [this page](https://docs.bepinex.dev/v5.4.21/articles/user_guide/installation/index.html) for a guide on how to download and install it.

If you are modding The Room or The Room Two, you need to change the entrypoint settings in the BepInEx config to prevent the game from crashing or freezing as soon as you launch it. The config file is found at `[game directory]\BepInEx\config\BepInEx.cfg` after the first time you launch the game. Change the `Type` setting under `Preloader.Entrypoint` (should be right at the bottom of the file) from `Application` to `MonoBehaviour` and the game should run properly again.

From the [latest release](https://github.com/dubblebrick/Roomod/releases/latest), download and unzip the mod version corresponding to the game you are modding, and place the Roomod folder in the BepInEx plugins directory for that game.

Developing with Roomod
---

Check out [the wiki](wiki) for guides and documentation on the Roomod framework.

Note: The wiki is still heavily WIP and will be missing significant information.

Building from Source
---

Download the project folder for the base module along with the folder for any extension module you want to build, then run `dotnet build` in the extension module's project directory. Requires .NET Framework 3.5 or newer.
