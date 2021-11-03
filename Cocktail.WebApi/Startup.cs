using Cocktail.WebApi.Repositories;
using CocktailApp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail.WebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cocktail.WebApiV2", Version = "v1" });
            });

            services.AddTransient(typeof(ICocktailRepository), typeof(CocktailRepository));

            services.AddDbContext<CocktailsDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("CocktailContext")));
        }






        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktail.WebApiV2 v1"));
            }

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }








    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {

    //        services.AddControllers();
    //        services.AddSwaggerGen(c =>
    //        {
    //            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cocktail.WebApi", Version = "v1" });
    //        });

    //        // insertion des ervices custom (les interfaces)
    //        services.AddTransient(typeof(ICocktailRepository), typeof(CocktailRepository));

    //        services.AddDbContext<CocktailsDbContext>(options =>
    //              options.UseSqlServer("CocktailAPIContext"));
    //        //Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Administrateur\\Desktop\\Guillaume\\2021-10-08_CocktailsApp2\\Solution1\\CocktailApp.Infrastructure\\Db\\CocktailDb.mdf;Integrated Security=True


    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //            app.UseSwagger();
    //            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktail.WebApi v1"));
    //        }

    //        app.UseHttpsRedirection();

    //        app.UseRouting();

    //        app.UseAuthorization();

    //        app.UseEndpoints(endpoints =>
    //        {
    //            endpoints.MapControllers();
    //        });
    //    }
    //}
}
