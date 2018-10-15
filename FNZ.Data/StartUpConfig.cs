using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using FNZ.Data.Data;
using FNZ.Share.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FNZ.Data
{
    public class StartUpConfig
    {
        public IConfiguration Configuration;
        public static string Server;
        public StartUpConfig(IConfiguration configuration)
        {
            Configuration = configuration;
            Server = Configuration.GetConnectionString("DefaultConnection");
        }

        public void PartOfConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(StartUpConfig).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(migrationAssembly)));

            services.AddScoped<IUserStore<Moderator>, UserOnlyStore<Moderator, ApplicationDbContext>>();
            services.AddIdentityCore<Moderator>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication("Identity.Application").AddCookie("Identity.Application");

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Events.OnRedirectToLogin = ctx =>
                {
                    if (ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 401;
                        return Task.FromResult<object>(null);
                    }
                    return Task.CompletedTask;
                };

                opt.Events.OnRedirectToAccessDenied = ctx => {
                    if (ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 403;
                        return Task.FromResult<object>(null);
                    }
                    return Task.CompletedTask;
                };

                opt.Cookie.Domain = null;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                opt.ExpireTimeSpan = TimeSpan.FromHours(24);
            });
        }
    }
}
