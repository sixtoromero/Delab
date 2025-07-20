﻿using Delab.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Delab.AccessData.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    //Manejo de UserRoles por Usuario
    public DbSet<UserRoleDetails> UserRoleDetails => Set<UserRoleDetails>();

    //Entities
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<SoftPlan> SoftPlans => Set<SoftPlan>();
    public DbSet<Corporation> Corporations => Set<Corporation>();    
    public DbSet<Manager> Managers => Set<Manager>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Para tomar los valores de ConfigEntities
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}
