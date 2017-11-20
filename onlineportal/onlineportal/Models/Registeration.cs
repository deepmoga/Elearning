using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineportal.Models
{
    public class Registeration
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }

        public int OTP { get; set; }
        public string OTPStatus { get; set; }


    }
}