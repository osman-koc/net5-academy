using NET5Academy.Services.Discount.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Discount.Application.Services
{
    public interface IDiscountService
    {
        Task<OkResponse<IEnumerable<DiscountDto>>> GetAll();
        Task<OkResponse<DiscountDto>> GetById(int id);
        Task<OkResponse<DiscountDto>> GetByCodeAndUserId(string code, string userId);
        Task<OkResponse<DiscountDto>> Create(DiscountCreateDto dto);
        Task<OkResponse<DiscountDto>> Update(DiscountUpdateDto dto);
        Task<OkResponse<object>> DeleteById(int id);
    }
}
