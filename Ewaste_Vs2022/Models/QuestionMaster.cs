using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class QuestionMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Qid { get; set; }


        [Column(TypeName = "varchar(250)")]
        [Required]  //for notnull
        public string QuestionText { get; set; }
    }
}
