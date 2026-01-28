using Ecom.Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.DrivedControllers
{
    [Route("Error/{statusCode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult HandleError(int statusCode)
        {
            return new ObjectResult (new ResponseAPI(statusCode));
        }
    }
}