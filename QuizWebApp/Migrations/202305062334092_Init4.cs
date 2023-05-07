namespace QuizWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reponses", "SessionId", c => c.Guid(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Reponses", "SessionId");
        }
    }
}