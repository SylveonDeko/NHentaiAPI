using System.Collections.Generic;
using Newtonsoft.Json;

namespace NHentaiAPI.Models.Books
{
    public class Images
    {
        [JsonProperty("pages")]
        public List<Image> Pages { get; set; }

        [JsonProperty("cover")]
        public Image Cover { get; set; }

        [JsonProperty("thumbnail")]
        public Image Thumbnail { get; set; }
    }
}
