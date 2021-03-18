using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;
using VertusNaurellesEcommerceV1.Models;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using Stripe;

namespace VertusNaurellesEcommerceV1
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
            //extraire  DbConnection du Json
            var StringConnection = Configuration["DbConnection"];
            services.AddControllersWithViews();
             ////avec lifetime scoped a chaque fois il ya une demande  une nouvelle creation d'instance
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(StringConnection), ServiceLifetime.Scoped);
            services.AddDistributedMemoryCache();
            services.AddSession(options=> {
                options.IOTimeout = TimeSpan.FromMinutes(120);
              
            });

            services.AddHttpContextAccessor();
            services.AddIdentity<AppUser, IdentityRole>(options =>
              {
                  options.Password.RequiredLength = 8;
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequireDigit = false;
                  options.Password.RequireUppercase = false;
                  options.User.RequireUniqueEmail = true;
              }).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication().AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
            });
            services.AddRazorPages();
            services.AddTransient<ICategory, clsCategory>();
            services.AddTransient<IProduct, clsProduct>();
            services.AddTransient<IOpinion, clsOpinion>();
            services.AddTransient<IOrder, clsOrder>();
            services.AddTransient<IOrderItem, clsOrderItem>();
            services.AddTransient<ISlider, clsSlider>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios
                app.UseHsts();
            }
   
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            //key stoker sur azure 

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
