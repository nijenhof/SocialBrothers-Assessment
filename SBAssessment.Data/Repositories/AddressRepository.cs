using SBAssessment.Data.Entities;
using SBAssessment.Data.Interfaces;

namespace SBAssessment.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DbContextBase context) : base(context)
        {
        }
    }
}
