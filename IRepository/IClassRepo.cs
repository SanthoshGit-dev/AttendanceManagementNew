using AttendanceManagement.Dbo;
using AttendanceManagement.Models;

namespace AttendanceManagement.IRepository
{
    public interface IClassRepo
    {
        public Task<IQueryable<Classes>> AddClassAsync(AddClass addClass);
        public Task<Classes> UpdateClassAsync(string ClassId, UpdateClass updateclass);
        public Task DeleteClassAsync(string StaffId);

        public Task<IEnumerable<Classes>> GetAll(CancellationToken cancellationToken);
        public Task<Classes> GetByIdAsync(string ClassId, CancellationToken cancellationToken);
    }
}
