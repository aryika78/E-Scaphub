using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewaste_Vs2022.Models
{
    public class CartMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cartid { get; set; }

        [Required]
        public int SCid { get; set; }

        [Required]
        public int Pid { get; set; }

        [Required]
        public int SCQty { get; set; }
    }
}
