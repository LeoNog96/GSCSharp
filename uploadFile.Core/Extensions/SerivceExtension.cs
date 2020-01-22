using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using uploadFile.Core.Services.Interfaces;
using uploadFile.Core.Services;
using storage.Client;

namespace uploadFile.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors (this IServiceCollection services)
        {
            services.AddCors();
        }

        public static void ConfigureIISIntegration (this IServiceCollection services)
        {
            services.Configure<IISOptions> ( _ => { });
        }

        public static void ConfigureBdContext(this IServiceCollection services, IConfiguration config)
        {
  
        }

        public static void ConfigureHangFire (this IServiceCollection services, IConfiguration config)
        {

        }

        public static void ConfigureRepositories (this IServiceCollection services)
        {
        }

        public static void ConfigureServices (this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void ConfigureStorage (this IServiceCollection services)
        {
            services.AddScoped<GoogleStorage>();
        }

        public static void ConfigureSwaggerDocs (this IServiceCollection services)
        {
            services.AddSwaggerGen (c =>
            {
                c.SwaggerDoc ("v1", new OpenApiInfo
                {
                    Version = "v1",
                        Title = "API",
                        Description = "Documentação da WebApi",
                        Contact = new OpenApiContact
                        {
                            Name = "Leonardo Nogueira da Silva",
                                Email = "lnogueiradasilva628@gmail.com",
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                        }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });
        }
    }
}