using Newtonsoft.Json;

namespace backend.Models.ResponseModels
{
    public class BaseResponseModel
    {
        [JsonProperty("status")]
        public int? Status { get; set; }

        public BaseResponseModel()
        {
            
        }

        public BaseResponseModel(int status)
        {
            Status = status;
        }
    }
}