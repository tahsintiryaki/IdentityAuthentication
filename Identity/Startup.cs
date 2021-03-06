using Identity.IOC;
using Identity.Models.Authentication;
using Identity.Models.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
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
            services.AddControllersWithViews()
                 .AddRazorRuntimeCompilation();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            //Identity ServiceInjector išerisinde Aktif edildi.
            ServiceInjector.Add(services, Configuration);
            //services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

            //services.AddAuthorization(x => x.AddPolicy("UserClaimNamePolicy", policy => policy.RequireClaim("EditProfile")));


            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditProfilePolicy",
                    policy => policy.RequireClaim("EditProfile"));

                options.AddPolicy("SignInPolicy",
                   policy => policy.RequireClaim("SignIn"));

                options.AddPolicy("PasswordResetPolicy",
                   policy => policy.RequireClaim("PasswordReset"));


            });

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //UseAuthenticationö metodu sayesinde uygulamanřn identity ile kimlik do­rulamasř geršekle■tirece­ini belirtmi■ bulunmaktayřz.
            app.UseAuthentication();

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
