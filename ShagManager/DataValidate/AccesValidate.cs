using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShagManager;
using ShagModel;

namespace DataValidate
{
    class AccesValidate
    {
        public bool IsAuthorisation(string Login, string Pass)
        {
            using(var db = new ManagerContext())
            {
                if (db.Credentials.FirstOrDefault(c => c.Login == Login && c.Password == Pass) == null)
                    return false;
                return true;
            }
        }
        public Manager GetManager(string Login)
        {
            using (var db = new ManagerContext())
            {
                return db.Managers.FirstOrDefault(m => m.Credential.Login == Login);
            }
        }

    }
}
