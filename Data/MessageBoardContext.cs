using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace iGotTheRuns.Data
{
    public class MessageBoardContext : DbContext
    {
        public MessageBoardContext()
            : base("MessageBoardConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            
            //for code first migrations (DB table changes)
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MessageBoardContext, MessageBoardMigrationsConfiguration>()
                );
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<HouseholdMember> HouseholdMembers { get; set; }
        public DbSet<FamilyHistoryConsultant> FamilyHistoryConsultants { get; set; }
    }
}