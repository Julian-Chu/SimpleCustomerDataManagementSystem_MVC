using SimpleCustomerDataManagementSystem_MVC.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System;

namespace SimpleCustomerDataManagementSystem_MVC.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db;
        private I客戶資料Repository repo;
        public 客戶資料Controller()
        {
            db = new 客戶資料Entities();
            repo = RepositoryHelper.Get客戶資料Repository();
            
        }
        public 客戶資料Controller(客戶資料Entities dbcontext)
        {
            db = dbcontext;
        }

        public 客戶資料Controller(I客戶資料Repository iRepo)
        {
            repo = iRepo;
        }

        // GET: 客戶資料
        public ActionResult Index(string keyword = null)
        {
            //var data = db.客戶資料.Where(客戶 => 客戶.是否已刪除 == false).AsQueryable();
            var data = repo.All().Where(客戶 => 客戶.是否已刪除 == false).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                data = data.Where(customer => customer.客戶名稱.ToLower().Contains(keyword));
                return View(data.ToList());
            }
            return View(data.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost ,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶資料).State = EntityState.Modified;
                MarkedAsModified(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        protected virtual void MarkedAsModified(客戶資料 客戶資料)
        {
            db.Entry(客戶資料).State = EntityState.Modified;
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            客戶資料.是否已刪除 = true;
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