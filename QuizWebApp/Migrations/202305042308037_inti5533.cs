namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inti5533 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "SkillsMeasured", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exams", "SkillsMeasured");
        }
    }
}
