namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initv : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exams", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Exams", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Exams", "Code", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exams", "Code", c => c.String());
            AlterColumn("dbo.Exams", "Description", c => c.String());
            AlterColumn("dbo.Exams", "Name", c => c.String());
        }
    }
}
