namespace TaskReminder.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemindDaysBefore : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tasks", "RemindDaysBefore", c => c.Int());
            AddColumn("TaskTemplates", "RemindDaysBefore", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("TaskTemplates", "RemindDaysBefore");
            DropColumn("Tasks", "RemindDaysBefore");
        }
    }
}
