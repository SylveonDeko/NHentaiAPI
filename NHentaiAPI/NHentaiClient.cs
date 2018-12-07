using Newtonsoft.Json;
using NHentaiAPI.Model;
using System;
using System.Linq;
using System.Net;

namespace HentaiAPI
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

        protected virtual string getSearchUrl(string content,int pageNum)
        { 
            return $"{NHENTAI_HOME}/api/galleries/search?" +
				$"query={content.Replace(" ", "+")}&" +
				"page=$pageNum";
        }
			
	    protected virtual string getBookDetailsUrl(string bookId)
        { 
            return $"{NHENTAI_HOME}/api/gallery/{bookId}";
        }
			
	    protected virtual string getBookRecommendUrl(string bookId)
        { 
            return $"{NHENTAI_HOME}/api/gallery/{bookId}/related";
        }
			
	    protected virtual string getGalleryUrl(string galleryId)
		{ 
            return $"{NHENTAI_I}/galleries/{galleryId}";
        }

	    protected virtual string getThumbGalleryUrl(string galleryId)
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
			
	    protected virtual string getHomePageUrl(int pageNum)
		{
            return $"{NHENTAI_HOME}/api/galleries/all?page={pageNum}";
        }

        #endregion

        #region Picture urls

        protected virtual string getPictureUrl(string galleryId ,string pageNum ,string fileType)
        { 
            return $"{getGalleryUrl(galleryId)}/{pageNum}.{fileType}";
        }
			
	    protected virtual string getThumbPictureUrl(string galleryId ,string pageNum ,string fileType)
        { 
            return $"{getThumbGalleryUrl(galleryId)}/${pageNum}t.{fileType}";
        }
			
	    protected virtual string getBigCoverUrl(string galleryId)
        {
            return $"{getThumbGalleryUrl(galleryId)}/cover.jpg";
        }
			
	    protected virtual string getOriginPictureUrl(string galleryId ,string pageNum)
        {
            return getPictureUrl(galleryId, pageNum, "jpg");
        }
			
	    protected virtual string getBookThumbUrl(string galleryId ,string fileType = "jpg")
        { 
            return $"{getThumbGalleryUrl(galleryId)}/thumb.${fileType ?? "jpg"}";
        }

        #endregion

        #region Utilities

        private async void RunApiCall<TOutput>(string rootUrl, Action<TOutput> succes, Action<string> fail)
        {
            try
            {
                var json = await _client.DownloadStringTaskAsync(rootUrl);
                    succes(JsonConvert.DeserializeObject<TOutput>(json));
            }
            catch (Exception e)
            {
                fail(e.Message);
            }
        }

        #endregion

        #region Books

        public Book GetBookASync(string bookId)
        { 
            var url = getBookDetailsUrl(bookId);

            //TODO : download 

            return null;
        }

        #endregion

        #region Page

        public PageResult GetPageList(string url)
        { 
            return null;
        }

	    public PageResult getHomePageList(int pageNum)
        { 
            var url = getHomePageUrl(pageNum);

            //TODO : download 

            return null;
        }

	    public PageResult GetSearchPageList(string keyword,int pageNum)	
		{ 
            var url = getSearchUrl(keyword, pageNum);

            //TODO : download 

            return null;
        }

        #endregion
    }
}
