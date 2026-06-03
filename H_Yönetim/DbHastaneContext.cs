using System;
using System.Data.Entity;

namespace H_Yonetim
{
    public class DbHastaneContext : DbContext
    {
        public DbHastaneContext() : base("name=DbHastaneContext")
        {
        }

        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
    }
}