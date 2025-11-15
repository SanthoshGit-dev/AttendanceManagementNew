using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace AttendanceManagement.Models
{
    public class Students : Shared
    {
        [Key]
        public string StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }

        [Required]
        [ForeignKey(nameof(ClassDetails))]
        public string ClassId { get; set; }

        [Required]
        [ForeignKey(nameof(InchargeStaff))]
        public string StaffId { get; set; }

        public Staffs InchargeStaff { get; set; }
        public Classes ClassDetails { get; set; }
    }
}
