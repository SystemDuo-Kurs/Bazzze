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
        public DbSet<Adresa> Adresses { get; set; }
        public DbSet<Zanimanje> Zanimanje { get; set; }

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

            modelBuilder.Entity<Adresa>()
                .Property<int>("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Adresa>().HasKey("Id");

            modelBuilder.Entity<Zanimanje>()
                            .Property<int>("Id")
                            .HasColumnType("int")
                            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Zanimanje>().HasKey("Id");

            modelBuilder.Entity<Osoba>().HasKey("Id"); modelBuilder.Entity<Osoba>().HasOne(o => o.Adresa)
                .WithOne(a => a.Osoba)
                .HasForeignKey<Osoba>("FK_Adresa");

            modelBuilder.Entity<Osoba>().HasMany(o => o.Zanimanja)
                .WithMany(z => z.Osobe);
        }
    }
}