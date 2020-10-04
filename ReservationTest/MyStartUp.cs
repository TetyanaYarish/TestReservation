using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ReservationTest.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservationTest.Services;
using Microsoft.AspNetCore.Http;

namespace ReservationTest
{
    public class MyStartUp
    {
        public MyStartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        private static void Cotacts(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Welcome to Cotacts page!");
            });
        }

        private static void Home(IApplicationBuilder app)
        {
            app.Run(async (contex) =>
            {
                await contex.Response.WriteAsync("Welcome to Home!!!");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Run/Use/Map три медоти обробки налаштування запросів
            //конвейера обработки запроса применяются методы Run, Map и Use
            //app.Map("/Cotacts", Cotacts);
            //app.Map("/Home", Home);

            app.Use(async (context, nextMethod) =>
            {
                // метод передающий обработку запроса далее по конвееру, следующему методу.
                // для передачи в следующий метод используется делегат (nextMethod-название не иммет значения) 
                // При использовании метода Use и передаче выполнения следующему делегату следует учитывать, что не рекомендуется вызывать метод next.Invoke после метода Response.WriteAsync(). Компонент middleware должен либо генерировать ответ с помощью Response.WriteAsync, либо вызывать следующий делегат посредством next.Invoke, но не выполнять оба этих действия одновременно.
                //await context.Response.WriteAsync("Use redirect to next method");
                await nextMethod.Invoke();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapGet("/env2", async context =>
                {
                    await context.Response.WriteAsync("app name: " + env.ApplicationName + " Hello World3!");
                });
            });
            //Не передає управління іншим мкетодам, повинен використовуватись в кінці

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Page not found");
            });


        }
    }
}
