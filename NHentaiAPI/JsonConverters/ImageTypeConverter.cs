using System.Text.Json;
using System.Text.Json.Serialization;
using NHentaiAPI.Models.Books;

namespace NHentaiAPI.JsonConverters;

/// <summary>
///     Converts between single-character string representations and ImageType enum values
/// </summary>
public class ImageTypeConverter : JsonConverter<ImageType>
{
    /// <summary>
    ///     Reads a JSON string value and converts it to an ImageType enum value
    /// </summary>
    /// <param name="reader">The Utf8JsonReader to read from</param>
    /// <param name="typeToConvert">The type to convert to</param>
    /// <param name="options">The JsonSerializerOptions to use</param>
    /// <returns>An ImageType enum value corresponding to the JSON string</returns>
    public override ImageType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value?.ToLower() switch
        {
            "j" => ImageType.Jpg,
            "p" => ImageType.Png,
            "g" => ImageType.Gif,
            _ => ImageType.Jpg // Default to JPG if unknown
        };
    }

    /// <summary>
    ///     Writes an ImageType enum value as a single-character JSON string
    /// </summary>
    /// <param name="writer">The JsonWriter to write to</param>
    /// <param name="value">The ImageType value to convert</param>
    /// <param name="options">The JsonSerializerOptions to use</param>
    public override void Write(Utf8JsonWriter writer, ImageType value, JsonSerializerOptions options)
    {
        var stringValue = value switch
        {
            ImageType.Jpg => "j",
            ImageType.Png => "p",
            ImageType.Gif => "g",
            _ => "j"
        };
        writer.WriteStringValue(stringValue);
    }
}