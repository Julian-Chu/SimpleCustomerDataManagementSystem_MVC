namespace SimpleCustomerDataManagementSystem_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using Validations;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人:IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext=null)
        {
            if (!檢查單一客戶聯絡人Email是唯一())
            {
                yield return new ValidationResult("Email不為唯一");
                yield break;
            }
            yield return ValidationResult.Success;
        }
        private IDbSet<客戶聯絡人> _dbSet;

        public 客戶聯絡人(IDbSet<客戶聯絡人> dbSet)
        {
            _dbSet = dbSet;
        }

        public 客戶聯絡人() : base()
        {
            _dbSet = new 客戶資料Entities().客戶聯絡人;
        }
        private bool 檢查單一客戶聯絡人Email是唯一()
        {
            var Emails = _dbSet.Where(contact => contact.Id != this.Id && contact.客戶Id == this.客戶Id && contact.Email == this.Email);

            return Emails.ToList().Count == 0 ? true : false;
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [手機號碼]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }


    }
}
