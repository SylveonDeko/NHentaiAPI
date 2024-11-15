using System.Text.Json.Serialization;

namespace NHentaiAPI.Models.Books;

/// <summary>
///     Contains all images associated with a book
/// </summary>
public class Images
{
    /// <summary>
    ///     Gets or sets the list of page images
    /// </summary>
    [JsonPropertyName("pages")]
    public List<Image> Pages { get; set; }

    /// <summary>
    ///     Gets or sets the cover image
    /// </summary>
    [JsonPropertyName("cover")]
    public Image Cover { get; set; }

    /// <summary>
    ///     Gets or sets the thumbnail image
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public Image Thumbnail { get; set; }
}