using AutoMapper;
using INNOVATEQ.API.Helper;
using INNOVATEQ.DAL;
using INNOVATEQ.DAL.Repository;
using INNOVATEQ.DATA.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API
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
        {  // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddDbContext<INNOVATEQDBContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("INNOVATEQConectionString"),
                    builder => builder.EnableRetryOnFailure());
            }

       );
            services.AddControllers();
            //automapper


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "INNOVATEQ.API", Version = "v1" });
                c.OperationFilter<SwaggerFileOperationFilter>();
            });

            //repo
            services.AddScoped<IUserRepo, UserRepo>();   
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {        // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "INNOVATEQ.API v1"));
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), @"uplods")),
                RequestPath = new PathString("/StaticFiles")
            });
            app.UseRouting();

            app.UseAuthorization();
    
            //Add our new middleware to the pipeline
           // app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
