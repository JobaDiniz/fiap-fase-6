﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace iFood.Reviews.Data.Tests
{
    public class IntegrationFixture : IDisposable
    {
        public IntegrationFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables("Test_")
                .Build();

            var connectionString = Configuration.GetConnectionString("storeContext");
            var database = $"test_db_{Guid.NewGuid()}";

            Settings = new StoreSettings(connectionString, database);
            Context = new StoreContext(Settings);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreDb, Store>();
                cfg.CreateMap<ReviewDb, Review>();
            });
            Mapper = config.CreateMapper();
        }

        public StoreSettings Settings { get; }
        public StoreContext Context { get; }
        public IConfiguration Configuration { get; }
        public IMapper Mapper { get; }

        void IDisposable.Dispose()
        {
            var client = new MongoClient(Settings.ConnectionString);
            client.DropDatabase(Settings.Database);
        }
    }
}
