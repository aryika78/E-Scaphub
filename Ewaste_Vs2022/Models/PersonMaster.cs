using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class PersonMaster
    {
        //bydefault first key is considered as primary key, but if you want you can explicity define a key as pk
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Pid { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]  //for notnull
        public string Pname { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Paddress { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Pdob { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Pgender { get; set; }

        [Column(TypeName = "varchar(15)")]
        [Required]
        public string Pphone { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Pemail { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Ppassword { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Pimage { get; set; }

        public int Pqid { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Panswer { get; set; }

        [Required]
        public int Proleid { get; set; }
    }
}
