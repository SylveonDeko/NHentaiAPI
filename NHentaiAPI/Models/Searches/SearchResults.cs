using System.Text.Json.Serialization;
using NHentaiAPI.Models.Books;

namespace NHentaiAPI.Models.Searches;

/// <summary>
///     Represents search results containing a list of books and pagination information
/// </summary>
public class SearchResults
{
    /// <summary>
    ///     Gets or sets the list of books returned in the search results
    /// </summary>
    [JsonPropertyName("result")]
    public List<Book> Result { get; set; }

    /// <summary>
    ///     Gets or sets the total number of pages in the search results
    /// </summary>
    [JsonPropertyName("num_pages")]
    public int NumPages { get; set; }

    /// <summary>
    ///     Gets or sets the number of items per page
    /// </summary>
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
}