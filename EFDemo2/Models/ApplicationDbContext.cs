using Microsoft.EntityFrameworkCore;

namespace EFDemo2.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");
                entity.HasMany(e => e.Students).WithOne("Course");
            }
            );
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");
                entity.HasMany(e => e.Fees).WithOne("Student");
                entity.HasOne(e => e.Photo).WithOne("Student");
            }
            );
            modelBuilder.Entity<Fee>(entity =>
            {
                entity.ToTable("Fee");
            }
            );
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");
            }
            );
        }
    }
}
