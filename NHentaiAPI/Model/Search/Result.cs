using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NHentaiAPI.Model.Book;

namespace NHentaiAPI.Model.Search
{
    public class Result
    {
        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("scanlator")]
        public string Scanlator { get; set; }

        [JsonProperty("upload_date")]
        public int UploadDate { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("num_pages")]
        public int NumPages { get; set; }

        [JsonProperty("num_favorites")]
        public int NumFavorites { get; set; }
    }
}
