namespace JsonCompressor
{
    namespace JsonCompressor
    {
        using Microsoft.Xna.Framework.Content.Pipeline;
        using System.IO;
        using System.IO.Compression;
        using System;
        using System.Text;

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

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] compressedData;

                // Compress using Deflate compression
                using (var memoryStream = new MemoryStream())
                {
                    using (var compressionStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
                    {
                        compressionStream.Write(inputBytes, 0, inputBytes.Length);
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
