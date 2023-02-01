using RedisAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisAPI.Data
{
    public class RedisPlatformRepository : IPlatformRepository
    {
        private readonly IConnectionMultiplexer _redis;

        IDatabase Db => _redis.GetDatabase();

        public RedisPlatformRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
                throw new ArgumentOutOfRangeException(nameof(platform));

            var serialPlat = JsonSerializer.Serialize(platform);

            //Db.StringSet(platform.Id, serialPlat);
            //Db.SetAdd("PlatformSet", serialPlat);

            Db.HashSet("hashplatform", 
                new HashEntry[] { new HashEntry(platform.Id, serialPlat) });
        }

        public IEnumerable<Platform?>? GetAllPlatform()
        {
            //var completSet = Db.SetMembers("PlatformSet");

            var completSetHash = Db.HashGetAll("hashplatform");

            if (completSetHash.Any())
            {
                var obj = Array.ConvertAll(completSetHash, val => JsonSerializer.Deserialize<Platform>(val.Value));
                return obj;
            }

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            //var plat = Db.StringGet(id);

            var platHash = Db.HashGet("hashplatform", id);

            if (string.IsNullOrEmpty(platHash))
            {
                return null;
            }

            return JsonSerializer.Deserialize<Platform>(platHash);
        }
    }
}
