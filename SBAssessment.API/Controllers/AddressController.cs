using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using SBAssessment.Data.Entities;
using SBAssessment.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SBAssessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressRepository _addressRepository;

        public AddressController(ILogger<AddressController> logger, IAddressRepository addressRepository)
        {
            _logger = logger;
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// Retrieves addresses with OData filter/search functionality
        /// </summary>
        /// <remarks>
        /// Filter - equals
        /// 
        ///     /api/Address?$filter=StreetName eq 'Lievensweg'
        ///     
        /// Filter - contains
        /// 
        ///     /api/Address?$filter=contains(StreetName, 'weg')
        ///     
        /// Select - only id
        /// 
        ///     /api/Address?$select=Id
        ///     
        /// OrderBy
        /// 
        ///     /api/Address?$orderby=StreetNumber desc
        /// </remarks>
        /// <param name="queryOptions" type="string">OData based filter string</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Address>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public IActionResult Get(ODataQueryOptions<Address> queryOptions)
        {
            try
            {
                queryOptions.Validate(GetValidationSettings());
            }
            catch (ODataException e)
            {
                _logger.LogDebug("Invalid query params were provided to oData", e.Message, e.Data, e.StackTrace);
                return BadRequest(e.Message);
            }

            IQueryable<Address> queryable = _addressRepository.GetAllAsQueryable();
            IQueryable oDataQueryable = queryOptions.ApplyTo(queryable);
            return Ok(oDataQueryable);
        }


        /// <summary>
        /// Returns details for a single address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get(int id)
        {
            var address = _addressRepository.Get(id);

            if (address == null) return NotFound("No address with that Id could be found");

            return Ok(address);
        }


        /// <summary>
        /// Creates a new address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] Address value)
        {
            if (!ModelState.IsValid) return BadRequest(GetValidationProblemDetails(ModelState));

            if (value.Id != 0)
            {
                Address existingEntity = _addressRepository.Get(value.Id);
                if (existingEntity != null) return BadRequest($"An address with id {value.Id} already exists.");
            }

            _addressRepository.Add(value);
            return Ok();
        }


        /// <summary>
        /// Updates an existing address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Put(int id, [FromBody] Address value)
        {
            if (!ModelState.IsValid) return BadRequest(GetValidationProblemDetails(ModelState));

            value.Id = id;

            _addressRepository.Update(value);
            return Ok();
        }


        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Delete(int id)
        {
            _addressRepository.Remove(id);
        }

        private static ODataValidationSettings GetValidationSettings()
        {
            return new ODataValidationSettings();
        }
        private static ProblemDetails GetValidationProblemDetails(ModelStateDictionary modelState)
        {
            return new ValidationProblemDetails(modelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
        }
    }
}
