using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnLinePrijaveMVC.Models
{
    public class BrokerFile
    {
        [Key]
        public int BrokerFileId { get; set; }

        [Required]
        public string FilePath {get; set;}

        [Required]
        public virtual Broker Broker { get; set; }
    }
}