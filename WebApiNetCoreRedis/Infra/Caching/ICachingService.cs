﻿namespace WebApiNetCoreRedis.Infra.Caching
{
    public interface ICachingService
    {

        Task SetAsync(string key, string value);
        Task<String> GetAsync(string key);

    }
}
