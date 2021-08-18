using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessMaster.CustomValidation
{
    public class CountryAttribute : ValidationAttribute,IClientModelValidator
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //}
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-country","Invalid country. Valid values are USA, UK, and India.");
        }
    }
}
