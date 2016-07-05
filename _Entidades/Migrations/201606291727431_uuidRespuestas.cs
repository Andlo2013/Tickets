namespace _Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uuidRespuestas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketsDetalles", "UUID", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketsDetalles", "UUID");
        }
    }
}
