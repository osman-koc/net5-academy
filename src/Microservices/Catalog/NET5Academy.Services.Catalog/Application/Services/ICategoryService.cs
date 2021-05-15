using NET5Academy.Services.Catalog.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Catalog.Application.Services
{
    public interface ICategoryService
    {
        Task<OkResponse<List<CategoryDto>>> GetAllAsync();
        Task<OkResponse<CategoryDto>> GetByIdAsync(string id);
        Task<OkResponse<CategoryDto>> CreateAsync(CategoryCreateDto dto);
        Task<OkResponse<CategoryDto>> UpdateAsync(CategoryUpdateDto dto);
        Task<OkResponse<object>> DeleteAsync(string id);
    }
}
