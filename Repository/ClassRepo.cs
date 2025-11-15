using AttendanceManagement.AppDbContext;
using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Repository
{
    public class ClassRepo : IClassRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Classes> _dbSet;
        public ClassRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Classes>();
        }
        public async Task<IQueryable<Classes>> AddClassAsync(AddClass addClass)
        {
            Classes classes = new Classes()
            {
               ClassId = addClass.ClassId,
               ClassName = addClass.ClassName,
               Section = addClass.Section,
               InchargeStaffId = addClass.InchargeStaffId
            };
            await _dbSet.AddAsync(classes);
            _context.SaveChanges();
            var classed = _context.Classes.Include(c => c.InchargeStaff).ThenInclude(c => c.Students).Where(c => c.ClassId == addClass.ClassId);
            return classed;
        }

        public async Task DeleteClassAsync(string ClassId)
        {
            var updatest = await _dbSet.FindAsync(ClassId);
            _dbSet.Remove(updatest);
            _context.SaveChanges();
           
        }

        public async Task<IEnumerable<Classes>> GetAllAsync()
        {
            var classed = _context.Classes.Include(c => c.InchargeStaff).ThenInclude(c => c.Students).ToList();
            return classed;
        }

        public async Task<Classes> GetByIdAsync(string ClassId)
        {
            var getid = await _dbSet.FindAsync(ClassId);
            return getid;
        }

        public async Task<IQueryable<Classes>> UpdateClassAsync(string ClassId, UpdateClass updateClass)
        {
            var updatest = await _dbSet.FindAsync(ClassId);
            updatest.Section = updateClass.Section;
            updatest.ClassName = updateClass.ClassName;
            updatest.InchargeStaffId = updateClass.InchargeStaffId;
            _dbSet.Update(updatest);
            _context.SaveChanges();
            var classed = _context.Classes.Include(c => c.InchargeStaff).ThenInclude(c => c.Students).Where(c => c.ClassId == ClassId);
            return classed;

        }
    }
}
