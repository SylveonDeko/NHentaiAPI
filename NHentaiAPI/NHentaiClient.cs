using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using NHentaiAPI.Models.Books;
using NHentaiAPI.Models.Recommends;
using NHentaiAPI.Models.Searches;

namespace NHentaiAPI;

/// <summary>
///     Client for interacting with the n-Hentai API
/// </summary>
public class NHentaiClient : IDisposable
{
    /// <summary>
    ///     Releases the resources used by the HTTP client
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    #region Client

    private readonly HttpClient _client;
    private readonly Random _rand;

    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    /// <summary>
    ///     Initializes a new instance of the NHentaiClient
    /// </summary>
    /// <param name="userAgent">User agent string to use for requests</param>
    /// <param name="cookies">Optional dictionary of cookies to include with requests</param>
    public NHentaiClient(string userAgent, Dictionary<string, string>? cookies = null)
    {
        _rand = new Random();
        var cookies1 = new CookieContainer();
        var handler = new HttpClientHandler { CookieContainer = cookies1 };
        _client = new HttpClient(handler);

        _client.DefaultRequestHeaders.Add("User-Agent", userAgent);

        if (cookies == null) return;
        var cookieUri = new Uri(ApiRootUrl);

        foreach (var cookie in cookies) cookies1.Add(cookieUri, new Cookie(cookie.Key, cookie.Value));
    }

    #endregion

    #region Urls

    /// <summary>
    ///     Gets the base API endpoint URL
    /// </summary>
    private string ApiRootUrl => "https://nhentai.net";

    /// <summary>
    ///     Gets the base URL for full-resolution images
    /// </summary>
    private string ImageRootUrl()
    {
        var rand = _rand.Next(1, 4);
        return $"https://i{rand}.nhentai.net";
    }

    /// <summary>
    ///     Gets the base URL for thumbnail images
    /// </summary>
    private string ThumbnailRootUrl()
    {
        var rand = _rand.Next(1, 4);
        return $"https://t{rand}.nhentai.net";
    }

    #endregion

    #region Data urls

    /// <summary>
    ///     Constructs the URL for retrieving the homepage content
    /// </summary>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>URL for the specified homepage</returns>
    private string GetHomePageUrl(int pageNum)
    {
        return $"{ApiRootUrl}/api/galleries/all?page={pageNum}";
    }

    /// <summary>
    ///     Constructs the URL for performing a search
    /// </summary>
    /// <param name="content">Search query text</param>
    /// <param name="pageNum">Page number of results</param>
    /// <returns>URL for the search with specified parameters</returns>
    private string GetSearchUrl(string content, int pageNum)
    {
        return $"{ApiRootUrl}/api/galleries/search?" +
               $"query={content.Replace(" ", "+")}&" +
               $"page={pageNum}";
    }

    /// <summary>
    ///     Constructs the URL for retrieving content with a specific tag
    /// </summary>
    /// <param name="tag">Tag to filter by</param>
    /// <param name="isPopularList">Whether to sort by popularity</param>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>URL for the tag search with specified parameters</returns>
    private string GetTagUrl(Tag tag, bool isPopularList, int pageNum)
    {
        return $"{ApiRootUrl}/api/galleries/tagged?" +
               $"tag_id={tag.Id}" +
               $"&page={pageNum}" +
               (isPopularList ? "&sort=popular" : "");
    }

    /// <summary>
    ///     Constructs the URL for retrieving book details
    /// </summary>
    /// <param name="bookId">ID of the book</param>
    /// <returns>URL for the specified book's details</returns>
    private string GetBookDetailsUrl(int bookId)
    {
        return $"{ApiRootUrl}/api/gallery/{bookId}";
    }

    /// <summary>
    ///     Constructs the URL for retrieving book recommendations
    /// </summary>
    /// <param name="bookId">ID of the book to get recommendations for</param>
    /// <returns>URL for recommendations related to the specified book</returns>
    private string GetBookRecommendUrl(int bookId)
    {
        return $"{ApiRootUrl}/api/gallery/{bookId}/related";
    }

    /// <summary>
    ///     Constructs the URL for accessing a gallery's images
    /// </summary>
    /// <param name="galleryId">ID of the gallery</param>
    /// <returns>Base URL for the gallery's full-size images</returns>
    private string GetGalleryUrl(int galleryId)
    {
        return $"{ImageRootUrl()}/galleries/{galleryId}";
    }

    /// <summary>
    ///     Constructs the URL for accessing a gallery's thumbnail images
    /// </summary>
    /// <param name="galleryId">ID of the gallery</param>
    /// <returns>Base URL for the gallery's thumbnail images</returns>
    private string GetThumbGalleryUrl(int galleryId)
    {
        return $"{ThumbnailRootUrl()}/galleries/{galleryId}";
    }

    #endregion

    #region Picture urls

