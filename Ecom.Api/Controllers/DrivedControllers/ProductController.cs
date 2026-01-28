using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTOs;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.DrivedControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await work.ProductRepository
                    .GetAllAsync(x => x.Category,x => x.Photos);

                if (products is null)
                    return NotFound(new ResponseAPI(404));
                var result = mapper.Map<IEnumerable<ProductDTO>>(products);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await work.ProductRepository
                    .GetByIdAsync(id, x => x.Category,x => x.Photos);

                if (product is null)
                    return NotFound(new ResponseAPI(404));
                var result = mapper.Map<ProductDTO>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> Add([FromForm]AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.AddAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product Has Been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("update-Product")]
        public async Task<IActionResult> Update([FromForm]UpdateProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product Has Been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("Delete-Product/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var product = await work.ProductRepository
                    .GetByIdAsync(Id, x => x.Photos, x => x.Category);

                await work.ProductRepository.DeleteAsync(product);

                return Ok(new ResponseAPI(200, "Product Has Been Deleted"));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}