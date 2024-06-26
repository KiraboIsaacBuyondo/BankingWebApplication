﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingWebApplication.Models;

namespace BankingWebApplication.Controllers
{
    public class AccountsController : Controller
    {
        private KirJemBankingEntities db = new KirJemBankingEntities();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,AccountNumber,AccountHolderName,AccountPin,Email,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,AccountNumber,AccountHolderName,AccountPin,Email,Balance")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        /*        // POST: Accounts/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public ActionResult DeleteConfirmed(int id)
                {
                    Account account = db.Accounts.Find(id);
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
        */

        // GET: Accounts/Deposit
        public ActionResult Deposit()
        {
            return View();
        }

        // GET: Accounts/Withdraw
        public ActionResult Withdraw()
        {
            return View();
        }

        // GET: Accounts/TransferFunds
        public ActionResult TransferFunds()
        {
            return View();
        }


        // POST: Accounts/Deposit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind(Include = "AccountNumber,Amount")] DepositViewModel model)
        {
            return View(model);
        }


        // POST: Accounts/Withdraw
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw([Bind(Include = "AccountNumber,Amount")] WithdrawViewModel model)
        {
            return View(model);
        }

        // POST: Accounts/TransferFunds
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransferFunds([Bind(Include = "SenderAccountNumber, ReceiverAccountNumber,Amount")] TransferFunds model)
        {
            return View(model);
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
