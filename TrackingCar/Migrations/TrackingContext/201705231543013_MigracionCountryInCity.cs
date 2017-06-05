namespace TrackingCar.Migrations.TrackingContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionCountryInCity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.States", "CountryID", "dbo.Countries");
            DropIndex("dbo.States", new[] { "CountryID" });
            AddColumn("dbo.Cities", "CountryID", c => c.Int());
            AddColumn("dbo.States", "Country_CountryID", c => c.Int());
            AddColumn("dbo.States", "Country_CountryID1", c => c.Int());
            AddColumn("dbo.States", "Country_CountryID2", c => c.Int());
            CreateIndex("dbo.Cities", "CountryID");
            CreateIndex("dbo.States", "Country_CountryID");
            CreateIndex("dbo.States", "Country_CountryID1");
            CreateIndex("dbo.States", "Country_CountryID2");
            AddForeignKey("dbo.States", "Country_CountryID1", "dbo.Countries", "CountryID");
            AddForeignKey("dbo.Cities", "CountryID", "dbo.Countries", "CountryID");
            AddForeignKey("dbo.States", "Country_CountryID", "dbo.Countries", "CountryID");
            AddForeignKey("dbo.States", "Country_CountryID2", "dbo.Countries", "CountryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "Country_CountryID2", "dbo.Countries");
            DropForeignKey("dbo.States", "Country_CountryID", "dbo.Countries");
            DropForeignKey("dbo.Cities", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.States", "Country_CountryID1", "dbo.Countries");
            DropIndex("dbo.States", new[] { "Country_CountryID2" });
            DropIndex("dbo.States", new[] { "Country_CountryID1" });
            DropIndex("dbo.States", new[] { "Country_CountryID" });
            DropIndex("dbo.Cities", new[] { "CountryID" });
            DropColumn("dbo.States", "Country_CountryID2");
            DropColumn("dbo.States", "Country_CountryID1");
            DropColumn("dbo.States", "Country_CountryID");
            DropColumn("dbo.Cities", "CountryID");
            CreateIndex("dbo.States", "CountryID");
            AddForeignKey("dbo.States", "CountryID", "dbo.Countries", "CountryID");
        }
    }
}
