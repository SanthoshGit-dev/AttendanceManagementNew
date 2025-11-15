using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepo _classRepo;
        public ClassController(IClassRepo classrepo, ICacheService cacheService)
        {
            _classRepo = classrepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetClass()
        {
            var classes = await _classRepo.GetAll();
            return Ok(classes);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var classes = await _classRepo.GetByIdAsync(id);
            return Ok(classes);
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
