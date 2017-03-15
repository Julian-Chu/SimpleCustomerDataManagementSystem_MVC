using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCustomerDataManagementSystem_MVC.Models;

namespace SimpleCustomerDataManagementSystem_MVC.Controllers
{
    public class 客戶資料統計Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料統計
        public ActionResult Index()
        {
            var data = db.客戶資料.Where(客戶資料 => 客戶資料.是否已刪除 == false).ToList();

            foreach (var customer in data)
            {
                int 聯絡人數量 = db.客戶聯絡人.Where(contact => contact.客戶Id == customer.Id).Count();
                int 銀行帳戶數量 = db.客戶銀行資訊.Where(bank => bank.客戶Id == customer.Id).Count();

                if (db.客戶資料統計.Count(p => p.Id == customer.Id) == 0)
                    db.客戶資料統計.Add(new 客戶資料統計() { Id = customer.Id, 客戶名稱 = customer.客戶名稱, 聯絡人數量 = 聯絡人數量, 銀行帳戶數量 = 銀行帳戶數量 });
                else
                {
                    var 客戶資料統計 = db.客戶資料統計.FirstOrDefault(p => p.Id == customer.Id);
                    客戶資料統計.聯絡人數量 = 聯絡人數量;
                    客戶資料統計.銀行帳戶數量 = 銀行帳戶數量;                    
                }
            }
            db.SaveChanges();

            return View(db.客戶資料統計.ToList());
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
