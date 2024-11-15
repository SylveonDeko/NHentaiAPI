using System.Text.Json.Serialization;
using NHentaiAPI.Models.Books;

namespace NHentaiAPI.Models.Recommends;

/// <summary>
///     Represents a list of recommended books
/// </summary>
public class BookRecommend
{
    /// <summary>
    ///     Gets or sets the list of recommended books
    /// </summary>
    [JsonPropertyName("result")]
    public List<Book> Result { get; set; }
}