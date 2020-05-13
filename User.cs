using System;
using Newtonsoft.Json;

namespace Deezcord
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("inscription_date")]
        public DateTimeOffset InscriptionDate { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("picture_small")]
        public string PictureSmall { get; set; }

        [JsonProperty("picture_medium")]
        public string PictureMedium { get; set; }

        [JsonProperty("picture_big")]
        public string PictureBig { get; set; }

        [JsonProperty("picture_xl")]
        public string PictureXl { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("is_kid")]
        public bool IsKid { get; set; }

        [JsonProperty("tracklist")]
        public string Tracklist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}