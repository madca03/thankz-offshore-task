using backend.Models.Database;
using Newtonsoft.Json;

namespace backend.Models.ResponseModels
{
    public class GetUserResponseModel : BaseResponseModel
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}