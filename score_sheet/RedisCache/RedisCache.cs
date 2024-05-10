using Newtonsoft.Json;
using StackExchange.Redis;

namespace score_sheet.RedisCache
{
    public class RedisCache:IRedisCache
    {
        public static class ConnectionHelper
        {
            private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("127.0.0.1:6379");
            });

            public static ConnectionMultiplexer Connection
            {
                get
                {
                    return lazyConnection.Value;
                }
            }
        }
        private IDatabase database;
        public RedisCache()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            database = ConnectionHelper.Connection.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = database.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = database.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = database.KeyExists(key);
            if (_isKeyExist == true)
            {
                return database.KeyDelete(key);
            }
            return false;
        }


    }
}
