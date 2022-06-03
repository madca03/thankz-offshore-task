using Newtonsoft.Json;

namespace backend.Models.ResponseModels
{
    public class ExceptionResponseModel : BaseResponseModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}