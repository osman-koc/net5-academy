using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
{
    internal interface ICourseService
    {
        Task<OkResponse<List<CourseDto>>> GetAllAsync();
        Task<OkResponse<CourseDto>> GetByIdAsync(string id);

    }
}
