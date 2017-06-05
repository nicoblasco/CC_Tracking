namespace TrackingCar.DAL
{
    using TrackingCar.Models;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class TrackingContext : DbContext
    {
        // Your context has been configured to use a 'TrackingContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TrackingCar.DAL.TrackingContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TrackingContext' 
        // connection string in the application configuration file.
        public TrackingContext() : base("GLOBAL")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<State>()
        //                .HasRequired(m => m.CountryID)
        //                .WithMany(t => t.States)
        //                .HasForeignKey(m => m.CountryID)
        //                .WillCascadeOnDelete(false);
        //}

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Tracking> Trackings { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}