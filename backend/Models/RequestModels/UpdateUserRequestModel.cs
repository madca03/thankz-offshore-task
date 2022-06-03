using Newtonsoft.Json;

namespace backend.Models.RequestModels
{
    public class UpdateUserRequestModel
    {
        public string UserId { get; set; }
        
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("middlename")]
        public string Middlename { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }
    }
}