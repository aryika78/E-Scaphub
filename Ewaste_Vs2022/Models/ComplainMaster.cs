using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class ComplainMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cid { get; set; }

        public int Pid { get; set; }


        [Column(TypeName = "varchar(500)")]
        [Required]  //for notnull
        public string Cdetails { get; set; }
    }
}
