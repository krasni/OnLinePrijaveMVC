using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnLinePrijaveMVC.Models
{
    public class OnLinePrijaveContext : DbContext
    {
        public OnLinePrijaveContext() : base("OnLinePrijaveDB")
        {
        }

        public DbSet<Distributer> Distributeri { get; set; }
        public DbSet<DistributerFile> DistributeriFondoviFiles { get; set; }
        public DbSet<MirovinskiFond> MirovinskiFondovi { get; set; }
        public DbSet<MirovinskiFondFile> MirovinskiFondoviFiles { get; set; }
        public DbSet<Broker> Brokeri { get; set; }
        public DbSet<BrokerFile> BrokerFiles { get; set; }
    }
}