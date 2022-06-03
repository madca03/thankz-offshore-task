using System.Collections.Generic;
using backend.Models.Database;
using Newtonsoft.Json;

namespace backend.Models.ResponseModels
{
    public class GetUsersResponseModel : BaseResponseModel
    {
        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }
}