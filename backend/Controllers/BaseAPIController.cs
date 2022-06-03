using backend.Models.ResponseModels;
using backend.References;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class BaseAPIController : ControllerBase
    {
        protected IActionResult HandleGenericFailure(object payload = null, int apiStatus = APIStatusCodes.GenericFailure)
        {
            if (payload == null)
            {
                payload = new BaseResponseModel { Status = apiStatus };
            }
            
            var res = new ObjectResult(payload)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            return res;
        }
        
        protected IActionResult HandleGenericSuccess<T>(T payload = null) where T : BaseResponseModel
        {
            if (payload == null)
            {
                return new ObjectResult(new BaseResponseModel { Status = APIStatusCodes.Success });
            }

            if (payload.Status == null) payload.Status = APIStatusCodes.Success;
            return new ObjectResult(payload);
        }
    }
}