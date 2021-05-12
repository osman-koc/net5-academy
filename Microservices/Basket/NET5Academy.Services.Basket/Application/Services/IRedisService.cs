using StackExchange.Redis;

namespace NET5Academy.Services.Basket.Application.Services
{
    public interface IRedisService
    {
        IDatabase GetDb(int db = 1);
    }
}
