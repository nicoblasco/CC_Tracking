namespace TrackingCar.Migrations.TrackingContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        CameraID = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        Direccion = c.String(),
                        CountryID = c.Int(),
                        StateID = c.Int(),
                        CityID = c.Int(),
                        MapCoordenadaX = c.String(),
                        MapCoordenadaY = c.String(),
                        Point = c.Int(),
                        Limite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CameraID)
                .ForeignKey("dbo.Cities", t => t.CityID)
                .ForeignKey("dbo.States", t => t.StateID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        StateID = c.Int(),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.States", t => t.StateID)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        CountryID = c.Int(),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Trackings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        CameraID = c.Int(),
                        Velocidad = c.Int(nullable: false),
                        Patente = c.String(),
                        FotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cameras", t => t.CameraID)
                .Index(t => t.CameraID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trackings", "CameraID", "dbo.Cameras");
            DropForeignKey("dbo.States", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Cameras", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Cities", "StateID", "dbo.States");
            DropForeignKey("dbo.Cameras", "StateID", "dbo.States");
            DropForeignKey("dbo.Cameras", "CityID", "dbo.Cities");
            DropIndex("dbo.Trackings", new[] { "CameraID" });
            DropIndex("dbo.States", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropIndex("dbo.Cameras", new[] { "CityID" });
            DropIndex("dbo.Cameras", new[] { "StateID" });
            DropIndex("dbo.Cameras", new[] { "CountryID" });
            DropTable("dbo.Trackings");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Cameras");
        }
    }
}
