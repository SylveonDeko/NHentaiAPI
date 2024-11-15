using System.Text.Json;
using System.Text.Json.Serialization;

namespace NHentaiAPI.JsonConverters;

/// <summary>
///     Converts between Unix timestamps and DateTime values during JSON serialization/deserialization
/// </summary>
public class UnixTimestampConverter : JsonConverter<DateTime>
{
    /// <summary>
    ///     Converts a Unix timestamp from JSON to a DateTime value
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <param name="typeToConvert">The type to convert to (DateTime)</param>
    /// <param name="options">Serialization options</param>
    /// <returns>A DateTime converted from the Unix timestamp</returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the Unix timestamp (seconds since epoch)
        var unixTime = reader.GetInt64();

        // Convert Unix timestamp to DateTime
        // Unix epoch starts from January 1, 1970
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }

    /// <summary>
    ///     Converts a DateTime value to a Unix timestamp for JSON
    /// </summary>
    /// <param name="writer">The JSON writer</param>
    /// <param name="value">The DateTime value to convert</param>
    /// <param name="options">Serialization options</param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Convert DateTime to Unix timestamp
        var unixTime = new DateTimeOffset(value).ToUnixTimeSeconds();
        writer.WriteNumberValue(unixTime);
    }
}