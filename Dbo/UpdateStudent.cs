using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Dbo
{
    public class UpdateStudent
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public string ClassId { get; set; }

        [Required]
        public string StaffId { get; set; }

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
