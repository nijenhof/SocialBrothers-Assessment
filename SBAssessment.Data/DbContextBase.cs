using Microsoft.EntityFrameworkCore;
using SBAssessment.Data.Entities;

namespace SBAssessment.Data
{
    public class DbContextBase : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbContextBase(DbContextOptions options) : base(options)
        {
        }
    }
}
