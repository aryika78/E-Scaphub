using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class AreaMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Aid { get; set; }


        [Column(TypeName = "varchar(50)")]
        [Required]  //for notnull
        public string Areaname { get; set; }
    }
}
