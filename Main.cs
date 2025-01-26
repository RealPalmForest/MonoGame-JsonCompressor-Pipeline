namespace JsonCompressor
{
    namespace JsonCompressor
    {
        using Microsoft.Xna.Framework.Content.Pipeline;
        using System.IO;
        using System.IO.Compression;
        using System;

        [ContentImporter(".json", DisplayName = "Json Importer - PalmForest", DefaultProcessor = "JsonCompressorProcessor")]
        public class JsonImporter : ContentImporter<string>
        {
            public override string Import(string filename, ContentImporterContext context)
            {
                // Read the raw JSON file as a string
                return File.ReadAllText(filename);
            }
        }

        [ContentProcessor(DisplayName = "Json Compressor - PalmForest")]
        public class JsonCompressorProcessor : ContentProcessor<string, string>
        {
            public override string Process(string input, ContentProcessorContext context)
            {
                // Log the process for debugging
                context.Logger.LogMessage("Compressing and obfuscating JSON content...");

                // Compress the JSON using Brotli
                byte[] compressedData;
                using (var memoryStream = new MemoryStream())
                {
                    using (var brotliStream = new BrotliStream(memoryStream, CompressionLevel.Optimal, leaveOpen: true))
                    {
                        using (var writer = new StreamWriter(brotliStream))
                        {
                            writer.Write(input);
                        }
                    }
                    compressedData = memoryStream.ToArray();
                }

                // Encode the compressed data in Base64
                var obfuscated = Convert.ToBase64String(compressedData);

                return obfuscated;
            }
        }
    }

}
