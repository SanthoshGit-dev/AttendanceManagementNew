using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepo _staffRepo;
        public StaffsController(IStaffRepo staffrepo)
        {
            _staffRepo = staffrepo;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddStaff(AddStaff addstaff)
        {
           var staff = await _staffRepo.AddStaffAsync(addstaff);
           return Ok(staff);
        }
        [HttpPut("update/{StaffId}")]
        public async Task<IActionResult> UpdateStaff(UpdateStaff updatestaff, string StaffId)
        {
            var staff = await _staffRepo.UpdateStaffAsync(StaffId, updatestaff);
            return Ok(staff);
        }
        [HttpDelete("delete/{StaffId}")]
        public async Task<IActionResult> RemoveStaff(string StaffId)
        {
            await _staffRepo.DeleteStaffAsync(StaffId);
            return Ok();
        }
    }
}
