namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "Pourcentage", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Skills", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skills", "Description", c => c.String());
            AlterColumn("dbo.Skills", "Name", c => c.String());
            DropColumn("dbo.Skills", "Pourcentage");
        }
    }
}
