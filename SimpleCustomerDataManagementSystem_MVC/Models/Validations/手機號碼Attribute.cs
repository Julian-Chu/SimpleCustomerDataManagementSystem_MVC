using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SimpleCustomerDataManagementSystem_MVC.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class 手機號碼Attribute:DataTypeAttribute
    {
        public 手機號碼Attribute(): base(DataType.Text)
        {
            ErrorMessage = "請輸入正確的手機號碼格式 09XX-XXXXXX";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string valueStr = value as string;
            return valueStr != null && Regex.Match(valueStr, @"^\d{4}-\d{6}$").Success;

        }

    }
}