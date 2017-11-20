using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onlineportal.Areas.AdminPanel.Models
{
    public class Course
    {
        public int id { get; set; }
        public string CourseName { get; set; }

        [Display (Name="Department Id")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [Display (Name ="Course Fee")]
        public string CourseFee { get; set; }
        public string Expiry { get; set; }
        public string CreateBy { get; set; }

        public virtual ICollection<Module> Modules { get; set; }

    }
}