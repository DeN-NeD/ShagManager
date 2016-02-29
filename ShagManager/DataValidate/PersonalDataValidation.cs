using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidate
{
    class PersonalDataValidation
    {
        public bool IsValidateBirthDate(DateTime _BirthDate)
        {
            var Age = DateTime.Now.Year - _BirthDate.Year;
            if (Age >= 9 && Age <= 55)
                return true;
            else
                return false;
        }
    }
}
