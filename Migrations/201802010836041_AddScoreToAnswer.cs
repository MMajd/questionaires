namespace HelwanQuestionnaires.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreToAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Score");
        }
    }
}
