using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineportal.Areas.AdminPanel.Models
{
    public class EmailSetting
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMSUser { get; set; }
        public string SMSPassword { get; set; }
        public string SenderId { get; set; }
        public string Api { get; set; }
        public bool SMSActive { get; set; }

    }
}