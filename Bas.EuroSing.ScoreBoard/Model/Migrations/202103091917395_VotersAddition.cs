namespace Bas.EuroSing.ScoreBoard.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotersAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Voters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FlagImage = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Voters");
        }
    }
}
