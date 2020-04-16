using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalBank_Core1.Models.DAL2;

namespace PharmaceuticalBank_Core1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        public IActionResult Index() {

            using (var db = new excelpro_pharmabankContext()) {
                return new JsonResult(db.Shipments.Take(100).ToList());
            }
        }

    }
}