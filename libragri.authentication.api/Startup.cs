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
            factory.Register<IUserRepository<string>,UserRepository<string>>();
            factory.Register<IRefreshTokenRepository<string>,RefreshTokenRepository<string>>();
            // unit of work factory
            factory.Register<IUnitOfWork<string>,UnitOfWork<string>>();
            // Store factory
            var store = new StoreInMemory<string>();
            factory.Register<IStore<string>>(store);

            services.AddSingleton<IFactory>(factory);
            services.AddMvc();
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
