using Newtonsoft.Json;

namespace Deezcord
{
    public class Track
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("readable")]
        public bool Readable { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_short")]
        public string TitleShort { get; set; }

        [JsonProperty("title_version")]
        public string TitleVersion { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("artist")]
        public Artist Artist { get; set; }

        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}