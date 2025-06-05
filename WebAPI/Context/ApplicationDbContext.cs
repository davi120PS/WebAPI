﻿using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace WebAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Insertar en tabla Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PKRol = 1,
                    Name = "User"
                },
                new Rol
                {
                    PKRol = 2,
                    Name = "Admin"
                });

            // Insertar en tabla Usuario
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    PKUser = 1,
                    Name = "David",
                    Username = "davi",
                    Password = "123",
                    FKRol = 2 // Asegúrate de que esta clave foránea corresponde a un Rol existente
                },
                new User
                {
                    PKUser = 2,
                    Name = "Jorge",
                    Username = "joge",
                    Password = "123",
                    FKRol = 1
                },
                new User
                {
                    PKUser = 3,
                    Name = "Yeriel",
                    Username = "cupi",
                    Password = "123",
                    FKRol = 1
                });
        }
    }
}
