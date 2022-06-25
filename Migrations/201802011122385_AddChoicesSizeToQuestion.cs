namespace HelwanQuestionnaires.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChoicesSizeToQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ChoicesSize", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "ChoicesSize");
        }
    }
}
