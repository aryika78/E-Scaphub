using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class DriverMaster
    {
        //bydefault first key is considered as primary key, but if you want you can explicity define a key as pk
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Drvid { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]  //for notnull
        public string Dname { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Daddress { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Ddob { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Dgender { get; set; }

        [Column(TypeName = "varchar(15)")]
        [Required]
        public string Dphone { get; set; }


        [Column(TypeName = "varchar(250)")]
        public string Dlicimage { get; set; }


        [Column(TypeName = "varchar(250)")]
        public string Dimage { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Dvehiclenumber { get; set; }

        [Required]
        public int DAid { get; set; }

        [Required]  //this field is not for parul students
        public int Tid { get; set; }
    }
}
