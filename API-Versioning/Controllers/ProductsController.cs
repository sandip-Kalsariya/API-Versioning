using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Versioning.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region V1
        // GET: api/<ProductsController>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductsController>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public void Delete(int id)
        {
        }
        #endregion

        #region V2
        // GET: api/<ProductsController>
        [HttpGet]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> GetV2()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public string GetV2(int id)
        {
            return "value";
        }

        // POST api/<ProductsController>
        [HttpPost]
        [MapToApiVersion("2.0")]
        public void PostV2([FromBody] string value)
        {
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [MapToApiVersion("2 .0")]
        public void PutV2(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [MapToApiVersion("2.0")]
        public void DeleteV2(int id)
        {
        }
        #endregion
    }
}
