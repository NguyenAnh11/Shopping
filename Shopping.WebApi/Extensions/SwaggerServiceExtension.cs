using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.WebApi.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            //SWAGGER Authorization
            var securityScheme = new OpenApiSecurityScheme()
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = @"Enter JWT token only.<br>
                                Example: abcdef12345",
                Reference = new OpenApiReference()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                }
            };
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Shopping API",
                    Description = "A simple Shopping APi",
                    Contact = new OpenApiContact()
                    {
                        Email = "anhnguyenviet11299@gmail.com",
                        Name = "Viet Anh",
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT"
                    },
                });

                option.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        securityScheme,new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
