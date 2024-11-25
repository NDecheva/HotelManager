using HotelManager.Data.Entities;
using HotelManager.Shared.Enum;
using HotelManager.Shared.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Data
{
    public class HotelManagementDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientReservation> ClientReservations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        public HotelManagementDbContext() { }

        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options) { }

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


            modelBuilder.Entity<Room>()
                .HasMany(room => room.Reservations)
                .WithOne(res => res.Room)
                .HasForeignKey(res => res.RoomId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = PasswordHasher.HashPassword("str"),
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
            });
        }

    }
}