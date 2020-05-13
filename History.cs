using Newtonsoft.Json;

namespace Deezcord
{
    public class History
    {
        [JsonProperty ("data")]
        public Track [] Tracks { get; set; }

        [JsonProperty ("total")]
        public int Total { get; set; }
        
        [JsonProperty ("next")]
        public string Next { get; set; }
    }
}