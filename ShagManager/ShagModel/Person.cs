using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShagModel
{
    //комплексный тип
    [ComplexType]
    public class Person
    {
        [Required]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool Sex { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        //КОНТАКТЫ
        public ContactInfo Info { get; set; }
        //ЛИЧНЫЕ ДАННЫЕ
        public string PassportNumber { get; set; }
        public DateTime PassportGettingDay { get; set; }
        public string PassportGettingPlace { get; set; }
        [StringLength(12, MinimumLength = 12)]
        public string IIN { get; set; }
        //СВИДЕТЕЛЬСТВО О РОЖДЕНИИ
        public string ICNumber { get; set; }
        public DateTime ICGettingDay { get; set; }
        public string ICGettingPlace { get; set; }
        public string BirthCardNumber { get; set; }
        public bool isIndividual { get; set; }
    }
}
