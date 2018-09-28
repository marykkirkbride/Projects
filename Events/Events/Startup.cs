using System;
using Events.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Events
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
            services.Configure<EventSettings>(Configuration);
            services.AddMvc();
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabaseUserPassword"];
            var connectionString = String.Format("Server={0};Database={1};User={2};Password={3};", server, database, user, password);

            services.AddDbContext<EventContext>(
                options => options.UseSqlServer(connectionString));

            //Add Swagger
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                   Title = "Events - HTTP API",
                   Version = "v1",
                   Description = "The Events Microservice HTTP API. This is a Data-Driven/CRUD microservice",
                   TermsOfService = "Terms Of Service"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Use Swagger
            app.UseSwagger().UseSwaggerUI(c =>
            {
                 c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Events V1");
            });

            app.UseMvc();
        }
    }
}
