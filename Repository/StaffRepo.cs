using AttendanceManagement.AppDbContext;
using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Models;
using AttendanceManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Repository
{
    public class StaffRepo : IStaffRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;
        private readonly DbSet<Staffs> _dbSet;
        public StaffRepo(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _dbSet = _context.Set<Staffs>();
            _cacheService = cacheService;
        }
        public async Task<Staffs> AddStaffAsync(AddStaff addStaff)
        {
            Staffs staffs = new Staffs()
            {
                StaffId = addStaff.StaffId,
                StaffName = addStaff.StaffName,
                Subject = addStaff.Subject,
                Email = addStaff.Email,
                Address = addStaff.Address,
                PhoneNumber = addStaff.PhoneNumber,
                FatherName = addStaff.FatherName,
            };
            await _dbSet.AddAsync(staffs);
            _context.SaveChanges();
            return staffs;
        }
        public async Task<IEnumerable<Staffs>> GetAll()
        {
            var cacheStaff = _cacheService.GetData<IEnumerable<Staffs>>("staffList");
            if (cacheStaff != null && cacheStaff.Count() > 0)
            {
                return cacheStaff;
            }
            var staff = await _dbSet.ToListAsync();
            var expiryTime = DateTimeOffset.Now.AddMinutes(2);
            _cacheService.SetData("staffList", staff, expiryTime);
            return staff;
        }

        public async Task<Staffs> GetByIdAsync(string staffId)
        {
            var getid = await _dbSet.FirstOrDefaultAsync(b => b.StaffId == staffId);
            return getid;
        }
        public async Task DeleteStaffAsync(string StaffId)
        {
            var updatest = await _dbSet.FindAsync(StaffId);
            _dbSet.Remove(updatest);
            _context.SaveChanges();
        }

        public async Task<Staffs> UpdateStaffAsync(string StaffId, UpdateStaff updateStaff)
        {
            var updatest = await _dbSet.FindAsync(StaffId);
            updatest.StaffName = updateStaff.StaffName;
            updatest.Subject = updateStaff.Subject;
            updatest.Email = updateStaff.Email;
            updatest.Address = updateStaff.Address;
            updatest.PhoneNumber = updateStaff.PhoneNumber;
            updatest.FatherName = updateStaff.FatherName;
            _dbSet.Update(updatest);
            _context.SaveChanges();
            return updatest;

        }
    }
}
