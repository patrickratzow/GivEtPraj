﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Commentor.GivEtPraj.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Case> Cases { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Employee> Employees {  get; set; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}