using Microsoft.EntityFrameworkCore;
using SBAssessment.Data.Entities;

namespace SBAssessment.Data
{
    public class DbContextBase : DbContext
    {
#pragma warning disable CS8618 // db context class, automatically set.
        public DbSet<Address> Addresses { get; set; }

        public DbContextBase(DbContextOptions options) : base(options)
        {
        }
#pragma warning restore CS8618 // 
    }
}
