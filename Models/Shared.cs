using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Models
{
    public class Shared
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
