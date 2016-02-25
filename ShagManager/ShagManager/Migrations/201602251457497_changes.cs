namespace ShagManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessOptions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AccessName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractDetail = c.String(),
                        DataStart = c.DateTime(nullable: false),
                        DataExpired = c.DateTime(nullable: false),
                        Manager_Id = c.Int(),
                        Student_Id = c.Int(),
                        Specialisation_Id = c.Int(),
                        Status_Id = c.Int(),
                        StudyForm_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Managers", t => t.Manager_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Specialisations", t => t.Specialisation_Id)
                .ForeignKey("dbo.ContractStatus", t => t.Status_Id)
                .ForeignKey("dbo.StudyForms", t => t.StudyForm_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Specialisation_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.StudyForm_Id);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Person_FirstName = c.String(nullable: false),
                        Person_SecondName = c.String(),
                        Person_LastName = c.String(nullable: false),
                        Person_Sex = c.Boolean(nullable: false),
                        Person_Birthday = c.DateTime(nullable: false),
                        Person_Info_Phone1 = c.String(maxLength: 12),
                        Person_Info_Phone2 = c.String(maxLength: 12),
                        Person_Info_Email = c.String(),
                        Person_Info_Address = c.String(),
                        Person_Info_IsResident = c.Boolean(nullable: false),
                        Person_Info_Country = c.String(),
                        Person_PassportNumber = c.String(),
                        Person_PassportGettingDay = c.DateTime(nullable: false),
                        Person_PassportGettingPlace = c.String(),
                        Person_IIN = c.String(maxLength: 12),
                        Person_ICNumber = c.String(),
                        Person_ICGettingDay = c.DateTime(nullable: false),
                        Person_ICGettingPlace = c.String(),
                        Person_BirthCardNumber = c.String(),
                        Credential_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credentials", t => t.Credential_id)
                .Index(t => t.Credential_id);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        AccessList_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AccessOptions", t => t.AccessList_id, cascadeDelete: true)
                .Index(t => t.AccessList_id);
            
            CreateTable(
                "dbo.DayTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detail = c.String(nullable: false, storeType: "ntext"),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                        ParentTask_Id = c.Int(),
                        Manager_Id = c.Int(),
                        Status_Id = c.Int(),
                        Student_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayTasks", t => t.ParentTask_Id)
                .ForeignKey("dbo.Managers", t => t.Manager_Id)
                .ForeignKey("dbo.DayTaskStatus", t => t.Status_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.DayTaskTypes", t => t.Type_Id)
                .Index(t => t.ParentTask_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.DayTaskStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Person_FirstName = c.String(nullable: false),
                        Person_SecondName = c.String(),
                        Person_LastName = c.String(nullable: false),
                        Person_Sex = c.Boolean(nullable: false),
                        Person_Birthday = c.DateTime(nullable: false),
                        Person_Info_Phone1 = c.String(maxLength: 12),
                        Person_Info_Phone2 = c.String(maxLength: 12),
                        Person_Info_Email = c.String(),
                        Person_Info_Address = c.String(),
                        Person_Info_IsResident = c.Boolean(nullable: false),
                        Person_Info_Country = c.String(),
                        Person_PassportNumber = c.String(),
                        Person_PassportGettingDay = c.DateTime(nullable: false),
                        Person_PassportGettingPlace = c.String(),
                        Person_IIN = c.String(maxLength: 12),
                        Person_ICNumber = c.String(),
                        Person_ICGettingDay = c.DateTime(nullable: false),
                        Person_ICGettingPlace = c.String(),
                        Person_BirthCardNumber = c.String(),
                        RegisterDay = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Person_FirstName = c.String(nullable: false),
                        Person_SecondName = c.String(),
                        Person_LastName = c.String(nullable: false),
                        Person_Sex = c.Boolean(nullable: false),
                        Person_Birthday = c.DateTime(nullable: false),
                        Person_Info_Phone1 = c.String(maxLength: 12),
                        Person_Info_Phone2 = c.String(maxLength: 12),
                        Person_Info_Email = c.String(),
                        Person_Info_Address = c.String(),
                        Person_Info_IsResident = c.Boolean(nullable: false),
                        Person_Info_Country = c.String(),
                        Person_PassportNumber = c.String(),
                        Person_PassportGettingDay = c.DateTime(nullable: false),
                        Person_PassportGettingPlace = c.String(),
                        Person_IIN = c.String(maxLength: 12),
                        Person_ICNumber = c.String(),
                        Person_ICGettingDay = c.DateTime(nullable: false),
                        Person_ICGettingPlace = c.String(),
                        Person_BirthCardNumber = c.String(),
                        isIndividual = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DayTaskTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specialisations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AcademyType = c.String(),
                        Weeks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudyForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmploymentType_Id = c.Int(),
                        Place_Id = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EmploymentTypes", t => t.EmploymentType_Id)
                .ForeignKey("dbo.Places", t => t.Place_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.EmploymentType_Id)
                .Index(t => t.Place_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.EmploymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceName = c.String(),
                        Address = c.String(),
                        Info_Phone1 = c.String(maxLength: 12),
                        Info_Phone2 = c.String(maxLength: 12),
                        Info_Email = c.String(),
                        Info_Address = c.String(),
                        Info_IsResident = c.Boolean(nullable: false),
                        Info_Country = c.String(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmploymentTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ParentStudents",
                c => new
                    {
                        Parent_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Parent_Id, t.Student_Id })
                .ForeignKey("dbo.Parents", t => t.Parent_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Parent_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employments", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Employments", "Place_Id", "dbo.Places");
            DropForeignKey("dbo.Places", "Type_Id", "dbo.EmploymentTypes");
            DropForeignKey("dbo.Employments", "EmploymentType_Id", "dbo.EmploymentTypes");
            DropForeignKey("dbo.Contracts", "StudyForm_Id", "dbo.StudyForms");
            DropForeignKey("dbo.Contracts", "Status_Id", "dbo.ContractStatus");
            DropForeignKey("dbo.Contracts", "Specialisation_Id", "dbo.Specialisations");
            DropForeignKey("dbo.DayTasks", "Type_Id", "dbo.DayTaskTypes");
            DropForeignKey("dbo.DayTasks", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ParentStudents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ParentStudents", "Parent_Id", "dbo.Parents");
            DropForeignKey("dbo.Contracts", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.DayTasks", "Status_Id", "dbo.DayTaskStatus");
            DropForeignKey("dbo.DayTasks", "Manager_Id", "dbo.Managers");
            DropForeignKey("dbo.DayTasks", "ParentTask_Id", "dbo.DayTasks");
            DropForeignKey("dbo.Managers", "Credential_id", "dbo.Credentials");
            DropForeignKey("dbo.Credentials", "AccessList_id", "dbo.AccessOptions");
            DropForeignKey("dbo.Contracts", "Manager_Id", "dbo.Managers");
            DropIndex("dbo.ParentStudents", new[] { "Student_Id" });
            DropIndex("dbo.ParentStudents", new[] { "Parent_Id" });
            DropIndex("dbo.Places", new[] { "Type_Id" });
            DropIndex("dbo.Employments", new[] { "Student_Id" });
            DropIndex("dbo.Employments", new[] { "Place_Id" });
            DropIndex("dbo.Employments", new[] { "EmploymentType_Id" });
            DropIndex("dbo.DayTasks", new[] { "Type_Id" });
            DropIndex("dbo.DayTasks", new[] { "Student_Id" });
            DropIndex("dbo.DayTasks", new[] { "Status_Id" });
            DropIndex("dbo.DayTasks", new[] { "Manager_Id" });
            DropIndex("dbo.DayTasks", new[] { "ParentTask_Id" });
            DropIndex("dbo.Credentials", new[] { "AccessList_id" });
            DropIndex("dbo.Managers", new[] { "Credential_id" });
            DropIndex("dbo.Contracts", new[] { "StudyForm_Id" });
            DropIndex("dbo.Contracts", new[] { "Status_Id" });
            DropIndex("dbo.Contracts", new[] { "Specialisation_Id" });
            DropIndex("dbo.Contracts", new[] { "Student_Id" });
            DropIndex("dbo.Contracts", new[] { "Manager_Id" });
            DropTable("dbo.ParentStudents");
            DropTable("dbo.Places");
            DropTable("dbo.EmploymentTypes");
            DropTable("dbo.Employments");
            DropTable("dbo.StudyForms");
            DropTable("dbo.ContractStatus");
            DropTable("dbo.Specialisations");
            DropTable("dbo.DayTaskTypes");
            DropTable("dbo.Parents");
            DropTable("dbo.Students");
            DropTable("dbo.DayTaskStatus");
            DropTable("dbo.DayTasks");
            DropTable("dbo.Credentials");
            DropTable("dbo.Managers");
            DropTable("dbo.Contracts");
            DropTable("dbo.AccessOptions");
        }
    }
}
