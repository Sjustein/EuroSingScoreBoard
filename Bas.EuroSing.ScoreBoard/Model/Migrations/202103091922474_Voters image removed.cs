namespace Bas.EuroSing.ScoreBoard.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votersimageremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Voters", "FlagImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Voters", "FlagImage", c => c.Binary());
        }
    }
}
