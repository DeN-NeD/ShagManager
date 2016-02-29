using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataValidate
{
    public class ContactValidate
    {
        //проверка email
        public bool MailValidate(string email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            return RegexMatching(pattern, email);
        }
        /*
         * 123-456-7890
         * (123) 456-7890
         * 123 456 7890
         * 123.456.7890
         * +91 (123) 456-7890
         */
        //проверка телефона
        public bool PhoneValidate(string phone)
        {
           // ((\(\d{3}\)?)|(\d{3}))([\s-./]?)(\d{3})([\s-./]?)(\d{4})
            string pattern = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$";
            return RegexMatching(pattern, phone);
        }
        private bool RegexMatching(string pattern, string input)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input.Trim());
            return match.Success;
        }
    }
}
