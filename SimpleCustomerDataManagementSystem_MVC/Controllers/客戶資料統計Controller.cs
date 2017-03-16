using PagedList;
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
        public ActionResult Index(string keyword="", int pageNo = 1)
        {
            var customers = db.客戶資料.Where(客戶資料 => 客戶資料.是否已刪除 == false && 客戶資料.客戶名稱.Contains(keyword)).ToList();
            var data = db.客戶資料統計.Where(客戶資料統計 => 客戶資料統計.客戶名稱.Contains(keyword)).AsQueryable();
            foreach (var customer in customers)
            {
                int 聯絡人數量 = db.客戶聯絡人.Where(contact => contact.客戶Id == customer.Id).Count();
                int 銀行帳戶數量 = db.客戶銀行資訊.Where(bank => bank.客戶Id == customer.Id).Count();

                if (data.Count(p => p.Id == customer.Id) == 0)
                {
                    var newItem = new 客戶資料統計[]{
                        new 客戶資料統計() { Id = customer.Id, 客戶名稱 = customer.客戶名稱, 聯絡人數量 = 聯絡人數量, 銀行帳戶數量 = 銀行帳戶數量 }
                    };

                    data.Concat(newItem);
                }

                else
                {
                    var 客戶資料統計 = data.FirstOrDefault(p => p.Id == customer.Id);
                    客戶資料統計.聯絡人數量 = 聯絡人數量;
                    客戶資料統計.銀行帳戶數量 = 銀行帳戶數量;
                }
            }
            db.SaveChanges();
            data = data.OrderBy(c => c.Id);
            ViewBag.pageNo = pageNo;
            return View(data.ToPagedList(pageNo, 5));
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