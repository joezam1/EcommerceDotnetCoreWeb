using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.Repositories;

namespace Project.Shop.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();
            return Ok(result);
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleCategory(int id)
        {
            var result =await _categoryService.GetSingleCategoryByIdAsync(id);
            if(result == null)
            {
                return NotFound("The category record couldn't be found.");
            }
            return Ok(result);
        }

        // POST api/category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if(category == null)
            {
                return BadRequest("category is Null.");
            }
            var result = await _categoryService.CreateCategoryAsync(category);

            return Ok($"{result} -created");
        }

        // PUT api/category/5
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
             if(category == null)
            {
                return BadRequest("Category is null");
            }
            var result =await _categoryService.UpdateCategoryAsync(category);
            return Ok($"{result} -Updated");
        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
           var result = _categoryService.DeleteCategoryAsync(id);
           return Ok($"{result} -Deleted");
        }
    }
}