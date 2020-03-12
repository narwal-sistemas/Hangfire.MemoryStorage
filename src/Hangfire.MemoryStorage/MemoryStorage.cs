﻿using System.Collections.Generic;
using Hangfire.MemoryStorage.Database;
using Hangfire.Server;
using Hangfire.Storage;

namespace Hangfire.MemoryStorage
{
    public class MemoryStorage : JobStorage
    {
        private readonly MemoryStorageOptions _options;

        public Data Data { get; }

        public MemoryStorage() : this(new MemoryStorageOptions(), new Data())
        {
        }

        public MemoryStorage(MemoryStorageOptions options) : this(options, new Data())
        {
        }

        public MemoryStorage(MemoryStorageOptions options, Data data)
        {
            _options = options;
            Data = data;
        }

        public override IStorageConnection GetConnection()
        {
            return new MemoryStorageConnection(Data, _options.FetchNextJobTimeout);
        }

        public override IMonitoringApi GetMonitoringApi()
        {
            return new MemoryStorageMonitoringApi(Data);
        }

#pragma warning disable 618
        public override IEnumerable<IServerComponent> GetComponents()
#pragma warning restore 618
        {
            yield return new ExpirationManager(Data, _options.JobExpirationCheckInterval);
            yield return new CountersAggregator(Data, _options.CountersAggregateInterval);
        }
    }
}