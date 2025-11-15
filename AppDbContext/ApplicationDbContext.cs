using AttendanceManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceManagement.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }
        public DbSet<Staffs> Staffs { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Students> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: prevent cascading delete for Class → Staff (Incharge)
            modelBuilder.Entity<Classes>()
                .HasOne(c => c.InchargeStaff)
                .WithMany(s => s.ClassesIncharge)
                .HasForeignKey(c => c.InchargeStaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
