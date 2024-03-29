﻿using System;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Infrastructure.Compression;
using Commentor.GivEtPraj.Infrastructure.Storage;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commentor.GivEtPraj.Infrastructure;

public static class DependencyInjection
{
    private static readonly ILoggerFactory EfLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            if (env.IsDevelopment())
            {
                options.UseLoggerFactory(EfLoggerFactory);
            }
        });
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddSingleton<IFileStorage, AzureBlobFileStorage>();
        services.AddSingleton<IImageStorage, ImageStorage>();
        services.AddSingleton<IImageCompression, BitmapImageCompression>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IDeviceService, DeviceService>();

        return services;
    }
}