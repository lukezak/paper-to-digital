using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaperToDigital.ViewModels
{
    public class UploadViewModel
    {
        public UploadViewModel()
        {
            date = DateTime.UtcNow.ToString("yyyy-MM-dd");
        }

        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }

        public string SelectId { get; set; }
        public string lang { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string date { get; set; }
        public string subject { get; set; }
        public string category { get; set; }
        public string rights { get; set; }
        public int? page { get; set; }
        public string unique { get; set; }
        public string hdncoords { get; set; }

        public string creator
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Creator"]; }
        }
        public string publisher
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Publisher"]; }
        }

        public IEnumerable<UploadViewModel> GetLang()
        {
            return new List<UploadViewModel>
            {
                new UploadViewModel() {SelectId = "en-AU", lang = "en-AU"},
                new UploadViewModel() {SelectId = "en-US", lang = "en-US"},
            };
        }
    }
}