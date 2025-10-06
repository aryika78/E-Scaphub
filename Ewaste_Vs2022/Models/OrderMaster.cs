using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class OrderMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ordid { get; set; }

        [Column(TypeName = "DateTime2")]
        [Required]  //for notnull
        public DateTime Orddate { get; set; }
        [Required]
        public int Pid { get; set; }
        [Required]
        public int Drvid { get; set; }
        [Required]
        public int Areaid { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required]
        public string Ordstatus { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal Ordtotal { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal Ordgrandtotal { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required]
        public string Ordpaymentmode { get; set; }
    }
}
