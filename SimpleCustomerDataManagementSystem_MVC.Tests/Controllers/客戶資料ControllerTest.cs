using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SimpleCustomerDataManagementSystem_MVC.Controllers;
using SimpleCustomerDataManagementSystem_MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SimpleCustomerDataManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class 客戶資料ControllerTest
    {
        private List<客戶資料> dummyCustomers;
        private IDbSet<客戶資料> mockDbSet;
        private 客戶資料Entities mockContext;

        [TestInitialize]
        public void InitializMethod()
        {
            dummyCustomers = new List<客戶資料>
            {
                new 客戶資料 { Id = 0, 客戶名稱 = "testUser0", 統一編號 = "testNum0", 電話 ="000000000" },
                new 客戶資料 { Id = 1, 客戶名稱 = "testUser1", 統一編號 = "testNum1", 電話 ="111111111" },
                new 客戶資料 { Id = 2, 客戶名稱 = "testUser2", 統一編號 = "testNum2", 電話 ="222222222" },
                new 客戶資料 { Id = 3, 客戶名稱 = "testUser3", 統一編號 = "testNum3", 電話 ="333333333" },
                new 客戶資料 { Id = 4, 客戶名稱 = "testUser4", 統一編號 = "testNum4", 電話 ="444444444" }
            };

            //mock DbSet
            mockDbSet = Substitute.For<DbSet<客戶資料>, IDbSet<客戶資料>>();
            mockDbSet.Provider.Returns(dummyCustomers.AsQueryable().Provider);
            mockDbSet.Expression.Returns(dummyCustomers.AsQueryable().Expression);
            mockDbSet.ElementType.Returns(dummyCustomers.AsQueryable().ElementType);
            mockDbSet.GetEnumerator().Returns(dummyCustomers.AsQueryable().GetEnumerator());

            //mock DbContext
            mockContext = Substitute.For<客戶資料Entities>();
            mockContext.客戶資料.Returns(mockDbSet);
        }

        [TestMethod]
        public void MockDbsetAdd()
        {
            //Assign
            //mock Dbset.Add(...)
            //mockDbset.When((x)=> x.Add(Arg.Any<客戶資料>())).Do(s => mockCustomers.Add(s.Arg<客戶資料>)); //錯誤用法

            //mockDbset.When((x) => x.Add(Arg.Any<客戶資料>())).Do(callinfo => mockCustomers.Add((客戶資料)callinfo[0]));  //正確用法
            //^^^^^^^^^^^^^^^^^^^^^ 將第一個參數轉型
            mockDbSet.Add(Arg.Do<客戶資料>(arg => dummyCustomers.Add(arg)));  //正確用法

            //Act
            mockContext.客戶資料.Add(new 客戶資料 { Id = 6, 客戶名稱 = "testUser6", 統一編號 = "testNum6", 電話 = "666666" });

            //Assert
            Assert.AreEqual(6, dummyCustomers.Count);
        }

        [TestMethod]
        public void Index_noArgs_Return_AllItems()
        {
            //Assign
            客戶資料Controller controller = new 客戶資料Controller(mockContext);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = result as ViewResult;
            var items = viewResult.Model as IEnumerable<客戶資料>;
            Assert.AreEqual(dummyCustomers.Count, items.Count());
        }

        [TestMethod]
        public void Index_KeywordSearch_Return_ItemsWithKeyword()
        {
            //Assign
            客戶資料Controller controller = new 客戶資料Controller(mockContext);
            /// only one item contains keyword
            //Act
            var result = controller.Index("USER1");
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = result as ViewResult;
            var items = viewResult.Model as IEnumerable<客戶資料>;
            Assert.AreEqual(1, items.Count());

            /// no items contain keyword
            //Act
            result = controller.Index("google");
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            viewResult = result as ViewResult;
            items = viewResult.Model as IEnumerable<客戶資料>;
            Assert.AreEqual(0, items.Count());

            // items contain keyword
            //Act
            result = controller.Index("test");
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            viewResult = result as ViewResult;
            items = viewResult.Model as IEnumerable<客戶資料>;
            Assert.AreEqual(5, items.Count());
        }
    }
}