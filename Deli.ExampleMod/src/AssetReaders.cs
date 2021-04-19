using System.IO;
using Deli.VFS;

namespace Deli.ExampleMod
{
    // Asset readers are methods that read a file as a certain type.
    // Most of the time they should be static, as they should be predictable. Leave mutation to asset loaders.
    public static class AssetReaders
    {
        // In this case, we make an asset reader for our ExampleAsset.
        public static ExampleAsset ExampleAssetOf(IFileHandle file)
        {
            // Open a raw stream to the file
            using var raw = file.OpenRead();
            // Wrap it in a StreamReader, so we can read text
            using var text = new StreamReader(raw);

            // Iterate through each line in the file
            string? line;
            var lineNumber = 0;
            while ((line = text.ReadLine()) != null)
            {
                const string selector = "> ";
                
                ++lineNumber;

                // Ignore all lines without the selector
                if (!line.StartsWith(selector))
                {
                    continue;
                }

                // Trim off the selector
                var selected = line.Substring(selector.Length);

                // Return the trimmed line
                return new ExampleAsset
                {
                    Line = lineNumber,
                    Message = selected
                };
            }

            // Return an empty asset as a last resort
            return new ExampleAsset
            {
                Line = 0,
                Message = string.Empty
            };
        }
    }
}