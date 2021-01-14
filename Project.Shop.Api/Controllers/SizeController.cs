using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Shop.BusinessLogic.DataServices;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.Api.Controllers
{
    [Route("api/size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly SizeService _sizeService;
        public SizeController (SizeService sizeService)
        {
            _sizeService = sizeService;
        }

        // GET api/size
        [HttpGet]
        public ActionResult<List<Size>> GetAll()
        {
            var result = _sizeService.GetAllSizes();
            return Ok(result);
        }

         // GET api/size/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleSize(int id)
        {
            var result = await _sizeService.GetSingleSizeByIdAsync(id);
            if(result == null)
            {
                return NotFound("The size record couldn't be found.");
            }
            return Ok(result);
        }

        // POST api/size
        [HttpPost]
        public async Task<IActionResult> CreateSize([FromBody] Size entity)
        {
            if(entity == null)
            {
                return BadRequest("size is Null.");
            }

           var result =await _sizeService.CreateSizeAsync (entity);

            return Ok($"{result} -created");
        }

        // PUT api/size/5
        [HttpPut]
        public async Task<IActionResult> UpdateSize([FromBody] Size entity)
        {
             if(entity == null)
            {
                return BadRequest("Size is null");
            }
            var result = await _sizeService.UpdateSizeAsync(entity);
            return Ok($"{result} -Updated");
        }

        // DELETE api/size/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSize(int id)
        {
           var result = await _sizeService.DeleteSizeAsync(id);
           return Ok($"{result} -Deleted");
        }
    }
}
