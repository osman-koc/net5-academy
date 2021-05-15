using NET5Academy.Shared.Config;
using StackExchange.Redis;

namespace NET5Academy.Services.Basket.Application.Services
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _conMultiplexer;
        public RedisService(IRedisSettings redisSettings)
        {
            _conMultiplexer = ConnectionMultiplexer.Connect($"{redisSettings.Host}:{redisSettings.Port}");
        }

        public IDatabase GetDb(int db = 1) => _conMultiplexer.GetDatabase(db);
    }
}
