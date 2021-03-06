using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.Catalog.Application.Dtos;
using NET5Academy.Services.Catalog.Application.Services;
using NET5Academy.Shared.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseController : OkBaseController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Get all courses
        /// Optional: by user id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>List CourseDto</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string userId = null)
        {
            var response = await _courseService.GetAllAsync(userId);
            return OkActionResult(response);
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id">Couser Id</param>
        /// <returns>CourseDto</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([Required] string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return OkActionResult(response);
        }

        /// <summary>
        /// Create course
        /// </summary>
        /// <param name="dto">CourseCreateDto</param>
        /// <returns>CourseDto</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            var response = await _courseService.CreateAsync(dto);
            return OkActionResult(response);
        }

        /// <summary>
        /// Update course
        /// </summary>
        /// <param name="dto">CourseUpdateDto</param>
        /// <returns>CourseDto</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CourseUpdateDto dto)
        {
            var response = await _courseService.UpdateAsync(dto);
            return OkActionResult(response);
        }

        /// <summary>
        /// Delete course by id
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return OkActionResult(response);
        }
    }
}
