using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Admin2.Models
{
    public class dbcontext:DbContext
    {
        public dbcontext() : base("dbcontext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<dbcontext, onlineportal.Migrations.Configuration>("dbcontext"));
        }
      
        public DbSet<Account>accounts{ get; set; }

        public System.Data.Entity.DbSet<onlineportal.Areas.AdminPanel.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<onlineportal.Areas.AdminPanel.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<onlineportal.Areas.AdminPanel.Models.Module> Modules { get; set; }

        public System.Data.Entity.DbSet<onlineportal.Areas.AdminPanel.Models.Lecture> Lectures { get; set; }

        public System.Data.Entity.DbSet<onlineportal.Models.Registeration> Registerations { get; set; }

        public System.Data.Entity.DbSet<onlineportal.Areas.AdminPanel.Models.EmailSetting> EmailSettings { get; set; }
    }
}