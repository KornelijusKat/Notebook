using Microsoft.EntityFrameworkCore;
using Notebook.Database.DatabaseRecords;

namespace Notebook.Database.DatabaseService
{
   public class NoteContext : DbContext
    {
       public DbSet<User> Users { get; set; }
       public DbSet<Record> Records { get; set; }
       public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var records = modelBuilder.Entity<Record>();
            var categories = modelBuilder.Entity<Category>();
            records.HasOne(x => x.Category).WithMany(b => b.RecordList).HasForeignKey("CategoryId").OnDelete(DeleteBehavior.NoAction);
            records.HasOne(x => x.User).WithMany(b => b.RecordList).HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
            categories.HasOne(x => x.User).WithMany(b => b.CategoryList).HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlServer($"Server = localhost; Database=NoteBookDB;Trusted_Connection=True;");
    }
}
