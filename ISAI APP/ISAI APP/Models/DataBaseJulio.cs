namespace ISAI_APP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataBaseJulio : DbContext
    {
        public DataBaseJulio()
            : base("name=DataBaseJulio")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<INEData> INELogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .Property(e => e.Error)
                .IsUnicode(false);
        }
    }
}
