using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePort.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Parkinglot> Parkinglots { get; set; }
        public DbSet<Parkingspot> Parkingspots { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Parkinglot
            modelBuilder.Entity<Parkinglot>().ToTable("Parkinglot");
            modelBuilder.Entity<Parkinglot>().HasKey(p => p.ParkinglotId);
            modelBuilder.Entity<Parkinglot>().HasData(new
            {
                ParkinglotId = 1,
                Name = "Hoth"
            }, new
            {
                ParkinglotId = 2,
                Name = "Kamino"
            }, new
            {
                ParkinglotId = 3,
                Name = "Dagobah"
            });
            // Parkingspot
            modelBuilder.Entity<Parkingspot>().ToTable("Parkingspot");
            modelBuilder.Entity<Parkingspot>().HasKey(p => p.ParkingspotId);
            modelBuilder.Entity<Parkingspot>().HasData(new
            {
                ParkingspotId = 1,
                ParkinglotId = 1,
                Size = 1
            }, new
            {
                ParkingspotId = 2,
                ParkinglotId = 1,
                Size = 2
            }, new
            {
                ParkingspotId = 3,
                ParkinglotId = 1,
                Size = 3
            }, new
            {
                ParkingspotId = 4,
                ParkinglotId = 2,
                Size = 1
            }, new
            {
                ParkingspotId = 5,
                ParkinglotId = 2,
                Size = 2
            }, new
            {
                ParkingspotId = 6,
                ParkinglotId = 2,
                Size = 3
            }, new
            {
                ParkingspotId = 7,
                ParkinglotId = 3,
                Size = 1
            }, new
            {
                ParkingspotId = 8,
                ParkinglotId = 3,
                Size = 2
            }, new
            {
                ParkingspotId = 9,
                ParkinglotId = 3,
                Size = 3
            });
            //// Driver
            modelBuilder.Entity<Driver>().ToTable("Driver");
            modelBuilder.Entity<Driver>().HasKey(p => p.DriverId);
        }
    }
}
