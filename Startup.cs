using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Monsterflix.Api.Configurations;
using Monsterflix.Api.Data;
using Monsterflix.Api.Repositories;
using Monsterflix.Api.Repositories.Contracts;
using Monsterflix.Api.Services;
using Monsterflix.Api.Services.Contracts;
using Swashbuckle.AspNetCore.SwaggerUI;

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

            // Autenticação Token Jwt Bearer
            byte[] key = Encoding.ASCII.GetBytes(AppSettingsProvider.Settings["KeyTokenJwtBearer"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Banco de dados - Docker
            services.AddDbContext<DataContext>(options => options.UseSqlServer(AppSettingsProvider.Settings["ConnectionStrings:connectionString"]));

            // Monsterflix serviços e repositórios
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileMovieRepository, ProfileMovieRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ITheMovieDBService, TheMovieDBService>();
            services.AddScoped<ITokenJwtService, TokenJwtService>();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Monsterflix Api",
                    Version = "v1",
                    Description = "É tipo um Netflix, só que com energia alternativa e ASP.NET!",
                    Contact = new OpenApiContact { Name = "LivBenedicto", Url = new Uri("https://github.com/LivBenedicto/Monsterflix") }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
						new[] { "", "" }
					}
				});
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

            // Adicionando autenticação
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
