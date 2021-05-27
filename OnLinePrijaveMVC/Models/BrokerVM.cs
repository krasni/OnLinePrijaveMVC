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
    public class BrokerVM
    {
        [Display(Name = "A) Podnosim prijavu za pristupanje ispitu za (zaokružiti):")]
        public VrstaPrijaveBroker VrstaPrijaveBroker { get; set; }

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

    public class BrokerVMValidator : AbstractValidator<BrokerVM>
    {
        public BrokerVMValidator()
        {
            RuleFor(BrokerVM => BrokerVM.VrstaPrijaveBroker).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.Ime).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.Prezime).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.DatumRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.MjestoRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.DrzavaRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.StupanjObrazovanja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.SteceniNaziv).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.Zanimanje).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(BrokerVM => BrokerVM.Ulica).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.KucniBroj).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.Grad).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(BrokerVM => BrokerVM.UlicaPrepiska).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.KucniBrojPrepiska).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.GradPrepiska).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(BrokerVM => BrokerVM.Telefon).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.Email).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").EmailAddress().WithMessage("Unesite ispravnu email adresu.");
            RuleFor(BrokerVM => BrokerVM.OIB).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB");
            RuleFor(BrokerVM => BrokerVM.SifraKandidata).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(BrokerVM => BrokerVM.IspitPolazem).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").GreaterThan(0).WithMessage("{PropertyName} mora biti pozitivan");
            RuleFor(BrokerVM => BrokerVM.IspitiPolozeniUHanfi).MaximumLength(250).WithMessage("Maksimalna dužina za ispite položene u Hanfi je 250 karaktera");
            RuleFor(BrokerVM => BrokerVM.IspitiPolozeniUOrganizacijiCFA).MaximumLength(250).WithMessage("Maksimalna dužina za ispite položene u organizaciji CFA je 250 karaktera");
        }
    }
}