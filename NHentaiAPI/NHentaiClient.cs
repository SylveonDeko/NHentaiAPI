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

        protected virtual string ApiRootUrl => "https://nhentai.net";

        protected virtual string ImageRootUrl => "https://i.nhentai.net";

        protected virtual string ThumbnailRootUrl => "https://t.nhentai.net";

        #endregion

        #region Data urls

        protected virtual string GetHomePageUrl(int pageNum)
        {
            return $"{ApiRootUrl}/api/galleries/all?page={pageNum}";
        }

        protected virtual string GetSearchUrl(string content,int pageNum)
        { 
            return $"{ApiRootUrl}/api/galleries/search?" +
				$"query={content.Replace(" ", "+")}&" +
                $"page={pageNum}";
        }

        protected virtual string GetTagUrl(Tag tag, bool isPopularList, int pageNum)
        {
            return $"{ApiRootUrl}/api/galleries/tagged?" +
                   $"tag_id={tag.Id}" +
                   $"&page={pageNum}" +
                   (isPopularList ? "&sort=popular" : "");
        }

        protected virtual string GetBookDetailsUrl(int bookId)
        { 
            return $"{ApiRootUrl}/api/gallery/{bookId}";
        }
			
	    protected virtual string GetBookRecommendUrl(int bookId)
        { 
            return $"{ApiRootUrl}/api/gallery/{bookId}/related";
        }
			
	    protected virtual string GetGalleryUrl(int galleryId)
		{ 
            return $"{ImageRootUrl}/galleries/{galleryId}";
        }

	    protected virtual string GetThumbGalleryUrl(int galleryId)
        { 
            return $"{ThumbnailRootUrl}/galleries/{galleryId}";
        }

        #endregion

        #region Picture urls

        protected virtual string GetPictureUrl(int galleryId , int pageNum ,string fileType)
        { 
            return $"{GetGalleryUrl(galleryId)}/{pageNum}.{fileType}";
        }
			
	    protected virtual string GetThumbPictureUrl(int galleryId , int pageNum ,string fileType)
        { 
            return $"{GetThumbGalleryUrl(galleryId)}/{pageNum}t.{fileType}";
        }
			
	    protected virtual string GetBigCoverUrl(int galleryId)
        {
            return $"{GetThumbGalleryUrl(galleryId)}/cover.jpg";
        }
			
	    protected virtual string GetOriginPictureUrl(int galleryId , int pageNum)
        {
            return GetPictureUrl(galleryId, pageNum, "jpg");
        }
			
	    protected virtual string GetBookThumbUrl(int galleryId ,string fileType = "jpg")
        { 
            return $"{GetThumbGalleryUrl(galleryId)}/thumb.{fileType ?? "jpg"}";
        }

        #endregion

        #region Utilities

        protected virtual async Task<TOutput> GetData<TOutput>(string rootUrl)
        {
            try
            {
                var json = await _client.GetStringAsync(rootUrl);
                return JsonConvert.DeserializeObject<TOutput>(json);
            }
            catch (Exception ex)
            {
                throw;
            }
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

            // Get page
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
            // Get image
            var image = GetImage(book, pageNum);

            // Get file type
            var fileType = ConvertType(image.Type);

            // Get binary file
            var url = GetPictureUrl(book.MediaId, pageNum, fileType);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetThumbPictureAsync(Book book, int pageNum)
        {
            // Get image
            var image = GetImage(book, pageNum);

            // Get file type
            var fileType = ConvertType(image.Type);

            // Get binary file
            var url = GetThumbPictureUrl(book.MediaId, pageNum, fileType);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetBigCoverPictureAsync(Book book)
        {
            // Get binary file
            var url = GetBigCoverUrl(book.MediaId);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetOriginPictureAsync(Book book, int pageNum)
        {
            // Get binary file
            var url = GetOriginPictureUrl(book.MediaId, pageNum);
            return GetByteData(url);
        }

        public virtual Task<byte[]> GetBookThumbPictureAsync(Book book)
        {
            // Get binary file
            var url = GetBookThumbUrl(book.MediaId);
            return GetByteData(url);
        }

        #endregion

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
