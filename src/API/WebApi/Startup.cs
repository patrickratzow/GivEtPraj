﻿using System.Reflection;
using Commentor.GivEtPraj.Application;
using Commentor.GivEtPraj.Application.Common.Options;
using Commentor.GivEtPraj.Infrastructure;
using Commentor.GivEtPraj.WebApi.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Commentor.GivEtPraj.WebApi;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ReCaptchaOptions>(_configuration.GetSection(ReCaptchaOptions.ReCaptcha));

        services.AddApplication();
        services.AddInfrastructure(_configuration, _env);
        services.AddCors();
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "GivEtPraj.API",
                Version = "v1"
            });
            
            c.OperationFilter<AddRequiredHeaderParameter>();
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commentor.GivEtPraj.API v1"));

            // app.UseRedisInformation();

            app.UseCors(options =>
                options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        }

        app.UseCustomExceptionHandler();
        app.UseFluentValidationExceptionHandler();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseLanguageService();
        app.UseDeviceService();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}