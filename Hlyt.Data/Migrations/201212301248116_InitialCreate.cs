using System.Data.Entity.Migrations;


namespace Hlyt.Data.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                     {
                         UserId = c.Int(nullable: false, identity: true),
                         Email = c.String(nullable: false),
                         FirstName = c.String(nullable: false),
                         LastName = c.String(nullable: false),
                         PasswordHash = c.String(),
                         DateCreated = c.DateTime(nullable: false),
                         LastLoginTime = c.DateTime(),
                         Activated = c.Boolean(nullable: false),
                         RoleId = c.Int(nullable: false),
                     })
                .PrimaryKey(t => t.UserId);
        }

        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
