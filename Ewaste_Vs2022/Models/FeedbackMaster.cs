using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class FeedbackMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Fid { get; set; }

        [Required]
        public int Pid { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Required]  //for notnull
        public string Feedbackdesc { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Feedbackdate { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string ExperienceRate { get; set; }
    }
}
