namespace QuizWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reponses",
                c => new
                {
                    reponseId = c.String(nullable: false, maxLength: 128),
                    ExamId = c.Int(nullable: false),
                    userId = c.String(),
                    questionId = c.Int(nullable: false),
                    isCorrect = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.reponseId);
        }

        public override void Down()
        {
            DropTable("dbo.Reponses");
        }
    }
}