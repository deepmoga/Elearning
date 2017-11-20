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
    public class CategoriesController :BaseController
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: AdminPanel/Categories
        public async Task<ActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }

        // GET: AdminPanel/Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: AdminPanel/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CategoryName,Description,Image,CreatedBy")] Category category, HttpPostedFileBase file,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Image = help.uploadfile(file);
                    db.Categories.Add(category);
                    await db.SaveChangesAsync();
                    this.SetNotification("Your Category Created Succesfully", NotificationEnumeration.Success);
                    return RedirectToAction("Index");

                }


                return View(category);
            }
            catch (Exception e)
            {
                this.SetNotification(e.Message, NotificationEnumeration.Error);
                throw;
            }
        }

        // GET: AdminPanel/Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            img = category.Image;
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminPanel/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,CategoryName,Description,Image,CreatedBy")] Category category, HttpPostedFileBase file,Helper help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Image = file != null ? help.uploadfile(file) : img;
                    db.Entry(category).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception e)
            {
                this.SetNotification(e.Message, NotificationEnumeration.Warning);
                throw;
            }
        }

        // GET: AdminPanel/Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminPanel/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            this.SetNotification("Category Deleted Suceesfully", NotificationEnumeration.Success);
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
