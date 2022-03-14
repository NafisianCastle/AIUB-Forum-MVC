﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIUB_Forum.Models.Database;

namespace AIUB_Forum.Controllers
{
    public class AnswerCommentsController : Controller
    {
        private AIUB_ForumEntities2 db = new AIUB_ForumEntities2();

        // GET: AnswerComments
        public ActionResult Index()
        {
            var answerComments = db.AnswerComments.Include(a => a.Answer).Include(a => a.User);
            return View(answerComments.ToList());
        }

        // GET: AnswerComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerComment answerComment = db.AnswerComments.Find(id);
            if (answerComment == null)
            {
                return HttpNotFound();
            }
            return View(answerComment);
        }

        // GET: AnswerComments/Create
        public ActionResult Create()
        {
            ViewBag.AnsId = new SelectList(db.Answers, "AnsId", "Body");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Location");
            return View();
        }

        // POST: AnswerComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnsCmntId,AnsId,Score,Text,Date,UserId")] AnswerComment answerComment)
        {
            if (ModelState.IsValid)
            {
                db.AnswerComments.Add(answerComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnsId = new SelectList(db.Answers, "AnsId", "Body", answerComment.AnsId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Location", answerComment.UserId);
            return View(answerComment);
        }

        // GET: AnswerComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerComment answerComment = db.AnswerComments.Find(id);
            if (answerComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnsId = new SelectList(db.Answers, "AnsId", "Body", answerComment.AnsId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Location", answerComment.UserId);
            return View(answerComment);
        }

        // POST: AnswerComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnsCmntId,AnsId,Score,Text,Date,UserId")] AnswerComment answerComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answerComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnsId = new SelectList(db.Answers, "AnsId", "Body", answerComment.AnsId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Location", answerComment.UserId);
            return View(answerComment);
        }

        // GET: AnswerComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerComment answerComment = db.AnswerComments.Find(id);
            if (answerComment == null)
            {
                return HttpNotFound();
            }
            return View(answerComment);
        }

        // POST: AnswerComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnswerComment answerComment = db.AnswerComments.Find(id);
            db.AnswerComments.Remove(answerComment);
            db.SaveChanges();
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