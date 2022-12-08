﻿using CabManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CabManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<book> books { get; set; }

        public DbSet<DriverDetails> DriverDetails { get; set; }

    }
}