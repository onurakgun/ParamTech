using Microsoft.Extensions.DependencyInjection;
using ParamTech.Core.Persistence.Utilities.Ioc;
using StackExchange.Redis;
using System.Text.RegularExpressions;

namespace ParamTech.Core.Persistence.CrosCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private IRedisConnection _redisConnction;
        private IDatabase _database;
        private IConnectionMultiplexer _connection;
        public RedisCacheManager()
        {
            _redisConnction = ServiceTool.ServiceProvider.GetService<IRedisConnection>();
            _connection = _redisConnction.Connection;
            _database = _redisConnction.Database;
        }

        public void Add(string key, object data, int duration)
        {
            string serializedJson = System.Text.Json.JsonSerializer.Serialize(data);
            _database.StringSet(key, serializedJson);
        }

        public void AllCacheRemove()
        {
            var GetEndPoints = _connection.GetEndPoints();
            var GetServer = _connection.GetServer(GetEndPoints[0]);
            GetServer.FlushDatabase();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            var response = _database.StringGet(key);
            return response;
        }

        public bool IsAdd(string key)
        {
            var response = (string)_database.StringGet(key);
            if (response == null) return false; return true;

        }

        public void PatternRemove(string pattern)
        {
            var GetEndPoints = _connection.GetEndPoints();

            List<string> listKeys = new List<string>();
            var GetServer = _connection.GetServer(GetEndPoints[0]);
            var keys = GetServer.Keys();
            listKeys.AddRange(keys.Select(key => (string)key).ToList());
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = listKeys.Where(d => regex.IsMatch(d.ToString())).ToList();
            if (keysToRemove.Count > 0)
            {
                foreach (string key in keysToRemove)
                {
                    Remove(key);
                }
            }
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var GetEndPoints = _connection.GetEndPoints();
            List<string> listKeys = new List<string>();
            var GetServer = _connection.GetServer(GetEndPoints[0]);
            var keys = GetServer.Keys();
            listKeys.AddRange(keys.Select(key => (string)key).ToList());
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = listKeys.Where(d => regex.IsMatch(d.ToString())).ToList();
            if (keysToRemove.Count > 0)
            {
                foreach (string key in keysToRemove)
                {
                    Remove(key);
                }
            }
        }

        public void Set(string key, object data, int duration)
        {
            _database.StringSet(key, data.ToString(), TimeSpan.FromDays(duration));
        }
    }
}
