using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.DrivedControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : BaseController
    {
        public PhotoController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
    }
}
