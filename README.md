# Deli.ExampleMod
This is an example mod for [Deli](https://github.com/Deli-Collective/Deli), a mod loading framework.
This example demonstrates the basic usage of the mod manifest, entrypoint, asset readers and asset loaders.

You'll find that creating plugins in this format is extremely similar to [BepInEx](https://github.com/BepInEx/BepInEx). A similar loading system is used, and Deli mods are
passed BepInEx objects such as a config and log sources. If you're porting a BepInEx plugin to Deli, it shouldn't require many logical changes.

Once you're ready to test your mod, simply add all your assets into a directory and edit the manifest to include them. Before releasing,
zip the contents of your directory and rename the zip file from `.zip` to `.deli`. For more details, see [the
documentation](https://deli-collective.github.io/Deli/articles/preparing/index.html).
