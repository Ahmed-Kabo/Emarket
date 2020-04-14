namespace Emarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategory : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id,Name,NumberOfProduct) VALUES(1,'Electronics',0)");
            Sql("INSERT INTO Categories (Id,Name,NumberOfProduct) VALUES (2,'Clothes',0)");
            Sql("INSERT INTO Categories (Id,Name,NumberOfProduct) VALUES (3,'Accessories',0)");
        }
        
        public override void Down()
        {
        }
    }
}
