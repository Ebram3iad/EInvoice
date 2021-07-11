using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EInvoice.Web.Helper;
using EInvoiceCore.Entities;
using EInvoiceInfrastructure;
using EInvoiceInfrastructure.EFRepository;
using EInvoiceInfrastructure.Services.CodeItemServices;
using EInvoiceInfrastructure.Services.InvoiceHeaderServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EInvoice.Web
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
           
            var conn = Configuration.GetConnectionString("DBConnectionString");
            services.AddDbContext<DBContext>(options =>
                        
            options.UseSqlServer(conn));
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
           
            #region Common
            services.AddSingleton(mapper);

            #endregion

            #region Repositories
            services.AddScoped<IRepository<InvoiceHeader>, Repository<InvoiceHeader>>();
            services.AddScoped<IRepository<InvoiceLine>, Repository<InvoiceLine>>();
            services.AddScoped<IRepository<CodeItem>, Repository<CodeItem>>();

            #endregion

            #region Services
            services.AddScoped<IInvoiceHeaderService, InvoiceHeaderService>();
            services.AddScoped<ICodeItemService, CodeItemService>();

            #endregion
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
