using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class ProductCategoryMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Catid { get; set; }


        [Column(TypeName = "varchar(100)")]
        [Required]  //for notnull
        public string Catname { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]  //for notnull
        public string Catimage { get; set; }
    }
}
