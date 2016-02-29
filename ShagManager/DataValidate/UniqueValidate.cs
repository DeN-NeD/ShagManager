using ShagManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidate
{
    class UniqueValidate
    {
        public bool IsUniqueStudentIIN(string _IIN)
        {
            using (var db = new ManagerContext())
            {
                if (db.Students.FirstOrDefault(q => q.Person.IIN == _IIN) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueParentIIN(string _IIN)
        {
            using (var db = new ManagerContext())
            {
                if (db.Parents.FirstOrDefault(q => q.Person.IIN == _IIN) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueStudentPassport(string _Passport)
        {
            using (var db = new ManagerContext())
            {
                if (db.Students.FirstOrDefault(q => q.Person.PassportNumber == _Passport) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueParentPassport(string _Passport)
        {
            using (var db = new ManagerContext())
            {
                if (db.Parents.FirstOrDefault(q => q.Person.PassportNumber == _Passport) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueBirthCardNumber(string _BirthCardNumber)
        {
            using (var db = new ManagerContext())
            {
                if (db.Students.FirstOrDefault(q => q.Person.BirthCardNumber == _BirthCardNumber) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueStudentICardNumber(string _ICNumber)
        {
            using (var db = new ManagerContext())
            {
                if (db.Students.FirstOrDefault(q => q.Person.ICNumber == _ICNumber) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueParentICardNumber(string _ICNumber)
        {
            using (var db = new ManagerContext())
            {
                if (db.Parents.FirstOrDefault(q => q.Person.ICNumber == _ICNumber) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniquePlaceName(string _PlaceName)
        {
            using (var db = new ManagerContext())
            {
                if (db.Places.FirstOrDefault(q => q.PlaceName == _PlaceName) == null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsUniqueManagerLogin(string _ManagerLogin)
        {
            using (var db = new ManagerContext())
            {
                if (db.Credentials.FirstOrDefault(q => q.Login == _ManagerLogin) == null)
                    return true;
                else
                    return false;
            }
        }
    }
}
