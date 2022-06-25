namespace HelwanQuestionnaires.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Surveys", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Surveys", "Name", c => c.String());
        }
    }
}
