using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.DrivedControllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if (categories == null)
                    return NotFound(new ResponseAPI(404));

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return NotFound(new ResponseAPI(404));

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                if (categoryDTO == null)
                    return NotFound(new ResponseAPI(404));

                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200, "Item Has Been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("remove-category/{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                    return NotFound(new ResponseAPI(404));

                await work.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item Has Been Removed"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                if (categoryDTO == null)
                    return NotFound(new ResponseAPI(404));

                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Item Has Been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}