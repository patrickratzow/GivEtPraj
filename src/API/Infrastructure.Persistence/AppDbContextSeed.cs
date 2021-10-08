﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Infrastructure.Persistence;

public static class AppDbContextSeed
{
    public static async Task SeedSampleData(AppDbContext context)
    {
        SeedCategories(context);
        await context.SaveChangesAsync();

        SeedCases(context);
        await context.SaveChangesAsync();
    }
    
    private static void SeedCategories(AppDbContext context)
    {
        var hasAny = context.Categories.Any();
        if (hasAny) return;

        context.Categories.Add(new()
        {
            Name = "Vejskade"
        });
    }

    private static void SeedSubCategories(AppDbContext context)
    {
        var hasAny = context.SubCategories.Any();
        if (hasAny) return;

        context.SubCategories.AddRange(new()
        {
            Name = "Toilet"
        }, new()
        {
            Name = "Vejskade"
        });
    }

    private static void SeedCases(AppDbContext context)
    {
        var hasAny = context.Cases.Any();
        if (hasAny) return;

        var category = context.Categories.First();
        context.Cases.AddRange(new()
        {
            Title = "Hul i vejen",
            Description = "Der er et stor hul i vejen på arbejde",
            Category = category,
            GeographicLocation = GeographicLocation.From(54, 54)
        }, new()
        {
            Title = "Hul",
            Description = "Hul vejen",
            Category = category,
            GeographicLocation = GeographicLocation.From(53, 53.5),
            Pictures = new()
            {
                new()
                {
                    Id = Guid.NewGuid()
                },
                new()
                {
                    Id = Guid.NewGuid()
                }
            }
        });
    }
}