using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SimpleCustomerDataManagementSystem_MVC.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            customer customer = new customer();
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            客戶資料 客戶資料 = new 客戶資料();
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(客戶資料, new ValidationContext(客戶資料), validationResults, true);

            Assert.IsFalse(actual);
        }
    }


    public class customer
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class 客戶資料
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
