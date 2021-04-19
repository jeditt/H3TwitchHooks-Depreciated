using System;
using Deli.Immediate;
using Deli.Setup;
using Deli.VFS;
using Deli.VFS.Disk;
using UnityEngine.SceneManagement;

namespace Deli.ExampleMod
{
    // DeliBehaviours are just MonoBehaviours that get added to a global game object when the game first starts.
    public class ExampleMod : DeliBehaviour
    {
        private ImmediateTypedFileHandle<string>? _textResource;
        
        // All Deli properties can be accessed here, but don't use Unity's API until Awake.
        public ExampleMod()
        {
            // There is 1 log message in each of the methods here.
            // Run this mod to see the order of execution logged to console.
            Logger.LogInfo("I am being constructed!");

            // Hook to the setup stage (the first stage we can access)
            // Do not forget! Awake is still ran before ANY event.
            Stages.Setup += OnSetup;

            // Check if the mod is on disk (as opposed to a zip file)
            if (Resources is IDiskHandle resourcesOnDisk)
            {
                Logger.LogInfo($"The mod is on disk at: '{resourcesOnDisk.PathOnDisk}'");
                
                // Every time the scene changes, reload our resources to see if they changed.
                SceneManager.activeSceneChanged += (_, _) => resourcesOnDisk.Refresh();
            }
        }

        // Now you can use Unity's API
        private void Awake()
        {
            Logger.LogInfo("I am awakening!");
            
            ShowConfigs();

            // TODO: Put the rest of your Unity startup code here
        }
        
        // The config system is identical to BepInEx, but here's a demo if you are unfamiliar.
        // They are good with simple types (primitives and small structs), but for larger sets of data you may want to
        // use an asset loader of some sort.
        private void ShowConfigs()
        {
            // Bind configs to create a config entry, and an object that represents the entry
            var number = Config.Bind("Favorites", "Number", 2, "This number is printed to the console.");
            var word = Config.Bind("Favorites", "Word", "deli", "This word is printed to the console.");
            
            // With the entry, you can read, refresh, even write new values
            Logger.LogInfo($"Your favorite number: {number.Value}");
            Logger.LogInfo($"Your favorite word: {word.Value}");
        }
        
        // And now you can access much more of Deli
        private void OnSetup(SetupStage stage)
        {
            Logger.LogInfo("I am operating on the setup stage!");
            
            // Our asset loaders aren't static, so we need to construct an instance before we access them.
            var loaders = new AssetLoaders(Logger);
            
            // Adds the reader defined in AssetReaders.cs
            stage.ImmediateReaders.Add(AssetReaders.ExampleAssetOf);
            // Adds the loader as our mod, with the name "example_asset", for the setup stage only.
            // Mods (including our own) can use this loader via "deli.example_mod:example_asset".
            stage.SetupAssetLoaders[Source, "example_asset"] = loaders.LoadExampleAsset;
            
            ConstructTextResource(stage);
            
            // TODO: Put anything else that needs to be loaded from/into Deli here 
        }

        private void ConstructTextResource(SetupStage stage)
        {
            // Get the reader responsible for strings
            var reader = stage.ImmediateReaders.Get<string>();
            
            // Get the resource file that we want to read.
            // If not found, this will be null. Be careful (and use C# 8.0+ if possible), or you might
            // get a NullReferenceException!
            var file = Resources.GetDirectory("res")?.GetFile("ExampleTextResource.txt");
            if (file is null)
            {
                throw new InvalidOperationException("The resource file was not found!");
            }

            _textResource = new ImmediateTypedFileHandle<string>(file, reader);
            _textResource.Updated += () => Logger.LogInfo($"Text resource updated: '{_textResource.GetOrRead()}'");
        }

        private void Start()
        {
            Logger.LogInfo("I am starting!");

            // Rather than get a NullReferenceException, throw a descriptive exception if it is null
            if (_textResource is null)
            {
                throw new InvalidOperationException("The text resource was still null in start! This " +
                                                    "shouldn't happen unless the code has been editted elsewhere.");
            }
            
            Logger.LogInfo($"Initial text resource: '{_textResource.GetOrRead()}'");
        }
    }
}