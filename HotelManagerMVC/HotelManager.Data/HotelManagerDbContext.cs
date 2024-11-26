using HotelManager.Data.Entities;
using HotelManager.Shared.Enum;
using HotelManager.Shared.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Data
{
    public class HotelManagerDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientReservation> ClientReservations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        public HotelManagerDbContext() { }

        public HotelManagerDbContext(DbContextOptions<HotelManagerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientReservation>()
                .HasKey(cr => cr.Id);


            modelBuilder.Entity<ClientReservation>()
                .HasOne(cr => cr.Client)
                .WithMany(c => c.ClientReservations)
                .HasForeignKey(cr => cr.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // Change Cascade to Restrict


            modelBuilder.Entity<ClientReservation>()
                .HasOne(cr => cr.Reservation)
                .WithMany(r => r.ClientReservations)
                .HasForeignKey(cr => cr.ReservationId)
                .OnDelete(DeleteBehavior.Cascade); // Change Cascade to Restrict


            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // Change Cascade to Restrict

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Change Cascade to Restrict




            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = PasswordHasher.HashPassword("str"),
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                MiddleName = "base",
                UCN = "0987654321",
                PhoneNumber = "1234567890",
                IsActive = true,
            });

        }

    }
}