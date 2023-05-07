namespace QuizWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Reponses");
            AddColumn("dbo.Reponses", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Reponses", "reponseId", c => c.String());
            AddPrimaryKey("dbo.Reponses", "Id");
        }

        public override void Down()
        {
            DropPrimaryKey("dbo.Reponses");
            AlterColumn("dbo.Reponses", "reponseId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Reponses", "Id");
            AddPrimaryKey("dbo.Reponses", "reponseId");
        }
    }
}