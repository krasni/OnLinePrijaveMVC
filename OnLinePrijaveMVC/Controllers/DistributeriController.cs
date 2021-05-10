using log4net;
using Newtonsoft.Json.Linq;
using OnLinePrijaveMVC.HelperClasses;
using OnLinePrijaveMVC.Models;
using OnLinePrijaveMVC.Mvc.Attributes;
using Spire.Doc;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace OnLinePrijaveMVC.Controllers
{
    public class DistributeriController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(DistributeriController));
        private readonly IEmailService emailService;

        public DistributeriController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpGet]
        public ActionResult Distributeri()
        {
            log.Info($"Browser: {Request.Browser.Browser}, Version: {Request.Browser.Version}, UserAgent: {Request.UserAgent}");

            DistributerVM distVM = new DistributerVM();
            return View(distVM);
        }

        [HttpPost]
        public JsonResult SaveFormData(DistributerVM distributerVM)
        {
            //Validate Google recaptcha here
            var response = Request["g-recaptcha-response"];
            string secretKey = ConfigurationManager.AppSettings["GoogleReCaptchaSecretKey"];
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            if ((!ModelState.IsValid) || !status)
            {
                if (!status)
                    ModelState.AddModelError("GoogleReCaptcha greška", "Google reCaptcha validacija nije uspjela");
                return Json(new { status = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["Distributer"] = distributerVM;

                var uploadPath = System.Configuration.ConfigurationManager.AppSettings["OnLinePrijaveUploadFolder"];
                var folderName = distributerVM.VrijemePrijave + "_" + distributerVM.Ime + "_" + distributerVM.Prezime + '_' + distributerVM.SifraKandidata + "_" + "Distributeri";
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
                var distributerVM = (DistributerVM)Session["Distributer"];

                string templateFolder = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Template/");

                log.Info($"Template folder distributeri:{templateFolder}");

                Document document = new Document();

                document.LoadFromFile(Path.Combine(templateFolder, "TemplateDistributerPotvrdaPrijave.docx"));

                document.Replace("%ImePrezime%", string.Format($"{distributerVM.Ime.ToUpper()} {distributerVM.Prezime.ToUpper()}"), false, true);
                document.Replace("%OIB%", distributerVM.OIB.ToUpper(), false, true);
                document.Replace("%DatumMjestoDrzavaRodjenja%", string.Format($"{distributerVM.DatumRodjenja.ToString("dd.MM.yyyy.")}, {distributerVM.MjestoRodjenja}, {distributerVM.DrzavaRodjenja.ToUpper()}"), false, true);

                document.Replace("%AdresaBrojTelefona%", string.Format($"{distributerVM.Ulica.ToUpper()} {distributerVM.KucniBroj.ToUpper()}, {distributerVM.Grad.ToUpper()}, {distributerVM.BrojTelefona.ToUpper()}"), false, true);

                document.Replace("%Email%", string.Format($"{distributerVM.Email.ToUpper()}"), false, true);
                document.Replace("%StrucnaSprema%", string.Format($"{distributerVM.StrucnaSprema.ToUpper()}"), false, true);

                if (!string.IsNullOrEmpty(distributerVM.Zanimanje))
                    document.Replace("%Zanimanje%", string.Format($"{distributerVM.Zanimanje.ToUpper()}"), false, true);
                else
                    document.Replace("%Zanimanje%", "", false, true);

                if (!string.IsNullOrEmpty(distributerVM.ZaposlenKod))
                    document.Replace("%ZaposlenKod%", string.Format($"{distributerVM.ZaposlenKod.ToUpper()}"), false, true);
                else
                    document.Replace("%ZaposlenKod%", "", false, true);

                document.Replace("%IspitPolazem%", string.Format($"{distributerVM.IspitPolazem.ToString()}"), false, true);
                document.Replace("%SifraKandidata%", string.Format($"{distributerVM.SifraKandidata}"), false, true);

                document.Replace("%DatumPrijave%", string.Format($"{DateTime.Now.ToString("dd.MM.yyyy.")}"), false, true);

                foreach (FormField field in document.Sections[0].Body.FormFields)
                {
                    if (field.Type == FieldType.FieldFormCheckBox)
                    {
                        CheckBoxFormField checkBox = field as CheckBoxFormField;

                        if (checkBox.Name == "ZivotnoOsig")
                            checkBox.Checked = distributerVM.ZivotnoOsiguranje;

                        if (checkBox.Name == "NezivotnoOsig")
                            checkBox.Checked = distributerVM.NezivotnoOsiguranje;

                        if (checkBox.Name == "DistribucijeOsig")
                            checkBox.Checked = distributerVM.DistribucijaOsiguranja;

                        if (checkBox.Name == "DistribucijeReosig")
                            checkBox.Checked = distributerVM.DistribucijaReosiguranja;

                        if (checkBox.Name == "ZaRacunDrustva")
                            checkBox.Checked = distributerVM.ZastupnikUOsiguranju;

                        if (checkBox.Name == "PoNaloguStrankeOsig")
                            checkBox.Checked = distributerVM.BrokerUOsiguranju;

                        if (checkBox.Name == "PoNaloguStrankeRe")
                            checkBox.Checked = distributerVM.BrokerUReosiguranju;

                        if (checkBox.Name == "SporedniPosrednik")
                            checkBox.Checked = distributerVM.PosrednikUOsiguranju;
                    }
                }

                var newPdfFileName = String.Format($"{distributerVM.VrijemePrijave}_{distributerVM.Ime}_{distributerVM.Prezime}_{distributerVM.SifraKandidata}_PrijavaDistributeri.pdf");

                document.SaveToFile(Path.Combine(fullUploadPath, newPdfFileName), FileFormat.PDF);

                using (MemoryStream stream = new MemoryStream())
                {
                    document.SaveToStream(stream, FileFormat.PDF);

                    //snimi u bazu još ovaj fajl, prijavu
                    OnLinePrijaveContext db = new OnLinePrijaveContext();
                    int distributerID = (int)Session["DistributerID"];
                    var distributer = db.Distributeri.FirstOrDefault(d => d.DistributerId == distributerID);
                    distributer.DistributerFiles.Add(new DistributerFile { FilePath = (Path.Combine(fullUploadPath, newPdfFileName))});
                    db.SaveChanges();

                    emailService.SendEmail(
                       fromDisplayName: "",
                       fromEmailAddress: "noreply@hanfa.hr",
                       toName: "",
                       toEmailAddress: distributerVM.Email,
                       subject: "Potvrda primitka prijave za polaganje ispita za distribuciju osiguranja i/ili reosiguranja",
                       message: "Vaša prijava za polaganje ispita za distribuciju osiguranja i/ili reosiguranja je uspješno zaprimljena.",
                       attachments: new Attachment(newPdfFileName, stream.ToArray()));

                    FlashMessage.Confirmation("Na adresu elektroničke pošte navedene na obrascu prijave, dobit ćete potvrdu primitka Vaše prijave. U slučaju da predmetnu potvrdu ne zaprimite, molimo da se javite na adresu distribucija.osiguranja@hanfa.hr");
                    return RedirectToAction("Distributeri");

                }
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
                return Json(new { status = false, message = ex.Message },JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SaveDataToDatabase()
        {
            try
            {
                var distributerVM = (DistributerVM)Session["Distributer"];
                var distributer = new Distributer();

                OnLinePrijaveContext db = new OnLinePrijaveContext();

                distributer.Ime = distributerVM.Ime;
                distributer.Prezime = distributerVM.Prezime;
                distributer.OIB = distributerVM.OIB;
                distributer.DatumRodjenja = distributerVM.DatumRodjenja;
                distributer.MjestoRodjenja = distributerVM.MjestoRodjenja;
                distributer.DrzavaRodjenja = distributerVM.DrzavaRodjenja;

                distributer.Ulica = distributerVM.Ulica;
                distributer.KucniBroj = distributerVM.KucniBroj;
                distributer.Grad = distributerVM.Grad;

                distributer.BrojTelefona = distributerVM.BrojTelefona;
                distributer.Email = distributerVM.Email;
                distributer.StrucnaSprema = distributerVM.StrucnaSprema;
                distributer.Zanimanje = distributerVM.Zanimanje;
                distributer.ZaposlenKod = distributerVM.ZaposlenKod;
                distributer.IspitPolazem = distributerVM.IspitPolazem;
                distributer.SifraKandidata = distributerVM.SifraKandidata;
                distributer.ZivotnoOsiguranje = distributerVM.ZivotnoOsiguranje;
                distributer.NezivotnoOsiguranje = distributerVM.NezivotnoOsiguranje;
                distributer.DistribucijaOsiguranja = distributerVM.DistribucijaOsiguranja;
                distributer.DistribucijaReosiguranja = distributerVM.DistribucijaReosiguranja;
                distributer.ZastupnikUOsiguranju = distributerVM.ZastupnikUOsiguranju;
                distributer.BrokerUOsiguranju = distributerVM.BrokerUOsiguranju;
                distributer.BrokerUReosiguranju = distributerVM.BrokerUReosiguranju;
                distributer.PosrednikUOsiguranju = distributerVM.PosrednikUOsiguranju;
                distributer.CreatedDate = DateTime.Now;

                //dodaj fajle iz download foldera
                var fullUploadPath = (string)Session["FullUploadPath"];
                string[] filePaths = Directory.GetFiles(fullUploadPath);

                foreach (var filePath in filePaths)
                {
                    distributer.DistributerFiles.Add(new DistributerFile { FilePath = filePath });
                }

                db.Distributeri.Add(distributer);
                db.SaveChanges();

                // zapamti id za poslje
                Session["DistributerID"] = distributer.DistributerId;

                return Json(new { status = true },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DownloadIzjavaPrimjerenostPovezanost()
        {
            string fullName = Server.MapPath("~/App_Data/Izjave/IzjavaPrimjerenostPovezanost.docx");

            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "IzjavaPrimjerenostPovezanost.docx");
        }
        public ActionResult DownloadIzjavaJavnaObjavaPodatakaRegistar()
        {
            string fullName = Server.MapPath("~/App_Data/Izjave/IzjavaJavnaObjavaPodatakaRegistar.docx");

            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "IzjavaJavnaObjavaPodatakaRegistar.docx");
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}