using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnLinePrijaveMVC.Models
{
    public enum VrstaPrijaveMirovinskiFond
    {
        [Display(Name = "Stjecanje zvanja ovlaštenog upravitelja mirovinskim fondovima")]
        PrijavaUpraviteljMirovinskimFondom,
        [Display(Name = "Stjecanje kvalifikacija za upravljanje mirovinskim osiguravajućim društvom")]
        PrijavaStjecanjeKvalifikacijeZaUpravljanjeMirovinskimFondom
    }
}