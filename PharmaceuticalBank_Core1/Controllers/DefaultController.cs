using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalBank_Core1.Models.DAL;

namespace PharmaceuticalBank_Core1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        public IActionResult GetValue() {

            using (var context = new pharmabank1Context()) {
                return new JsonResult(context.AspNetUsers.ToList());
            }
        }

    }
}