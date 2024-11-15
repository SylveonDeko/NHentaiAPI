using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NHentaiAPI.JsonConverters;

namespace NHentaiAPI.Models.Books;

/// <summary>
///     Represents an image's metadata
/// </summary>
public class Image
{
    /// <summary>
    ///     Gets or sets the type/format of the image
    /// </summary>
    [JsonPropertyName("t")]
    [JsonConverter(typeof(ImageTypeConverter))]
    public ImageType Type { get; set; }

    /// <summary>
    ///     Gets or sets the width of the image in pixels
    /// </summary>
    [JsonPropertyName("w")]
    public int Width { get; set; }

    /// <summary>
    ///     Gets or sets the height of the image in pixels
    /// </summary>
    [JsonPropertyName("h")]
    public int Height { get; set; }
}

/// <summary>
///     Defines the supported image formats
/// </summary>
public enum ImageType
{
    /// <summary>
    ///     JPEG image format
    /// </summary>
    [EnumMember(Value = "j")] Jpg,

    /// <summary>
    ///     PNG image format
    /// </summary>
    [EnumMember(Value = "p")] Png,

    /// <summary>
    ///     GIF image format
    /// </summary>
    [EnumMember(Value = "g")] Gif
}