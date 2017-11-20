using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineportal.Areas.AdminPanel.Models
{
    public class Category
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

    }
}