using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.authentication.repository.mongodb;
using libragri.authentication.service.impl;
using libragri.authentication.service.interfaces;
using libragri.core.common;
using libragri.core.repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using libragri.core.webapi;
using libragri.core.repository.mongodb;
using MongoDB.Driver;
using libragri.authentication.repository.inmemory;
using libragri.core.repository.inmemorydb;

namespace libragri.authentication.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var factory = new Factory();
            // service factory
            factory.Register<IUserService<string>,UserService<string>>();
            factory.Register<IRefreshTokenService<string>,RefreshTokenService<string>>();
            // repository factory
            // factory.Register<IUserRepository<string>,UserRepositoryMongodb<string>>();
            // factory.Register<IRefreshTokenRepository<string>,RefreshTokenRepositoryMongodb<string>>();
            
            // // Store factory
            // var conectionStr = Configuration.GetSection("DbConfiguration").GetValue("ConnectionStr","");
            // var dbName=Configuration.GetSection("DbConfiguration").GetValue("DatabaseName","");
            // var store = new StoreMongodb<string>(new MongoClient(conectionStr),dbName);
            // store.Upsert(new UserData<string>{
            //     Id="glefevre",
            //     UserName="glefevre",
            //     PwdSHA1="calorix",
            //     Email="glefevre@yahoo.fr"
            // });
            // factory.Register<IStore<string>>(store);
            // // unit of work factory
            // var uow = new UnitOfWorkMongodb<string>(store);

            factory.Register<IUserRepository<string>,UserRepositoryInMemory<string>>();
            factory.Register<IRefreshTokenRepository<string>,RefreshTokenRepositoryInMemory<string>>();
            
            // Store factory
            var store = new StoreInMemory<string>();
            store.UpsertAsync(new UserData<string>{
                Id="glefevre",
                UserName="glefevre",
                PwdSHA1="calorix",
                Email="glefevre@yahoo.fr"
            }).Wait();
            factory.Register<IStore<string>>(store);
            // unit of work factory
            var uow = new UnitOfWorkInMemory<string>(store);

            factory.Register<IUnitOfWork<string>>(uow);

            services.AddSingleton<IFactory>(factory);
            services.AddMvc();
            services.AddOptions();
            services.Configure<Audience>(Configuration.GetSection("Audience"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvc();
        }
    }
}
