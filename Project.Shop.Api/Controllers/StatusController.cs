using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Shop.BusinessLogic.DataServices;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.Api.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;
        public StatusController (StatusService statusService)
        {
            _statusService = statusService;
        }

        // GET api/status
        [HttpGet]
        public ActionResult<List<Status>> GetAll()
        {
            var result = _statusService.GetAllStatuses();
            return Ok(result);
        }

         // GET api/status/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleStatus(int id)
        {
            var result = await _statusService.GetSingleStatusByIdAsync(id);
            if(result == null)
            {
                return NotFound("The status record couldn't be found.");
            }
            return Ok(result);
        }

        // POST api/status
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] Status entity)
        {
            if(entity == null)
            {
                return BadRequest("size is Null.");
            }

           var result =await _statusService.CreateStatusAsync (entity);

            return Ok($"{result} -created");
        }

        // PUT api/status/5
        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] Status entity)
        {
             if(entity == null)
            {
                return BadRequest("Size is null");
            }
            var result = await _statusService.UpdateStatusAsync(entity);
            return Ok($"{result} -Updated");
        }

        // DELETE api/status/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
           var result = await _statusService.DeleteStatusAsync(id);
           return Ok($"{result} -Deleted");
        }
    }
}
