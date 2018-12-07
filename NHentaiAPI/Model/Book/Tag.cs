using Newtonsoft.Json;

namespace NHentaiAPI.Model.Book
{
    public class Tag
    {
        [JsonProperty("id")]
        public int Id{ get; set;}

        [JsonProperty("type")]
        public string Type{ get; set;}

        [JsonProperty("name")]
        public string Name{ get; set;}

        [JsonProperty("url")]
        public string Url { get; set;}

        [JsonProperty("count")]
        public int Count { get; set;}
    }
}
