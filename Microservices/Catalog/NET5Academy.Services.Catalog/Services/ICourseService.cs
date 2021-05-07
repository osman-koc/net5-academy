using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<OkResponse<List<CourseDto>>> GetAllAsync(string userId = null);
        Task<OkResponse<CourseDto>> GetByIdAsync(string id);
        Task<OkResponse<CourseDto>> CreateAsync(CourseCreateDto dto);
        Task<OkResponse<CourseDto>> UpdateAsync(CourseUpdateDto dto);
        Task<OkResponse<object>> DeleteAsync(string id);
    }
}
