using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnLinePrijaveMVC.Models
{
    public enum VrstaPrijaveBroker
    {
        [Display(Name = "L1 Informiranje")]
        PrijavaL1,
        [Display(Name = "L2 Brokerski poslovi")]
        PrijavaL2,
        [Display(Name = "L3 Investicijsko savjetovanje")]
        PrijavaL3
    }
}