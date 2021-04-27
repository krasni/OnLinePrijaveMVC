using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace OnLinePrijaveMVC.Models
{
    public class DistributerVM
    {
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Display(Name = "OIB")]
        public string OIB { get; set; }

        [Display(Name = "Datum rođenja (DD.MM.GGGG)")]
        public DateTime DatumRodjenja { get; set; }

        [Display(Name = "Mjesto rođenja")]
        public string MjestoRodjenja { get; set; }

        [Display(Name = "Država rođenja")]
        public string DrzavaRodjenja { get; set; }

        public string Ulica { get; set; }

        [Display(Name = "Kućni broj")]
        public string KucniBroj { get; set; }

        public string Grad { get; set; }

        [Display(Name = "Broj telefona")]
        public string BrojTelefona { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Stručna sprema")]
        public string StrucnaSprema { get; set; }

        [Display(Name = "Zanimanje")]
        public string Zanimanje { get; set; }

        [Display(Name = "Zaposlen kod")]
        public string ZaposlenKod { get; set; }

        [Display(Name = "Ispit polažem (navesti koji put)")]
        public int IspitPolazem { get; set; } = 1;

        [Display(Name = "Šifra kandidata")] 
        public string SifraKandidata { get; set; }

        [Display(Name = "životnih osiguranja i investicijskih osiguranja")]
        public bool ZivotnoOsiguranje { get; set; }

        [Display(Name = "neživotnih osiguranja")]
        public bool NezivotnoOsiguranje { get; set; }

        [Display(Name = "DISTRIBUCIJE OSIGURANJA")]
        public bool DistribucijaOsiguranja { get; set; }

        [Display(Name = "DISTRIBUCIJE REOSIGURANJA")]
        public bool DistribucijaReosiguranja { get; set; }

        [Display(Name = "u ime i za račun društva za osiguranje kao zastupnik u osiguranju")]
        public bool ZastupnikUOsiguranju { get; set; }

        [Display(Name = "po nalogu stranke kao broker u osiguranju")]
        public bool BrokerUOsiguranju { get; set; }

        [Display(Name = "po nalogu stranke kao broker u reosiguranju")]
        public bool BrokerUReosiguranju { get; set; }

        [Display(Name = "kao sporedni posrednik u osiguranju")]
        public bool PosrednikUOsiguranju { get; set; }

        public string VrijemePrijave { get; set; }
    }

    public class DistributeriVMValidator : AbstractValidator<DistributerVM>
    {
        public DistributeriVMValidator()
        {
            RuleFor(distributeriVM => distributeriVM.DatumRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.Ime).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.Prezime).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.OIB).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB");

            RuleFor(distributeriVM => distributeriVM.DatumRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(distributeriVM => distributeriVM.MjestoRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.DrzavaRodjenja).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(distributeriVM => distributeriVM.Ulica).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.KucniBroj).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.Grad).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(distributeriVM => distributeriVM.BrojTelefona).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");

            RuleFor(distributeriVM => distributeriVM.Email).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").EmailAddress().WithMessage("Unesite ispravnu email adresu.");

            RuleFor(distributeriVM => distributeriVM.StrucnaSprema).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(distributeriVM => distributeriVM.IspitPolazem).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.").GreaterThan(0).WithMessage("{PropertyName} mora biti pozitivan");
            RuleFor(distributeriVM => distributeriVM.SifraKandidata).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
        }
    }
}