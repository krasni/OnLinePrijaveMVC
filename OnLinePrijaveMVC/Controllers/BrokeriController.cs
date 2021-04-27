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
    public class BrokeriController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(BrokeriController));
        private readonly IEmailService emailService;

        public BrokeriController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpGet]
        public ActionResult Brokeri()
        {
            log.Info($"Browser: {Request.Browser.Browser}, Version: {Request.Browser.Version}, UserAgent: {Request.UserAgent}");

            BrokerVM mirVM = new BrokerVM();
            return View(mirVM);
        }

        [HttpPost]
        public JsonResult SaveFormData(BrokerVM BrokerVM)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["Broker"] = BrokerVM;

                var uploadPath = System.Configuration.ConfigurationManager.AppSettings["OnLinePrijaveUploadFolder"];

                var folderName = BrokerVM.VrijemePrijave + "_" + BrokerVM.Ime + "_" + BrokerVM.Prezime + '_' + BrokerVM.SifraKandidata + "_" + "Broker";
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
                var brokerVM = (BrokerVM)Session["Broker"];

                string templateFolder = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Template/");

                Document document = new Document();

                document.LoadFromFile(Path.Combine(templateFolder, "TemplateBrokerPotvrdaPrijave.docx"));

                foreach (FormField field in document.Sections[0].Body.FormFields)
                {
                    if (field.Type == FieldType.FieldFormCheckBox)
                    {
                        CheckBoxFormField checkBox = field as CheckBoxFormField;

                        if (checkBox.Name == "L1")
                            checkBox.Checked = (brokerVM.VrstaPrijaveBroker == VrstaPrijaveBroker.PrijavaL1);

                        if (checkBox.Name == "L2")
                            checkBox.Checked = (brokerVM.VrstaPrijaveBroker == VrstaPrijaveBroker.PrijavaL2);

                        if (checkBox.Name == "L3")
                            checkBox.Checked = (brokerVM.VrstaPrijaveBroker == VrstaPrijaveBroker.PrijavaL3);
                    }
                }

                document.Replace("%ImePrezime%", string.Format($"{brokerVM.Ime.ToUpper()} {brokerVM.Prezime.ToUpper()}"), false, true);

                document.Replace("%DatumMjestoDrzavaRodjenja%", string.Format($"{brokerVM.DatumRodjenja.ToString("dd.MM.yyyy.")}, {brokerVM.MjestoRodjenja.ToUpper()}, {brokerVM.DrzavaRodjenja.ToUpper()}"), false, true);

                document.Replace("%ObrazovanjeNazivZanimanje%", string.Format($"{brokerVM.StupanjObrazovanja.ToUpper()}, {brokerVM.SteceniNaziv.ToUpper()}, {brokerVM.Zanimanje.ToUpper()}"), false, true);

                document.Replace("%AdresaPrebivalista%", string.Format($"{brokerVM.Ulica.ToUpper()} {brokerVM.KucniBroj.ToUpper()}, {brokerVM.Grad.ToUpper()}"), false, true);

                document.Replace("%AdresaZaPrepisku%", string.Format($"{brokerVM.UlicaPrepiska.ToUpper()} {brokerVM.KucniBrojPrepiska.ToUpper()}, {brokerVM.GradPrepiska.ToUpper()}"), false, true);

                document.Replace("%Telefon%", string.Format($"{brokerVM.Telefon}"), false, true);

                document.Replace("%Email%", string.Format($"{brokerVM.Email.ToUpper()}"), false, true);

                document.Replace("%OIB%", brokerVM.OIB.ToUpper(), false, true);

                document.Replace("%SifraKandidata%", string.Format($"{brokerVM.SifraKandidata}"), false, true);

                if (brokerVM.IspitiPolozeniUHanfi != null)
                {
                    document.Replace("%IspitiPolozeniUHanfi%", string.Format($"{brokerVM.IspitiPolozeniUHanfi.ToUpper()}"), false, true);
                }
                else
                {
                    document.Replace("%IspitiPolozeniUHanfi%", string.Empty, false, true);

                }

                if (brokerVM.IspitiPolozeniUOrganizacijiCFA != null)
                {
                    document.Replace("%IspitiPolozeniUOrganizacijiCFA%", string.Format($"{brokerVM.IspitiPolozeniUOrganizacijiCFA.ToUpper()}"), false, true);
                }
                else
                {
                    document.Replace("%IspitiPolozeniUOrganizacijiCFA%", string.Empty, false, true);
                }

                document.Replace("%DatumPrijave%", string.Format($"{DateTime.Now.ToString("dd.MM.yyyy.")}"), false, true);

                var newPdfFileName = String.Format($"{brokerVM.VrijemePrijave}_{brokerVM.Ime}_{brokerVM.Prezime}_{brokerVM.SifraKandidata}_PrijavaBrokeri.pdf");

                document.SaveToFile(Path.Combine(fullUploadPath, newPdfFileName), FileFormat.PDF);

                using (MemoryStream stream = new MemoryStream())
                {
                    document.SaveToStream(stream, FileFormat.PDF);

                    //snimi u bazu još ovaj fajl, prijavu
                    OnLinePrijaveContext db = new OnLinePrijaveContext();
                    int brokerID = (int)Session["brokerID"];
                    var broker = db.Brokeri.FirstOrDefault(b => b.BrokerId == brokerID);
                    broker.BrokerFiles.Add(new BrokerFile { FilePath = (Path.Combine(fullUploadPath, newPdfFileName)) });
                    db.SaveChanges();

                    if (brokerVM.PosaljiNaEmail)
                    {
                        log.Info("PosaljiNaEmail:" + brokerVM.PosaljiNaEmail);

                        emailService.SendEmail(
                           fromDisplayName: "",
                           fromEmailAddress: "noreply@hanfa.hr",
                           toName: "",
                           toEmailAddress: brokerVM.Email,
                           subject: "Generirani PDF dokument",
                           message: "Generirani PDF dokument",
                           attachments: new Attachment(newPdfFileName, stream.ToArray()));

                        FlashMessage.Confirmation("Dokument je poslan na email: " + brokerVM.Email);
                        return RedirectToAction("Brokeri");
                    }
                    else
                    {

                        var downloadCookie = new System.Web.HttpCookie("onLinePrijaveBrokeriDownloadPDF", DownloadToken);
                        Response.AppendCookie(downloadCookie);
                        log.Info($"Download potvrde: {newPdfFileName}");
                        return File(stream.ToArray(), "application/pdf", newPdfFileName);
                    }
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
                var BrokerVM = (BrokerVM)Session["Broker"];
                var Broker = new Broker();

                OnLinePrijaveContext db = new OnLinePrijaveContext();

                Broker.VrstaPrijaveBroker = BrokerVM.VrstaPrijaveBroker;
                Broker.Ime = BrokerVM.Ime;
                Broker.Prezime = BrokerVM.Prezime;
                Broker.DatumRodjenja = BrokerVM.DatumRodjenja;
                Broker.MjestoRodjenja = BrokerVM.MjestoRodjenja;
                Broker.DrzavaRodjenja = BrokerVM.DrzavaRodjenja;
                Broker.StupanjObrazovanja = BrokerVM.StupanjObrazovanja;
                Broker.SteceniNaziv = BrokerVM.SteceniNaziv;
                Broker.Zanimanje = BrokerVM.Zanimanje;

                Broker.Ulica = BrokerVM.Ulica;
                Broker.KucniBroj = BrokerVM.KucniBroj;
                Broker.Grad = BrokerVM.Grad;

                Broker.UlicaPrepiska = BrokerVM.UlicaPrepiska;
                Broker.KucniBrojPrepiska = BrokerVM.KucniBrojPrepiska;
                Broker.GradPrepiska = BrokerVM.GradPrepiska;

                Broker.Telefon = BrokerVM.Telefon;
                Broker.Email = BrokerVM.Email;
                Broker.OIB = BrokerVM.OIB;
                Broker.SifraKandidata = BrokerVM.SifraKandidata;
                Broker.IspitiPolozeniUHanfi = BrokerVM.IspitiPolozeniUHanfi;
                Broker.IspitiPolozeniUOrganizacijiCFA = BrokerVM.IspitiPolozeniUOrganizacijiCFA;

                Broker.CreatedDate = DateTime.Now;

                //dodaj fajle iz download foldera
                var fullUploadPath = (string)Session["FullUploadPath"];
                string[] filePaths = Directory.GetFiles(fullUploadPath);

                foreach (var filePath in filePaths)
                {
                    Broker.BrokerFiles.Add(new BrokerFile { FilePath = filePath });
                }

                db.Brokeri.Add(Broker);
                db.SaveChanges();

                // zapamti id za poslje
                Session["BrokerID"] = Broker.BrokerId;

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