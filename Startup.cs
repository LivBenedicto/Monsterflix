using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Monsterflix.Api.Configurations;
using Monsterflix.Api.Data;
using Monsterflix.Api.Repositories;
using Monsterflix.Api.Repositories.Contracts;
using Monsterflix.Api.Services;
using Monsterflix.Api.Services.Contracts;

namespace Monsterflix.Api
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
            // Adicionando configurações do appsettings
            AppSettingsProvider.Settings = Configuration;
            
            services.AddControllers();

            #region Serviços adicionados
            services.AddMvc();

            services.AddResponseCompression();

            // Banco de dados - Docker
            services.AddDbContext<DataContext>(options => options.UseSqlServer(AppSettingsProvider.Settings["ConnectionStrings:connectionString"]));

            // Monsterflix serviços e repositórios
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileMovieRepository, ProfileMovieRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ITheMovieDBService, TheMovieDBService>();

            services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Monsterflix Api", Version = "v1" });
			});

			services.AddSwaggerGenNewtonsoftSupport();    
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Adicionando documentação - Swagger
            app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Monsterflix API");

				options.RoutePrefix = "swagger";

				options.DocumentTitle = "Monsterflix API";

				options.DisplayRequestDuration();
				options.EnableFilter();
			});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
