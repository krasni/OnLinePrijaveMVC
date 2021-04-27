using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace OnLinePrijaveMVC.Models
{
    public class Distributer
    {
        [Key]
        public int DistributerId { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string OIB { get; set; }

        [Required]
        public DateTime DatumRodjenja { get; set; } = DateTime.Now;

        [Required]
        public string MjestoRodjenja { get; set; }

        [Required]
        public string DrzavaRodjenja { get; set; }

        public int? BusinessEntityId { get; set; }

        public bool Processed { get; set; } = false;

        [Required]
        public string Ulica { get; set; }

        [Required]
        public string KucniBroj { get; set; }

        [Required]
        public string Grad { get; set; }

        [Required]
        public string BrojTelefona { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string StrucnaSprema { get; set; }

        public string Zanimanje { get; set; }

        public string ZaposlenKod { get; set; }

        [Required]
        public int IspitPolazem{ get; set; } = 1;

        [Required]
        public string SifraKandidata { get; set; }

        public bool ZivotnoOsiguranje { get; set; }
        public bool NezivotnoOsiguranje { get; set; }
        public bool DistribucijaOsiguranja { get; set; }
        public bool DistribucijaReosiguranja { get; set; }
        public bool ZastupnikUOsiguranju { get; set; }
        public bool BrokerUOsiguranju { get; set; }
        public bool BrokerUReosiguranju { get; set; }
        public bool PosrednikUOsiguranju { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool Error { get; set; }

        public List<DistributerFile> DistributerFiles { get; set; }

        public Distributer()
        {
            DistributerFiles = new List<DistributerFile>();
        }
    }
}