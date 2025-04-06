
using Microsoft.EntityFrameworkCore;
using ReadingTrackerAPIs.Models.Entity;

namespace ReadingTrackerAPIs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<DailyNote> DailyNotes { get; set; }
        public DbSet<ReadingProgress> ReadingProgress { get; set; }
        public DbSet<YearlyGoal> YearlyGoals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Book (One to Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Books)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
               

            // User -> ReadingProgress (1 to Many)
            modelBuilder.Entity<User>()
                .HasMany<ReadingProgress>()
                .WithOne(rp => rp.User)
                .HasForeignKey(rp => rp.UserId);
          

            // Book -> ReadingProgress (1 to Many)
            modelBuilder.Entity<Book>()
                .HasMany<ReadingProgress>()
                .WithOne(rp => rp.Book)
                .HasForeignKey(rp => rp.BookId);
            

            // User -> YearlyGoals (1 to Many)
            modelBuilder.Entity<User>()
                .HasMany<YearlyGoal>()
                .WithOne(yg => yg.User)
                .HasForeignKey(yg => yg.UserId);
          

            // User -> DailyNotes (1 to Many)
            modelBuilder.Entity<User>()
                .HasMany<DailyNote>()
                .WithOne(dn => dn.User)
                .HasForeignKey(dn => dn.UserId);
             

            // Book -> DailyNotes (optional 1 to Many)
            modelBuilder.Entity<Book>()
                .HasMany<DailyNote>()
                .WithOne(dn => dn.Book)
                .HasForeignKey(dn => dn.BookId);
            
        }

    }

}
