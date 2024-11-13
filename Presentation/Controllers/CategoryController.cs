using Business.Abstract;
using DataAccess.DTO.CategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Members

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryService.Add(createCategoryDto);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
        {
            var currentCategory = await _categoryService.Update(categoryDto);
            if (!currentCategory.Success)
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
            var result = await _categoryService.Delete(id);
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
