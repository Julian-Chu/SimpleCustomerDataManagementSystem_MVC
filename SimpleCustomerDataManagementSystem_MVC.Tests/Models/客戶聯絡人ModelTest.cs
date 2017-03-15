using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCustomerDataManagementSystem_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SimpleCustomerDataManagementSystem_MVC.Tests.Models
{
    [TestClass]
    public class 客戶聯絡人ModelTest
    {
        客戶聯絡人MetaData 客戶聯絡人;
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
    }
}
