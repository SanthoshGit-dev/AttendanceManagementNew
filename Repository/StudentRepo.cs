using AttendanceManagement.AppDbContext;
using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Students> _dbSet;
        public StudentRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Students>();
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
            var studentdata = _context.Students.Include(c => c.Staff).Include(c => c.Class).Where(c => c.StudentId == student.StudentId);
            return studentdata;
        }

        public async Task DeleteClassAsync(string StudentId)
        {
            var updatest = await _dbSet.FindAsync(StudentId);
            _dbSet.Remove(updatest);
            _context.SaveChanges();
           
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
            var studentdata = _context.Students.Include(c => c.Staff).Include(c => c.Class).Where(c => c.StudentId == updatest.StudentId);
            return studentdata;

        }
    }
}
