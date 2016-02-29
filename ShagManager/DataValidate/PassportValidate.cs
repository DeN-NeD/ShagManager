using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataValidate
{
    public class PassportValidate
    {

        //http://yvision.kz/post/114938
        public bool CodeValidate(string IIN, bool isIndividual, DateTime birthday, bool sex, bool isResident)
        {
            if (!LengthValidate(IIN))
                return false;
            if (!NumericValueValidate(IIN))
                return false;

            switch (isIndividual)
            {
                //физическое лицо
                case true:
                    if (!FirstPartValidate(IIN, birthday))
                        return false;
                    if (!SexValidate(IIN, sex))
                        return false;
                    if (!CenturyValidate(IIN, birthday))
                        return false;
                    break;
                //юридическое лицо
                case false:
                    if (!DateValidate(IIN))
                        return false;
                    if (ResidentValidate(IIN, isResident))
                        return false;
                    break;
            }
            if (!CheckSummValidate1(IIN))
                return false;
            return true;
        }
        public bool LengthValidate(string IIN)
        {
            if (IIN.Length != 12)
            {
                return false;
            }
            else
                return true;
        }
        public bool NumericValueValidate(string IIN)
        {
            Regex regexIIN = new Regex("[0-9]{12}");
            if (!regexIIN.IsMatch(IIN))
            {
                return false;
            }
            else
                return true;
        }
        public bool FirstPartValidate(string IIN, DateTime birthday)
        {
            string year = birthday.Year.ToString().Substring(2, 2);
            string month = string.Empty;
            if (birthday.Month < 10)
            {
                month = string.Format("0{0}", birthday.Month);
            }
            else
            {
                month = string.Format("{0}", birthday.Month);
            }
            string day = string.Empty;
            if (birthday.Day < 10)
            {
                day = string.Format("0{0}", birthday.Day);
            }
            else
            {
                day = string.Format("{0}", birthday.Day);
            }


            string firstPart = string.Format("{0}{1}{2}", year, month, day);
            //  Console.WriteLine(firstPart + " " + IIN.Substring(0, 6));
            if (IIN.Substring(0, 6).Equals(firstPart))
                return true;
            else
                return false;
        }
        private bool SexValidate(string IIN, bool sex)
        {
            int sexValue = Int32.Parse(IIN.Substring(6, 1));

            int sexOrigin = 0;
            if (sex)
                sexOrigin = 1;
            else
                sexOrigin = 0;
            //проверка
            if ((sexValue % 2) != sexOrigin)
                return true;
            else
                return false;
        }
        private bool CenturyValidate(string IIN, DateTime birthday)
        {
            int sexValue = Int32.Parse(IIN.Substring(6, 1));
            int validateValue = (sexValue / 2);

            if (birthday.Year < (1800 + validateValue * 100) || birthday.Year > (1900 + validateValue * 100))
                return false;
            else
                return true;
        }
        private bool DateValidate(string IIN)
        {
            int dataValue = Int32.Parse(IIN.Substring(2, 4));
            if (dataValue > 12)
                return false;
            else
                return true;
        }
        private bool ResidentValidate(string IIN, bool isResident)
        {
            var residentValue = Int32.Parse(IIN.Substring(4, 5));
            if (residentValue < 4 || residentValue > 6 || (residentValue == 4 && !isResident) || (residentValue == 5 && isResident))
                return false;
            else
                return true;
        }
        /*    
         * Формируем сумму. Сумма произведения порядка разряда на его значение. 
         * То есть для 850812 сумма начнется с (1*8+2*5+3*0+4*8+5*1+5*2+...). 
         * Берем остаток от делния суммы на 11. если остаток оказывается равен 10, то идем на второй шаг. 
         * Если не равен 10, то сравниваем контрольный разряд с остатком. Если равны - ИИН корректный.
         * Сюда мы попадаем если в результате первого шага мы получили остаток 10.
         * Здесь мы тоже будем суммировать произведения значений разрядов, только на этот раз не с порядковыми номерами, а с весами разрядов. 
         * Вес формируется следующим образом - остаток от деления на 11  суммы (порядковый номер разряда + 2). 
         * То есть для 850812, первый вес равен 3, второй 4 и так далее. Если вес оказывается равным 0, то вес приравнивают к 11. ну а дальше также. берем остаток от деления получившейся суммы на 11, если получаем 10, то данный ИИН не используется. 
         * Если не 10 сравниваем с контрольным разрядом.
        */
        public bool CheckSummValidate1(string IIN)
        {
            int summ = 0;
            //string[] iinNumbers = IIN.Split(' ');
            for (int i = 0; i < 11; i++)
            {
                summ = summ + (i + 1) * Int32.Parse(IIN[i].ToString());
            }
            int balance = summ % 11;
            if (balance == 10)
            {
                summ = 0;
                for (int i = 0; i < 11; i++)
                {
                    int weight = (i + 3) % 11;
                    if (weight == 0)
                    {
                        weight = 11;
                    }
                    summ = summ + weight * Int32.Parse(IIN[i].ToString());
                }
                balance = summ % 11;
                if (balance == 10)
                    return false;
                else
                    return (balance.ToString() == IIN.Substring(11, 1));
            }
            else
                return (balance.ToString() == IIN.Substring(11, 1));
        }
        public bool CheckSummValidate2(string IIN)
        {
            //Проверяем контрольный разряд
            int[] b1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            int[] b2 = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2 };
            int[] a = new int[12];
            int checkSumm = 0;
            for (var i = 0; i < 12; i++)
            {
                a[i] = (Int32.Parse(IIN[i].ToString()));
                if (i < 11) checkSumm += a[i] * b1[i];
            }
            checkSumm = checkSumm % 11;
            if (checkSumm == 10)
            {

                checkSumm = 0;
                for (var i = 0; i < 11; i++)
                    checkSumm += a[i] * b2[i];
                checkSumm = checkSumm % 11;
            }
            if (checkSumm != a[11]) return false;
            return true;

        }
    }
}
