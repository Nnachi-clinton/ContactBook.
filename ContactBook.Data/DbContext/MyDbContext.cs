
using ContactBook.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Data.DbContext
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    SeedRoles(builder);
        //}

        //Cascade deletion 
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(user => user.Contacts)
        //        .WithOne(contact => contact.User)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}

        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                  new IdentityRole() { Name = "Regular", ConcurrencyStamp = "2", NormalizedName = "Regular" }
                );
        }
    }
}
