using Newtonsoft.Json;

namespace backend.Models.RequestModels
{
    public class GetUsersRequestModel
    {
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("offset")]
        public int? Offset { get; set; }

        [JsonProperty("csv")]
        public bool? CSV { get; set; }
    }
}