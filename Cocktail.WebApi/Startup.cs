using Cocktail.WebApi.Helpers;
using Cocktail.WebApi.Repositories;
using Cocktail.WebApi.Services;
using CocktailApp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace Cocktail.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // (ajout jwt)
            services.AddCors();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cocktail.WebApiV2", Version = "v1" });
            });

            // Service JWT et injection de dépendance
            // Va attribuer la valeur de Secret au champs de AppSettings au démarrage
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //configure DI for application services

            services.AddScoped<IUserService, UserService>();

            // Injection de dépendance Db
            services.AddTransient(typeof(ICocktailRepository), typeof(CocktailRepository));
            services.AddDbContext<CocktailsDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("CocktailContext")));
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktail.WebApiV2 v1"));
            }
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            //app.UseAuthorization();
            // A ajouter après le MAPCONTROLLER pour que les redirections soient faites
            // Custom JWT authentication middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            //app.UseHttpsRedirection();

        }
    }
}
