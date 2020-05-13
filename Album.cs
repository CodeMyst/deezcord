using Newtonsoft.Json;

namespace Deezcord
{
    public class Album
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("cover_small")]
        public string CoverSmall { get; set; }

        [JsonProperty("cover_medium")]
        public string CoverMedium { get; set; }

        [JsonProperty("cover_big")]
        public string CoverBig { get; set; }

        [JsonProperty("cover_xl")]
        public string CoverXl { get; set; }

        [JsonProperty("tracklist")]
        public string Tracklist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}