using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace SQLDataAccess
{
    public class EventDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EventBase> Events { get; set; }
        public DbSet<Event> EventEntities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }

        //public EventDBContext()  { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;TrustServerCertificate=True");
        //}
        public EventDBContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Inheritance example
            modelBuilder.Entity<EventBase>()
                .HasDiscriminator<string>("EventType")
                .HasValue<EventBase>("EventBase")
                .HasValue<Event>("Event");

            modelBuilder.Entity<Event>()
                .Property(e => e.DateShift)
                .HasConversion(new TimeSpanToTicksConverter());


            modelBuilder.Entity<Event>()
                .Property(e => e.ProlongPeriod)
                .HasConversion(new TimeSpanToTicksConverter());

            // 1:1 relationship
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId);

            // 1:N relationship
            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.UserId);

            // N:N relationship
            modelBuilder.Entity<EventCategory>()
                .HasKey(ec => new { ec.EventId, ec.CategoryId });

            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventId);

            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Category)
                .WithMany(c => c.EventCategories)
                .HasForeignKey(ec => ec.CategoryId);
        }
    }
}
