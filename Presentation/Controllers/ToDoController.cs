using Business.Abstract;
using DataAccess.DTO.ToDoDtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        #region Members

        private readonly IToDoService _toDoService;

        #endregion

        #region Constructor
        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateToDoDto createToDoDto)
        {
            var result = await _toDoService.Add(createToDoDto);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateToDoDto updateToDoDto)
        {
            var currentToDo = await _toDoService.Update(updateToDoDto);
            if (!currentToDo.Success)
            {
                return BadRequest("could not be updated");
            }
            else
            {
                return Ok("Successful");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _toDoService.Delete(id);
            if (!result.Success)
            {
                return NotFound("could not found");
            }
            else
            {
                return Ok("successful");
            }
        }

        #endregion
    }
}
