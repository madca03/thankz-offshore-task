using backend.Models.ResponseModels;
using backend.References;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace backend.Attributes
{
    public class GlobalExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;
        
        public GlobalExceptionHandlerAttribute(ILogger logger)
        {
            this.logger = logger;
        }
        
        public override void OnException(ExceptionContext context)
        {
            logger.Error(context.Exception, context.Exception.Message);
            
            var res = new ExceptionResponseModel
            {
                Status = APIStatusCodes.ExceptionOccured,
                Message = context.Exception.Message
            };
            
            context.Result = new ObjectResult(res)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}