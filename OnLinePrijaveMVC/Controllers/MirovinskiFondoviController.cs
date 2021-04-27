using log4net;
using OnLinePrijaveMVC.HelperClasses;
using OnLinePrijaveMVC.Models;
using OnLinePrijaveMVC.Mvc.Attributes;
using Spire.Doc;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace OnLinePrijaveMVC.Controllers
{
    public class MirovinskiFondoviController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(MirovinskiFondoviController));
        private readonly IEmailService emailService;

        public MirovinskiFondoviController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpGet]
        public ActionResult MirovinskiFondovi()
        {
            log.Info($"Browser: {Request.Browser.Browser}, Version: {Request.Browser.Version}, UserAgent: {Request.UserAgent}");

            MirovinskiFondVM mirVM = new MirovinskiFondVM();
            return View(mirVM);
        }

        [HttpPost]
        public JsonResult SaveFormData(MirovinskiFondVM mirovinskiFondVM)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["MirovinskiFond"] = mirovinskiFondVM;

                var uploadPath = System.Configuration.ConfigurationManager.AppSettings["OnLinePrijaveUploadFolder"];
                var folderName = mirovinskiFondVM.VrijemePrijave + "_" + mirovinskiFondVM.Ime + "_" + mirovinskiFondVM.Prezime + '_' + mirovinskiFondVM.SifraKandidata + "_" + "MirovinskiFond";
                var fullUploadPath = uploadPath + folderName;

                bool exists = System.IO.Directory.Exists(fullUploadPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(fullUploadPath);

                Session["FullUploadPath"] = fullUploadPath;
                return Json(new { status = true });
            }
        }

        public JsonResult UploadFiles()
        {
            var files = Request.Files;

            if (files.Count > 0)
            {
                try
                {
                    var fullUploadPath = (string)Session["FullUploadPath"];

                    string fullFileName = Path.Combine(fullUploadPath, files[0].FileName); ;

                    using (FileStream fs = new FileStream(fullFileName, FileMode.Append))
                    {
                        var bytes = GetBytes(files[0].InputStream);
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    return Json(new { status = true });
                }
                catch (Exception ex)
                {
                    log.Error("Error", ex);
                    return Json(new { status = false, message = ex.Message });
                }
            }

            return Json(new { status = false });
        }

        private byte[] GetBytes(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        [HttpGet]
        public ActionResult DownloadPDF(string DownloadToken)
        {
            try
            {
                var fullUploadPath = (string)Session["FullUploadPath"];
                var mirovinskiFondVM = (MirovinskiFondVM)Session["MirovinskiFond"];

                string templateFolder = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Template/");

                Document document = new Document();

                document.LoadFromFile(Path.Combine(templateFolder, "TemplateMirovinskiFondPotvrdaPrijave.docx"));

                foreach (FormField field in document.Sections[0].Body.FormFields)
                {
                    if (field.Type == FieldType.FieldFormCheckBox)
                    {
                        CheckBoxFormField checkBox = field as CheckBoxFormField;

                        log.Info($"Mirovinski fondovi checkbox null = {checkBox == null}");

                        if (checkBox.Name == "Upravitelj")
                            checkBox.Checked = (mirovinskiFondVM.VrstaPrijaveMirovinskiFond == VrstaPrijaveMirovinskiFond.PrijavaUpraviteljMirovinskimFondom);

                        if (checkBox.Name == "Kvalifikacija")
                            checkBox.Checked = (mirovinskiFondVM.VrstaPrijaveMirovinskiFond == VrstaPrijaveMirovinskiFond.PrijavaStjecanjeKvalifikacijeZaUpravljanjeMirovinskimFondom);
                    }
                }

                document.Replace("%ImePrezime%", string.Format($"{mirovinskiFondVM.Ime.ToUpper()} {mirovinskiFondVM.Prezime.ToUpper()}"), false, true);

                document.Replace("%DatumMjestoDrzavaRodjenja%", string.Format($"{mirovinskiFondVM.DatumRodjenja.ToString("dd.MM.yyyy.")}, {mirovinskiFondVM.MjestoRodjenja.ToUpper()}, {mirovinskiFondVM.DrzavaRodjenja.ToUpper()}"), false, true);

                document.Replace("%ObrazovanjeNazivZanimanje%", string.Format($"{mirovinskiFondVM.StupanjObrazovanja.ToUpper()}, {mirovinskiFondVM.SteceniNaziv.ToUpper()}, {mirovinskiFondVM.Zanimanje.ToUpper()}"), false, true);

                document.Replace("%AdresaPrebivalista%", string.Format($"{mirovinskiFondVM.Ulica.ToUpper()} {mirovinskiFondVM.KucniBroj.ToUpper()}, {mirovinskiFondVM.Grad.ToUpper()}"), false, true);

                document.Replace("%AdresaZaPrepisku%", string.Format($"{mirovinskiFondVM.UlicaPrepiska.ToUpper()} {mirovinskiFondVM.KucniBrojPrepiska.ToUpper()}, {mirovinskiFondVM.GradPrepiska.ToUpper()}"), false, true);

                document.Replace("%Telefon%", string.Format($"{mirovinskiFondVM.Telefon}"), false, true);

                document.Replace("%Email%", string.Format($"{mirovinskiFondVM.Email.ToUpper()}"), false, true);

                document.Replace("%OIB%", mirovinskiFondVM.OIB.ToUpper(), false, true);

                document.Replace("%SifraKandidata%", string.Format($"{mirovinskiFondVM.SifraKandidata}"), false, true);

                if (mirovinskiFondVM.IspitiPolozeniUHanfi != null)
                {
                    document.Replace("%IspitiPolozeniUHanfi%", string.Format($"{mirovinskiFondVM.IspitiPolozeniUHanfi.ToUpper()}"), false, true);
                }
                else
                {
                    document.Replace("%IspitiPolozeniUHanfi%", string.Empty, false, true);
                }

                if (mirovinskiFondVM.IspitiPolozeniUOrganizacijiCFA != null)
                {
                    document.Replace("%IspitiPolozeniUOrganizacijiCFA%", string.Format($"{mirovinskiFondVM.IspitiPolozeniUOrganizacijiCFA.ToUpper()}"), false, true);
                }
                else
                {
                    document.Replace("%IspitiPolozeniUOrganizacijiCFA%", string.Empty, false, true);

                }

                document.Replace("%DatumPrijave%", string.Format($"{DateTime.Now.ToString("dd.MM.yyyy.")}"), false, true);

                var newPdfFileName = String.Format($"{mirovinskiFondVM.VrijemePrijave}_{mirovinskiFondVM.Ime}_{mirovinskiFondVM.Prezime}_{mirovinskiFondVM.SifraKandidata}_PrijavaMirovinskiFondovi.pdf");

                document.SaveToFile(Path.Combine(fullUploadPath, newPdfFileName), FileFormat.PDF);

                using (MemoryStream stream = new MemoryStream())
                {
                    document.SaveToStream(stream, FileFormat.PDF);

                    //snimi u bazu još ovaj fajl, prijavu
                    OnLinePrijaveContext db = new OnLinePrijaveContext();
                    int mirovinskiFondID = (int)Session["MirovinskiFondID"];
                    var mirovinskiFond = db.MirovinskiFondovi.FirstOrDefault(mf => mf.MirovinskiFondId == mirovinskiFondID);
                    mirovinskiFond.MirovinskiFondFiles.Add(new MirovinskiFondFile { FilePath = (Path.Combine(fullUploadPath, newPdfFileName)) });
                    db.SaveChanges();

                    emailService.SendEmail(
                       fromDisplayName: "",
                       fromEmailAddress: "noreply@hanfa.hr",
                       toName: "",
                       toEmailAddress: mirovinskiFondVM.Email,
                       subject: "Generirani PDF dokument",
                       message: "Generirani PDF dokument",
                       attachments: new Attachment(newPdfFileName, stream.ToArray()));

                    FlashMessage.Confirmation("Na adresu elektroničke pošte navedene na obrascu prijave, dobit ćete potvrdu primitka Vaše prijave. U slučaju da predmetnu potvrdu ne zaprimite, molimo da se javite na adresu distribucija.osiguranja@hanfa.hr");
                    return RedirectToAction("MirovinskiFondovi");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SaveDataToDatabase()
        {
            try
            {
                var mirovinskiFondVM = (MirovinskiFondVM)Session["MirovinskiFond"];
                var mirovinskiFond = new MirovinskiFond();

                OnLinePrijaveContext db = new OnLinePrijaveContext();

                mirovinskiFond.VrstaPrijaveMirovinskiFond = mirovinskiFondVM.VrstaPrijaveMirovinskiFond;
                mirovinskiFond.Ime = mirovinskiFondVM.Ime;
                mirovinskiFond.Prezime = mirovinskiFondVM.Prezime;
                mirovinskiFond.DatumRodjenja = mirovinskiFondVM.DatumRodjenja;
                mirovinskiFond.MjestoRodjenja = mirovinskiFondVM.MjestoRodjenja;
                mirovinskiFond.DrzavaRodjenja = mirovinskiFondVM.DrzavaRodjenja;
                mirovinskiFond.StupanjObrazovanja = mirovinskiFondVM.StupanjObrazovanja;
                mirovinskiFond.SteceniNaziv = mirovinskiFondVM.SteceniNaziv;
                mirovinskiFond.Zanimanje = mirovinskiFondVM.Zanimanje;

                mirovinskiFond.Ulica = mirovinskiFondVM.Ulica;
                mirovinskiFond.KucniBroj = mirovinskiFondVM.KucniBroj;
                mirovinskiFond.Grad = mirovinskiFondVM.Grad;

                mirovinskiFond.UlicaPrepiska = mirovinskiFondVM.UlicaPrepiska;
                mirovinskiFond.KucniBrojPrepiska = mirovinskiFondVM.KucniBrojPrepiska;
                mirovinskiFond.GradPrepiska = mirovinskiFondVM.GradPrepiska;

                mirovinskiFond.Telefon = mirovinskiFondVM.Telefon;
                mirovinskiFond.Email = mirovinskiFondVM.Email;
                mirovinskiFond.OIB = mirovinskiFondVM.OIB;
                mirovinskiFond.SifraKandidata = mirovinskiFondVM.SifraKandidata;
                mirovinskiFond.IspitiPolozeniUHanfi = mirovinskiFondVM.IspitiPolozeniUHanfi;
                mirovinskiFond.IspitiPolozeniUOrganizacijiCFA = mirovinskiFondVM.IspitiPolozeniUOrganizacijiCFA;

                mirovinskiFond.CreatedDate = DateTime.Now;

                //dodaj fajle iz download foldera
                var fullUploadPath = (string)Session["FullUploadPath"];
                string[] filePaths = Directory.GetFiles(fullUploadPath);

                foreach (var filePath in filePaths)
                {
                    mirovinskiFond.MirovinskiFondFiles.Add(new MirovinskiFondFile { FilePath = filePath });
                }

                db.MirovinskiFondovi.Add(mirovinskiFond);
                db.SaveChanges();

                // zapamti id za poslje
                Session["MirovinskiFondID"] = mirovinskiFond.MirovinskiFondId;

                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}