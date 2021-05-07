using NET5Academy.Services.Catalog.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Services
{
    internal interface ICategoryService
    {
        Task<OkResponse<List<CategoryDto>>> GetAllAsync();
        Task<OkResponse<CategoryDto>> GetByIdAsync(string id);
        Task<OkResponse<CategoryDto>> CreateAsync(string name);
    }
}
