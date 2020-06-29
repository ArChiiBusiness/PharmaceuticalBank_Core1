using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB.Core.DE;

namespace PharmaceuticalBank_Core1.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataEverywhereProcessorController : ControllerBase
    {
        // GET: api/DataEverywhereProcessor
        [HttpGet]
        public IActionResult Get()
        {
            return BadRequest();
        }

        // GET: api/DataEverywhereProcessor/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            if (id == "0304f783-49d4-475c-8335-36c324ef5223")
            {
                DataEverywhereProcessor DEProcessor = new DataEverywhereProcessor();
                var feeds = DEProcessor.GetFeeds();

                foreach (var feed in feeds)
                {
                    if (DEProcessor.PendingUpdate(feed))
                    {
                        DEProcessor.UpdateFeed(feed);
                    }
                }

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: api/DataEverywhereProcessor
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/DataEverywhereProcessor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
