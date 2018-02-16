using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationMgrApp.Models
{
    [Table("organisations")]
    public class Organisation
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}