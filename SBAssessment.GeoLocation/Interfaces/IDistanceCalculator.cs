using System.Threading.Tasks;

namespace SBAssessment.GeoLocation.Interfaces
{
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Determines the distance (in km) between two addresses
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>The distance in kilometres</returns>
        Task<double> GetDistanceBetween(IAddress start, IAddress end);
    }
}
