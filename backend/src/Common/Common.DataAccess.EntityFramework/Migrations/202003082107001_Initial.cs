namespace Common.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "starter.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "starter.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("starter.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("starter.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "starter.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        Age = c.Int(),
                        Street = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        Lat = c.Double(),
                        Lng = c.Double(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "starter.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(nullable: false),
                        ClaimValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("starter.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "starter.UserPhotos",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Image = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("starter.Users", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "starter.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ThemeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("starter.Users", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("starter.UserRoles", "UserId", "starter.Users");
            DropForeignKey("starter.Settings", "Id", "starter.Users");
            DropForeignKey("starter.UserPhotos", "Id", "starter.Users");
            DropForeignKey("starter.UserClaims", "UserId", "starter.Users");
            DropForeignKey("starter.UserRoles", "RoleId", "starter.Roles");
            DropIndex("starter.Settings", new[] { "Id" });
            DropIndex("starter.UserPhotos", new[] { "Id" });
            DropIndex("starter.UserClaims", new[] { "UserId" });
            DropIndex("starter.UserRoles", new[] { "RoleId" });
            DropIndex("starter.UserRoles", new[] { "UserId" });
            DropTable("starter.Settings");
            DropTable("starter.UserPhotos");
            DropTable("starter.UserClaims");
            DropTable("starter.Users");
            DropTable("starter.UserRoles");
            DropTable("starter.Roles");
        }
    }
}
