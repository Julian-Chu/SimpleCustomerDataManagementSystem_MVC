using System;
using TechTalk.SpecFlow;
using SimpleCustomerDataManagementSystem_MVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleCustomerDataManagementSystem_MVC.Tests
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        [Given(@"在客戶名稱欄位不輸入資料")]
        public void Given在特定欄位不輸入資料()
        {
            //ScenarioContext.Current.Pending();
            ScenarioContext.Current.Set<string>(null, "客戶名稱");
        }
        
        [When(@"按下create")]
        public void When按下Create()
        {
            //var 客戶資料 = new 客戶資料();
            //客戶資料.客戶名稱 = ScenarioContext.Current.Get<string>()
        }
        
        [Then(@"顯示錯誤訊息必填")]
        public void Then顯示錯誤訊息必填()
        {
            var 客戶資料 = new 客戶資料();
            客戶資料.客戶名稱 = ScenarioContext.Current.Get<string>("客戶名稱");
            Assert.IsNull(客戶資料);
        }
    }
}
