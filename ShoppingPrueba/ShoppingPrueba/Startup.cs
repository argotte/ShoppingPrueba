using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shooping.Data;
using Shooping.Helpers;
using ShoppingPrueba.Data;
using ShoppingPrueba.Data.Entities;
using ShoppingPrueba.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba
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
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            //TODO: make strongest pw
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.SignIn.RequireConfirmedEmail = true;
// lo de arriba es para confirmar tu email
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                cfg.Lockout.MaxFailedAccessAttempts = 5;
                cfg.Lockout.AllowedForNewUsers = true;

                //cfg.Password.RequiredLength = 6;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();
            // el AddDefaultTokenProviders fue implementado al confirmar lo del email

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });


            services.AddScoped<SeedDb>(); //se inyecta y se ejecuta 1 sola vez y la destruye
            //services.AddScope<SeedDb>();//se inyecta y se ejecuta cada vez que la necesita y la destruyte cuando la deja de usar
            //services.AddSingleton<>(); //lo inyecta y no lo destruye sino q lo  deja enmemoria
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<ICombosHelper, CombosHelper>();
            services.AddScoped<IBlobHelper, BlobHelper>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }
   



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
           
            
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
