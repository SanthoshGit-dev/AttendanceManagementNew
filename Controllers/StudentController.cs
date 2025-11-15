using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentrepo)
        {
            _studentRepo = studentrepo;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddClass(AddStudent addclass)
        {
           var students = await _studentRepo.AddStudentAsync(addclass);
           return Ok(students);
        }
        [HttpPut("update/{StudentId}")]
        public async Task<IActionResult> UpdateClass(UpdateStudent updatestudent, string StudentId)
        {
            var students = await _studentRepo.UpdateStudentAsync(StudentId, updatestudent);
            return Ok(students);
        }
        [HttpDelete("delete/{ClassId}")]
        public async Task<IActionResult> RemoveStaff(string StudentId)
        {
            await _studentRepo.DeleteClassAsync(StudentId);
            return Ok();
        }
    }
}
