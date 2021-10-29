namespace SBAssessment.GeoLocation.Interfaces
{
    public interface IAddress
    {
        /// <summary>
        /// Converts the contents of the IAddress to a formatted string
        /// </summary>
        /// <returns>a formatted string with the address data</returns>
        string GetAsAddressString();
    }
}
