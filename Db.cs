using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazzze
{
    internal class Db : DbContext
    {
        public DbSet<Osoba> Osobas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-IR3PAUI;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Osoba>()
                .Property<int>("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Osoba>().HasKey("Id");
        }
    }
}