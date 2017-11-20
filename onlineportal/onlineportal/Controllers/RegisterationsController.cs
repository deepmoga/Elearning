using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineportal.Models;
using Admin2.Models;
using onlineportal.Areas.AdminPanel.Models;
using System.Net;
using System.Data.Entity;

namespace onlineportal.Controllers
{
    public class RegisterationsController : AlertsController
    {
        dbcontext db = new dbcontext();
        public static int code;
        // GET: Registerations
        public ActionResult Index()
        {
            return View();
        }

        // GET: Registerations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registerations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registerations/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="id,Name,Mobile,Email")] Registeration Register,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var phone = db.Registerations.FirstOrDefault(x => x.Mobile == Register.Mobile);
                    if (phone != null)
                    {
                        this.SetNotification("Sorry Mobile No Already Exist. Please Use Another Number", Models.NotificationEnumeration.Error);
                        return View();
                    }
                    else
                    {
                        // Generate Otp
                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                        string sRandomOTP = help.GenerateRandomOTP(6, saAllowedCharacters);
                        Register.OTP = Convert.ToInt32(sRandomOTP);
                        Register.OTPStatus = "Not Verify";
                        db.Registerations.Add(Register);
                        db.SaveChanges();
                        // Send Otp
                        help.smssetting(Register.Mobile, "Yout OTP Is : " + sRandomOTP + "");
                        return RedirectToAction("OTPVerifications", new { id = Register.id });
                    }
               
                    
                }
                return View();

               
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OTPVerifications(int? id)
        {
            TempData["code"] = Convert.ToInt32(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OTPVerifications([Bind(Include = "id,Name,Mobile,Email")] Registeration Register, Helper help,string otpvalue, int? id)
        {
            try
            {
                
                Register = db.Registerations.FirstOrDefault(x => x.id == id);
                if (ModelState.IsValid)
                {
                    if (Register.OTP == Convert.ToInt32(otpvalue))
                    {
                        Register.OTPStatus = "Verify";
                        db.Entry(Register).State = EntityState.Modified;
                        db.SaveChanges();
                        this.SetNotification("Thanks For Your Account Verification, Your Password Will Be Send On Your Email", Models.NotificationEnumeration.Success);
                        // Send Email for Password
                    }
                    else
                    {
                        this.SetNotification("Wrong OTP", Models.NotificationEnumeration.Success);

                    }


                }

                return View();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        // GET: Registerations/Edit/5
        public ActionResult Resendotp(Registeration Register,Helper help,int? id)
        {
            Register = db.Registerations.FirstOrDefault(x => x.id == id);
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sRandomOTP = help.GenerateRandomOTP(6, saAllowedCharacters);
            Register.OTP =Convert.ToInt32(sRandomOTP);

            db.Entry(Register).State = EntityState.Modified;
            db.SaveChanges();
            // Resend OTP
            help.smssetting(Register.Mobile, "Your OTP is : " + sRandomOTP + "");
            this.SetNotification("Your OTP Send On Your Phone", Models.NotificationEnumeration.Success);
            return RedirectToAction("OTPVerifications", new { id = id });
            
        }

        // POST: Registerations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registerations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registerations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
