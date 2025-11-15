
using System.Runtime.Caching;

namespace AttendanceManagement.Services
{
    public class CacheService : ICacheService
    {
        private ObjectCache _memoryCache = MemoryCache.Default;
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object RemoveData(string key)
        {
            var res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var removedItem = _memoryCache.Remove(key);
                }
                else
                    res = false;

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expiration)
        {
            var res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Set(key, value, expiration);
                else
                    res = false;

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
