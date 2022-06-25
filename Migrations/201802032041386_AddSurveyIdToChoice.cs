namespace HelwanQuestionnaires.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSurveyIdToChoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Choices", "SurveyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Choices", "SurveyId");
        }
    }
}
