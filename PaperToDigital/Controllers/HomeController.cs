using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PaperToDigital.ViewModels;
using PaperToDigital.Models;
using System.Security.Cryptography;

namespace PaperToDigital.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(UploadViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(UploadViewModel model)
        {
            string crop;

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = model.file;
                var fileName = Guid.NewGuid();
                string ext = System.IO.Path.GetExtension(file.FileName);
                string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["imgfolder"]);
                string ocrpath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["tesseract"]);

                if (file != null && file.ContentLength > 0)
                {
                    if (model.hdncoords != null && model.hdncoords.Length > 0)
                    {
                        crop = model.hdncoords;
                    }
                    else
                    {
                        crop = "";
                    }

                    ImageResizer.ImageJob i = new ImageResizer.ImageJob(file, path + fileName + ext, new ImageResizer.Instructions("format=jpg&quality=90&maxwidth=2200" + crop));
                    i.CreateParentDirectory = true;
                    i.Build();

                    System.Diagnostics.Process si = new System.Diagnostics.Process();
                    si.StartInfo.WorkingDirectory = ocrpath;
                    si.StartInfo.UseShellExecute = false;
                    si.StartInfo.FileName = "cmd.exe";
                    si.StartInfo.Arguments = "/c tesseract " + "\"" + path + "\\" + fileName + ext + "\" \"" + path + "\\" + fileName + "\" -l eng";
                    si.StartInfo.CreateNoWindow = true;
                    si.StartInfo.RedirectStandardInput = true;
                    si.StartInfo.RedirectStandardOutput = true;
                    si.StartInfo.RedirectStandardError = true;
                    si.Start();
                    si.StandardOutput.ReadToEnd();
                    si.Close();
                }

                string md5new = ComputeMD5Hash(path + fileName + ext);
                string ocrtext = ReadOcrFile(path + fileName + ".txt");

                using (DigitalContext db = new DigitalContext())
                {
                    db.Digitals.Add(new Digital
                        {
                            unique = model.unique,
                            title = model.title,
                            desc = model.desc,
                            date = model.date,
                            language = model.lang,
                            creator = model.creator,
                            publisher = model.publisher,
                            subject = model.subject,
                            category = model.category,
                            rights = model.rights,
                            ocr = ocrtext,
                            image = fileName,
                            md5 = md5new,
                            page = model.page
                        });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private string ComputeMD5Hash(object filePath)
        {
            try
            {
                using (var stream = new FileStream((string)filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var md5gen = new MD5CryptoServiceProvider())
                    {
                        md5gen.ComputeHash(stream);
                        String md5Hash = BitConverter.ToString(md5gen.Hash).Replace("-", "").ToLower();
                        return (md5Hash);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex.ToString());
            }
        }

        private string ReadOcrFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String line = sr.ReadToEnd();
                    return (line);
                }
            }
            catch (Exception ex)
            {
                return (ex.ToString());
            }
        }
    }
}