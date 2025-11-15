using AttendanceManagement.Dbo;
using AttendanceManagement.Models;

namespace AttendanceManagement.IRepository
{
    public interface IStudentRepo
    {
        public Task<IQueryable<Students>> AddStudentAsync(AddStudent addStudent);
        public Task<IQueryable<Students>> UpdateStudentAsync(string StudentId, UpdateStudent updateStudent);
        public Task DeleteClassAsync(string StudentId);

    }
}
