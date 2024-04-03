using lazurdit_task1.Shared;
using Microsoft.EntityFrameworkCore;

namespace lazurdit_task1.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Contact entity
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contacts"); // Set the table name
                entity.HasKey(e => e.Id); // Define primary key
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Configure Id as auto-generated
                entity.Property(e => e.FirstName).IsRequired(); // FirstName is required
                entity.Property(e => e.LastName).IsRequired(); // LastName is required
                entity.Property(e => e.Email).IsRequired(); // Email is required
                entity.Property(e => e.PhoneNumber).IsRequired(); // PhoneNumber is required
            });

            // Seed initial data if needed
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "123-456-7890"
                },
                new Contact
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "987-654-3210"
                }
                // Add more seed data as needed
            );
        }
    }
}
