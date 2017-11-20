using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onlineportal.Areas.AdminPanel.Models
{
    public class Module
    {
        public int id { get; set; }
        public string ModuleName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string ModuleFee { get; set; }

        [Display(Name = "Course Name")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }

    }
}