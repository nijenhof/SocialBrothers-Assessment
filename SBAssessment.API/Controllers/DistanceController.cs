using Microsoft.AspNetCore.Mvc;
using SBAssessment.Data.Entities;
using SBAssessment.Data.Interfaces;
using SBAssessment.GeoLocation.Interfaces;
using SBAssessment.GeoLocation.Models;
using System.Net;
using System.Threading.Tasks;

namespace SBAssessment.API.Controllers
{
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;
        private readonly IDistanceCalculator distanceCalculator;

        public DistanceController(IAddressRepository addressRepository, IDistanceCalculator distanceCalculator)
        {
            this.addressRepository = addressRepository;
            this.distanceCalculator = distanceCalculator;
        }

        /// <summary>
        /// Calculates distance (in km) between two addresses
        /// </summary>
        /// <param name="startAddressId"></param>
        /// <param name="endAddressId"></param>
        /// <returns></returns>
        [HttpGet("api/calculate-distance")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int startAddressId, [FromQuery] int endAddressId)
        {
            if (startAddressId == 0 || endAddressId == 0) return BadRequest("Both a start and end location need to be provided");
            if (startAddressId == endAddressId) return BadRequest("The start location cannot be the same as the end location");

            Address startAddress = await addressRepository.GetAsync(startAddressId);
            if (startAddress == null) return BadRequest($"{nameof(startAddressId)} {startAddressId} could not be found.");

            Address endAddress = await addressRepository.GetAsync(endAddressId);
            if (endAddress == null) return BadRequest($"{nameof(endAddressId)} {endAddressId} could not be found.");

            double distanceInKilometres = await distanceCalculator.GetDistanceBetween(
                new GeoAddress(startAddress.StreetName, startAddress.StreetNumber, startAddress.PostalCode, startAddress.City, startAddress.Country),
                new GeoAddress(endAddress.StreetName, endAddress.StreetNumber, endAddress.PostalCode, endAddress.City, endAddress.Country)
            );

            if (distanceInKilometres == 0) return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong while retrieving a distance");
            return Ok(distanceInKilometres);
        }
    }
}
