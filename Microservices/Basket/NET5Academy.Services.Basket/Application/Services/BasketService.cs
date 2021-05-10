using NET5Academy.Services.Basket.Application.Dtos;
using NET5Academy.Shared.Models;
using StackExchange.Redis;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace NET5Academy.Services.Basket.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRedisService _redisService;
        public BasketService(IRedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task<OkResponse<BasketDto>> GetByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return OkResponse<BasketDto>.Error(HttpStatusCode.BadRequest, "UserId cannot be null!");
            }

            RedisValue existBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return OkResponse<BasketDto>.Error(HttpStatusCode.NotFound, "Basket not found!");
            }

            var basketDto = JsonSerializer.Deserialize<BasketDto>(existBasket);
            return OkResponse<BasketDto>.Success(HttpStatusCode.OK, basketDto);
        }

        public async Task<OkResponse<bool>> CreateOrUpdate(BasketDto dto)
        {
            if(dto == null || string.IsNullOrEmpty(dto.UserId))
            {
                return OkResponse<bool>.Error(HttpStatusCode.BadRequest, "Model is not valid.");
            }

            var basketJson = JsonSerializer.Serialize(dto);
            var isSuccess = await _redisService.GetDb().StringSetAsync(dto.UserId, basketJson);
            var response = isSuccess
                ? OkResponse<bool>.Success(HttpStatusCode.OK, true)
                : OkResponse<bool>.Error(HttpStatusCode.InternalServerError, "Basket could not save or update");

            return response;
        }

        public async Task<OkResponse<object>> DeleteByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return OkResponse<object>.Error(HttpStatusCode.BadRequest, "UserId cannot be null!");
            }

            var isSuccess = await _redisService.GetDb().KeyDeleteAsync(userId);
            var response = isSuccess
                ? OkResponse<object>.Success(HttpStatusCode.OK, true)
                : OkResponse<object>.Error(HttpStatusCode.NotFound, "Basket not found!");

            return response;
        }
    }
}
