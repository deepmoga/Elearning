using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin2.Models;
using onlineportal.Areas.AdminPanel.Models;

namespace onlineportal.Areas.AdminPanel.Controllers
{
    public class ModulesController : BaseController
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: AdminPanel/Modules
        public async Task<ActionResult> Index()
        {
            var modules = db.Modules.Include(m => m.Course);
            return View(await modules.ToListAsync());
        }

        // GET: AdminPanel/Modules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: AdminPanel/Modules/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "id", "CourseName");
            return View();
        }

        // POST: AdminPanel/Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,ModuleName,Description,Image,ModuleFee,CourseId")] Module module, HttpPostedFileBase file,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    module.Image = help.uploadfile(file);
                    db.Modules.Add(module);
                    await db.SaveChangesAsync();
                     this.SetNotification("Module Create Suceessfully", NotificationEnumeration.Success);
                    return RedirectToAction("Index");
                }

                ViewBag.CourseId = new SelectList(db.Courses, "id", "CourseName", module.CourseId);
                return View(module);
            }
            catch (Exception e)
            {
                this.SetNotification(e.Message, NotificationEnumeration.Error);
                throw;
            }
        }

        // GET: AdminPanel/Modules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            img = module.Image;
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "id", "CourseName", module.CourseId);
            return View(module);
        }

        // POST: AdminPanel/Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,ModuleName,Description,Image,ModuleFee,CourseId")] Module module, HttpPostedFileBase file,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    module.Image = file != null ? help.uploadfile(file) : img;
                    db.Entry(module).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                     this.SetNotification("Module Edit Suceessfully", NotificationEnumeration.Success);
                    return RedirectToAction("Index");
                }
                ViewBag.CourseId = new SelectList(db.Courses, "id", "CourseName", module.CourseId);
                return View(module);
            }
            catch (Exception e)
            {
                this.SetNotification(e.Message, NotificationEnumeration.Error);
                throw;
            }
        }

        // GET: AdminPanel/Modules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: AdminPanel/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Module module = await db.Modules.FindAsync(id);
            db.Modules.Remove(module);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
