using Newtonsoft.Json;

namespace NHentaiAPI.Models.Books
{
    public class Title
    {
        [JsonProperty("english")]
        public string English { get; set; }

        [JsonProperty("japanese")]
        public string Japanese { get; set; }

        [JsonProperty("pretty")]
        public string Pretty { get; set; }
    }
}
