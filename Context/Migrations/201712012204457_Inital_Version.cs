namespace Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital_Version : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Raffles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        cicle = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Prize_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prizes", t => t.Prize_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Prize_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        First = c.Int(nullable: false),
                        Last = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Raffles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Raffles", "Prize_Id", "dbo.Prizes");
            DropIndex("dbo.Raffles", new[] { "User_Id" });
            DropIndex("dbo.Raffles", new[] { "Prize_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Raffles");
            DropTable("dbo.Prizes");
        }
    }
}
