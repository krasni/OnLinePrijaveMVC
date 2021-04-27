using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace OnLinePrijaveMVC.Models
{
    public class MirovinskiFond
    {
        [Key]
        public int MirovinskiFondId { get; set; }

        [Required]
        public VrstaPrijaveMirovinskiFond VrstaPrijaveMirovinskiFond { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public DateTime DatumRodjenja { get; set; } = DateTime.Now;

        [Required]
        public string MjestoRodjenja { get; set; }

        [Required]
        public string DrzavaRodjenja { get; set; }

        [Required]
        public string StupanjObrazovanja { get; set; }

        [Required]
        public string SteceniNaziv { get; set; }

        [Required]
        public string Zanimanje { get; set; }

        public int? BusinessEntityId { get; set; }

        public bool Processed { get; set; } = false;

        [Required]
        public string Ulica { get; set; }

        [Required]
        public string KucniBroj { get; set; }

        [Required]
        public string Grad { get; set; }

        [Required]
        public string UlicaPrepiska { get; set; }

        [Required]
        public string KucniBrojPrepiska { get; set; }

        [Required]
        public string GradPrepiska { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string OIB { get; set; }

        [Required]
        public string SifraKandidata { get; set; }

        [StringLength(250)]
        public string IspitiPolozeniUHanfi { get; set; }

        [StringLength(250)]
        public string IspitiPolozeniUOrganizacijiCFA { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public List<MirovinskiFondFile> MirovinskiFondFiles { get; set; }

        [Required]
        public bool Error { get; set; }

        public MirovinskiFond()
        {
            MirovinskiFondFiles = new List<MirovinskiFondFile>();
        }
    }
}