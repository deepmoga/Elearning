using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace onlineportal.Areas.AdminPanel.Models
{
    public class Lecture
    {
        public int id { get; set; }
        [Display(Name = "Department Id")]
        public int Moduleid { get; set; }

        public virtual Module Modules { get; set; }
        public string TestName { get; set; }
        public string VideoLink { get; set; }
        public string Description { get; set; }
    }
}