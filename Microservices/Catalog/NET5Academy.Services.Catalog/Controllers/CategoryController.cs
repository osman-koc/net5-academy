using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Services.Catalog.Services;
using NET5Academy.Shared.ControllerBases;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : OkBaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List CategoryDto</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CategoryDto</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="dto">CategoryCreateDto</param>
        /// <returns>CategoryDto</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var response = await _categoryService.CreateAsync(dto);
            return CreateActionResultInstance(response);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="dto">CategoryUpdateDto</param>
        /// <returns>CategoryDto</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateDto dto)
        {
            var response = await _categoryService.UpdateAsync(dto);
            return CreateActionResultInstance(response);
        }

        /// <summary>
        /// Category delete by id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
