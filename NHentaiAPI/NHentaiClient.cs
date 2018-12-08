using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHentaiAPI.Model.Book;
using NHentaiAPI.Model.Recommend;
using NHentaiAPI.Model.Search;

namespace NHentaiAPI
{
    /// <summary>
    /// nHentai Client
    /// </summary>
    public class NHentaiClient
    {
        private readonly WebClient _client = new WebClient();

        //copied from : https://github.com/NHMoeDev/NHentai-android/blob/master/app/src/main/kotlin/moe/feng/nhentai/api/ApiConstants.kt
        const string NHENTAI_HOME = "https://nhentai.net";
	    const string NHENTAI_I = "https://i.nhentai.net";
	    const string NHENTAI_T = "https://t.nhentai.net";

        const string NHBOOKS_UA = "NHBooks ${BuildConfig.VERSION_NAME}/Android " +
			"Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.0.0 Mobile";

        #region Data urls

        protected virtual string getHomePageUrl(int pageNum)
        {
            return $"{NHENTAI_HOME}/api/galleries/all?page={pageNum}";
        }

        protected virtual string getSearchUrl(string content,int pageNum)
        { 
            return $"{NHENTAI_HOME}/api/galleries/search?" +
				$"query={content.Replace(" ", "+")}&" +
                $"page={pageNum}";
        }

        protected virtual string getTagUrl(Tag tag, bool isPopularList, int pageNum)
        {
            return $"{NHENTAI_HOME}/api/galleries/tagged?" +
                   $"tag_id={tag.Id}" +
                   $"&page={pageNum}" +
                   (isPopularList ? "&sort=popular" : "");
        }

        protected virtual string getBookDetailsUrl(int bookId)
        { 
            return $"{NHENTAI_HOME}/api/gallery/{bookId}";
        }
			
	    protected virtual string getBookRecommendUrl(int bookId)
        { 
            return $"{NHENTAI_HOME}/api/gallery/{bookId}/related";
        }
			
	    protected virtual string getGalleryUrl(int galleryId)
		{ 
            return $"{NHENTAI_I}/galleries/{galleryId}";
        }

	    protected virtual string getThumbGalleryUrl(int galleryId)
        { 
            return $"{NHENTAI_T}/galleries/{galleryId}";
        }

        #endregion

        #region Picture urls

        protected virtual string getPictureUrl(int galleryId , int pageNum ,string fileType)
        { 
            return $"{getGalleryUrl(galleryId)}/{pageNum}.{fileType}";
        }
			
	    protected virtual string getThumbPictureUrl(int galleryId , int pageNum ,string fileType)
        { 
            return $"{getThumbGalleryUrl(galleryId)}/{pageNum}t.{fileType}";
        }
			
	    protected virtual string getBigCoverUrl(int galleryId)
        {
            return $"{getThumbGalleryUrl(galleryId)}/cover.jpg";
        }
			
	    protected virtual string getOriginPictureUrl(int galleryId , int pageNum)
        {
            return getPictureUrl(galleryId, pageNum, "jpg");
        }
			
	    protected virtual string getBookThumbUrl(int galleryId ,string fileType = "jpg")
        { 
            return $"{getThumbGalleryUrl(galleryId)}/thumb.{fileType ?? "jpg"}";
        }

        #endregion

        #region Utilities

        protected virtual async Task<TOutput> getData<TOutput>(string rootUrl)
        {
            var json = await _client.DownloadStringTaskAsync(rootUrl);
            return JsonConvert.DeserializeObject<TOutput>(json);
        }

        protected virtual async Task<byte[]> getByteData(string rootUrl)
        {
            var data = await _client.DownloadDataTaskAsync(rootUrl);
            return data;
        }

        protected virtual Image GetImage(Book book, int pageNum)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            //get page
            var page = book.Images.Pages[pageNum - 1];
            return page;
        }

        protected virtual string ConvertType(ImageType type)
        {
            if (type == ImageType.Jpg)
                return "jpg";

            return "png";
        }

        #endregion

        #region Search

        public Task<SearchResults> GetHomePageListAsync(int pageNum)
        { 
            var url = getHomePageUrl(pageNum);
            return getData<SearchResults>(url);
        }

	    public Task<SearchResults> GetSearchPageListAsync(string keyword,int pageNum)	
		{ 
            var url = getSearchUrl(keyword, pageNum);
            return getData<SearchResults>(url);
        }

        public Task<SearchResults> GetTagPageListAsync(Tag tag, SortBy sortBy, int pageNum)
        {
            var url = getTagUrl(tag, sortBy == SortBy.Popular, pageNum);
            return getData<SearchResults>(url);
        }

        #endregion

        #region Books

        public Task<Book> GetBookAsync(int bookId)
        {
            var url = getBookDetailsUrl(bookId);
            return getData<Book>(url);
        }

        public Task<BookRecommend> GetBookRecommendAsync(int bookId)
        {
            var url = getBookRecommendUrl(bookId);
            return getData<BookRecommend>(url);
        }

        #endregion

        #region Picture

        public Task<byte[]> GetPictureAsync(Book book, int pageNum)
        {
            //get image
            var image = GetImage(book, pageNum);

            //get file type
            var fileType = ConvertType(image.Type);

            //get binary file
            var url = getPictureUrl(book.MediaId, pageNum, fileType);
            return getByteData(url);
        }

        public Task<byte[]> GetThumbPictureAsync(Book book, int pageNum)
        {
            //get image
            var image = GetImage(book, pageNum);

            //get file type
            var fileType = ConvertType(image.Type);

            //get binary file
            var url = getThumbPictureUrl(book.MediaId, pageNum, fileType);
            return getByteData(url);
        }

        public Task<byte[]> GetBigCoverPictureAsync(Book book)
        {
            //get binary file
            var url = getBigCoverUrl(book.MediaId);
            return getByteData(url);
        }

        public Task<byte[]> GetOriginPictureAsync(Book book, int pageNum)
        {
            //get image
            var image = GetImage(book, pageNum);

            //get binary file
            var url = getOriginPictureUrl(book.MediaId, pageNum);
            return getByteData(url);
        }

        public Task<byte[]> GetBookThumbPictureAsync(Book book)
        {
            //get binary file
            var url = getBookThumbUrl(book.MediaId);
            return getByteData(url);
        }

        #endregion
    }
}
