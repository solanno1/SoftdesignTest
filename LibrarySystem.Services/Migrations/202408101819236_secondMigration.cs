namespace LibrarySystem.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "RentDate", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Rentals", "ReturnDate", c => c.DateTime(precision: 0));
            DropColumn("dbo.Books", "Description");
            DropColumn("dbo.Rentals", "RentalDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "RentalDate", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Books", "Description", c => c.String(unicode: false));
            DropColumn("dbo.Rentals", "ReturnDate");
            DropColumn("dbo.Rentals", "RentDate");
        }
    }
}
