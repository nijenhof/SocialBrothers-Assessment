using System.ComponentModel.DataAnnotations;

namespace SBAssessment.Data.Entities
{
    public class Address
    {
#pragma warning disable CS8618 // Filled by entity framework
        [Required]
        public int Id { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        [Required]
        [RegularExpression(@"^[1-9][0-9]{3} *(?:[a-zA-Z]{2})$", ErrorMessage = "No valid postal code was provided")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
#pragma warning restore CS8618 
    }
}
