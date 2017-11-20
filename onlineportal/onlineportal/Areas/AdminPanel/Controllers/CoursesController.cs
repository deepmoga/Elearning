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
    public class CoursesController : BaseController
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: AdminPanel/Courses
        public async Task<ActionResult> Index()
        {
            var courses = db.Courses.Include(c => c.Category);
            return View(await courses.ToListAsync());
        }

        // GET: AdminPanel/Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: AdminPanel/Courses/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "id", "CategoryName");
            return View();
        }

        // POST: AdminPanel/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CourseName,CategoryId,Image,Description,CourseFee,Expiry,CreateBy")] Course course, HttpPostedFileBase file,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    course.Image = help.uploadfile(file);
                    db.Courses.Add(course);
                    await db.SaveChangesAsync();
                    this.SetNotification("Course Created Successfully", NotificationEnumeration.Success);
                    return RedirectToAction("Index");
                }

                ViewBag.CategoryId = new SelectList(db.Categories, "id", "CategoryName", course.CategoryId);
                return View(course);
            }
            catch (Exception e)
            {
                this.SetNotification(e.Message, NotificationEnumeration.Error);
                throw;
            }
        }

        // GET: AdminPanel/Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            img = course.Image;
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "id", "CategoryName", course.CategoryId);
            return View(course);
        }

        // POST: AdminPanel/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,CourseName,CategoryId,Image,Description,CourseFee,Expiry,CreateBy")] Course course, HttpPostedFileBase file,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    course.Image = file != null ? help.uploadfile(file) : img;
                    db.Entry(course).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    this.SetNotification("Course Edit Suceessfully", NotificationEnumeration.Success);
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "id", "CategoryName", course.CategoryId);
                return View(course);
            }
            catch (Exception e)
            {
                this.SetNotification(e.Message, NotificationEnumeration.Error);
                throw;
            }
        }

        // GET: AdminPanel/Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: AdminPanel/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            db.Courses.Remove(course);
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
