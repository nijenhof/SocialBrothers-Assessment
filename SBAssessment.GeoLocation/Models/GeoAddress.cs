using SBAssessment.GeoLocation.Interfaces;

namespace SBAssessment.GeoLocation.Models
{
    public class GeoAddress : IAddress
    {
        public string StreetName { get; }
        public string StreetNumber { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string Country { get; }
        public GeoAddress(string streetName, string streetNumber, string postalCode, string city, string country)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public string GetAsAddressString()
        {
            return $"{StreetName}, {StreetNumber} {PostalCode}, {City}, {Country}";
        }
    }
}
