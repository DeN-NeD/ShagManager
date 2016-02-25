using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShagModel
{
    public class Manager
    {
        public Manager()
        {
            Contracts = new HashSet<Contract>();
            DayTasks = new HashSet<DayTask>();
        }
        [Key]
        public int Id { get; set; }
        public Person Person { get; set; }
        public virtual Credential Credential { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<DayTask> DayTasks { get; set; }
    }
}
