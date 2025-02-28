using Newtonsoft.Json;

namespace AutomationTests.APITests.Models
{
    public class GetUsers
    {
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        public List<User> Data { get; set; }

        public Support Support { get; set; }
    }
}
