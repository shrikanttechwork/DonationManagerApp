using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DonationMgrApp.Models
{
    [Table("campaigns")]
    public class Campaign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Campid { get; set; }

        [Required]
        public string CampaignName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Campaign Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Required]
        public DateTime StartDate { get; set; }

        [DisplayName("Organisation")]
        [ForeignKey("Organisation")]
        [Required]
        public int OrgId { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}