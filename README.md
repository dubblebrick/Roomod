Roomod
===

Roomod is a modding framework for The Room, The Room Two, and The Room Three.

Features
---

- Overriding text per language
- Triggering custom text boxes and hints at any time

Installation
---

From the [latest release](releases/latest), download the base module (`Roomod.dll`) along with the extension module corresponding to the game you are modding, and place both files in the BepInEx plugins folder for that game.

Building Instructions
---

Download the project folder for the base module along with the folder for any extension plugin you want to build, then run `dotnet build` in the extension module's project directory.
