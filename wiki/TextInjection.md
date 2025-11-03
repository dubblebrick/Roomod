Text Injection
===

The Room games use localization keys to easily make text change depending on the game's language setting. Roomod contains a function hook that allows you to hijack these localization keys and replace them with custom strings per language, as well as create your own localization keys to make custom text for multiple languages.

See [this spreadsheet](https://docs.google.com/spreadsheets/d/1t16va6qv8e2bDRM3_Y3wie7EJs7qzZ-m1qHmgjmsrpM/edit?usp=sharing) for a reference of what the localization keys correspond to in the English localization of each game.

Localization Files
---

A localization file is a text file containing a series of key-value pairs, where the key is the localization key and the value is the string to display.

The text file should contain exactly one key-value pair per line, and these key-value pairs should be formatted like this:

    LOCALIZATION_KEY=Value

The localization key should always be formatted in `SCREAMING_SNAKE_CASE` and only contain alphanumeric characters and underscores. The value can be any string, but cannot contain newline characters or the equals sign (`=`). Both the key and the value must be at least one character long. If a key-value pair is formatted incorrectly, Roomod will throw an exception.
Here is an example of a correctly formatted key-value pair:

    HUB_SCREWDRIVER=A screwdriver

Installing localization files
---

To install a localization file, call the method

    Roomod.CreateCustomLocalization(Language lang, string path)

where `lang` is a value from the `Roomod.Languages.Language` enum and `path` is a string containing the path to the localization file. This path starts at `[game directory]\BepInEx\plugins`, so all localization files must be placed in that directory or a subdirectory.

For example, if I made a localization file for English, named it `English.txt`, and placed it directly in the plugins directory, I would install it like this:

    Roomod.CreateCustomLocalization(Roomod.Languages.Language.English, "English.txt")

Note that you will likely have to directly reference `Roomod.Languages`, since the games' assemblies also contain a `Languages` type and that will take priority over the one from Roomod.
