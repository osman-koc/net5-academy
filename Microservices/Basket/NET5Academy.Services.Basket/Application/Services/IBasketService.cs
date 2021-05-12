using NET5Academy.Services.Basket.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Threading.Tasks;

namespace NET5Academy.Services.Basket.Application.Services
{
    public interface IBasketService
    {
        Task<OkResponse<BasketDto>> GetByUserId(string userId);
        Task<OkResponse<bool>> CreateOrUpdate(BasketDto dto);
        Task<OkResponse<object>> DeleteByUserId(string userId);
    }
}
