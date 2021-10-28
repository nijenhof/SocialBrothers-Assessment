using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using SBAssessment.Data.Entities;
using SBAssessment.Data.Interfaces;
using System.Linq;

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

        [HttpGet]
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


        [HttpGet("{id}")]
        public Address Get(int id)
        {
            return _addressRepository.Get(id);
        }


        [HttpPost]
        public void Post([FromBody] Address value)
        {
            _addressRepository.Add(value);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Address value)
        {
            _addressRepository.Update(id, value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _addressRepository.Remove(id);
        }

        private static ODataValidationSettings GetValidationSettings()
        {
            return new ODataValidationSettings();
        }
    }
}
