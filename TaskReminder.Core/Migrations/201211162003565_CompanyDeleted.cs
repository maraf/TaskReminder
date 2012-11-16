namespace TaskReminder.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CompanyDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("Companies", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Companies", "Deleted");
        }
    }
}
