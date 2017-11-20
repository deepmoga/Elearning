using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using onlineportal.Areas.AdminPanel.Models;
using Admin2.Models;

namespace onlineportal.Areas.AdminPanel.Models
{
    public class Helper
    {
        dbcontext db = new dbcontext();
        public string _FileName;
        public string uploadfile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);

                    _FileName = Guid.NewGuid().ToString().Substring(0, 4) + _FileName;
                    string _path = (HttpContext.Current.Server.MapPath("/UploadedFiles/" + _FileName));

                    file.SaveAs(_path);
                }

                return _FileName;
            }
            catch (Exception e)
            {
                return "False";
            }
        }
        public string PopulateBody(string userName, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Template/ConfirmEmail.txt")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", description);
            return body;
        }
        public string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            
            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        //public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        //{
        //    dbcontext db = new dbcontext();
        //   // var office = db.OfficeDetails.ToList();

        //    using (MailMessage mailMessage = new MailMessage(office[0].Email, recepientEmail))
        //    {

        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        mailMessage.IsBodyHtml = true;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(office[0].Email,office[0].Password);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mailMessage);
        //    }
        //}
        public string SendSMS(string User, string sender, string to, string message, string type,string api)
        {
            string stringpost = "username=" + User + "&message=" + message + "&sendername=" + sender + "&smstype=" + type + "&numbers=" + to + "&apikey="+api+"";
            //Response.Write(stringpost)
            string functionReturnValue = null;
            functionReturnValue = "";

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;

                objWebRequest = (HttpWebRequest)WebRequest.Create("http://sms.officialsolutions.in/sendSMS");
                //domain name: Domain name Replace With Your Domain  
                objWebRequest.Method = "Post";

                // Response.Write(objWebRequest)

                // Use below code if you want to SETUP PROXY.
                //Parameters to pass: 1. ProxyAddress 2. Port
                //You can find both the parameters in Connection settings of your internet explorer.


                // If You are In the proxy Then You Uncomment the below lines and Enter IP And Port Number


                //System.Net.WebProxy myProxy = new System.Net.WebProxy("192.168.1.108", 6666);
                //myProxy.BypassProxyOnLocal = true;
                //objWebRequest.Proxy = myProxy;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";

                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();

                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();


                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();

                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.ToString());

            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;

            }
        }

        public string smssetting(string mobile,string Message)
        {
            var setting = db.EmailSettings.ToList();
            if (setting != null)
            {
                if (setting[0].SMSActive == true)
                {
                    SendSMS(setting[0].SMSUser, setting[0].Password, mobile, Message, "Trans", setting[0].Api);
                }
                return ("Done");
            }
            else
            {
                return ("SMS Not Activated");
            }
        }
    }
    public enum NotificationEnumeration
    {
        Success,
        Error,
        Warning
    }
  
}