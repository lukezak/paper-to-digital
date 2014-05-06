using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaperToDigital.Models
{
    public class Digital
    {
        [Key]
        public int Id { get; set; }
        public string unique { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string date { get; set; }
        public string language { get; set; }
        public string creator { get; set; }
        public string publisher { get; set; }
        public string subject { get; set; }
        public string category { get; set; }
        public string rights { get; set; }
        public string ocr { get; set; }
        public Guid image { get; set; }
        public string md5 { get; set; }
        public int? page { get; set; }
    }
}