    /// <summary>
    ///     Gets the URL for a specific page image in a book
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>Full URL for the specified page image</returns>
    public string GetPictureUrl(Book book, int pageNum)
    {
        var image = GetImage(book, pageNum);
        var fileType = ConvertType(image.Type);
        return GetPictureUrl(book.MediaId, pageNum, fileType);
    }

    /// <summary>
    ///     Gets the URL for a thumbnail of a specific page in a book
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>Full URL for the specified page's thumbnail</returns>
    public string GetThumbPictureUrl(Book book, int pageNum)
    {
        var image = GetImage(book, pageNum);
        var fileType = ConvertType(image.Type);
        return GetThumbPictureUrl(book.MediaId, pageNum, fileType);
    }

    /// <summary>
    ///     Gets the URL for a book's large cover image
    /// </summary>
    /// <param name="book">Book to get the cover for</param>
    /// <returns>Full URL for the book's cover image</returns>
    public string GetBigCoverUrl(Book book)
    {
        return GetBigCoverUrl(book.MediaId);
    }

    /// <summary>
    ///     Gets the URL for the original full-size version of a page
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>Full URL for the original version of the specified page</returns>
    public string GetOriginPictureUrl(Book book, int pageNum)
    {
        return GetOriginPictureUrl(book.MediaId, pageNum);
    }

    /// <summary>
    ///     Gets the URL for a book's thumbnail image
    /// </summary>
    /// <param name="book">Book to get the thumbnail for</param>
    /// <returns>Full URL for the book's thumbnail image</returns>
    public string GetBookThumbUrl(Book book)
    {
        var fileType = ConvertType(book.Images.Cover.Type);
        return GetBookThumbUrl(book.MediaId, fileType);
    }

    /// <summary>
    ///     Constructs the URL for a full-size page image
    /// </summary>
    /// <param name="galleryId">ID of the gallery containing the image</param>
    /// <param name="pageNum">Page number of the image</param>
    /// <param name="fileType">File extension of the image</param>
    /// <returns>Full URL for the specified page image</returns>
    private string GetPictureUrl(int galleryId, int pageNum, string fileType)
    {
        return $"{GetGalleryUrl(galleryId)}/{pageNum}.{fileType}";
    }

    /// <summary>
    ///     Constructs the URL for a page's thumbnail image
    /// </summary>
    /// <param name="galleryId">ID of the gallery containing the image</param>
    /// <param name="pageNum">Page number of the image</param>
    /// <param name="fileType">File extension of the image</param>
    /// <returns>Full URL for the specified page's thumbnail</returns>
    private string GetThumbPictureUrl(int galleryId, int pageNum, string fileType)
    {
        return $"{GetThumbGalleryUrl(galleryId)}/{pageNum}t.{fileType}";
    }

    /// <summary>
    ///     Constructs the URL for a gallery's cover image
    /// </summary>
    /// <param name="galleryId">ID of the gallery</param>
    /// <returns>Full URL for the gallery's cover image</returns>
    private string GetBigCoverUrl(int galleryId)
    {
        return $"{GetThumbGalleryUrl(galleryId)}/cover.jpg";
    }

    /// <summary>
    ///     Constructs the URL for a page's original full-size image
    /// </summary>
    /// <param name="galleryId">ID of the gallery containing the image</param>
    /// <param name="pageNum">Page number of the image</param>
    /// <returns>Full URL for the original version of the specified page</returns>
    private string GetOriginPictureUrl(int galleryId, int pageNum)
    {
        return GetPictureUrl(galleryId, pageNum, "jpg");
    }

    /// <summary>
    ///     Constructs the URL for a gallery's thumbnail image
    /// </summary>
    /// <param name="galleryId">ID of the gallery</param>
    /// <param name="fileType">File extension of the image, defaults to jpg</param>
    /// <returns>Full URL for the gallery's thumbnail image</returns>
    private string GetBookThumbUrl(int galleryId, string fileType = "jpg")
    {
        return $"{GetThumbGalleryUrl(galleryId)}/thumb.{fileType ?? "jpg"}";
    }

    #endregion

    #region Utilities

    /// <summary>
    ///     Retrieves and deserializes JSON data from the specified URL
    /// </summary>
    /// <typeparam name="TOutput">Type to deserialize the JSON into</typeparam>
    /// <param name="rootUrl">URL to retrieve the JSON from</param>
    /// <returns>Deserialized object of type TOutput</returns>
    private async Task<TOutput> GetData<TOutput>(string rootUrl)
    {
        var json = await _client.GetStreamAsync(rootUrl);
        return await JsonSerializer.DeserializeAsync<TOutput>(json, _options);
    }

    /// <summary>
    ///     Downloads binary data from the specified URL
    /// </summary>
    /// <param name="rootUrl">URL to download from</param>
    /// <returns>Byte array containing the downloaded data</returns>
    private async Task<byte[]> GetByteData(string rootUrl)
    {
        var data = await _client.GetByteArrayAsync(rootUrl);
        return data;
    }

