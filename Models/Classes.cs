using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AttendanceManagement.Models
{
    public class Classes
    {
        [Key]
        public string ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }

        [Required]
        public string Section { get; set; }
        
        [Required]
        [ForeignKey(nameof(InchargeStaff))]
        public string InchargeStaffId { get; set; }
        public Staffs InchargeStaff { get; set; }

        [InverseProperty("Class")]
        public ICollection<Students> Students { get; set; }
    }
}
