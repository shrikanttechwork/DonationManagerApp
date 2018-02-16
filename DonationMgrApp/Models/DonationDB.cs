using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DonationMgrApp.Models
{
    public class DonationDB : DbContext
    {
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
    }
}