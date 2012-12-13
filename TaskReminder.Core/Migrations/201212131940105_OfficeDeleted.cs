namespace TaskReminder.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class OfficeDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("Offices", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Offices", "Deleted");
        }
    }
}
