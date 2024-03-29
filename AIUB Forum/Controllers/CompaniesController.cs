﻿using AIUB_Forum.Models.Database;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AIUB_Forum.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AIUB_ForumEntities2 _db = new AIUB_ForumEntities2();
        [Authorize]
        [HttpGet]
        public ActionResult CompanyRegister()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult CompanyRegister(Company company)
        {
            if (!ModelState.IsValid) return View(company);
            _db.Companies.Add(company);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Companies
        public ActionResult Index()
        {
            var companies = _db.Companies.Include(c => c.User);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = _db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "Location");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,UserId,JobPostCount,CompanyName,Location,CDescription,CCategory")] Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Companies.Add(company);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_db.Users, "UserId", "Location", company.UserId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = _db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "Location", company.UserId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,UserId,JobPostCount,CompanyName,Location,CDescription,CCategory")] Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(company).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_db.Users, "UserId", "Location", company.UserId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = _db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var company = _db.Companies.Find(id);
            if (company != null) _db.Companies.Remove(company);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
