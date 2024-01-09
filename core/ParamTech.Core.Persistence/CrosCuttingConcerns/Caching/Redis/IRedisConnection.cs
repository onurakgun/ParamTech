using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamTech.Core.Persistence.CrosCuttingConcerns.Caching.Redis
{
    public interface IRedisConnection
    {
        public IConnectionMultiplexer Connection { get; }
        public IDatabase Database { get; }
    }
}
