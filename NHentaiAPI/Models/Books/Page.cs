using System.Text.Json.Serialization;

namespace NHentaiAPI.Models.Books;

/// <summary>
///     Represents a page's basic information
/// </summary>
public class Page
{
    /// <summary>
    ///     Gets or sets the type identifier of the page
    /// </summary>
    [JsonPropertyName("t")]
    public string T { get; set; }

    /// <summary>
    ///     Gets or sets the width of the page image
    /// </summary>
    [JsonPropertyName("w")]
    public int W { get; set; }

    /// <summary>
    ///     Gets or sets the height of the page image
    /// </summary>
    [JsonPropertyName("h")]
    public int H { get; set; }
}