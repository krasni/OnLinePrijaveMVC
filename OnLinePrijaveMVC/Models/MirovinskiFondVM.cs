using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace OnLinePrijaveMVC.Models
{
    public class MirovinskiFondVM
    {
        [Display(Name = "A) Podnosim prijavu za pristupanje ispitu za (zaokružiti):")]
        public VrstaPrijaveMirovinskiFond VrstaPrijaveMirovinskiFond { get; set; }

        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Display(Name = "Datum rođenja (DD.MM.GGGG)")]
        public DateTime DatumRodjenja { get; set; }

        [Display(Name = "Mjesto rođenja")]
        public string MjestoRodjenja { get; set; }

        [Display(Name = "Država rođenja")]
        public string DrzavaRodjenja { get; set; }

        [Display(Name = "Stupanj obrazovanja")]
        public string StupanjObrazovanja { get; set; }

        [Display(Name = "Stečeni naziv")]
        public string SteceniNaziv { get; set; }

        [Display(Name = "Zanimanje")]
        public string Zanimanje { get; set; }

        public string Ulica { get; set; }

        [Display(Name = "Kućni broj")]
        public string KucniBroj { get; set; }

        public string Grad { get; set; }

        [Display(Name = "Ulica")]
        public string UlicaPrepiska { get; set; }

        [Display(Name = "Kućni broj")]
        public string KucniBrojPrepiska { get; set; }

        [Display(Name = "Grad")]
        public string GradPrepiska { get; set; }


        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "OIB")]
        public string OIB { get; set; }

        [Display(Name = "Ispit polažem (navesti koji put)")]
        public int IspitPolazem { get; set; } = 1;

        [Display(Name = "Šifra kandidata")]
        public string SifraKandidata { get; set; }

        [Display(Name = "C) Podaci o ispitima položenima u organizaciji Hanfe (naziv i godina)")]
        [StringLength(250)]
        public string IspitiPolozeniUHanfi { get; set; }

        [Display(Name = "D) Podaci o ispitima položenima u organizaciji CFA Instituta")]
        [StringLength(250)]
        public string IspitiPolozeniUOrganizacijiCFA { get; set; }

        [Display(Name = "Vrijeme prijave")]
        public string VrijemePrijave { get; set; }
    }

    public class MirovinskiFondVMValidator : AbstractValidator<MirovinskiFondVM>
    {
        public MirovinskiFondVMValidator()
        {
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.VrstaPrijaveMirovinskiFond).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Ime).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Prezime).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.DatumRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.MjestoRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.DrzavaRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.StupanjObrazovanja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.SteceniNaziv).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Zanimanje).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Ulica).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.KucniBroj).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Grad).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(mirovinskiFondVM => mirovinskiFondVM.UlicaPrepiska).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.KucniBrojPrepiska).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.GradPrepiska).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Telefon).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.Email).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").EmailAddress().WithMessage("Unesite ispravnu email adresu.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.OIB).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB");

            RuleFor(mirovinskiFondVM => mirovinskiFondVM.IspitPolazem).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").GreaterThan(0).WithMessage("{PropertyName} mora biti pozitivan");


            RuleFor(mirovinskiFondVM => mirovinskiFondVM.SifraKandidata).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.IspitiPolozeniUHanfi).MaximumLength(250).WithMessage("Maksimalna dužina za ispite položene u Hanfi je 250 karaktera");
            RuleFor(mirovinskiFondVM => mirovinskiFondVM.IspitiPolozeniUOrganizacijiCFA).MaximumLength(250).WithMessage("Maksimalna dužina za ispite položene u organizaciji CFA je 250 karaktera");
        }
    }
}