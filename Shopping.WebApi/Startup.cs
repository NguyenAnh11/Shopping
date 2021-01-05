using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopping.Data.EF;
using Shopping.Utilities.Constants;
using Shopping.Application.Catalog.Products;
using Shopping.Application.Catalog.Common;
using Shopping.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Shopping.Application.Catalog.System.User;
using Shopping.WebApi.Extensions;
using Shopping.ViewModel.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using Shopping.ViewModel.Catalog.System.User.Validators;

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
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
             
            services.AddDbContext<ShoppingDBContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString(SystemConstant.MainConnectionString));
            });
            services.AddIdentity<AppUser, AppRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
            }) .AddEntityFrameworkStores<ShoppingDBContext>()
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
            services.AddSwaggerService();

            //ADD CONFIGURATION OPTION
            services.Configure<JwtOptionConfiguration>(Configuration.GetSection("Tokens"));

            var jwtOption = new JwtOptionConfiguration();
            Configuration.GetSection("Tokens").Bind(jwtOption);
            byte[] signingKeyBytes = Encoding.UTF8.GetBytes(jwtOption.Key);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = true;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = jwtOption.Issuer,
                    ValidateIssuer = true,
                    ValidIssuer = jwtOption.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
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
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Shopping V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
