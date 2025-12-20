using AttendanceManagement.AppDbContext;
using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Models;
using AttendanceManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AttendanceManagement.Repository
{
    public class ClassRepo : IClassRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _distributedCache;
        private readonly DbSet<Classes> _dbSet;
        public ClassRepo(ApplicationDbContext context, IDistributedCache cacheService)
        {
            _context = context;
            _dbSet = _context.Set<Classes>();
            _distributedCache = cacheService;
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

        public async Task<IEnumerable<Classes>> GetAll(CancellationToken cancellationToken = default)
        {
            string key = "get-classes";
            string? cachedMember = await _distributedCache.GetStringAsync(key, cancellationToken);
            IEnumerable<Classes> classed;
            if(string.IsNullOrEmpty(cachedMember))
            {
                classed = await _dbSet.ToListAsync();
                if(classed is null)
                {
                    return classed;
                }
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(classed), cancellationToken);
                return classed;
            }
            classed = JsonConvert.DeserializeObject<IEnumerable<Classes>>(cachedMember);
            return classed;
            
        }

        public async Task<Classes> GetByIdAsync(string classId, CancellationToken cancellationToken = default)
        {
            string key = $"get-classes-{classId}";
            string? cachedMember = await _distributedCache.GetStringAsync(key, cancellationToken);
            Classes getid;
            if (string.IsNullOrEmpty(cachedMember)) { 
                getid = await _dbSet.FirstOrDefaultAsync(b => b.ClassId == classId);
                if(getid is null)
                {
                    return getid;
                }
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(getid), cancellationToken);
                return getid;
            }
            getid = JsonConvert.DeserializeObject<Classes>(cachedMember);
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
