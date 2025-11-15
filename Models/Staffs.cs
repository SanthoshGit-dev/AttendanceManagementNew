using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace AttendanceManagement.Models
{
    public class Staffs : Shared
    {
        [Key]
        public string StaffId { get; set; }

        [Required]
        public string StaffName { get; set; }

        [Required]
        public string Subject { get; set; }

    }
}
