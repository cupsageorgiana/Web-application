using System;
using System.Collections.Generic;
using System.Text;
using AtelierAuto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtelierAuto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Appointment> Appointment { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AtelierAutoContext-1;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
