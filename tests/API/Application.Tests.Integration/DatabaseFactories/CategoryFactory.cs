﻿using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CategoryFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _created;
    protected override int Created
    {
        get => _created; 
        set => _created = value;
    }

    public CategoryFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    public Category Create(string? name = null)
    {
        lock (CreationLock)
        {
            return CreateCategory(name);
        }
    }

    public List<Category> CreateMany(int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateCategory())
                .ToList();
        }
    }

    private Category CreateCategory(string? name = null)
    {
        Created++;

        name ??= $"Category #{Created}";

        return Add(new Category
        {
            Name = name
        });
    }
}