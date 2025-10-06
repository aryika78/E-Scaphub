using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class ProductSubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SCid { get; set; }


        [Column(TypeName = "varchar(100)")]
        [Required]  //for notnull
        public string SCname { get; set; }


        [Column(TypeName = "varchar(250)")]
        [Required]  //for notnull
        public string SCimage { get; set; }

        public int SCpriceperunit { get; set; }

        public int Catid { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Required]  //for notnull
        public string SCdesc { get; set; }
    }
}
