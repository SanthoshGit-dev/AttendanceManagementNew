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
        [ForeignKey(nameof(Classes))]
        public string ClassId { get; set; }

        [Required]
        [ForeignKey(nameof(Staffs))]
        public string StaffId { get; set; }

        // Navigation Properties
        public Classes Class { get; set; }
        public Staffs Staff { get; set; }

    }
}
