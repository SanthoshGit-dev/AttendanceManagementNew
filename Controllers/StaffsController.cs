using AttendanceManagement.Dbo;
using AttendanceManagement.Hangfire.Email.Interface;
using AttendanceManagement.IRepository;
using AttendanceManagement.Repository;
using Hangfire;
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
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetClass()
        {
            var staffs = await _staffRepo.GetAll();
            return Ok(staffs);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var staffs = await _staffRepo.GetByIdAsync(id);
            return Ok(staffs);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddStaff(AddStaff addstaff)
        {
           var staff = await _staffRepo.AddStaffAsync(addstaff);
            var job = BackgroundJob.Enqueue<IEmailService>(x => x.SendWelcomeMail("staff@.com", $"{staff.StaffName}"));
            Console.WriteLine(job);
           return Ok(staff);
        }
        [HttpPut("update/{StaffId}")]
        public async Task<IActionResult> UpdateStaff(UpdateStaff updatestaff, string StaffId)
        {
            var staff = await _staffRepo.UpdateStaffAsync(StaffId, updatestaff);
            var job = BackgroundJob.Schedule<IManitanaceService>(x => x.SyncRecords(), TimeSpan.FromSeconds(20));
            return Ok(staff);
        }
        [HttpDelete("delete/{StaffId}")]
        public async Task<IActionResult> RemoveStaff(string StaffId)
        {
            await _staffRepo.DeleteStaffAsync(StaffId);
            RecurringJob.AddOrUpdate<IMerchService>(x => x.RemoveMerch(StaffId), Cron.Minutely);
            return Ok();
        }
    }
}
