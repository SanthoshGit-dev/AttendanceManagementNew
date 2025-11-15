using AttendanceManagement.AppDbContext;
using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Models;
using AttendanceManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Repository
{
    public class ClassRepo : IClassRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;   
        private readonly DbSet<Classes> _dbSet;
        public ClassRepo(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _dbSet = _context.Set<Classes>();
            _cacheService = cacheService;
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
            var classed = _context.Classes;
            return classed;
        }

        public async Task DeleteClassAsync(string ClassId)
        {
            var updatest = await _dbSet.FindAsync(ClassId);
            _dbSet.Remove(updatest);
            _context.SaveChanges();
           
        }

        public async Task<IEnumerable<Classes>> GetAll()
        {
            var cacheClass = _cacheService.GetData<IEnumerable<Classes>>("classList");
            if (cacheClass != null && cacheClass.Count() > 0)
            {
                return cacheClass;
            }
            var classed = await _dbSet.ToListAsync();
            var expiryTime = DateTimeOffset.Now.AddMinutes(2);
            _cacheService.SetData("classList", classed, expiryTime);
            return classed;
        }

        public async Task<Classes> GetByIdAsync(string classId)
        {
            var getid = await _dbSet.FirstOrDefaultAsync(b => b.ClassId == classId);
            return getid;
        }

        public async Task<Classes> UpdateClassAsync(string ClassId, UpdateClass updateClass)
        {
            var updatest = await _dbSet.FindAsync(ClassId);
            updatest.Section = updateClass.Section;
            updatest.ClassName = updateClass.ClassName;
            updatest.InchargeStaffId = updateClass.InchargeStaffId;
            _dbSet.Update(updatest);
            _context.SaveChanges();
            var classed = await _dbSet.FirstOrDefaultAsync(b => b.ClassId == ClassId);
            return classed;

        }

    }
}
