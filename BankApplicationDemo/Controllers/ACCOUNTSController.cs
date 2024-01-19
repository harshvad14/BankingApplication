using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankApplicationDemo;

namespace BankApplicationDemo.Controllers
{
    public class ACCOUNTSController : Controller
    {
        private BankingAppContext db = new BankingAppContext();

        // GET: ACCOUNTS
        public ActionResult Index()
        {
            return View(db.T_ACCOUNTS.ToList());
        }

        // GET: ACCOUNTS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_ACCOUNTS t_ACCOUNTS = db.T_ACCOUNTS.Find(id);
            if (t_ACCOUNTS == null)
            {
                return HttpNotFound();
            }
            return View(t_ACCOUNTS);
        }

        // GET: ACCOUNTS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACCOUNTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,AccountName,AccountBalance")] T_ACCOUNTS t_ACCOUNTS)
        {
            if (ModelState.IsValid)
            {
                db.T_ACCOUNTS.Add(t_ACCOUNTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_ACCOUNTS);
        }

        // GET: ACCOUNTS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_ACCOUNTS t_ACCOUNTS = db.T_ACCOUNTS.Find(id);
            if (t_ACCOUNTS == null)
            {
                return HttpNotFound();
            }
            return View(t_ACCOUNTS);
        }

        // POST: ACCOUNTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,AccountName,AccountBalance")] T_ACCOUNTS t_ACCOUNTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_ACCOUNTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_ACCOUNTS);
        }

        // GET: ACCOUNTS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_ACCOUNTS t_ACCOUNTS = db.T_ACCOUNTS.Find(id);
            if (t_ACCOUNTS == null)
            {
                return HttpNotFound();
            }
            return View(t_ACCOUNTS);
        }

        // POST: ACCOUNTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_ACCOUNTS t_ACCOUNTS = db.T_ACCOUNTS.Find(id);
            db.T_ACCOUNTS.Remove(t_ACCOUNTS);
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
