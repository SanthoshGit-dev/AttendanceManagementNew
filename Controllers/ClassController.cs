using AttendanceManagement.Dbo;
using AttendanceManagement.IRepository;
using AttendanceManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AttendanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepo _classRepo;
        private readonly IDistributedCache _distributedCache;
        public ClassController(IClassRepo classrepo, ICacheService cacheService, IDistributedCache distributedcacheService)
        {
            _classRepo = classrepo;
            _distributedCache = distributedcacheService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetClass()
        {
            CancellationToken cancellationToken = default;
            var classes = await _classRepo.GetAll(cancellationToken);
            return Ok(classes);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            CancellationToken cancellationToken = default;
            var classes = await _classRepo.GetByIdAsync(id, cancellationToken);
            return Ok(classes);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddClass(AddClass addclass)
        {
            
            var classes = await _classRepo.AddClassAsync(addclass);
           await _distributedCache.RemoveAsync("get-classes");
            return Ok(classes);
        }
        [HttpPut("update/{ClassId}")]
        public async Task<IActionResult> UpdateClass(UpdateClass updateclass, string ClassId)
        {
            var classes = await _classRepo.UpdateClassAsync(ClassId, updateclass);
            await _distributedCache.RemoveAsync("get-classes");
            await _distributedCache.RemoveAsync($"get-classes-{ClassId}");
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
