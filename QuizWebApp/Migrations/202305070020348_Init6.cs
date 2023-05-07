namespace QuizWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init6 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Reponses", "questionId");
            AddForeignKey("dbo.Reponses", "questionId", "dbo.Questions", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Reponses", "questionId", "dbo.Questions");
            DropIndex("dbo.Reponses", new[] { "questionId" });
        }
    }
}