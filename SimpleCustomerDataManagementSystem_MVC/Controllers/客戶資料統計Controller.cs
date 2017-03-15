using SimpleCustomerDataManagementSystem_MVC.Models;
using SimpleCustomerDataManagementSystem_MVC.Models.ViewModels;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleCustomerDataManagementSystem_MVC.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginVM)
        {
            if (loginVM.Username == "test" && loginVM.Password == "1234")
            {
                FormsAuthentication.RedirectFromLoginPage(loginVM.Username, false);
            }
            ViewBag.ErrorMessage = "密碼錯誤";
            return View(loginVM);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "客戶資料統計");
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