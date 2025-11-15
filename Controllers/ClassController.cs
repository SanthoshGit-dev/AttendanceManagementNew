using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepo _classRepo;
        public ClassController(IClassRepo classrepo)
        {
            _classRepo = classrepo;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddClass(AddClass addclass)
        {
           var classes = await _classRepo.AddClassAsync(addclass);
           return Ok(classes);
        }
        [HttpPut("update/{ClassId}")]
        public async Task<IActionResult> UpdateClass(UpdateClass updateclass, string ClassId)
        {
            var classes = await _classRepo.UpdateClassAsync(ClassId, updateclass);
            return Ok(classes);
        }
        [HttpDelete("delete/{ClassId}")]
        public async Task<IActionResult> RemoveStaff(string ClassId)
        {
            await _classRepo.DeleteClassAsync(ClassId);
            return Ok();
        }
    }
}
