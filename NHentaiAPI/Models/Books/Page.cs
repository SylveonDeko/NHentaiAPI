using Newtonsoft.Json;

namespace NHentaiAPI.Models.Books
{
    public class Page
    {
        [JsonProperty("t")] public string T { get; set; }

        [JsonProperty("w")] public int W { get; set; }

        [JsonProperty("h")] public int H { get; set; }
    }
}