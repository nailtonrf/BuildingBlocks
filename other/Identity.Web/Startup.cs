﻿using System;
using BuildingBlocks.Mediatr.Autofac;
using Identity.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityApplication(_configuration);
            services.AddMvc();
            services.AddFeatureFoldersSupport();

            return services.ConvertToAutofac(
                MediatrModule.Create(typeof(Application.Startup).Assembly)
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityApplication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
