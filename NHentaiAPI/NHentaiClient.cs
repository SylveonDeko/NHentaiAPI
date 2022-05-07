using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHentaiAPI.Models.Books;
using NHentaiAPI.Models.Recommends;
using NHentaiAPI.Models.Searches;

namespace NHentaiAPI
{
    /// <summary>
    /// n-Hentai Client
    /// copied from : https://github.com/NHMoeDev/NHentai-android/blob/master/app/src/main/kotlin/moe/feng/nhentai/api/ApiConstants.kt
    /// </summary>
    public class NHentaiClient : IDisposable
    {
        #region Client

        private readonly HttpClient _client = new HttpClient();

        #endregion

        #region Urls

        private static string ApiRootUrl => "https://nhentai.net";

        private static string ImageRootUrl => "https://i.nhentai.net";

        private static string ThumbnailRootUrl => "https://t.nhentai.net";

        #endregion

        #region Data urls

        private static string GetHomePageUrl(int pageNum)
            => $"{ApiRootUrl}/api/galleries/all?page={pageNum}";

        private static string GetSearchUrl(string content, int pageNum)
            => $"{ApiRootUrl}/api/galleries/search?" +
               $"query={content.Replace(" ", "+")}&" +
               $"page={pageNum}";

        private static string GetTagUrl(Tag tag, bool isPopularList, int pageNum)
            => $"{ApiRootUrl}/api/galleries/tagged?" +
               $"tag_id={tag.Id}" +
               $"&page={pageNum}" +
               (isPopularList ? "&sort=popular" : "");

        private static string GetBookDetailsUrl(int bookId)
            => $"{ApiRootUrl}/api/gallery/{bookId}";

        private static string GetBookRecommendUrl(int bookId)
            => $"{ApiRootUrl}/api/gallery/{bookId}/related";

        private static string GetGalleryUrl(int galleryId)
            => $"{ImageRootUrl}/galleries/{galleryId}";

        private static string GetThumbGalleryUrl(int galleryId)
            => $"{ThumbnailRootUrl}/galleries/{galleryId}";

        #endregion

        #region Picture urls

        public string GetPictureUrl(Book book, int pageNum)
        {
            var image = GetImage(book, pageNum-1);
            var fileType = ConvertType(image.Type);
            return GetPictureUrl(book.MediaId, pageNum, fileType);
        }

        public string GetThumbPictureUrl(Book book, int pageNum)
        {
            var image = GetImage(book, pageNum);
            var fileType = ConvertType(image.Type);
            return GetThumbPictureUrl(book.MediaId, pageNum, fileType);
        }

        public string GetBigCoverUrl(Book book)
            => GetBigCoverUrl(book.MediaId);

        public string GetOriginPictureUrl(Book book, int pageNum)
            => GetOriginPictureUrl(book.MediaId, pageNum);

        public string GetBookThumbUrl(Book book)
        {
            var fileType = ConvertType(book.Images.Cover.Type);
            return GetBookThumbUrl(book.MediaId, fileType);
        }

        private static string GetPictureUrl(int galleryId, int pageNum, string fileType)
            => $"{GetGalleryUrl(galleryId)}/{pageNum}.{fileType}";

        private static string GetThumbPictureUrl(int galleryId, int pageNum, string fileType)
            => $"{GetThumbGalleryUrl(galleryId)}/{pageNum}t.{fileType}";

        private static string GetBigCoverUrl(int galleryId)
            => $"{GetThumbGalleryUrl(galleryId)}/cover.jpg";

        private static string GetOriginPictureUrl(int galleryId, int pageNum)
            => GetPictureUrl(galleryId, pageNum, "jpg");

        private static string GetBookThumbUrl(int galleryId, string fileType = "jpg")
            => $"{GetThumbGalleryUrl(galleryId)}/thumb.{fileType ?? "jpg"}";

        #endregion

        #region Utilities

        private async Task<TOutput> GetData<TOutput>(string rootUrl)
        {
            var json = await _client.GetStringAsync(rootUrl);
            return JsonConvert.DeserializeObject<TOutput>(json);
        }

        private async Task<byte[]> GetByteData(string rootUrl)
        {
            var data = await _client.GetByteArrayAsync(rootUrl);
            return data;
        }

        private static Image GetImage(Book book, int pageNum)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            
            var page = book.Images.Pages[pageNum];
            return page;
        }

        private static string ConvertType(ImageType type)
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

        public Task<SearchResults> GetHomePageListAsync(int pageNum)
        {
            var url = GetHomePageUrl(pageNum);
            return GetData<SearchResults>(url);
        }

        public Task<SearchResults> GetSearchPageListAsync(string keyword, int pageNum)
        {
            var url = GetSearchUrl(keyword, pageNum);
            return GetData<SearchResults>(url);
        }

        public Task<SearchResults> GetTagPageListAsync(Tag tag, SortBy sortBy, int pageNum)
        {
            var url = GetTagUrl(tag, sortBy == SortBy.Popular, pageNum);
            return GetData<SearchResults>(url);
        }

        #endregion

        #region Books

        public async Task<Book> GetBookAsync(int bookId)
        {
            var url = GetBookDetailsUrl(bookId);
            return await GetData<Book>(url);
        }

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

        public Task<byte[]> GetPictureAsync(Book book, int pageNum)
        {
            var url = GetPictureUrl(book, pageNum);
            return GetByteData(url);
        }

        public Task<byte[]> GetThumbPictureAsync(Book book, int pageNum)
        {
            var url = GetThumbPictureUrl(book, pageNum);
            return GetByteData(url);
        }

        public Task<byte[]> GetBigCoverPictureAsync(Book book)
        {
            var url = GetBigCoverUrl(book.MediaId);
            return GetByteData(url);
        }

        public Task<byte[]> GetOriginPictureAsync(Book book, int pageNum)
        {
            var url = GetOriginPictureUrl(book.MediaId, pageNum);
            return GetByteData(url);
        }

        public Task<byte[]> GetBookThumbPictureAsync(Book book)
        {
            var url = GetBookThumbUrl(book);
            return GetByteData(url);
        }

        #endregion

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}