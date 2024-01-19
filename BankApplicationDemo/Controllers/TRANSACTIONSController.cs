using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankApplicationDemo;

namespace BankApplicationDemo.Controllers
{
    public class TRANSACTIONSController : Controller
    {
        private BankingAppContext db = new BankingAppContext();

        // GET: TRANSACTIONS
        public ActionResult Index()
        {
            var t_TRANSACTIONS = db.T_TRANSACTIONS.Include(t => t.T_ACCOUNTS).Include(t => t.T_ACCOUNTS1);
            return View(t_TRANSACTIONS.ToList());
        }

        // GET: TRANSACTIONS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_TRANSACTIONS t_TRANSACTIONS = db.T_TRANSACTIONS.Find(id);
            if (t_TRANSACTIONS == null)
            {
                return HttpNotFound();
            }
            return View(t_TRANSACTIONS);
        }

        // GET: TRANSACTIONS/Create
        public ActionResult Create()
        {
            ViewBag.FromAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName");
            ViewBag.ToAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName");
            return View();
        }

        // POST: TRANSACTIONS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FromAccountID,ToAccountID,TransactionTime,TransactionAmout,FromAccountBalance,ToAccountBalance,ID")] T_TRANSACTIONS t_TRANSACTIONS)
        {
            if (ModelState.IsValid)
            {
                var fromAccount = db.T_ACCOUNTS.Find(t_TRANSACTIONS.FromAccountID);
                var toAccount = db.T_ACCOUNTS.Find(t_TRANSACTIONS.ToAccountID);
                var fromAccountBal = fromAccount.AccountBalance;
                var toAccountBal = toAccount.AccountBalance;
                var amount = t_TRANSACTIONS.TransactionAmout;
                t_TRANSACTIONS.FromAccountBalance = fromAccount.AccountBalance - amount;
                t_TRANSACTIONS.ToAccountBalance = toAccount.AccountBalance + amount;
                t_TRANSACTIONS.TransactionTime = DateTime.Now;
                if(fromAccount.AccountBalance<=0 || amount >10000 || fromAccount.AccountBalance < amount || t_TRANSACTIONS.ToAccountID == t_TRANSACTIONS.FromAccountID)
                {
                    return RedirectToAction("Index");
                }

                db.T_TRANSACTIONS.Add(t_TRANSACTIONS);

                fromAccount.AccountBalance = fromAccountBal - amount;
                toAccount.AccountBalance = toAccountBal + amount;
                db.T_ACCOUNTS.AddOrUpdate(fromAccount);
                db.T_ACCOUNTS.AddOrUpdate(toAccount);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.FromAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName", t_TRANSACTIONS.FromAccountID);
            ViewBag.ToAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName", t_TRANSACTIONS.ToAccountID);
            return View(t_TRANSACTIONS);
        }

        // GET: TRANSACTIONS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_TRANSACTIONS t_TRANSACTIONS = db.T_TRANSACTIONS.Find(id);
            if (t_TRANSACTIONS == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName", t_TRANSACTIONS.FromAccountID);
            ViewBag.ToAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName", t_TRANSACTIONS.ToAccountID);
            return View(t_TRANSACTIONS);
        }

        // POST: TRANSACTIONS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FromAccountID,ToAccountID,TransactionTime,TransactionAmout,FromAccountBalance,ToAccountBalance,ID")] T_TRANSACTIONS t_TRANSACTIONS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_TRANSACTIONS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName", t_TRANSACTIONS.FromAccountID);
            ViewBag.ToAccountID = new SelectList(db.T_ACCOUNTS, "AccountID", "AccountName", t_TRANSACTIONS.ToAccountID);
            return View(t_TRANSACTIONS);
        }

        // GET: TRANSACTIONS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_TRANSACTIONS t_TRANSACTIONS = db.T_TRANSACTIONS.Find(id);
            if (t_TRANSACTIONS == null)
            {
                return HttpNotFound();
            }
            return View(t_TRANSACTIONS);
        }

        // POST: TRANSACTIONS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_TRANSACTIONS t_TRANSACTIONS = db.T_TRANSACTIONS.Find(id);
            db.T_TRANSACTIONS.Remove(t_TRANSACTIONS);
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
