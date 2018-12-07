using NHentaiAPI.Model;
using System;

namespace HentaiAPI
{
    /// <summary>
    /// NHentai
    /// </summary>
    public class NHentaiClient
    {
        const string NHENTAI_HOME = "https://nhentai.net";
	    const string NHENTAI_I = "https://i.nhentai.net";
	    const string NHENTAI_T = "https://t.nhentai.net";

        const string NHBOOKS_UA = "NHBooks ${BuildConfig.VERSION_NAME}/Android " +
			"Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.0.0 Mobile";

        #region Data urls

        protected string getSearchUrl(string content,int pageNum)
        { 
            return $"{NHENTAI_HOME}/api/galleries/search?" +
				$"query={content.Replace(" ", "+")}&" +
				"page=$pageNum";
        }
			
	    protected string getBookDetailsUrl(string bookId)
        { 
            return $"{NHENTAI_HOME}/api/gallery/{bookId}";
        }
			
	    protected string getBookRecommendUrl(string bookId)
        { 
            return $"{NHENTAI_HOME}/api/gallery/{bookId}/related";
        }
			
	    protected string getGalleryUrl(string galleryId)
		{ 
            return $"{NHENTAI_I}/galleries/{galleryId}";
        }

	    protected string getThumbGalleryUrl(string galleryId)
        { 
            return $"{NHENTAI_T}/galleries/{galleryId}";
        }
			
	    protected string getTagUrl(Tag tag,bool isPopularList,int pageNum)
        { 
            return $"{NHENTAI_HOME}/api/galleries/tagged?" +
					$"tag_id={tag.Id}" +
					$"&page={pageNum}" +
					(isPopularList ? "&sort=popular" : "");
        }
			
	    protected string getHomePageUrl(int pageNum)
		{
            return $"{NHENTAI_HOME}/api/galleries/all?page={pageNum}";
        }

        #endregion

        #region Picture urls

        // Picture urls
        protected string getPictureUrl(string galleryId ,string pageNum ,string fileType)
        { 
            return $"{getGalleryUrl(galleryId)}/{pageNum}.{fileType}";
        }
			
	    protected string getThumbPictureUrl(string galleryId ,string pageNum ,string fileType)
        { 
            return $"{getThumbGalleryUrl(galleryId)}/${pageNum}t.{fileType}";
        }
			
	    protected string getBigCoverUrl(string galleryId)
        {
            return $"{getThumbGalleryUrl(galleryId)}/cover.jpg";
        }
			
	    protected string getOriginPictureUrl(string galleryId ,string pageNum)
        {
            return getPictureUrl(galleryId, pageNum, "jpg");
        }
			
	    protected string getBookThumbUrl(string galleryId ,string fileType = "jpg")
        { 
            return $"{getThumbGalleryUrl(galleryId)}/thumb.${fileType ?? "jpg"}";
        }

        #endregion
    }
}
