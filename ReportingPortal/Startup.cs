using DataLayer.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using ServiceLayer.Auth;
using ServiceLayer.Auth.Interface;
using ServiceLayer.DTO;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace ReportingPortal
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReportingPortal", Version = "v1" });
            });

            services.AddDbContext<ReportingPortalDbContext>();
            services.AddScoped<IJWTInterface, JWTUtilities>();
            services.AddHttpContextAccessor();
            services.TryAddScoped<IPasswordHasher<UserDTO>, PasswordHasher<UserDTO>>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(options => options.AddPolicy("MyPolicy", builder => { builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader(); }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerAPI v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseCors();
            app.UseAuthorization();




            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
