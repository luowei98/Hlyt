using System.Data.Entity;
using Hlyt.Model.Security;


namespace Hlyt.Data
{
    public class HlytDataContex : DbContext
    {
        public DbSet<User> Users { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
