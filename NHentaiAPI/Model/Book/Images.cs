using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHentaiAPI.Model.Book
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
