using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Dbo
{
    public class AddStaff
    {
        [Key]
        public string StaffId { get; set; }

        [Required]
        public string StaffName { get; set; }

        [Required]
        public string Subject { get; set; }

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
