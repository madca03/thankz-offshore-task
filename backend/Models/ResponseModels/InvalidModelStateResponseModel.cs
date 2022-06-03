using System.Collections.Generic;
using backend.References;
using Newtonsoft.Json;

namespace backend.Models.ResponseModels
{
    public class InvalidModelStateResponseModel : BaseResponseModel
    {
        [JsonProperty("errors")]
        public object Errors { get; set; }
        
        public InvalidModelStateResponseModel()
        {
            Status = APIStatusCodes.InvalidModelState;
        }
    }
}