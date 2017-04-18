using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class AppContext : DbContext
    {
       
        public DbSet<Gouvernorat> gouvernorats { get; set; }
        public DbSet<Commune> communes { get; set; }
        public DbSet<Graph> graphs { get; set; }
        public DbSet<Decideur> decideurs { get; set; }
        public DbSet<Authentification> authentifications { get; set; }

        public const string cs = @"Data Source=JIHEN\JIIHEN;Initial Catalog=webApplicationDB;Integrated Security=True";

            public AppContext()
            {

                Database.Connection.ConnectionString = cs;
                Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                 Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());
        }
         
        
    }
}