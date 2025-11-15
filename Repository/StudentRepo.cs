using AttendanceManagement.AppDbContext;
using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Models;
using AttendanceManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Students> _dbSet;
        private readonly ICacheService _cacheService;
        public StudentRepo(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _dbSet = _context.Set<Students>();
            _cacheService = cacheService;
        }
        public async Task<IQueryable<Students>> AddStudentAsync(AddStudent addStudent)
        {
            Students student = new Students()
            {
               StudentId = addStudent.StudentId,
               StudentName = addStudent.StudentName,
               Email = addStudent.Email,
               PhoneNumber = addStudent.PhoneNumber,
               StaffId = addStudent.StaffId,
               ClassId = addStudent.ClassId,
               Address = addStudent.Address,
               FatherName = addStudent.FatherName,

            };
            await _dbSet.AddAsync(student);
            _context.SaveChanges();
            var studentdata = _context.Students;
            return studentdata;
        }

        public async Task DeleteClassAsync(string StudentId)
        {
            var updatest = await _dbSet.FindAsync(StudentId);
            _dbSet.Remove(updatest);
            _context.SaveChanges();
           
        }
        public async Task<IEnumerable<Students>> GetAll()
        {
            var cacheStudent = _cacheService.GetData<IEnumerable<Students>>("studentList");
            if (cacheStudent != null && cacheStudent.Count() > 0)
            {
                return cacheStudent;
            }
            var students = await _dbSet.Include(c => c.ClassDetails).Include(c => c.InchargeStaff).ToListAsync();
            return students;
        }
        public async Task<Students> GetByIdAsync(string studentId)
        {
            var getid = await _dbSet.Include(c => c.ClassDetails).Include(c => c.InchargeStaff).FirstOrDefaultAsync(b => b.StudentId == studentId);
            return getid;
        }
        public async Task<IQueryable<Students>> UpdateStudentAsync(string StudentId, UpdateStudent updateStudent)
        {
            var updatest = await _dbSet.FindAsync(StudentId);
            updatest.StudentName = updateStudent.StudentName;
            updatest.Email = updateStudent.Email;
            updatest.PhoneNumber = updateStudent.PhoneNumber;
            updatest.StaffId = updateStudent.StaffId;
            updatest.ClassId = updateStudent.ClassId;
            updatest.Address = updateStudent.Address;
            updatest.FatherName = updateStudent.FatherName;
            _dbSet.Update(updatest);
            _context.SaveChanges();
            var studentdata = _context.Students;
            return studentdata;

        }
    }
}
