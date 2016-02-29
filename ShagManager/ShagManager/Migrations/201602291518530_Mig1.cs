namespace ShagManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Managers", "Person_isIndividual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Person_isIndividual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Parents", "Person_isIndividual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parents", "Person_isIndividual");
            DropColumn("dbo.Students", "Person_isIndividual");
            DropColumn("dbo.Managers", "Person_isIndividual");
        }
    }
}
