using GroceryStoreAPI.ActionFilters;
using GroceryStoreAPI.Frameworks;
using GroceryStoreAPI.Services;
using GroceryStoreAPI.Services.Frameworks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace GroceryStoreAPI
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
            services.AddControllers();
            services.AddTransient<CustomerService>();
            services.AddBuisnessConfiguration();
            services.AddAutoMapperConfiguration();

            services.AddScoped<ValidationFilterAttribute>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Grocery_Store_API", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Grocery Store API",
                    Description = "Grocery Store API To Manage Customers"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.ConfigureCustomExceptionMiddleware();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger(c =>
			{
				c.SerializeAsV2 = true;
			});

			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Grocery_Store_API/swagger.json", "Grocery Store API");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
