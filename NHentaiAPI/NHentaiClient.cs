using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHentaiAPI.Model.Book;
using NHentaiAPI.Model.Search;

namespace NHentaiAPI
{
    /// <summary>
    /// NHentai
    /// </summary>
    public class NHentaiClient
    {
        private readonly WebClient _client = new WebClient();

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
			
	    protected virtual string getTagUrl(Tag tag,bool isPopularList,int pageNum)
        { 
            return $"{NHENTAI_HOME}/api/galleries/tagged?" +
					$"tag_id={tag.Id}" +
					$"&page={pageNum}" +
					(isPopularList ? "&sort=popular" : "");
        }

        #endregion

        #region Picture urls

        protected virtual string getPictureUrl(int galleryId , int pageNum ,string fileType)
        { 
            return $"{getGalleryUrl(galleryId)}/{pageNum}.{fileType}";
        }
			
	    protected virtual string getThumbPictureUrl(int galleryId , int pageNum ,string fileType)
        { 
            return $"{getThumbGalleryUrl(galleryId)}/${pageNum}t.{fileType}";
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
            return $"{getThumbGalleryUrl(galleryId)}/thumb.${fileType ?? "jpg"}";
        }

        #endregion

        #region Utilities

        protected async Task<TOutput> getData<TOutput>(string rootUrl)
        {
            var json = await _client.DownloadStringTaskAsync(rootUrl);
            return JsonConvert.DeserializeObject<TOutput>(json);
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

        #endregion

        #region Books

        public Task<Result> GetBookAsync(int bookId)
        {
            var url = getBookDetailsUrl(bookId);
            return getData<Result>(url);
        }

        public Task<SearchResults> GetBookRecommendAsync(int bookId)
        {
            var url = getBookRecommendUrl(bookId);
            return getData<SearchResults>(url);
        }

        #endregion
    }
}
