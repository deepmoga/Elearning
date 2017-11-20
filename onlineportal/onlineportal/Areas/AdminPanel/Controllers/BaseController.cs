using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineportal.Areas.AdminPanel.Models;


namespace onlineportal.Areas.AdminPanel.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="autoHideNotification">Determines whether the notification will stay visible or auto-hide.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void SetNotification(string message, NotificationEnumeration notifyType, bool autoHideNotification = true)
        {
            this.TempData["Notification"] = message;
            this.TempData["NotificationAutoHide"] = (autoHideNotification) ? "true" : "false";

            switch (notifyType)
            {
                case NotificationEnumeration.Success:
                    this.TempData["NotificationCSS"] = "alert alert-success";
                    break;
                case NotificationEnumeration.Error:
                    this.TempData["NotificationCSS"] = "alert alert-danger";
                    break;
                case NotificationEnumeration.Warning:
                    this.TempData["NotificationCSS"] = "alert alert-warning";
                    break;
            }
        }
    }
}