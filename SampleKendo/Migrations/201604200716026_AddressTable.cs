namespace SampleKendo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressDesc = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "PersonId", "dbo.People");
            DropIndex("dbo.Addresses", new[] { "PersonId" });
            DropTable("dbo.Addresses");
        }
    }
}
