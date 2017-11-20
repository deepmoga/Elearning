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
    public class LecturesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: AdminPanel/Lectures
        public async Task<ActionResult> Index()
        {
            var lectures = db.Lectures.Include(l => l.Modules);
            return View(await lectures.ToListAsync());
        }

        // GET: AdminPanel/Lectures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // GET: AdminPanel/Lectures/Create
        public ActionResult Create()
        {
            ViewBag.Moduleid = new SelectList(db.Modules, "id", "ModuleName");
            return View();
        }

        // POST: AdminPanel/Lectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Moduleid,TestName,VideoLink,Description")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                db.Lectures.Add(lecture);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Moduleid = new SelectList(db.Modules, "id", "ModuleName", lecture.Moduleid);
            return View(lecture);
        }

        // GET: AdminPanel/Lectures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            ViewBag.Moduleid = new SelectList(db.Modules, "id", "ModuleName", lecture.Moduleid);
            return View(lecture);
        }

        // POST: AdminPanel/Lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Moduleid,TestName,VideoLink,Description")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecture).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Moduleid = new SelectList(db.Modules, "id", "ModuleName", lecture.Moduleid);
            return View(lecture);
        }

        // GET: AdminPanel/Lectures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: AdminPanel/Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lecture lecture = await db.Lectures.FindAsync(id);
            db.Lectures.Remove(lecture);
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
