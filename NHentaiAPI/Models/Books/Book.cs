using System.Text.Json.Serialization;
using NHentaiAPI.JsonConverters;

namespace NHentaiAPI.Models.Books;

/// <summary>
///     Represents a complete book with all its metadata
/// </summary>
public class Book
{
    /// <summary>
    ///     Gets or sets the unique identifier for the book
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the media identifier for the book
    /// </summary>
    [JsonPropertyName("media_id")]
    public int MediaId { get; set; }

    /// <summary>
    ///     Gets or sets the titles of the book in different languages
    /// </summary>
    [JsonPropertyName("title")]
    public Title Title { get; set; }

    /// <summary>
    ///     Gets or sets the collection of images associated with the book
    /// </summary>
    [JsonPropertyName("images")]
    public Images Images { get; set; }

    /// <summary>
    ///     Gets or sets the scanlator/translator of the book
    /// </summary>
    [JsonPropertyName("scanlator")]
    public string Scanlator { get; set; }

    /// <summary>
    ///     Gets or sets the date when the book was uploaded
    /// </summary>
    [JsonPropertyName("upload_date")]
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime UploadDate { get; set; }

    /// <summary>
    ///     Gets or sets the list of tags associated with the book
    /// </summary>
    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; }

    /// <summary>
    ///     Gets or sets the total number of pages in the book
    /// </summary>
    [JsonPropertyName("num_pages")]
    public int NumPages { get; set; }

    /// <summary>
    ///     Gets or sets the number of times the book has been favorited
    /// </summary>
    [JsonPropertyName("num_favorites")]
    public int NumFavorites { get; set; }
}