namespace QuizWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exams", "SkillsMeasured");
        }

        public override void Down()
        {
            AddColumn("dbo.Exams", "SkillsMeasured", c => c.String());
        }
    }
}