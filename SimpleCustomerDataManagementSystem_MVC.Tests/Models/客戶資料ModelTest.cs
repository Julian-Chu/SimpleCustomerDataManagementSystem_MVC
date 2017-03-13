using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using SimpleCustomerDataManagementSystem_MVC.Models;
using System.Collections.Generic;

namespace SimpleCustomerDataManagementSystem_MVC.Tests.Models
{
    [TestClass]
    public class 客戶資料ModelTest
    {
        客戶資料MetaData 客戶資料;

        [TestInitialize]
        public void Initialize()
        {
            客戶資料 = new 客戶資料MetaData()
            {
                Id = 0,
                客戶名稱 = "test",
                統一編號 = "22222222",
                電話 = "2222222"
            };

        }

        [TestMethod]
        public void 客戶名稱欄位必填()
        {
            //Assign
            客戶資料.客戶名稱 = null;
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("客戶名稱"));
        }

        [TestMethod]
        public void 客戶名稱欄位長度不得大於50個字元()
        {
            //Assign
            客戶資料.客戶名稱 = GetStringWithLength(51);
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("欄位長度") && errMsg.Contains("50"));

            //Assign
            客戶資料.客戶名稱 = GetStringWithLength(50);
            //Act
            validationResults = new List<ValidationResult>();
            actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            //Assert    
            Assert.IsTrue(actual);

        }

        [TestMethod]
        public void 統一編號欄位必填()
        {
            //Assign
            客戶資料.統一編號 = null;
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("統一編號"));
        }

        [TestMethod]
        public void 統一編號欄位長度不得大於8個字元()
        {
            //Assign
            客戶資料.統一編號 = GetStringWithLength(9);
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("欄位長度") && errMsg.Contains("8"));

            //Assign
            客戶資料.統一編號= GetStringWithLength(8);
            //Act
            validationResults = new List<ValidationResult>();
            actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            //Assert    
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void 電話欄位必填()
        {
            //Assign
            客戶資料.電話 = null;
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("電話"));
        }

        [TestMethod]
        public void 電話欄位長度不得大於50個字元()
        {
            //Assign
            客戶資料.電話= GetStringWithLength(51);
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("欄位長度") && errMsg.Contains("50"));

            //Assign
            客戶資料.電話 = GetStringWithLength(50);
            //Act
            validationResults = new List<ValidationResult>();
            actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            //Assert    
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void 傳真欄位長度不得大於50個字元()
        {
            //Assign
            客戶資料.傳真 = GetStringWithLength(51);
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("欄位長度") && errMsg.Contains("50"));

            //Assign
            客戶資料.傳真= GetStringWithLength(50);
            //Act
            validationResults = new List<ValidationResult>();
            actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            //Assert    
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void 地址欄位長度不得大於100個字元()
        {
            //Assign
            客戶資料.地址 = GetStringWithLength(101);
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("欄位長度") && errMsg.Contains("100"));

            //Assign
            客戶資料.地址 = GetStringWithLength(100);
            //Act
            validationResults = new List<ValidationResult>();
            actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            //Assert    
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Email欄位長度不得大於250個字元()
        {
            //Assign
            客戶資料.Email =  GetStringWithLength(50)+"@"+GetStringWithLength(200);
            //Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            var errMsg = validationResults[0].ErrorMessage;
            //Assert    
            Assert.IsFalse(actual);
            Assert.IsTrue(errMsg.Contains("欄位長度") && errMsg.Contains("250"));

            //Assign
            客戶資料.Email = "test@gmail.com";
            //Act
            validationResults = new List<ValidationResult>();
            actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);
            //Assert    
            Assert.IsTrue(actual);
        }



        private string GetStringWithLength(int length)
        {
            string input="";
            for (int i = 0; i < length; i++)
            {
                input += "a";
            }
            return input;
        }

    }
}
