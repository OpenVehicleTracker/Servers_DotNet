namespace dotnet.openvehicletracker.org.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fleets", "Organization_Id", c => c.Int());
            AddColumn("dbo.Vehicles", "Fleet_Id", c => c.Int());
            AlterColumn("dbo.Fleets", "Name", c => c.String(maxLength: 80));
            AlterColumn("dbo.Organizations", "Name", c => c.String(maxLength: 80));
            AlterColumn("dbo.Vehicles", "Name", c => c.String(maxLength: 80));
            CreateIndex("dbo.Fleets", "Organization_Id");
            CreateIndex("dbo.Vehicles", "Fleet_Id");
            AddForeignKey("dbo.Fleets", "Organization_Id", "dbo.Organizations", "Id");
            AddForeignKey("dbo.Vehicles", "Fleet_Id", "dbo.Fleets", "Id");

            CreateIndex("dbo.Organizations", "Name", unique: true);
            CreateIndex("dbo.Fleets", new[] { "Organization_Id", "Name" }, unique: true);
            CreateIndex("dbo.Vehicles", new[] { "Fleet_Id", "Name" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Organizations", new[] { "Name" });
            DropIndex("dbo.Fleets", new[] { "Organization_Id", "Name" });
            DropIndex("dbo.Vehicles", new[] { "Fleet_Id", "Name" });
            
            DropForeignKey("dbo.Vehicles", "Fleet_Id", "dbo.Fleets");
            DropForeignKey("dbo.Fleets", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.Vehicles", new[] { "Fleet_Id" });
            DropIndex("dbo.Fleets", new[] { "Organization_Id" });
            AlterColumn("dbo.Vehicles", "Name", c => c.String());
            AlterColumn("dbo.Organizations", "Name", c => c.String());
            AlterColumn("dbo.Fleets", "Name", c => c.String());
            DropColumn("dbo.Vehicles", "Fleet_Id");
            DropColumn("dbo.Fleets", "Organization_Id");
        }
    }
}
