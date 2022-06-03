using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Models.RequestModels
{
    public class CreateUserRequestModel
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("middlename")]
        public string Middlename { get; set; }

        [Required]
        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }
    }
}