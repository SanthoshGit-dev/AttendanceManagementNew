using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Dbo
{
    public class AddClass
    {
        [Key]
        public string ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        public string InchargeStaffId { get; set; }
    }
}
