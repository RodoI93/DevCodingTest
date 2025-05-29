using System.Text.Json.Serialization;

namespace DevCodingTest.Models
{
    public class StoryResponse
    {
        [JsonIgnore]
        public int id { get; set; }
        public int score { get; set; }
        public int time { get; set; }
        public string title { get; set; }
        [JsonIgnore]
        public string type { get; set; }
        public string uri { get; set; }
        public string postedBy { get; set; }
        [JsonIgnore]
        public int descendants { get; set; }
        
        public int? commentCount { get; set; }
    }
}
