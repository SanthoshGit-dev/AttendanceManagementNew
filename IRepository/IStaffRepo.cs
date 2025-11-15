using AttendanceManagement.Dbo;
using AttendanceManagement.Models;

namespace AttendanceManagement.IRepository
{
    public interface IStaffRepo
    {
        public Task<Staffs> AddStaffAsync(AddStaff addStaff);
        public Task<Staffs> UpdateStaffAsync(string StaffId, UpdateStaff updatestaff);
        public Task DeleteStaffAsync(string StaffId);

    }
}
