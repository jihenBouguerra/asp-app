using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class AppContext : DbContext
    {
       
        public DbSet<Gouvernorat> gouvernorats { get; set; }
        public DbSet<Commune> communes { get; set; }
        public DbSet<Graph> graphs { get; set; }
        public DbSet<compte> comptes { get; set; }
        public static Authentification log { get; set; }
        public DbSet<DecideurCommune> decideursCommune { get; set; }
        public DbSet<DecideurGouvernorat> decideursGouvernorat { get; set; }
        public DbSet<Authentification> authentifications { get; set; }

        public const string cs = @"Data Source=JIHEN\JIIHEN;Initial Catalog=webApplicationDB;Integrated Security=True";

            public AppContext()
            {

                Database.Connection.ConnectionString = cs;
                Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                 Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Commune>()
                      .HasOptional<Gouvernorat>(s => s.gouvernorat)
                      .WithMany(s => s.communes)
                      .WillCascadeOnDelete(true);
             modelBuilder.Entity<DecideurGouvernorat>()
                      .HasOptional<Gouvernorat>(s => s.gouvernorat)
                      .WithMany(s => s.decideurs)
                      .WillCascadeOnDelete(true);
       modelBuilder.Entity<DecideurCommune>()
                      .HasOptional<Commune>(s => s.commune)
                      .WithMany(s => s.decideurs)
                      .WillCascadeOnDelete(true);
             modelBuilder.Entity<Authentification>()
                     .HasOptional<DecideurCommune>(s => s.decideurCommune)
                     .WithMany(s => s.authentifications)
                     .WillCascadeOnDelete(true); 

        
               
             modelBuilder.Entity<Authentification>()
                      .HasOptional<compte>(s => s.adminOuMinitre)
                      .WithMany(s => s.authentifications)
                      .WillCascadeOnDelete(true);
               /* modelBuilder.Entity<Graph>()
                        .HasOptional<Commune>(s => s.commune)
                        .WithMany(s => s.graphs)
                        .WillCascadeOnDelete(true);
                modelBuilder.Entity<Graph>()
                     .HasOptional<Gouvernorat>(s => s.gouvernorat)
                     .WithMany(s => s.graphs)
                     .WillCascadeOnDelete(true);*/
        }
        public bool mailExiste(string mail)
        {
            var resu = from b in this.decideursCommune
                       where (b.Email.Trim()).Equals(mail.Trim())
                       select b;
            if (resu != null)
                return true;
            resu = from b in this.decideursCommune
                   where (b.Email.Trim()).Equals(mail.Trim())
                   select b;
            if (resu != null)
                return true;
            else
            return false;
        }
        public void effaceGouv( Gouvernorat g)
        {
            foreach(var decideurGouvernorat in g.decideurs.ToList())

            {
                if (decideurGouvernorat.authentifications != null)
                {
                    foreach (var c in decideurGouvernorat.authentifications.ToList())
                    {
                        Authentification a = this.authentifications.Find(c.id);
                        this.authentifications.Remove(a);
                    }
                }
              
                this.decideursGouvernorat.Remove(decideurGouvernorat);
                this.SaveChanges();

            }

            
        }
        public bool login(Authentification a)
        {
            log= this.authentifications.SingleOrDefault(d => d.pseudo== a.pseudo && d.mdp== a.mdp);
            
            if (log!= null)
                return true;
            
                return false;
        }
        public bool pseudoExiste(string pseudo)
        {
            var resu = from b in this.authentifications
                       where (b.pseudo.Trim()).Equals(pseudo.Trim())
                       select b;
            if (resu != null)
                return true;
            return false;
        }
        public bool communeExiste(string nom)
        {
            var resu = from b in this.communes
                       where (b.nomfr.Trim()).Equals(nom.Trim())
                       select b;
            if (resu != null)
                return true;
            return false;
        }
/*public bool gouvernoratExiste(string nom)
        {
            var resu = from b in this.gouvernorats
                       where (b.nomfr.Trim()).Equals(nom.Trim())
                       select b;
            if (resu != null)
                return true;
            return false;
        }*/
        public bool cinExiste(int cin  )
        {
            var resu = from b in this.decideursCommune
                       where  b.cin == cin
                       select b;
            if (resu != null)
                return true;
             var a = from b in this.decideursGouvernorat
                       where b.cin == cin
                       select b;
            if (a != null)
                return true;
            return false;
        }
        public int getId(int cin)
        {
            if (getDc(cin) != null)
                return getDc(cin).id;
            else
                return getDg(cin).id;
                
        }
        public DecideurCommune getDc(int cin)
        {
             
            DecideurCommune dc = this.decideursCommune.SingleOrDefault(d =>d.cin == cin);
            return dc;
        }
        public compte getAM(int cin)
        {

            compte dc = this.comptes.SingleOrDefault(d => d.cin == cin);
            return dc;
        }
        public DecideurGouvernorat getDg(int cin)
        {

            DecideurGouvernorat dg = this.decideursGouvernorat.SingleOrDefault(d => d.cin == cin);
            return dg;
        }

    }
}