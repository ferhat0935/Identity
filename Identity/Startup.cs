using Identity.Context;
using Identity.CustomDescriber;
using Identity.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireDigit = false;  //þifrede sayý zorunlu olmasýn dedik
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                

            }).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<IdentityDbContext>();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict; //sadece ilgili domaninde kullanýlabilir
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.Name = "Cookie";
                opt.ExpireTimeSpan = TimeSpan.FromDays(25);  //kullanýcýnýn bilgisini 25 gün boyunca hatýrla
                opt.LoginPath = new PathString("/Home/SignIn"); //home/adminpanel yazýlýnca signin e yönlendiriyor.Yani gitmek istediði yerde giriþ yapýlmasý gerekmiþse signin sayfasýna gönderir
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");

            });
            services.AddDbContext<IdentityDbContext>(opt =>
            {
                opt.UseSqlServer("Server=FERHATSOLMAZZ\\SQLEXPRESS; Database=IdentityDb; User Id=ferhat; Password=ferhat0935; TrustServerCertificate=True");
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath="/node_modules",
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules"))
            });
            app.UseRouting();
            //giriþ yapmýþ kullanýcýlar için bu ikisini kesinlikle buraya yazmamýz gerekiyor
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
