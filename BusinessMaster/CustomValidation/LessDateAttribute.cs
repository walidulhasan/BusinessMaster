using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessMaster.CustomValidation
{
    public class LessDateAttribute : ValidationAttribute
    {
        //LessDate
        public LessDateAttribute() : base("{0} can't taken Less Then Current Date.")
        {

        }

        public override bool IsValid(object value)
        {
            DateTime propValue = Convert.ToDateTime(value);
            if (propValue > DateTime.Now)
                return true;
            else
                return false;
        }

    }
}
