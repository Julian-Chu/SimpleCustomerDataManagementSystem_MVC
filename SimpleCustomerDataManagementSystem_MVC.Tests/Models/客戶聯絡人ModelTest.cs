using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SimpleCustomerDataManagementSystem_MVC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace SimpleCustomerDataManagementSystem_MVC.Tests.Models
{
    [TestClass]
    public class 客戶聯絡人ModelTest
    {
        private 客戶聯絡人MetaData 客戶聯絡人;
        private List<客戶聯絡人> dummyContacts;
        private IDbSet<客戶聯絡人> mockDbSet;

        [TestInitialize]
        public void Initializing()
        {
            客戶聯絡人 = new 客戶聯絡人MetaData
            {
                Id = 0,
                客戶Id = 0,
                職稱 = "Tester",
                姓名 = "TestName",
                Email = "TestUser@testmail.com",
            };

            dummyContacts = new List<客戶聯絡人>
            {
                new 客戶聯絡人 {Id = 0, 客戶Id = 0, 職稱 = "Tester0", 姓名 = "TestName0", Email="TestUser0@testmail.com"  },
                new 客戶聯絡人{Id = 1, 客戶Id = 1, 職稱 = "Tester1", 姓名 = "TestName1", Email="TestUser1@testmail.com"  },
                new 客戶聯絡人{Id = 2, 客戶Id = 2, 職稱 = "Tester2", 姓名 = "TestName2", Email="TestUser2@testmail.com"  },
            };

            //mock DbSet
            mockDbSet = Substitute.For<DbSet<客戶聯絡人>, IDbSet<客戶聯絡人>>();
            mockDbSet.Provider.Returns(dummyContacts.AsQueryable().Provider);
            mockDbSet.Expression.Returns(dummyContacts.AsQueryable().Expression);
            mockDbSet.ElementType.Returns(dummyContacts.AsQueryable().ElementType);
            mockDbSet.GetEnumerator().Returns(dummyContacts.AsQueryable().GetEnumerator());
        }

        [TestMethod]
        public void 手機號碼格式合法驗證()
        {
            //Assign
            客戶聯絡人.手機 = "0911-111111";
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶聯絡人, new ValidationContext(客戶聯絡人), validationResults, true);
            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void 手機號碼格式非法驗證()
        {
            List<string> 驗證手機號碼列表 = new List<string>
            {
                "09111-111111", //位數不符
                "0911-1111111", //位數不符
                "0911111111",  //無 "-"
                "aabbccdd",
                "0911$111111,",
            };

            foreach (string 欲驗證手機號碼 in 驗證手機號碼列表)
            {
                //Assign
                客戶聯絡人.手機 = 欲驗證手機號碼;
                //Act
                var validationResults = new List<ValidationResult>();
                var actual = Validator.TryValidateObject(客戶聯絡人, new ValidationContext(客戶聯絡人), validationResults, true);
                //Assert
                Assert.IsFalse(actual, $"Failed in {欲驗證手機號碼}");
                var errMsg = validationResults[0].ErrorMessage;
                Assert.IsTrue(errMsg.Contains("手機"), $"Failed in {欲驗證手機號碼}");
            }
        }

        [TestMethod]
        public void 檢查單一客戶聯絡人Email是唯一()
        {
            客戶聯絡人 contact = new 客戶聯絡人(mockDbSet)
            {
                Id = 10,
                客戶Id = 10,
                職稱 = "Tester10",
                姓名 = "TestName0",
                Email = "TestUser10@testmail.com"
            };

            var result = contact.Validate(null);
            Assert.IsTrue(result.Contains(ValidationResult.Success));
        }

        [TestMethod]
        public void 檢查單一客戶聯絡人Email非唯一()
        {
            客戶聯絡人 contact = new 客戶聯絡人(mockDbSet)
            {
                Id = 10,
                客戶Id = 0,
                職稱 = "Tester10",
                姓名 = "TestName10",
                Email = "TestUser0@testmail.com"
            };

            var result = contact.Validate(null);
            Assert.IsFalse(result.Contains(ValidationResult.Success));
        }
    }
}