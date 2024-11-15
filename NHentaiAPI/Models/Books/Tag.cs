using System.Text.Json.Serialization;

namespace NHentaiAPI.Models.Books;

/// <summary>
///     Represents a tag used to categorize books
/// </summary>
public class Tag
{
    /// <summary>
    ///     Gets or sets the unique identifier for the tag
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the type of the tag
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Gets or sets the name of the tag
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the URL for the tag's page
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    ///     Gets or sets the number of books with this tag
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}