    /// <summary>
    ///     Gets the image information for a specific page in a book
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to retrieve (1-based index)</param>
    /// <returns>Image information for the specified page</returns>
    /// <exception cref="ArgumentNullException">Thrown when book is null</exception>
    private Image GetImage(Book book, int pageNum)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        var page = book.Images.Pages[pageNum - 1];
        return page;
    }

    /// <summary>
    ///     Converts an ImageType enum value to its corresponding file extension
    /// </summary>
    /// <param name="type">ImageType to convert</param>
    /// <returns>File extension string (without dot)</returns>
    /// <exception cref="NotSupportedException">Thrown when the image type is not supported</exception>
    private string ConvertType(ImageType type)
    {
        switch (type)
        {
            case ImageType.Gif:
                return "gif";
            case ImageType.Jpg:
                return "jpg";
            case ImageType.Png:
                return "png";
            default:
                throw new NotSupportedException($"Format {nameof(type)}  does not support.");
        }
    }

    #endregion

    #region Search

    /// <summary>
    ///     Retrieves a list of content from the homepage
    /// </summary>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>Search results containing content from the specified page</returns>
    public async Task<SearchResults> GetHomePageListAsync(int pageNum)
    {
        var url = GetHomePageUrl(pageNum);
        return await GetData<SearchResults>(url);
    }

    /// <summary>
    ///     Searches for content using the specified keyword
    /// </summary>
    /// <param name="keyword">Search term to look for</param>
    /// <param name="pageNum">Page number of results to retrieve</param>
    /// <returns>Search results matching the keyword</returns>
    public async Task<SearchResults> GetSearchPageListAsync(string keyword, int pageNum)
    {
        var url = GetSearchUrl(keyword, pageNum);
        return await GetData<SearchResults>(url);
    }

    /// <summary>
    ///     Retrieves content tagged with a specific tag
    /// </summary>
    /// <param name="tag">Tag to filter by</param>
    /// <param name="sortBy">How to sort the results</param>
    /// <param name="pageNum">Page number to retrieve</param>
    /// <returns>Search results for content with the specified tag</returns>
    public async Task<SearchResults> GetTagPageListAsync(Tag tag, SortBy sortBy, int pageNum)
    {
        var url = GetTagUrl(tag, sortBy == SortBy.Popular, pageNum);
        return await GetData<SearchResults>(url);
    }

    #endregion

    #region Books

    /// <summary>
    ///     Retrieves details for a specific book
    /// </summary>
    /// <param name="bookId">ID of the book to retrieve</param>
    /// <returns>Book details for the specified ID</returns>
    public async Task<Book> GetBookAsync(int bookId)
    {
        var url = GetBookDetailsUrl(bookId);
        return await GetData<Book>(url);
    }

    /// <summary>
    ///     Gets recommendations based on a specific book
    /// </summary>
    /// <param name="bookId">ID of the book to get recommendations for</param>
    /// <returns>List of recommended books</returns>
    public async Task<BookRecommend> GetBookRecommendAsync(int bookId)
    {
        var url = GetBookRecommendUrl(bookId);
        var book = await GetData<Book>(url);
        return new BookRecommend
        {
            Result = new List<Book> { book }
        };
    }

    #endregion

    #region Picture

    /// <summary>
    ///     Downloads a full-size page image
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to download</param>
    /// <returns>Byte array containing the image data</returns>
    public async Task<byte[]> GetPictureAsync(Book book, int pageNum)
    {
        var url = GetPictureUrl(book, pageNum);
        return await GetByteData(url);
    }

    /// <summary>
    ///     Downloads a thumbnail image for a page
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to download</param>
    /// <returns>Byte array containing the thumbnail data</returns>
    public async Task<byte[]> GetThumbPictureAsync(Book book, int pageNum)
    {
        var url = GetThumbPictureUrl(book, pageNum);
        return await GetByteData(url);
    }

    /// <summary>
    ///     Downloads a book's large cover image
    /// </summary>
    /// <param name="book">Book to get the cover for</param>
    /// <returns>Byte array containing the cover image data</returns>
    public async Task<byte[]> GetBigCoverPictureAsync(Book book)
    {
        var url = GetBigCoverUrl(book.MediaId);
        return await GetByteData(url);
    }

    /// <summary>
    ///     Downloads an original full-size page image
    /// </summary>
    /// <param name="book">Book containing the image</param>
    /// <param name="pageNum">Page number to download</param>
    /// <returns>Byte array containing the original image data</returns>
    public async Task<byte[]> GetOriginPictureAsync(Book book, int pageNum)
    {
        var url = GetOriginPictureUrl(book.MediaId, pageNum);
        return await GetByteData(url);
    }

    /// <summary>
    ///     Downloads a book's thumbnail image
    /// </summary>
    /// <param name="book">Book to get the thumbnail for</param>
    /// <returns>Byte array containing the thumbnail data</returns>
    public async Task<byte[]> GetBookThumbPictureAsync(Book book)
    {
        var url = GetBookThumbUrl(book);
        return await GetByteData(url);
    }

    #endregion
}