﻿using System.ComponentModel.DataAnnotations;

namespace SBAssessment.Data.Entities
{
    public class Address
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}