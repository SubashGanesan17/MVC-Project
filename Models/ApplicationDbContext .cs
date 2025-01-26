using System.Collections.Generic;
using System.Data.Entity;

namespace MVC_Project.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("LOGISTICSA_PREPROD") { }

        public DbSet<Registration> Registrations { get; set; }
    }
}
