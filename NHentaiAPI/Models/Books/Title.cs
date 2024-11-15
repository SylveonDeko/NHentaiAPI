using System.Text.Json.Serialization;

namespace NHentaiAPI.Models.Books;

/// <summary>
///     Represents a book's title in different languages
/// </summary>
public class Title
{
    /// <summary>
    ///     Gets or sets the English title
    /// </summary>
    [JsonPropertyName("english")]
    public string English { get; set; }

    /// <summary>
    ///     Gets or sets the Japanese title
    /// </summary>
    [JsonPropertyName("japanese")]
    public string Japanese { get; set; }

    /// <summary>
    ///     Gets or sets the formatted/pretty title
    /// </summary>
    [JsonPropertyName("pretty")]
    public string Pretty { get; set; }
}