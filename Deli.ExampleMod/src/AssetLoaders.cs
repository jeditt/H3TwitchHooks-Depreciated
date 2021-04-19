using System;
using BepInEx.Logging;
using Deli.Setup;
using Deli.VFS;

namespace Deli.ExampleMod
{
    // Asset loaders are methods that assets can declare themselves to be loaded into
    // In this case, we'll implement a loader for our example asset.
    public class AssetLoaders
    {
        private readonly ManualLogSource _logger;

        public AssetLoaders(ManualLogSource logger)
        {
            _logger = logger;
        }

        public void LoadExampleAsset(SetupStage stage, Mod mod, IHandle handle)
        {
            // The handle could be a directory or file, but we only want file.
            if (handle is not IFileHandle file)
            {
                throw new ArgumentException("The example loader needs a file, not directory.", nameof(handle));
            }
            
            // Get the reader we defined in AssetReaders.cs and registered in ExampleMod.cs
            var assetReader = stage.ImmediateReaders.Get<ExampleAsset>();
            
            // Run the reader
            var asset = assetReader(file);

            // Log the asset's message to our mod's logger
            _logger.LogInfo($"Read message from {mod} on line {asset.Line}: '{asset.Message}'");
        }
    }
}