using System;
using System.Collections.Generic;
using System.Net;
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

        private readonly HttpClient _client;

        public NHentaiClient(string userAgent, Dictionary<string, string> cookies = null)
        {
            var cookies1 = new CookieContainer();
            var handler = new HttpClientHandler { CookieContainer = cookies1 };
            _client = new HttpClient(handler);

            _client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            if (cookies == null) return;
            var cookieUri = new Uri(ApiRootUrl);
                
            foreach (var cookie in cookies)
            {
                cookies1.Add(cookieUri, new Cookie(cookie.Key, cookie.Value));
            }
        }

        #endregion

        #region Urls

        protected virtual string ApiRootUrl => "https://nhentai.net";

        protected virtual string ImageRootUrl => "https://i.nhentai.net";

        protected virtual string ThumbnailRootUrl => "https://t.nhentai.net";

        #endregion

        #region Data urls

        protected virtual string GetHomePageUrl(int pageNum) 
            => $"{ApiRootUrl}/api/galleries/all?page={pageNum}";

        protected virtual string GetSearchUrl(string content,int pageNum) 
            => $"{ApiRootUrl}/api/galleries/search?" +
               $"query={content.Replace(" ", "+")}&" +
               $"page={pageNum}";

        protected virtual string GetTagUrl(Tag tag, bool isPopularList, int pageNum) 
            => $"{ApiRootUrl}/api/galleries/tagged?" +
               $"tag_id={tag.Id}" +
               $"&page={pageNum}" +
               (isPopularList ? "&sort=popular" : "");

        protected virtual string GetBookDetailsUrl(int bookId) 
            => $"{ApiRootUrl}/api/gallery/{bookId}";

        protected virtual string GetBookRecommendUrl(int bookId) 
            => $"{ApiRootUrl}/api/gallery/{bookId}/related";

        protected virtual string GetGalleryUrl(int galleryId) 
            => $"{ImageRootUrl}/galleries/{galleryId}";

        protected virtual string GetThumbGalleryUrl(int galleryId) 
            => $"{ThumbnailRootUrl}/galleries/{galleryId}";

        #endregion

        #region Picture urls

        public virtual string GetPictureUrl(Book book, int pageNum)
        {
            var image = GetImage(book, pageNum);
            var fileType = ConvertType(image.Type);
            return  GetPictureUrl(book.MediaId, pageNum, fileType);
        }

        public virtual string GetThumbPictureUrl(Book book, int pageNum)
        {
            var image = GetImage(book, pageNum);
            var fileType = ConvertType(image.Type);
            return GetThumbPictureUrl(book.MediaId, pageNum, fileType);
        }

        public virtual string GetBigCoverUrl(Book book)
            => GetBigCoverUrl(book.MediaId);

        public virtual string GetOriginPictureUrl(Book book, int pageNum) 
            => GetOriginPictureUrl(book.MediaId, pageNum);

        public virtual string GetBookThumbUrl(Book book)
        {
            var fileType = ConvertType(book.Images.Cover.Type);
            return  GetBookThumbUrl(book.MediaId, fileType);
        }

        protected virtual string GetPictureUrl(int galleryId , int pageNum ,string fileType) 
            => $"{GetGalleryUrl(galleryId)}/{pageNum}.{fileType}";

        protected virtual string GetThumbPictureUrl(int galleryId , int pageNum ,string fileType) 
            => $"{GetThumbGalleryUrl(galleryId)}/{pageNum}t.{fileType}";

        protected virtual string GetBigCoverUrl(int galleryId) 
            => $"{GetThumbGalleryUrl(galleryId)}/cover.jpg";

        protected virtual string GetOriginPictureUrl(int galleryId , int pageNum) 
            => GetPictureUrl(galleryId, pageNum, "jpg");

        protected virtual string GetBookThumbUrl(int galleryId ,string fileType = "jpg") 
            => $"{GetThumbGalleryUrl(galleryId)}/thumb.{fileType ?? "jpg"}";

        #endregion

        #region Utilities

        protected virtual async Task<TOutput> GetData<TOutput>(string rootUrl)
        {
            var json = await _client.GetStringAsync(rootUrl);
            return JsonConvert.DeserializeObject<TOutput>(json);
        }

        protected virtual async Task<byte[]> GetByteData(string rootUrl)
        {
            var data = await _client.GetByteArrayAsync(rootUrl);
            return data;
        }

        protected virtual Image GetImage(Book book, int pageNum)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var page = book.Images.Pages[pageNum - 1];
            return page;
        }

        protected virtual string ConvertType(ImageType type)
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

        public virtual Task<SearchResults> GetHomePageListAsync(int pageNum)
        { 
            var url = GetHomePageUrl(pageNum);
            return GetData<SearchResults>(url);
        }

	    public virtual Task<SearchResults> GetSearchPageListAsync(string keyword,int pageNum)	
		{ 
            var url = GetSearchUrl(keyword, pageNum);
            return GetData<SearchResults>(url);
        }

        public virtual Task<SearchResults> GetTagPageListAsync(Tag tag, SortBy sortBy, int pageNum)
        {
            var url = GetTagUrl(tag, sortBy == SortBy.Popular, pageNum);
            return GetData<SearchResults>(url);
        }

        #endregion

        #region Books

        public virtual Task<Book> GetBookAsync(int bookId)
        {
            var url = GetBookDetailsUrl(bookId);
            return GetData<Book>(url);
        }

        public virtual async Task<BookRecommend> GetBookRecommendAsync(int bookId)
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

        public virtual Task<byte[]> GetPictureAsync(Book book, int pageNum)
        {
            var url = GetPictureUrl(book, pageNum);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetThumbPictureAsync(Book book, int pageNum)
        {
            var url = GetThumbPictureUrl(book, pageNum);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetBigCoverPictureAsync(Book book)
        {
            var url = GetBigCoverUrl(book.MediaId);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetOriginPictureAsync(Book book, int pageNum)
        {
            var url = GetOriginPictureUrl(book.MediaId, pageNum);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetBookThumbPictureAsync(Book book)
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
