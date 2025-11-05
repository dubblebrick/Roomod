Roomod
===

Roomod is a modular modding framework for The Room, The Room Two, and The Room Three.

(The Room Two is not yet supported. Support will be added in a coming update.)

Due to significant techincal differences from the first three games, there is no plan to extend support to The Room: Old Sins.

Features
---

- Overriding text per language
- Triggering custom text boxes and hints at any time
- More features coming soon

Installation
---

From the [latest release](releases/latest), download and unzip the mod version corresponding to the game you are modding, and place the Roomod folder in the BepInEx plugins folder for that game.

Developing with Roomod
---

Check out [the wiki](wiki) for guides and documentation on the Roomod framework.

Note: The wiki is still heavily WIP and will be missing significant information.

Building Instructions
---

Download the project folder for the base module along with the folder for any extension plugin you want to build, then run `dotnet build` in the extension module's project directory. Requires .NET Framework 3.5 or newer.
