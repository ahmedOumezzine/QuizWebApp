namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inti55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exams", "Code");
        }
    }
}
