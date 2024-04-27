using System.ComponentModel.DataAnnotations;

namespace TestVersion.Data
{
    public class CustomerValuationAgent :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string a =Convert.ToString(value);
            if(a.Length > 10) {
                return new ValidationResult(this.ErrorMessage = "dai hon 10");
            }
            return ValidationResult.Success;
        }
    }
}
