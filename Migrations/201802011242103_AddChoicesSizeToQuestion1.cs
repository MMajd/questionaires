namespace HelwanQuestionnaires.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChoicesSizeToQuestion1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "ChoicesSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ChoicesSize", c => c.Int());
        }
    }
}
