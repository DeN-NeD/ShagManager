using ShagModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShagManager
{
    public class ManagerContext : DbContext
    {
        public ManagerContext() :
            base(@"data source=.\SQLEXPRESS1303;
                   initial catalog=ManagementDB;
                   integrated security=True;
                   MultipleActiveResultSets=True;
                   App=EntityFramework")
        {

        }

        //ОСНОВНЫЕ СУЩНОСТИ
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Manager> Managers { get; set; }
        //ДОПОЛНИТЕЛЬНЫЕ АТРИБУТЫ
        public DbSet<Employment> Employments { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Specialisation> Specialisations { get; set; }
        public DbSet<StudyForm> StudyForms { get; set; }
        //КОНТРАКТЫ
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStatus> ContractStatuses { get; set; }
        //БЕЗОПАСНОСТЬ
        public DbSet<AccessOption> AccessOptions { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        //ЗАДАЧИ
        public DbSet<DayTask> DayTasks { get; set; }
        public DbSet<DayTaskStatus> DayTaskStatuses { get; set; }
        public DbSet<DayTaskType> DayTaskTypes { get; set; }
    }
}
