using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamTech.Core.Persistence.CrosCuttingConcerns.Caching.Redis
{
    public class RedisConnection : IRedisConnection
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigurationOptions configuration = null;
        private Lazy<IConnectionMultiplexer> _Connection = null;
        private readonly string _redisHost;
        private readonly int _redisPort;
        public RedisConnection(bool allowAdmin = false)
        {
            _redisHost = "localhost"; //_configuration[""] appsetting oku
            _redisPort = 6379;

            configuration = new ConfigurationOptions()
            {
                EndPoints = { { _redisHost, _redisPort }, },
                AllowAdmin = allowAdmin,
                ClientName = "Client",
                ReconnectRetryPolicy = new LinearRetry(5000),
                AbortOnConnectFail = false,
            };
            _Connection = new Lazy<IConnectionMultiplexer>(() =>
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
                //redis.ErrorMessage += _Connection_ErrorMessage;
                //redis.InternalError += _Connection_InternalError;
                //redis.ConnectionFailed += _Connection_ConnectionFailed;
                //redis.ConnectionRestored += _Connection_ConnectionRestored;
                return redis;
            });
        }

        public IConnectionMultiplexer Connection { get { return _Connection.Value; } }

        public IDatabase Database
        {
            get
            {
                return Connection.GetDatabase();
            }
        }
    }
}
