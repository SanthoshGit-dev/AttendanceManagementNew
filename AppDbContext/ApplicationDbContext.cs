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
        
    }
}
