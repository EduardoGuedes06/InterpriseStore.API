﻿namespace Data.context
{
    using Microsoft.EntityFrameworkCore;
    using Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Text;
    using System.Threading.Tasks;

    namespace Data
    {
        public class MeuDbContext : DbContext
        {
            public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }


            public DbSet<Produto> Produtos { get; set; }
            public DbSet<Categoria> Categorias { get; set; }
            

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetProperties()
                        .Where(p => p.ClrType == typeof(string))))
                    property.SetColumnType("varchar(100)");

                modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

                foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

                base.OnModelCreating(modelBuilder);
            }

            public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCadastro").IsModified = false;
                    }
                }

                return base.SaveChangesAsync(cancellationToken);
            }




        }
    }

}