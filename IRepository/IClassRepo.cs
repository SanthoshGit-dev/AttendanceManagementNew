using AttendanceManagement.Dbo;
using AttendanceManagement.Models;

namespace AttendanceManagement.IRepository
{
    public interface IClassRepo
    {
        public Task<IQueryable<Classes>> AddClassAsync(AddClass addClass);
        public Task<IQueryable<Classes>> UpdateClassAsync(string ClassId, UpdateClass updateclass);
        public Task DeleteClassAsync(string StaffId);

        public Task<IEnumerable<Classes>> GetAllAsync();
        public Task<Classes> GetByIdAsync(string ClassId);
    }
}
