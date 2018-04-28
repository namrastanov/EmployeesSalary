using System;
using EmployeesSalary.Data;
using EmployeesSalary.Data.Extensions;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Data.UnitOfWork;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesSalary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

            services.Configure<DbOption>(Configuration.GetSection("DbOption"));
            services.ApplyEmployeesSalaryBindings(Configuration.GetSection("DbOption").GetSection("ESDbConnection").Value);

            InitTotalSalary(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ApiRoot",
                    template: "api/{controller}/{id?}");

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitTotalSalary(IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var dbContext = (DbContext)serviceProvider.GetService(typeof(DbContext));
                var unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork));
                var @employeeService = (IEmployeeService)serviceProvider.GetService(typeof(IEmployeeService));

                employeeService.InitTotalSalary();
            }
        }
    }
}
