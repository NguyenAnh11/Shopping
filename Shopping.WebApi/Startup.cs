using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopping.Data.EF;
using Shopping.Utilities.Constants;
using Shopping.Application.Catalog.Products;
using Microsoft.OpenApi.Models;
using Shopping.Application.Catalog.Common;
using Shopping.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Shopping.Application.Catalog.System.User;
using Shopping.ViewModel.Common;

namespace Shopping.WebApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //ADD DBCONTEXT
            services.AddControllersWithViews();
            services.AddDbContext<ShoppingDBContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString(SystemConstant.MainConnectionString));
            });
            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<ShoppingDBContext>()
                    .AddDefaultTokenProviders();
            //ADD SERVICE
            services.AddTransient<IPublicProductService, PublicProductService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IManageProductService, ManageProductService>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();

            //ADD SWAGGER
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
                    }
                });
            });
            //ADD PASSWORDIDENTITY OPTIONs
            services.Configure<IdentityOptions>(option =>
            {
                //password
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
            });
            //ADD CONFIGURATION
            services.Configure<JwtOptionConfiguration>(this.Configuration.GetSection("Tokens"));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Shopping V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
