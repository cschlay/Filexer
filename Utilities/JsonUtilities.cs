using System.IO;
using System.Text.Json;

namespace Filexer.Utilities
{
    /// <summary>
    /// Simple abstractions over the built-in serialization.
    /// </summary>
    public static class JsonUtilities
    {
        /// <summary>Reads data from JSON file into a object.</summary>
        /// <param name="path">the location of JSON file</param>
        /// <typeparam name="T">the type to deserialize</typeparam>
        /// <returns>An instance of object</returns>
        public static T ReadFromJson<T>(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            var reader = new Utf8JsonReader(data);
            var result = JsonSerializer.Deserialize<T>(ref reader, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            if (result == null)
            {
                throw new JsonException($"File {path} could not be deserialized into an object.");
            }

            return result;
        }
    }
}
